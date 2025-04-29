using Microsoft.AspNetCore.Mvc;

namespace MyECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult getProducts()
    {
        return Ok("Hello From Product Controller.");
    }
}
