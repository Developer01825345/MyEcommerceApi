using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Infrastructure.Identity;

namespace MyECommerceApi.Infrastructure;

public class MyECommerceApiDbContext : IdentityDbContext<User>
{
    public MyECommerceApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
