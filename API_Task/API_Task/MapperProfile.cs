using API_Task.DTOs.CategoryDto;
using API_Task.Entities;
using AutoMapper;

namespace API_Task
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(destination => destination.FullName, opt => opt.MapFrom(src => src.FirstName + "" + src.LastName));
        }

    }
}
