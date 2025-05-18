using DataModel;
using DataService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;


namespace JayanWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost(Name = "Create")]
        [Authorize]
        public Boolean Create(ProductDataModel product)
        {
            _logger.LogInformation($"Product Name:{product.name}  Colour :{product.colour}");
            return _productService.add(product);
        }

        [HttpGet]
        [ActionName("GetAll")]
        [Authorize]
        public IEnumerable<ProductDataModel> GetAll()
        {
            _logger.LogInformation($"GetAll Products");
            return _productService.getAll();
        }

        [HttpGet(Name = "Get")]
        [Authorize]
        public IEnumerable<ProductDataModel> Get(string colour)
        {
            _logger.LogInformation($"Get Specific Product's Based on colour : {colour}");
            return _productService.get(colour);
        }
    }
}
