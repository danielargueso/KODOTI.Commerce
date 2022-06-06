using Customer.Persistance.Database;
using Microsoft.EntityFrameworkCore;

namespace Customer.Tests.Config
{
    public static class CustomerDbContextInMemory
	{
		public static CustomerDbContext Get()
        {
			var options = new DbContextOptionsBuilder<CustomerDbContext>()
				.UseInMemoryDatabase(databaseName: "Customer.Db").
				Options;

			return new CustomerDbContext(options);
				
        }
	}
}

