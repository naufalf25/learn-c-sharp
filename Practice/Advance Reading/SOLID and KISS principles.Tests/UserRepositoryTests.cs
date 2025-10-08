using Moq;
using SOLID_and_KISS_principles.GoodExample;
using SOLID_and_KISS_principles.Interfaces;
using SOLID_and_KISS_principles.Models;

namespace SOLID_and_KISS_principles.Tests;

[TestFixture]
public class UserRepositoryTests
{
    private Mock<ILogger> _mockLogger;
    private IUserRepository _userRepository;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger>();
        _userRepository = new UserRepository(_mockLogger.Object);
    }

    [Test]
    public void SaveUser_ValidUser_SaveUserSuccessfull()
    {
        // Arrange
        var user = new User("user@test.com", "secretpassword123");

        // Act
        _userRepository.SaveUser(user);

        // Assert
        Assert.That(_userRepository.UserExists(user.Email), Is.True);
    }

    [Test]
    public void GetUserByEmail_EmptyUser_ReturnNull()
    {
        // Assert
        Assert.That(_userRepository.GetUserByEmail("user@test.com"), Is.Null);
    }

    [Test]
    public void GetUserByEmail_ValidUser_ReturnUser()
    {
        // Arrange
        var user = new User("user@test.com", "secretpassword123");

        // Act
        _userRepository.SaveUser(user);

        // Assert
        Assert.That(_userRepository.GetUserByEmail(user.Email), Is.EqualTo(user));
    }

    [Test]
    public void UserExists_NotFoundEmail_ReturnFalse()
    {
        // Assert
        Assert.That(_userRepository.UserExists("user@test.com"), Is.False);
    }

    [Test]
    public void UserExists_FoundEmail_ReturnTrue()
    {
        // Arrange
        var user = new User("user@test.com", "secretpassword123");

        // Act
        _userRepository.SaveUser(user);

        // Assert
        Assert.That(_userRepository.UserExists(user.Email), Is.True);
    }
}