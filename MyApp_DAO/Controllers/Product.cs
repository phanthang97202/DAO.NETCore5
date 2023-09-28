using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyApp_DAO.Services;
using System.Threading.Tasks;

namespace MyApp_DAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Product : ControllerBase
    {
        private readonly ProductService _productService;
        public Product(IConfiguration config) 
        {
            _productService = new ProductService(config.GetConnectionString("ConnectDB"));
        }


        // Lấy toàn bộ danh sách sản phẩm 
        [HttpGet]
        public IActionResult GetAll()
        {
            var products =  _productService.GetAllProducts();
            return Ok(products);
        }

        // lấy thông tin chi tiết của 1 sản phẩm theo id 

        // cập nhật thông tin sản phẩm

        // thêm mới 1 sản phẩm

        // xóa sản phẩm
    }
}
