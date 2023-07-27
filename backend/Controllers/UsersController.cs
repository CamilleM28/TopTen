using backend.Data;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]

public class UsersController : ControllerBase
{
    private readonly TopTenContext _context;
    private readonly IUserRepository _userRepository;

    public UsersController(TopTenContext context, IUserRepository userRepository)
    {
        _context = context;
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
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}