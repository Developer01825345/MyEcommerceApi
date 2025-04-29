namespace MyECommerceApi.Domain.Models.Domain;

public class Product
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public required string Sku { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}