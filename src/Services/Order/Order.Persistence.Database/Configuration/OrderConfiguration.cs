using Common.Helpers.RandomHelper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Order.Common.Enums.Enums;

namespace Order.Persistence.Database.Configuration
{
    public class OrderConfiguration
	{
		public OrderConfiguration(EntityTypeBuilder<Domain.Order> entityBuilder)
		{
			entityBuilder.HasKey(e => e.OrderId);

			entityBuilder.Property(e => e.ClientId)
				.IsRequired();

			entityBuilder.Property(e => e.CreatedAt);

			//entityBuilder.Property(x => x.Total)
			//	.HasColumnType("Decimal");

			entityBuilder.HasMany(e => e.Items)
				.WithOne();

			entityBuilder.HasData(GenerateRandomOrders());
		}
		private static List<Domain.Order> GenerateRandomOrders()
        {
			var orders = new List<Domain.Order>();
			var random = new Random();

            for (int i = 1; i <= 100; i++)
            {
				orders.Add(
					new()
					{
						OrderId = i,
						Status = (OrderStatus)random.Next(0, 2),
						PaymentType = (OrderPayment)random.Next(0,2),
						ClientId = random.Next(1,100),
						CreatedAt = DateTime.UtcNow,
						Total = random.NextDecimal(1, 999),
					});
            }
			return orders;
        }
	}
}

