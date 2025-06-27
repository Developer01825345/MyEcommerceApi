using AutoMapper;
using FluentAssertions;
using Moq;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;
using MyECommerceApi.Infrastructure.Services;

namespace MyECommerceApi.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ProductService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public void GetListProducts_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var products = new List<Product> {
            new() { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock },
            new Product { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock },
        };

        _mockRepo.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _service.ListProducts();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].ProductName.Should().Be(TestConstants.ProductName);
    }

    [Fact]
    public void GetListProducts_ShouldThrowException_WhenProductNotExists()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Product>());

        // Act
        var result = _service.ListProducts();

        // Assert
        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }

    [Fact]
    public void GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = TestConstants.ProductId, ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock };
        _mockRepo.Setup(repo => repo.GetById(TestConstants.ProductId)).Returns(product);

        // Act
        var result = _service.GetProductById(TestConstants.ProductId);

        // Assert
        result.Should().NotBeNull();
        result.ProductName.Should().Be(TestConstants.ProductName);
    }

    [Fact]
    public void GetProductById_ShouldThrowException_WhenProductNotExists()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetById(TestConstants.ProductId)).Returns((Product)null);

        // Act
        var result = () => _service.GetProductById(TestConstants.ProductId);

        // Assert
        var exception = Assert.Throws<ApplicationException>(result);
        exception.Message.Should().Be(TestConstants.ProductExceptionMessage);
    }

    [Fact]
    public void CreateProduct_ShouldReturnNewProduct_WhenValidRequest()
    {
        // Arrange
        var productdto = new CreateProduct { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock };
        var product = new Product { Id = TestConstants.ProductId, ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock };

        _mockMapper.Setup(mapper => mapper.Map<Product>(productdto)).Returns(product);
        _mockRepo.Setup(repo => repo.Add(product)).Returns(product);
        _mockMapper.Setup(mapper => mapper.Map<CreateProduct>(product)).Returns(productdto);

        // Act
        var result = _service.CreateProduct(productdto);

        // Assert
        result.Should().NotBeNull();
        result.ProductName.Should().Be(TestConstants.ProductName);
        result.Sku.Should().Be(TestConstants.Sku);
    }

    [Fact]
    public void CreateProduct_ShouldThrowException_WhenSkuAlreadyExists()
    {
        // Arrange
        var productdto = new CreateProduct { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku, Price = TestConstants.Price, ProductDescription = TestConstants.ProductDescription, Stock = TestConstants.Stock };
        var product = new Product { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku };

        _mockRepo.Setup(repo => repo.GetBySku(productdto.Sku)).Returns(product);

        // Act
        var result = () => _service.CreateProduct(productdto);

        // Assert
        var exception = Assert.Throws<ApplicationException>(result);
        exception.Message.Should().Be(TestConstants.SkuExceptionMessage);
    }
}