namespace MyECommerceApi.Domain.Models.DTO;

public class UpdateProduct
{
    public required string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public required string Sku { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
