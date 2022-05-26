using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Warehouse.Api.Services.Connections
{
    public class SsoConnectionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _usersApiUrl;

        public SsoConnectionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _usersApiUrl = configuration["Magnus:URLS:SSO:Api"] + "/users/validate-token?accessToken=";
        }

        public async Task<string> ValidateToken(string token)
            => await _httpClient.GetStringAsync($"{_usersApiUrl}{token}");
    }
}