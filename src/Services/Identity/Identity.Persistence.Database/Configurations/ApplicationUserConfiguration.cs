using System;
using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Database.Configurations;

public class ApplicationUserConfiguration
{
	public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entityBuilder)
	{
		entityBuilder.HasKey(e => e.Id);

		entityBuilder.Property(e => e.FirstName)
			.IsRequired()
			.HasMaxLength(100);

		entityBuilder.Property(e => e.LastName)
			.IsRequired()
			.HasMaxLength(100);

		// Each User can have many entries in the UserRole join table
		entityBuilder
			.HasMany(e => e.UserRoles)
			.WithOne(e => e.User)
			.HasForeignKey(ur => ur.UserId)
			.IsRequired();

	}
}

