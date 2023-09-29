using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyApp_DAO.Services;
using System.Collections.Generic;
using MyApp_DAO.Models;

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

        public static List<Product> pro = new List<Product>();

        // Lấy toàn bộ danh sách sản phẩm 
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // lấy thông tin chi tiết của 1 sản phẩm theo id 
        [HttpGet("detail")]
        public IActionResult GetDetail(int id)
        {
            var products = _productService.GetDetailProduct(id);
            return Ok(products);
        }
        // thêm mới 1 sản phẩm
        [HttpPost]
        public IActionResult CreateNew(MyApp_DAO.Models.Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }
            int result = _productService.CreateNewProduct(product);

            if (result > 0)
            {
                return Ok(result);
            }
            return StatusCode(500, "Failed to create the product.");
        }

        // xóa sản phẩm
        [HttpDelete("delete")]
        public IActionResult DeleteMultiProducts(int[] ids)
        {
            var result = _productService.DeleteMultiProduct(ids);
            return Ok(new
            {
                EffectedRows = result
            });
        }

        // cập nhật thông tin sản phẩm
        [HttpPatch("update")]
        public IActionResult UpdateProduct(MyApp_DAO.Models.Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }
            int result = _productService.UpdateProduct(product);

            if (result > 0)
            {
                return Ok(result);
            }
            return StatusCode(500, "Failed to update the product.");
        }

    }
}
