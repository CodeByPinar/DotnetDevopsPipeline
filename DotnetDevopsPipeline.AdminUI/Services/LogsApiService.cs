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

            // ✅ PROD API BASE ADDRESS
            _httpClient.BaseAddress = new Uri("https://dotnet-devops-pipeline.onrender.com/");
        }

        public async Task<List<string>> GetLogsAsync(int take = 200)
        {
            try
            {
                var url = $"api/v1/logs?take={take}";
                var response = await _httpClient.GetFromJsonAsync<List<LogEntryDto>>(url);

                return response?.Select(x => x.Raw).ToList() ?? new List<string>();
            }
            catch
            {
                // ✅ 404, bağlantı hatası vs. UI’yi patlatmasın
                return new List<string>
                {
                    "API log endpoint erişilemedi (404 veya bağlantı hatası)."
                };
            }
        }

        public class LogEntryDto
        {
            public string Raw { get; set; } = string.Empty;
        }
    }
}
