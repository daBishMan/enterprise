
using Microsoft.EntityFrameworkCore;
using Enterprise.Dotnet.Infrastructure.Data;

namespace Enterprise.Dotnet.API.Extensions;

public static class SwaggerServiceExtensions
{
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
  {
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    return services;
  }

  public static WebApplication UseSwaggerDocumentation(this WebApplication app)
  {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solutions.Dotnet.API V1");
    });

    return app;
  }

  public static async Task<WebApplication> PopulateDatabaseAsync(this WebApplication app)
  {
    // To Seed the Data
    using (var scope = app.Services.CreateScope())
    {
      var services = scope.ServiceProvider;
      var loggerFactory = services.GetRequiredService<ILoggerFactory>();
      try
      {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
      }
    }

    return app;
  }
}
