using ExampleTest2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public TwoController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
    }
}
