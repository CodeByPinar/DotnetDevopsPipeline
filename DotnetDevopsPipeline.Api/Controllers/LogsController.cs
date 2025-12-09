using Microsoft.AspNetCore.Mvc;
using DotnetDevopsPipeline.Api.Models;

namespace DotnetDevopsPipeline.Api.Controllers
{
    [ApiController]
    [Route("api/v1/logs")] // ✅ NET ve KESİN ROUTE
    public class LogsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public LogsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult GetLogs([FromQuery] int take = 200)
        {
            // ✅ Render + Docker + Local uyumlu gerçek log path
            var logDirectory = Path.Combine(_env.ContentRootPath, "Logs");

            if (!Directory.Exists(logDirectory))
                return Ok(new List<string>());

            var latestFile = Directory
                .GetFiles(logDirectory, "log-*.txt")
                .OrderByDescending(x => x)
                .FirstOrDefault();

            if (latestFile == null)
                return Ok(new List<string>());

            var allLines = System.IO.File.ReadAllLines(latestFile);

            var lastLines = allLines
                .TakeLast(take)
                .ToList();

            return Ok(lastLines); // ✅ AdminUI artık direkt List<string> alacak
        }
    }
}
