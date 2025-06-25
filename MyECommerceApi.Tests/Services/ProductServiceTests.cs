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
    public void GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = TestConstants.ProductId, ProductName = "Test Product", Sku = "PROD1", Price = 100, ProductDescription = "Test Product Description", Stock = 10 };
        _mockRepo.Setup(repo => repo.GetById(TestConstants.ProductId)).Returns(product);

        // Act
        var result = _service.GetProductById(TestConstants.ProductId);

        // Assert
        result.Should().NotBeNull();
        result.ProductName.Should().Be("Test Product");
    }
}