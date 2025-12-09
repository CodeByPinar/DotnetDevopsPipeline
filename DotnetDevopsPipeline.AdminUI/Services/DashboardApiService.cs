using System.Net.Http;
using System.Net.Http.Json;

namespace DotnetDevopsPipeline.AdminUI.Services
{
    public class DashboardApiService
    {
        private readonly HttpClient _httpClient;

        public DashboardApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetHealthStatus()
        {
            var response = await _httpClient.GetAsync("https://dotnet-devops-pipeline.onrender.com/api/v1/health");
            return response.IsSuccessStatusCode ? "Healthy" : "Unhealthy";
        }
    }
}
