using AutoMapper;
using MyECommerceApi.Domain.Models.Domain;
using MyECommerceApi.Domain.Models.DTO;

namespace MyECommerceApi.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, CreateProduct>().ReverseMap();
        CreateMap<Product, UpdateProduct>().ReverseMap();
    }
}
