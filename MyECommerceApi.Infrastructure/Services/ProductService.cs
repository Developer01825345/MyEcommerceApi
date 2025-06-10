using AutoMapper;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Infrastructure.Services;

public class ProductService : IProductService
{
     private IProductRepository _productRepository;
     private IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public List<CreateProduct> ListProducts()
    {
        var products = _productRepository.GetAll();
        var str = new Object {};

        return products.Select(p => new CreateProduct()
        {
            ProductName = p.ProductName,
            Sku = p.Sku,
            ProductDescription = p.ProductDescription,
            Price = p.Price,
            Stock = p.Stock
        }).ToList();
    }

    public Product GetProductById(Guid id)
    {
        var product = _productRepository.GetById(id) ?? throw new ApplicationException("Product does not found.");

        return product;
    }

    public CreateProduct CreateProduct(CreateProduct createProduct)
    {
        IsProductSkuExists(createProduct.Sku);

        var mapToProduct = _mapper.Map<Product>(createProduct);

        var newProduct =_productRepository.Add(mapToProduct);

        var mapFromProduct = _mapper.Map<CreateProduct>(newProduct);

        return mapFromProduct;
    }

    public UpdateProduct UpdateProduct(Guid id, UpdateProduct updateProduct)
    {
        var product = _productRepository.GetById(id) ?? throw new ApplicationException("Product does not found.");

        IsProductSkuExists(updateProduct.Sku);

        _mapper.Map<Product>(product);

        var updatedProduct = _productRepository.Update(id, product);

        var mapFromProduct = _mapper.Map<UpdateProduct>(updatedProduct);

        return mapFromProduct;
    }

    public void DeleteProduct(Guid id)
    {
        var product = _productRepository.GetById(id) ?? throw new ApplicationException("Product not found.");
        _productRepository.Delete(product);
    }

    private void IsProductSkuExists(string sku)
    {
        /// Check for duplicate Sku
        if (_productRepository.GetBySku(sku) is not null)
            throw new ApplicationException("Duplicate Sku. Please correct.");
    }
}
