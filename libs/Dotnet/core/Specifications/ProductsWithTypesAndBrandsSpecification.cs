
using Enterprise.Dotnet.Core.Entities;

namespace Enterprise.Dotnet.Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
  public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(product =>
    (!productParams.BrandId.HasValue || product.ProductBrandId == productParams.BrandId)
    && (!productParams.TypeId.HasValue || product.ProductTypeId == productParams.TypeId)
  )
  {
    AddInclude(p => p.ProductType);
    AddInclude(p => p.ProductBrand);
    AddOrderBy(p => p.Name);
    ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

    if (!string.IsNullOrEmpty(productParams.Sort))
    {
      switch (productParams.Sort)
      {
        case "priceAsc":
          AddOrderBy(p => p.Price);
          break;
        case "priceDesc":
          AddOrderByDescending(p => p.Price);
          break;
        default:
          AddOrderBy(p => p.Name);
          break;
      }
    }
  }

  public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
  {
    AddInclude(p => p.ProductType);
    AddInclude(p => p.ProductBrand);
  }
}
