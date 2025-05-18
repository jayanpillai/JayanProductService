using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JayanWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestServiceController : ControllerBase
    {
        private readonly ILogger<TestServiceController> _logger;

        public TestServiceController(ILogger<TestServiceController> logger) {
        _logger = logger;
        }

        [HttpGet(Name = "GetServiceStatus")]
        [ActionName("GetServiceStatus")]
        [AllowAnonymous]
        public String Get()
        {
            return "Live";
        }

    }
}
