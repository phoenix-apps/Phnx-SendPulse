using System.Net.Http;
using System.Threading.Tasks;
using Phnx.SendPulse.Models;

namespace Phnx.SendPulse
{
    public interface ISendPulseAuthService
    {
        Task AttachCredentialsToRequest(HttpRequestMessage request);
        void AttachCredentialsToRequest(HttpRequestMessage request, SendPulseCredentials credentials);
        Task<SendPulseCredentials> GetCredentials(bool forceRenew = false);
    }
}