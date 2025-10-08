using SOLID_and_KISS_principles.GoodExample;
using SOLID_and_KISS_principles.Interfaces;
using Moq;
using SOLID_and_KISS_principles.Models;

namespace SOLID_and_KISS_principles.Tests;

[TestFixture]
public class UserServiceTests
{
    private Mock<IEmailService> _mockEmailService;
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<ILogger> _mockLogger;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _mockEmailService = new Mock<IEmailService>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger>();

        _userService = new UserService(
            _mockEmailService.Object,
            _mockUserRepository.Object,
            _mockLogger.Object
        );
    }

    [Test]
    public void RegisterUser_InvalidEmailAndPassword_ThrowArgumenException()
    {
        // Arrange
        string email = "user@test.com";
        _mockEmailService.Setup(e => e.ValidateEmail(email)).Returns(true);

        // Assert
        Assert.That(() => _userService.RegisterUser("", ""), Throws.ArgumentException.With.Message.EqualTo("Invalid email format"));
        Assert.That(() => _userService.RegisterUser(email, ""), Throws.ArgumentException.With.Message.EqualTo("Password must be at least 6 characters"));
    }

    [Test]
    public void RegisterUser_ExistingEmail_ThrowInvalidOperationException()
    {
        // Arrange
        var email = "user@test.com";
        _mockEmailService.Setup(e => e.ValidateEmail(email)).Returns(true);
        _mockUserRepository.Setup(u => u.UserExists(email)).Returns(true);

        // Assert
        Assert.That(() => _userService.RegisterUser(email, "secretpassword456"), Throws.InvalidOperationException.With.Message.EqualTo("User already exists"));
    }

    [Test]
    public void RegisterUser_ValidInput_CreateUserSuccessfully()
    {
        // Arrange
        var email = "user@test.com";
        var password = "secretpassword123";

        _mockEmailService.Setup(e => e.ValidateEmail(email)).Returns(true);
        _mockUserRepository.Setup(u => u.UserExists(email)).Returns(false);

        // Assert
        Assert.DoesNotThrow(() => _userService.RegisterUser(email, password));
        _mockUserRepository.Verify(u => u.SaveUser(It.Is<User>(user => user.Email == email)), Times.Once);
        _mockEmailService.Verify(e => e.SendWelcomeEmail(email), Times.Once);
        _mockLogger.Verify(l => l.LogInfo($"User registered successfully: {email}"), Times.Once);
    }
}
