using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2.Requests;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private IAuthRepository _authRepository;
    private IUserRepository _userRepository;

    public AuthController(IAuthRepository authRepository, IUserRepository userRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;

    }

    private readonly Lists lists = new()
    {
        Movies = new List<string>(new string[10]),
        TV = new List<string>(new string[10]),
        Music = new List<string>(new string[10]),
        Books = new List<string>(new string[10]),
    };

    [HttpPost("register")]
    public ActionResult<User> Register(RegistrationRequestDto request)
    {
        var existingUser = _userRepository.GetByEmail(request.Email);

        if (existingUser != null)
        {
            return Conflict("Account using this email already exists");
        }

        _authRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Lists = lists

        };

        _authRepository.Add(user);
        _authRepository.Save();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        };

        return Ok(response);
    }

    [HttpPost("Login")]
    public ActionResult<string> Login(LoginRequestDto request)
    {
        var user = _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (!_authRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong Password");
        }

        var token = _authRepository.CreateToken(user);

        var returnObject = new
        {
            token = token,
            Id = user.Id
        };

        var refreshToken = _authRepository.GetRefreshToken();
        _authRepository.SetRefreshToken(refreshToken, Response, user);

        _authRepository.Save();

        return Ok(returnObject);
    }


    [HttpPost("auth-google")]
    public async Task<ActionResult<string>> GetGoogleToken(GoogleTokenRequestDto request)
    {
        var tokenRequest = new AuthorizationCodeTokenRequest()
        {
            Code = request.Code,
            RedirectUri = request.Uri,
            ClientId = request.Id,
            ClientSecret = request.Secret
,
        };

        var source = new CancellationTokenSource();
        var cancellationToken = source.Token;

        var clock = new Clock();

        var response = await tokenRequest.ExecuteAsync(new HttpClient(), "https://oauth2.googleapis.com/token", cancellationToken, clock);

        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(response.IdToken);
        var tokenInfo = decodedToken.Payload;

        var name = tokenInfo.First(r => r.Key == "name").Value.ToString();
        var email = tokenInfo.First(r => r.Key == "email").Value.ToString();


        var existingUser = _userRepository.GetByEmail(email);
        User user;
        if (existingUser == null)
        {
            user = new User
            {
                Name = name,
                Email = email,
                Lists = lists
            };

            _authRepository.Add(user);
            _authRepository.Save();
        }

        else
        {
            user = existingUser;
        }

        var token = _authRepository.CreateToken(user);
        var refreshToken = _authRepository.GetRefreshToken();
        _authRepository.SetRefreshToken(refreshToken, Response, user);
        _authRepository.Save();


        return Ok(new { id = user.Id, name = name, email = email, token = token });
    }



    [HttpPost("refresh-token"), HttpPost("auth/google/refresh-token")]
    public IActionResult RefreshToken(int Id)
    {
        var user = _userRepository.GetById(Id);

        var refreshToken = Request.Cookies["refreshToken"];

        if (!user.RefreshToken.Equals(refreshToken))
        {
            return Unauthorized("Invalid Refresh Token");
        }

        if (user.Expires < DateTime.Now)
        {
            return Unauthorized("Token expired");
        }

        var token = _authRepository.CreateToken(user);
        var newRefreshToken = _authRepository.GetRefreshToken();
        _authRepository.SetRefreshToken(newRefreshToken, Response, user);

        _authRepository.Save();

        return Ok(token);
    }

}
