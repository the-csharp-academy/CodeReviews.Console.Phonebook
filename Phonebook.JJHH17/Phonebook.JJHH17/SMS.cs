using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Phonebook.JJHH17
{
    public class SMS
    {
        // Twilio credentials. Store these in App.Config
        readonly static string accountSid = ConfigurationManager.AppSettings["TwilioSID"];
        readonly static string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        readonly static string senderPhoneNumber = ConfigurationManager.AppSettings["SenderPhoneNumber"];

        public static void SendSms(string recipientPhoneNumber, string messageBody)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken); // pulled from app.config
                var message = MessageResource.Create(
                    to: new PhoneNumber(recipientPhoneNumber),
                    from: new PhoneNumber(senderPhoneNumber), // pulled from app.config
                    body: messageBody);

                Console.WriteLine("SMS sent successfully to " + recipientPhoneNumber + ". Message SID: " + message.Sid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send SMS. Error: " + ex.Message);
            }
        }
    }
}
