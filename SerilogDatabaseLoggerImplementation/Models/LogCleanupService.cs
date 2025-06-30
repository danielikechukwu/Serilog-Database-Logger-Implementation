using Microsoft.Data.SqlClient;

namespace SerilogDatabaseLoggerImplementation.Models
{
    public class LogCleanupService : BackgroundService
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<LogCleanupService> _logger;

        public LogCleanupService(IConfiguration configuration, ILogger<LogCleanupService> logger)
        {
            _configuration = configuration;

            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("LogCleanupService is starting.");

            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("LogCleanupService is working.");

                DeleteOldLogs();

                // Wait for 24 hours before the next cleanup
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

            }
        }

        private void DeleteOldLogs()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("LoggingDBConnection")))
                {
                    var command = new SqlCommand("DELETE FROM Logs WHERE TimeStamp < DATEADD(day, -30, GETDATE())", connection);

                    connection.Open();

                    int affectedRows = command.ExecuteNonQuery();

                    _logger.LogInformation($"{affectedRows} log enteries deleted.");

                }

                _logger.LogInformation("Logs cleaned up successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning logs.");
            }
        }
    }
}
