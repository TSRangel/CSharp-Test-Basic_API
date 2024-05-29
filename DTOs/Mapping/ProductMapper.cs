using AutoMapper;
using MinhaApi.Models;

namespace MinhaApi.DTOs.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper() 
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductDTOUpdateRequest, Product>().ReverseMap();
            CreateMap<Product, ProductDTOUpdateResponse>();
        }
    }
}
