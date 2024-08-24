using Microsoft.EntityFrameworkCore;
using Operators.Server.Entities.Database;

namespace Operators.Server.Entities.Extensions
{
	public static class ProgramExtension
	{
		public static void Deploy(this WebApplication app)
		{
			using var context = app.Services.GetService<ApplicationContext>();

			// If has any not applied migrations try apply it
			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
		}
	}
}
