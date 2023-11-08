using AutoMapper;
using RunGroops.Models;
using RunGroops.ViewModels;

namespace RunGroops.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreateClubViewModel, Club>();
            CreateMap<CreateRaceViewModel, Race>();
            CreateMap<Club, EditClubViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.URL, opt => opt.MapFrom(s => s.Image));
            CreateMap<EditClubViewModel, Club>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s => s.URL));
            CreateMap<EditRaceViewModel,Race>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s => s.URL));
            CreateMap<Race, EditRaceViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.URL, opt => opt.MapFrom(s => s.Image));
        }
    }
}
