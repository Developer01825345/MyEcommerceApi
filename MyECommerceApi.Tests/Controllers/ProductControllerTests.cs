using Moq;
using MyECommerceApi.Api.Controllers;
using MyECommerceApi.Domain.Interfaces;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace MyECommerceApi.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }

    [Fact]
    public void GetProduct_ShouldReturnOkResult_WithProduct()
    {
        // Arrange
        var productId = TestConstants.ProductId;
        var expectedProduct = new CreateProduct { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku };
        var product = new Product { ProductName = TestConstants.ProductName, Sku = TestConstants.Sku };

        _mockService.Setup(service => service.GetProductById(productId)).Returns(product);

        // Act
        var result = _controller.GetProductById(productId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(expectedProduct);
    }
}
