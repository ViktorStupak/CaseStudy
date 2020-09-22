using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return this.Ok();
        }

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
