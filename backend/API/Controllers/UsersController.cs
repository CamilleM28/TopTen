using API.Data;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]

public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_userRepository.GetAll());
    }

    [HttpGet("user")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _userRepository.GetById(id);

        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        };


        return Ok(response);
    }


    [HttpDelete]
    public ActionResult Delete(int id)
    {
        var user = _userRepository.GetById(id);

        if (user == null)
        {
            return NotFound();
        }

        _userRepository.Delete(user);
        _userRepository.Save();

        return NoContent();
    }
}