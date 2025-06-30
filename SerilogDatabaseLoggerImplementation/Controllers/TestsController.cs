using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SerilogDatabaseLoggerImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;

        public TestsController(ILogger<TestsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all-logs")]
        public IActionResult LogAllLevels()
        {
            string UniqueId = Guid.NewGuid().ToString();

            try
            {
                // First approach: include UniqueId directly in the log message
                _logger.LogInformation("{UniqueId} This is an information log.", UniqueId);

                _logger.LogWarning("This is Warning log. UniqueId: {UniqueId}", UniqueId);

                _logger.LogCritical("This is a Critical log, indicating a serious failure.");

                // Second approach: use ForContext to add UniqueId to the log context
                Log.ForContext("UniqueId", UniqueId).Information("Processing Request Information");

                Log.ForContext("UniqueId", UniqueId).Warning("Processing Request Warning");

                //Simulate an error 
                int x = 10, y = 0;

                int z = x / y;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UniqueId} An error occurred.", UniqueId);

                Log.ForContext("UniqueId", UniqueId).Error("Processing Request Error");
            }

            return Ok("Check your logs to see the different logging levels in action!");

        }
    }
}
