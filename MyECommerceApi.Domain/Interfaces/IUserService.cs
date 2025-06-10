using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Domain.Interfaces;

public interface IUserService
{
    public Task<RegisterRequest> RegisterUser(RegisterRequest registerRequest);
    public string VerifyUser(LoginRequest loginRequest);
}
