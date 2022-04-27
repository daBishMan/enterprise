using Enterprise.Dotnet.Core.Entities;
using Enterprise.Dotnet.Core.Interfaces;
using Enterprise.Dotnet.Core.Specifications;
using Microsoft.EntityFrameworkCore;


namespace Enterprise.Dotnet.Infrastructure.Data;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
  private readonly StoreContext context;

  public GenericRepository(StoreContext context)
  {
    this.context = context;
  }

  public async Task<T> GetByIdAsync(int id)
  {
    return await this.context.Set<T>().FindAsync(id);
  }

  public async Task<IReadOnlyList<T>> ListAllAsync()
  {
    return await this.context.Set<T>().ToListAsync();
  }

  public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
  {
    return await ApplySpecification(spec).FirstOrDefaultAsync();
  }

  public async Task<int> CountAsync(ISpecification<T> spec)
  {
    return await ApplySpecification(spec).CountAsync();
  }

  public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
  {
    return await ApplySpecification(spec).ToListAsync();
  }

  private IQueryable<T> ApplySpecification(ISpecification<T> spec)
  {
    return SpecificationEvaluator<T>.GetQuery(this.context.Set<T>().AsQueryable(), spec);
  }

}
