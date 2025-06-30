using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogDatabaseLoggerImplementation.Models;

namespace SerilogDatabaseLoggerImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>
        {
            new Book() { Id = 1001, Title = "ASP.NET Core", Author = "Pranaya", YearPublished = 2019 },
            new Book() { Id = 1002, Title = "SQL Server", Author = "Pranaya", YearPublished = 2022 }
        };

        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;   
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            Books.Add(book);

            string UniqueId = Guid.NewGuid().ToString();

            //Strutured Logging: log both with property injection and ForContext
            _logger.LogInformation("{UniqueId}, Added a new book {@Book}", UniqueId, book);

            Log.ForContext("UniqueId", UniqueId).Information("Added a new book {@Book}", book);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetBook()
        {
            string UniqueId = Guid.NewGuid().ToString();

            _logger.LogInformation("Retrieved all books. Books: {@Books}", Books);

            Log.ForContext("UniqueId", UniqueId).Information("Added a new book {@Book}", Books);

            return Ok(Books);
        }
    }
}
