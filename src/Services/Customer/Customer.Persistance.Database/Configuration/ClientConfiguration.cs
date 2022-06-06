using Customer.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistance.Database.Configuration;

public class ClientConfiguration
{
	public ClientConfiguration(EntityTypeBuilder<Client> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(e => e.ClientId);

		entityTypeBuilder.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(150);

		entityTypeBuilder.HasData(GenerateRandomClients());

	}

	private List<Client> GenerateRandomClients()
    {
		var clients = new List<Client>();

        for (int i = 1; i <= 100; i++)
        {
			clients.Add(
				new()
				{
					ClientId = i,
					Name = $"Client {i:000}"
				});
        }

		return clients;
    }
}

