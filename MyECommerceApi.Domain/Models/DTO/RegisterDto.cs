namespace MyECommerceApi.Domain.Models.DTO;

public class RegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
}
