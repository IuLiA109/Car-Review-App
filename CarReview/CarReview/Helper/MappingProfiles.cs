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

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto , Review>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserDetails, UserDetailsDto>();
            CreateMap<UserDetailsDto , UserDetails>();
            /*CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Profil, ProfilDto>();
            CreateMap<ProfilDto , Profil>();*/
        }
    }
}
