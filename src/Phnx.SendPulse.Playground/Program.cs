using Phnx.SendPulse.Models;
using System;
using System.Net.Http;

namespace Phnx.SendPulse.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            (var clientId, var clientSecret) = IO.GetKeysFromUser();
            var httpClient = new HttpClient();

            var authService = new SendPulseAuthService(httpClient, clientId, clientSecret);

            var emailService = new SendPulseEmailService(authService, httpClient);

            var sendFrom = IO.GetFromEmailAddress();
            var sendTo = IO.GetToEmailAddresses();

            var message = new SendPulseSmtpRequestModel
            {
                Email = new SmtpRequestEmailModel
                {
                    From = sendFrom,
                    To = sendTo,
                    Html = "<h1>Test Email</h1>",
                    Text = "Test Email",
                    Subject = "Test Email"
                }
            };

            Console.WriteLine("Sending email...");
            var response = emailService.SendAsync(message);

            response.ContinueWith(responseAsync =>
            {
                if (responseAsync.IsFaulted)
                {
                    Console.WriteLine("Error!");
                    Console.WriteLine(responseAsync.Exception);
                    return;
                }

                Console.WriteLine("Sent.");

                var result = responseAsync.Result;
                Console.WriteLine("Response: " + (int)result.StatusCode + ", " + result.StatusCode.ToString());

                var body = result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(body);
            });

            Console.ReadLine();
        }
    }
}
