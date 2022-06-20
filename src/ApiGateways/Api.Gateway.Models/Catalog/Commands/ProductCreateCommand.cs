namespace Api.Gateway.Models.Catalog.Commands;

public class ProductCreateCommand
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
}

