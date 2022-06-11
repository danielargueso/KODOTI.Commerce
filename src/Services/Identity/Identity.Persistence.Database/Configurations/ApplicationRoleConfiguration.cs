using System;
using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Database.Configurations;

public class ApplicationRoleConfiguration
{
	public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityBuilder)
	{
		entityBuilder.HasKey(x => x.Id);

		entityBuilder.HasData(
			new ApplicationRole
            {
				Id = Guid.NewGuid().ToString().ToLower(),
				Name = "Admin",
				NormalizedName = "ADMIN"
            });

		// Each Role can have many entries in the UserRole join table
		entityBuilder
			.HasMany(e => e.UserRoles)
			.WithOne(e => e.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired();
	}
}

