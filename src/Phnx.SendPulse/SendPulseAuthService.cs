using Newtonsoft.Json;
using Phnx.SendPulse.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.SendPulse
{
    public class SendPulseAuthService : ISendPulseAuthService
    {
        public SendPulseAuthService(HttpClient httpClient, string clientId, string clientSecret)
        {
            HttpClient = httpClient;
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
        }

        public HttpClient HttpClient { get; }

        private readonly string _clientId;
        private readonly string _clientSecret;
        private SendPulseCredentials _credentialsCache;

        public async Task<SendPulseCredentials> GetCredentials(bool forceRenew = false)
        {
            if (forceRenew || _credentialsCache.ExpiresOn <= DateTime.UtcNow.AddMinutes(1))
            {
                // Renew
                var credentials = await Login();

                _credentialsCache = credentials;
            }

            return _credentialsCache;
        }

        public async Task AttachCredentialsToRequest(HttpRequestMessage request)
        {
            var credentials = await GetCredentials();
            AttachCredentialsToRequest(request, credentials);
        }

        public void AttachCredentialsToRequest(HttpRequestMessage request, SendPulseCredentials credentials)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(credentials.AuthorizationType, credentials.AuthorizationToken);
        }

        private SendPulseLoginRequestModel BuildLoginRequest()
        {
            return new SendPulseLoginRequestModel
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                GrantType = "client_credentials"
            };
        }

        private async Task<SendPulseCredentials> Login()
        {
            var loginRequest = new HttpRequestMessage(new HttpMethod("POST"), $"{Config.SendPulseApiUrl}oauth/access_token");

            var requestBody = BuildLoginRequest();

            var jsonBody = JsonConvert.SerializeObject(requestBody);
            loginRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await HttpClient.SendAsync(loginRequest);

            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException("Error logging in: " + response.StatusCode + ". " + body);
            }

            var credentials = JsonConvert.DeserializeObject<SendPulseLoginResponseModel>(body);

            return new SendPulseCredentials
            {
                AuthorizationType = credentials.TokenType,
                AuthorizationToken = credentials.AccessToken,
                ExpiresOn = DateTime.UtcNow.AddSeconds(credentials.ExpiresIn)
            };
        }
    }
}
