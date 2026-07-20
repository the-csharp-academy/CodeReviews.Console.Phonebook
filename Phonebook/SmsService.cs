using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Phonebook
{
    public class SmsConfig
    {
        public bool IsSimulation { get; set; } = true;
        public string AccountSid { get; set; } = "";
        public string AuthToken { get; set; } = "";
        public string FromPhoneNumber { get; set; } = "";
    }

    public class SmsService
    {
        public bool SendSms(string recipientPhoneNumber, string message, SmsConfig config)
        {
            if (string.IsNullOrWhiteSpace(recipientPhoneNumber) || !Validator.IsValidNumber(recipientPhoneNumber))
            {
                Console.WriteLine("\n[SMS Error] Recipient phone number is missing or invalid.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("\n[SMS Error] SMS message body cannot be empty.");
                return false;
            }

            if (config.IsSimulation)
            {
                Console.WriteLine($"\n[SMS SIMULATION] Message sent to {recipientPhoneNumber}:");
                Console.WriteLine($"\"{message}\"");
                return true;
            }

            if (string.IsNullOrWhiteSpace(config.AccountSid) || string.IsNullOrWhiteSpace(config.AuthToken) || string.IsNullOrWhiteSpace(config.FromPhoneNumber))
            {
                Console.WriteLine("\n[SMS Error] SMS configuration is incomplete. Account SID, Auth Token, and Sender Phone Number are required for live sending.");
                return false;
            }

            try
            {
                using var httpClient = new HttpClient();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{config.AccountSid}:{config.AuthToken}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var url = $"https://api.twilio.com/2010-04-01/Accounts/{config.AccountSid}/Messages.json";
                var postData = new[]
                {
                    new KeyValuePair<string, string>("To", recipientPhoneNumber),
                    new KeyValuePair<string, string>("From", config.FromPhoneNumber),
                    new KeyValuePair<string, string>("Body", message)
                };

                var response = httpClient.PostAsync(url, new FormUrlEncodedContent(postData)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\nSMS successfully sent to {recipientPhoneNumber}!");
                    return true;
                }
                else
                {
                    var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine($"\n[SMS Error] API request failed with status code {(int)response.StatusCode} ({response.StatusCode}).");
                    Console.WriteLine($"Response details: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Unexpected SMS Error] {ex.Message}");
                return false;
            }
        }
    }
}
