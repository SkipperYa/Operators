using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Operators.Server.Entities.ControllerFilters;
using Operators.Server.Entities.Database;
using Operators.Server.Entities.Extensions;
using Operators.Server.Interfaces;
using Operators.Server.Services;
using System.IO.Compression;

namespace Operators.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			// Add services to the container.

			builder.Services.AddResponseCaching();
			builder.Services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<BrotliCompressionProvider>();
			});

			builder.Services.AddRequestTimeouts(options =>
			{
				options.AddPolicy("DefaultTimeout10s", new RequestTimeoutPolicy()
				{
					Timeout = TimeSpan.FromMilliseconds(10000),
				});
			});

			builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Optimal;
			});

			builder.Services.AddControllers(options =>
			{
				options.Filters.Add(typeof(GlobalExceptionFilter));
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var connection = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection), ServiceLifetime.Transient, ServiceLifetime.Transient);

			builder.Services.AddTransient<IOperatorsRepositoryService, OperatorsRepositoryService>();
			builder.Services.AddTransient<IOperatorValidatorService, OperatorValidatorService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers();

			app.UseResponseCompression();

			app.MapFallbackToFile("/index.html");

			app.Deploy();

			app.Run();
		}
	}
}
