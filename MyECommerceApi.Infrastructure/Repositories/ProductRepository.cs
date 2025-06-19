using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;

namespace MyECommerceApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private MyECommerceApiDbContext _dbContext;
    public ProductRepository(MyECommerceApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Product> GetAll()
    {
        return _dbContext.Products.ToList();
    }

    public Product GetById(Guid id)
    {
        return _dbContext.Products.Find(id);
    }

    public Product GetBySku(string sku)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Sku == sku);
    }

    public Product Add(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();

        return product;
    }

    public Product Update(Guid id, Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();

        return product;
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
}
