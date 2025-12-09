using Microsoft.AspNetCore.Mvc;
using DotnetDevopsPipeline.Api.Models;

namespace DotnetDevopsPipeline.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LogsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLogs([FromQuery] int take = 200)
        {
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");

            if (!Directory.Exists(logDirectory))
                return Ok(Array.Empty<LogEntry>());

            // Bugünün log dosyasını al
            var todayFile = Directory
                .GetFiles(logDirectory, "log-*.txt")
                .OrderByDescending(x => x)
                .FirstOrDefault();

            if (todayFile is null || !System.IO.File.Exists(todayFile))
                return Ok(Array.Empty<LogEntry>());

            var allLines = System.IO.File.ReadAllLines(todayFile);
            var lastLines = allLines
                .Reverse()
                .Take(take)
                .Reverse()
                .Select(l => new LogEntry { Raw = l })
                .ToList();

            return Ok(lastLines);
        }
    }
}
