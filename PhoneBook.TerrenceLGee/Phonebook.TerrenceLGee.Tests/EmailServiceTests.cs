using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Moq;
using PhoneBook.TerrenceLGee.Services;
using PhoneBook.TerrenceLGee.Services.Interfaces;



namespace Phonebook.TerrenceLGee.Tests;

public class EmailServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly Mock<ISmtpClientFactory> _mockClientFactory;
    private readonly Mock<ILogger<EmailService>> _mockLogger;
    private readonly IEmailService _emailService;

    public EmailServiceTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Resources.GetConfiguration())
            .Build();
        _mockClientFactory = new Mock<ISmtpClientFactory>();
        _mockLogger = new Mock<ILogger<EmailService>>();
        _emailService = new EmailService(_configuration, _mockLogger.Object, _mockClientFactory.Object);
    }

    [Fact]
    public async Task SendEmailAsync_Returns_True_WhenEmailSentSuccessfully()
    {
        var expectedResult = true;

        var emailData = Resources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.SendAsync(It.IsAny<MimeMessage>()))
            .Returns(Task.FromResult(It.IsAny<string>()));

        mockClient
            .Setup(c => c.DisconnectAsync(It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        var result = await _emailService.SendEmailAsync(emailData);

        Assert.True(result);
        Assert.Equal(expectedResult, result);
        mockClient.Verify(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        mockClient.Verify(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockClient.Verify(c => c.SendAsync(It.IsAny<MimeMessage>()), Times.Once);
        mockClient.Verify(c => c.DisconnectAsync(It.IsAny<bool>()), Times.Once);
    }

    [Fact]
    public async Task SendEmailAsync_Returns_False_WhenConnectionFails()
    {
        var expectedResult = false;

        var emailData = Resources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
            .ThrowsAsync(new IOException());

        mockClient
            .Setup(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.SendAsync(It.IsAny<MimeMessage>()))
            .Returns(Task.FromResult(It.IsAny<string>()));

        mockClient
            .Setup(c => c.DisconnectAsync(It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        var result = await _emailService.SendEmailAsync(emailData);

        Assert.False(result);
        Assert.Equal(expectedResult, result);
        mockClient.Verify(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        mockClient.Verify(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        mockClient.Verify(c => c.SendAsync(It.IsAny<MimeMessage>()), Times.Never);
        mockClient.Verify(c => c.DisconnectAsync(It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public async Task SendEmailAsync_Returns_False_WhenAuthenticationFails()
    {
        var expectedResult = false;

        var emailData = Resources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new AuthenticationException());

        mockClient
            .Setup(c => c.SendAsync(It.IsAny<MimeMessage>()))
            .Returns(Task.FromResult(It.IsAny<string>()));

        mockClient
            .Setup(c => c.DisconnectAsync(It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        var result = await _emailService.SendEmailAsync(emailData);

        Assert.False(result);
        Assert.Equal(expectedResult, result);
        mockClient.Verify(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        mockClient.Verify(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockClient.Verify(c => c.SendAsync(It.IsAny<MimeMessage>()), Times.Never);
        mockClient.Verify(c => c.DisconnectAsync(It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public async Task SendEmailAsync_Returns_False_WhenSendingFails()
    {
        var expectedResult = false;

        var emailData = Resources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.SendAsync(It.IsAny<MimeMessage>()))
            .ThrowsAsync(new Exception());

        mockClient
            .Setup(c => c.DisconnectAsync(It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        var result = await _emailService.SendEmailAsync(emailData);

        Assert.False(result);
        Assert.Equal(expectedResult, result);
        mockClient.Verify(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        mockClient.Verify(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockClient.Verify(c => c.SendAsync(It.IsAny<MimeMessage>()), Times.Once);
        mockClient.Verify(c => c.DisconnectAsync(It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public async Task SendEmailAsync_Returns_False_EvenAfterSendingEmailWhenDisconnectionFails()
    {
        var expectedResult = false;

        var emailData = Resources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockClient
            .Setup(c => c.SendAsync(It.IsAny<MimeMessage>()))
            .Returns(Task.FromResult(It.IsAny<string>()));

        mockClient
            .Setup(c => c.DisconnectAsync(It.IsAny<bool>()))
            .ThrowsAsync(new ObjectDisposedException(nameof(_emailService)));

        var result = await _emailService.SendEmailAsync(emailData);

        Assert.False(result);
        Assert.Equal(expectedResult, result);
        mockClient.Verify(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        mockClient.Verify(c => c.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockClient.Verify(c => c.SendAsync(It.IsAny<MimeMessage>()), Times.Once);
        mockClient.Verify(c => c.DisconnectAsync(It.IsAny<bool>()), Times.Once);
    }
}
