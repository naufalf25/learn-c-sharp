using Moq;
using SOLID_and_KISS_principles.GoodExample;
using SOLID_and_KISS_principles.Interfaces;

namespace SOLID_and_KISS_principles.Tests;

[TestFixture]
public class EmailServiceTests
{
    private Mock<ILogger> _mockLogger;
    private IEmailService _emailService;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger>();
        _emailService = new EmailService(_mockLogger.Object);
    }

    [Test]
    public void ValidateEmail_InvalidInput_ReturnFalse()
    {
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_emailService.ValidateEmail("test"), Is.False);
            Assert.That(_emailService.ValidateEmail("test@testcom"), Is.False);
            Assert.That(_emailService.ValidateEmail("test.123@testcom"), Is.False);
        });
    }

    [Test]
    public void ValidateEmail_ValidInput_ReturnTrue()
    {
        // Assert
        Assert.That(_emailService.ValidateEmail("user@test.com"), Is.True);
    }

    [Test]
    public void SendWelcomeEmail_EmailInput_SendEmailSuccessfull()
    {
        // Arrange
        var email = "user@test.com";

        // Act
        _emailService.SendWelcomeEmail(email);

        // Assert
        _mockLogger.Verify(l => l.LogInfo($"Welcome email sent to {email}"), Times.Once);
    }

    [Test]
    public void SendOrderInformation_EmailAndOrderIdInput_SendEmailSuccessfull()
    {
        // Arrange
        var email = "user@test.com";

        // Act
        _emailService.SendOrderInformation(email, "12");

        // Assert
        _mockLogger.Verify(l => l.LogInfo($"Order confirmation sent to {email}"), Times.Once);
    }
}
