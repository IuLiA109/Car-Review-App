using AutoMapper;
using CarReview.Dto;
using CarReview.Models;

namespace CarReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto , Car>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto , Category>();
        }
    }
}
