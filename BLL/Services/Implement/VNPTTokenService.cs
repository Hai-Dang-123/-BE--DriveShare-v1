using BLL.Services.Interface;
using Common.Constants;
using Common.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class VNPTTokenService : IVNPTTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly VNPTAuthSettings _settings;

        private string _accessToken;
        private DateTime _accessTokenExpiry;

        private string _publicKey;
        private string _uuidProjectServicePlan;

        public VNPTTokenService(HttpClient httpClient, IOptions<VNPTAuthSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _accessTokenExpiry)
            {
                var payload = new
                {
                    username = _settings.Username,
                    password = _settings.Password,
                    client_id = _settings.ClientId,
                    grant_type = _settings.GrantType,
                    client_secret = _settings.ClientSecret
                };

                var response = await _httpClient.PostAsJsonAsync(VNPTEndpoints.TokenEndpoint, payload);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                _accessToken = doc.RootElement.GetProperty("access_token").GetString();
                var expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();

                _accessTokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn - 60);
            }

            return _accessToken;
        }

        public async Task<(string TokenKey, string TokenId)> GetServiceTokensAsync(string channelCode)
        {
            if (string.IsNullOrEmpty(_publicKey) || string.IsNullOrEmpty(_uuidProjectServicePlan))
            {
                var token = await GetAccessTokenAsync();
                var req = new HttpRequestMessage(HttpMethod.Post, VNPTEndpoints.CheckRegisterEndpoint);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                req.Content = JsonContent.Create(new { channelCode });

                var response = await _httpClient.SendAsync(req);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                _publicKey = doc.RootElement.GetProperty("publicKey").GetString();
                _uuidProjectServicePlan = doc.RootElement.GetProperty("uuidProjectServicePlan").GetString();
            }

            return (_publicKey, _uuidProjectServicePlan);
        }
    }
}
