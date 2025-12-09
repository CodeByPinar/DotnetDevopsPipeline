using Microsoft.AspNetCore.Mvc;
using DotnetDevopsPipeline.AdminUI.Services;

namespace DotnetDevopsPipeline.AdminUI.Controllers
{
    public class LogsController : Controller
    {
        private readonly LogsApiService _logsApiService;

        public LogsController(LogsApiService logsApiService)
        {
            _logsApiService = logsApiService;
        }

        public async Task<IActionResult> Index(int take = 200)
        {
            var logs = await _logsApiService.GetLogsAsync(take);
            ViewBag.Take = take;
            return View(logs);
        }
    }
}
