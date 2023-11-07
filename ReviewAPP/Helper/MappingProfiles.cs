using AutoMapper;
using ReviewAPP.Dto;
using ReviewAPP.Models;

namespace ReviewAPP.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Place, PlaceDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();     
            CreateMap<Review,ReviewDto>().ReverseMap();  
            CreateMap<Reviewer,ReviewerDto>().ReverseMap();  
        }
    }
}
