using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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

    [HttpPost("register")]
    public ActionResult<User> Register(RegistrationRequestDto request)
    {
        var exictingUser = _userRepository.GetByEmail(request.Email);

        if (exictingUser != null)
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
            Lists = new Lists
            {
                Movies = new List<string>(new string[10]),
                TV = new List<string>(new string[10]),
                Music = new List<string>(new string[10]),
                Books = new List<string>(new string[10]),
            }

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

    [HttpPost("refresh-token")]
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
