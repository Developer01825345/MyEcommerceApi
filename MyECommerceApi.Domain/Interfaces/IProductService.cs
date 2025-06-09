using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Domain.Interfaces;

/// <summary>
/// Business Login, Validations
/// </summary>
public interface IProductService
{
    public List<CreateProduct> ListProducts();
    public Product GetProductById(Guid productId);
    public CreateProduct CreateProduct(CreateProduct createProduct);
    public UpdateProduct UpdateProduct(Guid id, UpdateProduct updateProduct);
    public void DeleteProduct(Guid id);
}
