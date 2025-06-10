using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Infrastructure.Helpers;

public class UserHelper
{
    /// <summary>
    /// Validate User inputs
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void ValidateUser(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
        {
            throw new ArgumentNullException(nameof(registerRequest));
        }

        if (string.IsNullOrWhiteSpace(registerRequest.Email))
        {
            throw new ArgumentException("Email is required", nameof(registerRequest.Email));
        }

        if (string.IsNullOrWhiteSpace(registerRequest.Password))
        {
            throw new ArgumentException("Password is required", nameof(registerRequest.Password));
        }
    }
    /// <summary>
    /// Apply hashing to password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verify Password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    /// <summary>
    /// Generate JWT Token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public static string GenerateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsA32ByteLongSecretKey!123456"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "jwtApi",
            audience: "jwtClient",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}