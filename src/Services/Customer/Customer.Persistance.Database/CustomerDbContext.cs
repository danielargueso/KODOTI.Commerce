using Customer.Domain;
using Customer.Persistance.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Customer.Persistance.Database;

public class CustomerDbContext : DbContext
{
	public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
	{
	}

	public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Database schema
        modelBuilder.HasDefaultSchema(PersistenceSettings.DbSchemaName);

        _ = new ClientConfiguration(modelBuilder.Entity<Client>());
    }
}

