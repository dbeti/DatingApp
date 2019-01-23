using System.Linq;
using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(destination => destination.PhotoUrl, options => {
                    options.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(destination => destination.Age, options => {
                    options.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(destination => destination.PhotoUrl, options => {
                    options.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(destination => destination.Age, options => {
                    options.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDto>();
        } 
    }
}