using MyECommerceApi.Domain.Models.Domain;

namespace MyECommerceApi.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> Create(User user);
    public User GetUserDetails(string email);

    public List<User> GetAllUsers();
}
