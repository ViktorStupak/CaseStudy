using CaseStudy.WebApi.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    /// <summary>
    /// Test controller. Try to connect to this instance or DB.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TestController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pings this instance.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns if success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return this.Ok();
        }

        /// <summary>
        /// Pings the database.
        /// </summary>
        /// <remarks>
        /// Determines whether or not the database is available and can be connected to.
        /// </remarks>
        /// <returns>Return OK in case when database is available.</returns>
        /// <response code="200">Returns if database is available and can be connected to.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("dbPing")]
        public async Task<IActionResult> PingDb()
        {
            try
            {
                await this._context.Database.CanConnectAsync().ConfigureAwait(false);
                return this.Ok();
            }
            catch (SqlException e)
            {
                return this.BadRequest(e);
            }

        }
    }
}
