using System.Net.Http;
using System.Net.Http.Json;

namespace DotnetDevopsPipeline.AdminUI.Services
{
    public class LogsApiService
    {
        private readonly HttpClient _httpClient;

        public LogsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetLogsAsync(int take = 200)
        {
            var url = $"https://dotnet-devops-pipeline.onrender.com/api/v1/logs?take={take}";
            var response = await _httpClient.GetFromJsonAsync<List<LogEntryDto>>(url);

            return response?.Select(x => x.Raw).ToList() ?? new List<string>();
        }

        public class LogEntryDto
        {
            public string Raw { get; set; } = string.Empty;
        }
    }
}
