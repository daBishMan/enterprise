using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Enterprise.Dotnet.API.DTO;
using Enterprise.Dotnet.API.Errors;
using Enterprise.Dotnet.Core.Entities;
using Enterprise.Dotnet.Core.Interfaces;
using Enterprise.Dotnet.Core.Specifications;
using Enterprise.Dotnet.API.Helpers;

namespace Enterprise.Server.DotnetApi.Controllers;

public class ProductsController : BaseApiController
{
  private readonly IGenericRepository<Product> productsRepo;
  private readonly IGenericRepository<ProductBrand> productBrandRepo;
  private readonly IGenericRepository<ProductType> productTypeRepo;
  private readonly IMapper mapper;

  public ProductsController(
    IGenericRepository<Product> productsRepo,
    IGenericRepository<ProductBrand> productBrandRepo,
    IGenericRepository<ProductType> productTypeRepo,
    IMapper mapper)
  {
    this.productsRepo = productsRepo;
    this.productBrandRepo = productBrandRepo;
    this.productTypeRepo = productTypeRepo;
    this.mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
   [FromQuery] ProductSpecParams productParams)
  {
    var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
    var countSpec = new ProductWithFiltersForCountSpecification(productParams);

    var products = await this.productsRepo.ListAsync(spec);
    var totalItems = await this.productsRepo.CountAsync(countSpec);

    var data = this.mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
    return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
  {
    var spec = new ProductsWithTypesAndBrandsSpecification(id);
    var product = await this.productsRepo.GetEntityWithSpec(spec);

    if (product == null)
    {
      return NotFound(new ApiResponse(404));
    }

    return this.mapper.Map<Product, ProductToReturnDTO>(product);
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
  {
    return Ok(await this.productBrandRepo.ListAllAsync());
  }

  [HttpGet("types")]
  public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
  {
    return Ok(await this.productTypeRepo.ListAllAsync());
  }
}
