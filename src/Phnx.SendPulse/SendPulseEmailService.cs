using Newtonsoft.Json;
using Phnx.SendPulse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.SendPulse
{
    public class SendPulseEmailService : ISendPulseEmailService
    {
        public SendPulseEmailService(ISendPulseAuthService sendPulseAuthService, HttpClient httpClient)
        {
            SendPulseAuthService = sendPulseAuthService;
            HttpClient = httpClient;
        }

        public ISendPulseAuthService SendPulseAuthService { get; }
        public HttpClient HttpClient { get; }

        public async Task SendAsync(
            SmtpRequestEmailAddressModel[] to, SmtpRequestEmailAddressModel from,
            string subject,
            string htmlBody, string plaintextBody,
            Dictionary<string, string> attachments = null,
            Dictionary<string, string> attachmentsBinary = null)
        {
            var request = new SendPulseSmtpRequestModel
            {
                Email = new SmtpRequestEmailModel
                {
                    Attachments = attachments,
                    AttachmentsBinary = attachmentsBinary,
                    From = from,
                    Html = htmlBody,
                    Subject = subject,
                    Text = plaintextBody,
                    To = to
                }
            };

            await SendAsync(request);
        }

        public async Task<HttpResponseMessage> SendAsync(SendPulseSmtpRequestModel request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpRequest = new HttpRequestMessage(new HttpMethod("POST"), $"{Config.SendPulseApiUrl}smtp/emails")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            await SendPulseAuthService.AttachCredentialsToRequest(httpRequest);

            return await HttpClient.SendAsync(httpRequest);
        }
    }
}
