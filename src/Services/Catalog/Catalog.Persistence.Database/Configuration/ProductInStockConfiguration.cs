using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductInStockConfiguration
	{
		public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder)
		{
			entityBuilder.HasKey(x => x.ProductInStockId);

			entityBuilder.HasData(GenerateRandomProductsInStock());
		}

		private static List<ProductInStock> GenerateRandomProductsInStock()
		{
			var productsInStock = new List<ProductInStock>();
			var random = new Random();

			for (int i = 1; i <= 100; i++)
			{
				productsInStock.Add(new ProductInStock
				{
						ProductInStockId = i,
						ProductId = i,
						Stock = random.Next(1, 100)
					
				});
			}

			return productsInStock;
		}
	}
}

