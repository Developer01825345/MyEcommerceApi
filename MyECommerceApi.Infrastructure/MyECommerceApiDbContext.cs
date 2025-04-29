using Microsoft.EntityFrameworkCore;
using MyECommerceApi.Domain.Models.Domain;

namespace MyECommerceApi.Infrastructure;

public class MyECommerceApiDbContext : DbContext
{
    public MyECommerceApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
