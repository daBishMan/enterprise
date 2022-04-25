using AutoMapper;
using Enterprise.Dotnet.API.DTO;
using Enterprise.Dotnet.Core.Entities;

namespace Enterprise.Dotnet.API.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Product, ProductToReturnDTO>()
      .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
      .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
      .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
  }
}
