using AutoMapper;
using MinhaApi.Models;

namespace MinhaApi.DTOs.Mapping
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper() 
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
