using Common.Helpers.MathHelpers;
using Common.Helpers.RandomHelper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.Configuration
{
    public class OrderDetailConfiguration
	{
		public OrderDetailConfiguration(EntityTypeBuilder<OrderDetail> entityBuilder)
		{
			entityBuilder.HasKey(e => e.OrderDetailId);

			entityBuilder.Property(e => e.ProductId)
				.IsRequired();

			//entityBuilder.Property(x => x.UnitPrice)
			//	.HasColumnType("Decimal");

			//entityBuilder.Property(x => x.Total)
			//	.HasColumnType("Decimal");

			entityBuilder.HasData(GenerateRandomOrderDetails());
		}
		private static List<OrderDetail> GenerateRandomOrderDetails()
		{

			/*
			 * Ejecutar tras migración para calcular importes de la tabla Orders.

				UPDATE o
				SET o.Total = t.sumPrice
				FROM KodotiOrder.Orders AS o
				INNER JOIN
				(
					SELECT OrderId, SUM(Total) sumPrice
					FROM KodotiOrder.OrderDetails d
					GROUP BY OrderId
				) t
				ON t.OrderId = o.OrderId
			*/
			var ordersDetails = new List<OrderDetail>();
			var random = new Random();
			var precision = 2;

			for (int i = 1; i <= 400; i++)
			{
				var qty = random.Next(1, 10);
				var unitPrice = random.NextDecimal(1, 50).Round(precision);
				var total = (qty * unitPrice).Round(precision);


				ordersDetails.Add(
					new()
					{
						OrderDetailId = i,
						OrderId = random.Next(1,101),
						ProductId = random.Next(1, 101),
						Quantity = qty,
						UnitPrice = unitPrice,
						Total = total,
					});
			}
			return ordersDetails;
		}
	}
}

