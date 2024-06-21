using ExampleTest2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public OneController(IDbService dbService)
        {
            _dbService = dbService;
        }
    }
}
