namespace MyECommerceApi.Domain.Models.DTO;

public class RegisterRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
