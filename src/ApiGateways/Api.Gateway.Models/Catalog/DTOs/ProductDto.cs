namespace Api.Gateway.Models.Catalog.DTOs;

public class ProductDto
{
    public int ProductId { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public ProductInStockDto Stock { get; set; } = new();
}

