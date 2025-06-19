namespace MyECommerceApi.Domain.Models.DTO;

public class LoginDto
{
    public required string Email { get; set; }
    public string? Password { get; set; }
}
