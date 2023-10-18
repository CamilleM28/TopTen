namespace API.Tests;
using API.Controllers;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class ListsContollerTests
{
    private readonly IListsRepository _listsRepository;
    private readonly ListsController _listsController;
    private readonly int _id;
    public ListsContollerTests()
    {
        _listsRepository = A.Fake<IListsRepository>();
        _listsController = new ListsController(_listsRepository);
        _id = 1;
    }


    [Fact]
    public void GetTopTens_Returns_OK()
    {
        //Arrange
        var lists = A.Fake<Lists>();
        A.CallTo(() => _listsRepository.GetTopTens(_id)).Returns(lists);
        //Act
        var result = _listsController.GetTopTens(_id);

        //Assert
        result.Should().BeOfType(typeof(ActionResult<IEnumerable<Lists>>));
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void GetTopTens_Returns_NotFound_With_Invalid_ID()
    {
        //Arrange
        A.CallTo(() => _listsRepository.GetTopTens(_id)).Returns(null);

        //Act
        var result = _listsController.GetTopTens(_id);

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void Update_Returns_OK()
    {
        //Arrange
        var request = A.Fake<UpdateListRequest>();

        //Act
        var result = _listsController.Update(request);

        //Assert
        result.Should().BeOfType<OkResult>();
    }
}