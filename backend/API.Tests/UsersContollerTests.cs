namespace API.Tests;
using API.Controllers;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class UsersContollerTests
{
    private readonly IUserRepository _usersRepository;
    private readonly UsersController _usersController;
    private readonly int _id;
    public UsersContollerTests()
    {
        _usersRepository = A.Fake<IUserRepository>();
        _usersController = new UsersController(_usersRepository);
        _id = 1;
    }


    [Fact]
    public void GetUsers_Returns_Users()
    {
        //Arrange
        var fakeUsers = A.Fake<List<User>>();
        A.CallTo(() => _usersRepository.GetAll()).Returns(fakeUsers);

        //Act
        var result = _usersController.GetUsers();

        //Assert
        result.Should().BeOfType(typeof(ActionResult<IEnumerable<User>>));
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void GetUsersById_Returns_User()
    {
        //Arrange
        var user = A.Fake<User>();
        A.CallTo(() => _usersRepository.GetById(_id)).Returns(user);

        //Act
        var result = _usersController.GetUserById(_id);

        //Assert

        result.Should().BeOfType<ActionResult<User>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void GetUsersById_Invalid_Returns_NotFound()
    {
        //Arrange
        A.CallTo(() => _usersRepository.GetById(_id)).Returns(null);

        //Act
        var result = _usersController.GetUserById(_id);

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void Delete_Returns_NoContent()
    {
        //Arrange
        var user = A.Fake<User>();
        A.CallTo(() => _usersRepository.GetById(_id)).Returns(user);

        //Act
        var result = _usersController.Delete(_id);

        //Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public void Delete_Invalid_Returns_NotFound()
    {
        //Arrange
        A.CallTo(() => _usersRepository.GetById(_id)).Returns(null);

        //Act
        var result = _usersController.Delete(_id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

}