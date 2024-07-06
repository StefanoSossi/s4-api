using Microsoft.AspNetCore.Mvc;
using s4.Logic.Managers.Interfaces;
using s4.Presentation.Middleware;

namespace S4.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/dataSeeder")]
    public class DataSeederController : Controller
    {
        private readonly IDataSeederManager _dataSeederManager;

        public DataSeederController(IDataSeederManager dataSeederManager)
        {
            _dataSeederManager = dataSeederManager;
        }

        /// <summary>
        /// Seeds initial Database
        /// </summary>
        /// <response code="200">Success or some expected Error</response>

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> SeedDatabase()
        {
            string result = await _dataSeederManager.SeedData();
            return Ok(new MiddlewareResponse<string>(result));
        }
    }
}
