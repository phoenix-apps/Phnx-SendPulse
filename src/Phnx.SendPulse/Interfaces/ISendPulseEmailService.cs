using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Phnx.SendPulse.Models;

namespace Phnx.SendPulse
{
    public interface ISendPulseEmailService
    {
        Task<HttpResponseMessage> SendAsync(SendPulseSmtpRequestModel request);
        Task SendAsync(SmtpRequestEmailAddressModel[] to, SmtpRequestEmailAddressModel from, string subject, string htmlBody, string plaintextBody, Dictionary<string, string> attachments = null, Dictionary<string, string> attachmentsBinary = null);
    }
}