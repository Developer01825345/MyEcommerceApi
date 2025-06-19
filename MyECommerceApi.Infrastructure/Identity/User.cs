using Microsoft.AspNetCore.Identity;

namespace MyECommerceApi.Infrastructure.Identity;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required override string Email { get; set; }
    public string Password { get; set; }
}
