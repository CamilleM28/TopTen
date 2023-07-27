using backend.Data;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]

public class ListsController : ControllerBase
{
    private readonly TopTenContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IListsRepository _listsRepository;

    public ListsController(TopTenContext context, IUserRepository userRepository, IListsRepository listsRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _listsRepository = listsRepository;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Lists>> GetTopTens(int id)
    {
        var tens = _listsRepository.GetTopTens(id);

        if (tens == null)
        {
            return NotFound();
        }

        return Ok(tens);

    }

    [HttpPut]
    public ActionResult<Lists> Update(AddItemRequest request)
    {
        var lists = _listsRepository.GetTopTens(request.userId);

        var updatedList = _listsRepository.UpdateList(request);

        var category = _listsRepository.GetCategory(lists, request.category);
        category = updatedList;

        _listsRepository.Save();
        return Ok(lists);

    }
}