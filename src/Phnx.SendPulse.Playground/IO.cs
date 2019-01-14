using Phnx.SendPulse.Models;
using System;
using System.Collections.Generic;

namespace Phnx.SendPulse.Playground
{
    public static class IO
    {
        public static (string clientId, string clientSecret) GetKeysFromUser()
        {
            string clientId;
            do
            {
                Console.Write("Enter your client ID: ");
                clientId = Console.ReadLine();


            } while (!YesNo($"Your client ID is {clientId}. Is this okay?"));

            string clientSecret;
            do
            {
                Console.Write("Enter your client secret: ");
                clientSecret = Console.ReadLine();
            } while (!YesNo($"Your client secret is {clientSecret}. Is this okay?"));

            return (clientId, clientSecret);
        }

        public static bool YesNo(string question)
        {
            Console.Write(question.Trim() + " (y/n): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);

            Console.WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        public static SmtpRequestEmailAddressModel[] GetToEmailAddresses()
        {
            var emailsToSendTo = new List<SmtpRequestEmailAddressModel>();

            do
            {
                Console.WriteLine("Enter an email to send to: ");
                var emailToSendTo = Console.ReadLine();
                Console.WriteLine("Enter the name of the recipient above");
                var emailName = Console.ReadLine();

                emailsToSendTo.Add(new SmtpRequestEmailAddressModel
                {
                    Email = emailToSendTo,
                    Name = emailName
                });
            } while (YesNo("Add another email?"));

            return emailsToSendTo.ToArray();
        }

        public static SmtpRequestEmailAddressModel GetFromEmailAddress()
        {
            Console.WriteLine("Enter an email to send from: ");
            var emailToSendTo = Console.ReadLine();
            Console.WriteLine("Enter the name of the sender above");
            var emailName = Console.ReadLine();

            return new SmtpRequestEmailAddressModel
            {
                Email = emailToSendTo,
                Name = emailName
            };
        }
    }
}
