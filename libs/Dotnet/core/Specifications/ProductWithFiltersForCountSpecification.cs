
using Enterprise.Dotnet.Core.Entities;

namespace Enterprise.Dotnet.Core.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
  public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) : base(product =>
  (!productParams.BrandId.HasValue || product.ProductBrandId == productParams.BrandId)
  && (!productParams.TypeId.HasValue || product.ProductTypeId == productParams.TypeId)
)
  {

  }
}
