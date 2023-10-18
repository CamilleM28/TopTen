using API.Data;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]

public class ListsController : ControllerBase
{

    private readonly IListsRepository _listsRepository;

    public ListsController(IListsRepository listsRepository)
    {

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
    public ActionResult Update(UpdateListRequest request)
    {

        _listsRepository.UpdateList(request);
        _listsRepository.Save();

        // var lists = GetTopTens(request.UserId);

        return Ok();

    }
}