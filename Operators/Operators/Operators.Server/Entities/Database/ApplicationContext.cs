using Microsoft.EntityFrameworkCore;
using Operators.Server.Entities.Operators;

namespace Operators.Server.Entities.Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Operator> Operators { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Operator>()
				.HasKey(q => q.Code);

			modelBuilder.Entity<Operator>()
				.HasIndex(q => q.Name)
				.IsUnique();
		}
	}
}
