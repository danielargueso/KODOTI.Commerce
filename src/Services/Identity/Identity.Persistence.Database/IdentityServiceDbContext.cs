using System;
using Identity.Domain;
using Identity.Persistence.Database.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Database
{
	public class IdentityServiceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public IdentityServiceDbContext(
			DbContextOptions<IdentityServiceDbContext> options
			) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			// Database schema

			builder.HasDefaultSchema(PersistenceSettings.DbSchemaName);

			ModelConfiguration(builder);
        }

		private void ModelConfiguration(ModelBuilder modelBuilder)
        {
			new ApplicationUserConfiguration(modelBuilder.Entity<ApplicationUser>());
			new ApplicationRoleConfiguration(modelBuilder.Entity<ApplicationRole>());
        }
    }
}

