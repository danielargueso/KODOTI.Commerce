using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.Database.Configuration;

namespace Order.Persistence.Database;

public class OrderDbContext : DbContext
{
	public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
	{
	}

	public DbSet<Domain.Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Database schema
        modelBuilder.HasDefaultSchema(PersistenceSettings.DbSchemaName);

        ModelConfig(modelBuilder);
    }
    private void ModelConfig(ModelBuilder modelBuilder)
    {
        _ = new OrderConfiguration(modelBuilder.Entity<Domain.Order>());
        _ = new OrderDetailConfiguration(modelBuilder.Entity<OrderDetail>());
    }
}

