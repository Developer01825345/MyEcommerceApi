namespace MyECommerceApi.Tests;

public static class TestConstants
{
    public static readonly Guid ProductId = Guid.NewGuid();
    public const string ProductName = "Test Product";
    public const string Sku = "PROD1";
    public const decimal Price = 100;
    public const string ProductDescription = "Test Product Description";
    public const int Stock = 10;
    public const string ProductExceptionMessage = "Product does not found.";
    public const string SkuExceptionMessage = "Duplicate Sku. Please correct.";
}
