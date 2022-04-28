
using Enterprise.Dotnet.Core.Entities;
using Enterprise.Dotnet.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Dotnet.Infrastructure.Data;
public class ProductRepository : IProductRepository
{
  private readonly StoreContext context;

  public ProductRepository(StoreContext context)
  {
    this.context = context;
  }

  public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
  {
    return (IReadOnlyList<ProductBrand>)await context.ProductBrands.ToListAsync();
  }

  public async Task<Product> GetProductByIdAsync(int id)
  {
    return await context.Products
      .Include(p => p.ProductType)
      .Include(p => p.ProductBrand)
      .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task<IReadOnlyList<Product>> GetProductsAsync()
  {
    return await context.Products
      .Include(p => p.ProductType)
      .Include(p => p.ProductBrand)
      .ToListAsync();
  }

  public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
  {
    return (IReadOnlyList<ProductType>)await context.ProductTypes.ToListAsync();
  }
}

