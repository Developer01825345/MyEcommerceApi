using Microsoft.AspNetCore.Mvc;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var token = _userService.VerifyUser(loginRequest);
        return Ok(token);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var newUser = await _userService.RegisterUser(registerRequest);
        return Ok(newUser);
    }
}
