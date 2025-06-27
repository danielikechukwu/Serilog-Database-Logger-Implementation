using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SerilogDatabaseLoggerImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggersController : ControllerBase
    {
        private readonly ILogger<LoggersController> _logger;

        public LoggersController(ILogger<LoggersController> logger)
        {
            logger = _logger;
        }

        [HttpGet("all-logs")]
        public IActionResult LogAllLevels()
        {
            _logger.LogTrace("LogTrace: Entering the LogAllLevels endpoint with Trace-level logging.");

            int calculation = 5 * 10;

            _logger.LogTrace("LogTrace: Calculation value is {calculation}", calculation);

            _logger.LogDebug("LogDebug: Initializing debug-level logs for debugging purposes.");

            // Log structured information
            var complexEmployee = new { Id = 10, Name = "Pranaya", Gender = "Male", Department = "IT" };

            _logger.LogInformation("LogInformation: Employee details: {@complexEmployee}", complexEmployee);

            // Log a warning if a condition is met
            bool resourceLimitApproaching = true;

            if(resourceLimitApproaching)
            {
                _logger.LogWarning("LogWarning: Resource usage is nearing the limit. Action may be required soon.");
            }

            try
            {
                // Simulate an error scenario
                int x = 0;
                int result = 10 / x;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LogError: An error occurred while processing the request.");
            }

            // Log a critical error scenario
            bool criticalFailure = true;

            if(criticalFailure)
            {
                _logger.LogCritical("LogCritical: A critical system failure has been detected. Immediate attention is required.");
            }

            return Ok("All logging levels demonstrated in this endpoint.");

        }
    }
}
