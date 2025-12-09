using Microsoft.AspNetCore.Mvc;
using DotnetDevopsPipeline.AdminUI.Services;

namespace DotnetDevopsPipeline.AdminUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardApiService _apiService;

        public DashboardController(DashboardApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var health = await _apiService.GetHealthStatus();
            ViewBag.HealthStatus = health;
            return View();
        }
    }
}
