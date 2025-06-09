using MyECommerceApi.Domain.Models.Domain;

namespace MyECommerceApi.Domain.Interfaces;

/// <summary>
/// Database operations
/// </summary>
public interface IProductRepository
{
    public List<Product> GetAll();
    public Product? GetById(Guid id);
    public Product? GetBySku(string sku);
    public Product Add(Product product);
    public Product Update(Guid id, Product product);
    public void Delete(Product product);
}
