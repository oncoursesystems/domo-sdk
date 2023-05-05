
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnCourse.Domo
{
    public class DomoHttpClient
    {
        private readonly IDomoConfig _config;
        private DomoAuthToken _authToken;

        public HttpClient Client;

        public DomoHttpClient(IDomoConfig config)
        {
            Client = new HttpClient();
            _config = config;
            InitializeDefaultClient();
        }

        private void InitializeDefaultClient()
        {
            GetDomoAuthAsync().Wait();
            Client.BaseAddress = _config.ApiHost;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _authToken.Token);
        }

        private async Task GetAuthToken()
        {
            await GetDomoAuthAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _authToken.Token);
        }

        private async Task GetDomoAuthAsync()
        {
            using (var client = new HttpClient())
            {
                var authScope = _config.Scope.ToString().ToLower().Replace(", ", "%20");

                var clientCreds = Encoding.ASCII.GetBytes($"{_config.ClientId}:{_config.ClientSecret}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(clientCreds));
                var tokenUrl = $"{_config.ApiHost.AbsoluteUri}oauth/token?grant_type=client_credentials&scope={authScope}";
                var response = await client.PostAsync(tokenUrl, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("", "") }));

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var authToken = JsonSerializer.Deserialize<DomoAuthToken>(jsonResponse);

                _authToken = authToken;
            }
        }

        public void SetAcceptRequestHeaders(string mediaType)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"{mediaType}"));
        }

        public void SetAcceptRequestHeaders(string mediaType, string mediaType2)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"{mediaType}"));
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"{mediaType2}"));
        }

        public void SetAuthorizationHeader(string schema, string parameter)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"{schema}", $"{parameter}");
        }
    }
}