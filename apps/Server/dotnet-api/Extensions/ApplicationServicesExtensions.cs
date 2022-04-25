using Microsoft.AspNetCore.Mvc;
using Enterprise.Dotnet.API.Errors;
using Enterprise.Dotnet.Infrastructure.Data;
using Enterprise.Dotnet.Core.Interfaces;

namespace Enterprise.Dotnet.API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    // custom error handling for validation errors
    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.InvalidModelStateResponseFactory = actionContext =>
      {
        var errors = actionContext.ModelState
          .Where(e => e.Value.Errors.Count > 0)
          .SelectMany(x => x.Value.Errors)
          .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse { Errors = errors };

        return new BadRequestObjectResult(errorResponse);
      };
    });

    return services;
  }
}

