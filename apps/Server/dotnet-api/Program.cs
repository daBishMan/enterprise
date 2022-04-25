using Microsoft.EntityFrameworkCore;
using Enterprise.Dotnet.API.Extensions;
using Enterprise.Dotnet.API.Helpers;
using Enterprise.Dotnet.API.Middleware;
using Enterprise.Dotnet.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container, using the extension method.
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x =>
  x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ApplicationExceptionMiddleware>();
app.UseSwaggerDocumentation();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.PopulateDatabaseAsync().Wait();

app.Run();
