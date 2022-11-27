using AutoMapper;
using Journal.Dto.Create;
using Journal.Dto.Get;
using Journal.Dto.Update;
using Journal.Models;

namespace Journal.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            CreateMap<Department, GetDepartmentDto>();
            CreateMap<PostPersonDto, Person>();
            CreateMap<UpdatePersonDto, Person>();
            CreateMap<Person, GetPersonDto>()
                .ForMember(dest => dest.GetDepartmentDto, opt => opt.MapFrom(src => src.Department));
            CreateMap<PostWorkLogDto, WorkLog>();
            CreateMap<UpdateWorkLogDto, WorkLog>();
            CreateMap<WorkLog, GetWorkLogDto>()
                .ForMember(dest => dest.GetPersonDto, opt => opt.MapFrom(src => src.Person));
            CreateMap<WorkLog, GetWorkLogTableDto>()
                .ForMember(dest => dest.GetWorkLogDto, opt => opt.MapFrom(src => src));
        }
    }
}
