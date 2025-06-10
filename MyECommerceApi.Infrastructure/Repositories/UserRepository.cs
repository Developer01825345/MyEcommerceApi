using Microsoft.EntityFrameworkCore;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;

namespace MyECommerceApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private MyECommerceApiDbContext _dbContext;
    public UserRepository(MyECommerceApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public User? GetUserDetails(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
            return null;

            var users = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Email == email);
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the user.", ex);
        }
    }

    public List<User> GetAllUsers()
    {
        return _dbContext.Users.ToList();
    }

    public async Task<User> Create(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

}
