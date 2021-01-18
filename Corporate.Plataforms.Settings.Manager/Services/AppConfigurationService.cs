using Corporate.Plataforms.Settings.Manager.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Services
{
    public class AppConfigurationService : IAppConfigurationService
    {

        private HttpClient _client { get; }
        private readonly string _baseUrl;
        private readonly string _apiVersion;
        private readonly string _credentialId;
        private readonly string _secret;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">HttpClient</param>
        /// <param name="configuration">HttpClient</param>
        public AppConfigurationService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetValue<string>("AzureAppConfiguration:Url");
            _credentialId = configuration.GetValue<string>("AzureAppConfiguration:CredentialId");
            _secret = configuration.GetValue<string>("AzureAppConfiguration:Secret");
            _apiVersion = configuration.GetValue<string>("AzureAppConfiguration:ApiVersion");
        }

        public async Task<string> GetKeyValue(string key, string label)
        {
            string resultado = string.Empty;

            string url = Path.Combine(_baseUrl, $"kv/{key}?label={label}api-version={_apiVersion}");
            var request = CreateAzureHeadersAuth(new HttpRequestMessage(HttpMethod.Get, new Uri(url)));

            var response = await _client.SendAsync(request);
            string data = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error request {url}: StatusCode: {response.StatusCode} - Response Body{data}");

            if (string.IsNullOrWhiteSpace(data))
                return JsonConvert.DeserializeObject<string>(data);

            return default;

        }

        private HttpRequestMessage CreateAzureHeadersAuth(HttpRequestMessage request)
        {
            string host = request.RequestUri.Authority;
            string verb = request.Method.ToString().ToUpper();
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            string contentHash = Convert.ToBase64String(ComputeSha256Hash(request.Content));

            var stringToSign = $"{verb}\n{request.RequestUri.PathAndQuery}\n{utcNow.ToString("r")};{host};{contentHash}";

            string signature;
            using (var hmac = new HMACSHA256(Convert.FromBase64String(_secret)))
            {
                signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(stringToSign)));
            }

            request.Headers.Date = utcNow;
            request.Headers.Add("x-ms-content-sha256", contentHash);
            request.Headers.Authorization = new AuthenticationHeaderValue("HMAC-SHA256", $"Credential={_credentialId}&SignedHeaders=date;host;x-ms-content-sha256&Signature={signature}");

            return request;
        }

        private byte[] ComputeSha256Hash(HttpContent content)
        {
            using (var stream = new MemoryStream())
            {
                if (content != null)
                {
                    content.CopyToAsync(stream).Wait();
                    stream.Seek(0, SeekOrigin.Begin);
                }

                using (var alg = SHA256.Create())
                {
                    return alg.ComputeHash(stream.ToArray());
                }
            }
        }
    }
}
