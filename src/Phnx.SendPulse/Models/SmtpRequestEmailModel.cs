using Newtonsoft.Json;
using System.Collections.Generic;

namespace Phnx.SendPulse.Models
{
    public class SmtpRequestEmailModel
    {
        public string Html { get; set; }

        public string Text { get; set; }

        public string Subject { get; set; }

        public SmtpRequestEmailAddressModel From { get; set; }

        public SmtpRequestEmailAddressModel[] To { get; set; }

        public Dictionary<string, string> Attachments { get; set; }

        [JsonProperty("attachments_binary")]
        public Dictionary<string, string> AttachmentsBinary { get; set; }
    }
}
