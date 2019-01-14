using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.SendPulse.Models
{
    public class SendPulseCredentials
    {
        public string AuthorizationType { get; set; }

        public string AuthorizationToken { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
