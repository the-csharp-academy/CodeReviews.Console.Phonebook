using System.Security.Authentication;
using Microsoft.Extensions.Logging;
using phoneBook._0lcm.Configuration;
using phoneBook._0lcm.Models;
using phoneBook._0lcm.ServiceContracts;
using Spectre.Console;
using static phoneBook._0lcm.UserInterface.DisplayHelper;

namespace phoneBook._0lcm.UserInterface;

public class MessagingUi(IMessagingService messagingService, AppSettings appSettings, ILogger<MessagingUi> logger)
{
    //------- Messaging Methods -------
    internal void WriteAndSendEmail(ContactClass contact)
    {
        if (appSettings.UserEmail == appSettings.UserEmailDefault ||
            appSettings.UserAppPassword == appSettings.UserAppPasswordDefault ||
            appSettings.UserName == appSettings.UserNameDefault)
        {
            DisplayWarning(
                "This feature is currently unavailable because you have not set up your email details. Please set them up in the settings menu to proceed.");
            WaitForUser();
            return;
        }

        var subjectText = DisplayQuestion("Please enter a short subject title:");
        var messageText = GetMessage();
        if (string.IsNullOrEmpty(messageText))
            return;

        try
        {
            messagingService.SendEmail(contact, subjectText, messageText);
            DisplaySuccess("Email sent.");
            WaitForUser();
            return;
        }
        catch (ArgumentNullException ex)
        {
            DisplayWarning(
                $"An exception has been caught: {ex.Message}. Please check your details and message and ensure that none are null or empty.");
        }
        catch (ArgumentException ex)
        {
            DisplayWarning(
                $"Exception has been caught: {ex.Message}. Please check that your details are correct, and that your email has a valid domain. Supported domains are: @gmail.com, @hotmail.com, @yahoo.com");
        }
        catch (AuthenticationException ex)
        {
            DisplayWarning(
                $"An exception has been caught: {ex.Message}. Please ensure all your authentication details are correct and valid.");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Exception caught while trying to send an email.");
            throw;
        }

        WaitForUser();
    }

    internal void WriteAndSendSms(ContactClass contact)
    {
        if (string.IsNullOrWhiteSpace(appSettings.UserAppPassword))
        {
            DisplayWarning("Please set a textbelt key in settings to use this feature.");
            WaitForUser();
            return;
        }

        var messageText = GetMessage();
        if (string.IsNullOrEmpty(messageText))
            return;

        try
        {
            var response = messagingService.SendSms(contact, messageText);

            if (response.Success)
            {
                DisplaySuccess("\nSms message successfully sent.");
                DisplayInfo($"Quota remaining: {response.QuotaRemaining}");
            }
            else
            {
                DisplayError("\nSms message failed.");
                DisplayInfo($"Details: {response.Error}. Quota remaining: {response.QuotaRemaining}");
            }

            if (appSettings.TextBeltKey == appSettings.TextBeltKeyDefault)
                DisplayWarning(
                    "[[This is a test and no real sms was sent, please set a textbelt key in settings to send real messages.]]");

            WaitForUser();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An exception was caught while sending an sms message.");
            throw;
        }
    }

    //------- Helper Methods -------
    /// <summary>
    ///     Gets a basic text message from the user
    /// </summary>
    /// <returns>a message in the form of a string, or null if the message was cancelled</returns>
    private string? GetMessage()
    {
        Console.Clear();
        DisplayMessage("Please write your message, and press enter when you're done.");
        var message = DisplayQuestion("\n");

        if (AnsiConsole.Confirm("Are you sure you want to send this message? It will be deleted if you choose no."))
            return message;

        return null;
    }

    private void WaitForUser()
    {
        DisplayInfo("Press enter to continue.");
        Console.ReadLine();
    }
}