using Microsoft.AspNetCore.Mvc;
using MyECommerceApi.Domain.Constants;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetProductById(Guid id)
    {
        return Ok(_productService.GetProductById(id));
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_productService.ListProducts());
    }

    [HttpPost]
    public IActionResult AddProduct(CreateProduct createProduct)
    {
        _productService.CreateProduct(createProduct);
        return Ok(Message.CreateSuccess);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateProduct(Guid id, UpdateProduct updateProduct)
    {
        _productService.UpdateProduct(id, updateProduct);
        return Ok(Message.UpdateSuccess);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteProduct(Guid id)
    {
        _productService.DeleteProduct(id);
        return Ok(Message.DeleteSuccess);
    }
}
