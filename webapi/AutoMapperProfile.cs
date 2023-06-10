using AutoMapper;
using Newtonsoft.Json;
using webapi.Entities;
using webapi.Entities.Dto;

namespace webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Holiday, HolidayDto>()
                .ForMember(dest => dest.ReferenceLinks, 
                            opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.ReferenceLinks)));

            CreateMap<HolidayDto, Holiday>()
                .ForMember(dest => dest.ReferenceLinks,
                            opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ReferenceLinks)));
        }
    }
}
