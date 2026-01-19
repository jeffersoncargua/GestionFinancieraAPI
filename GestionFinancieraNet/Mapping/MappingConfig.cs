using AutoMapper;
using GestionFinancieraNet.Models.Dtos;
using GestionFinancieraNet.Models.Entity;

namespace GestionFinancieraNet.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserDto, UserIdentity>().ReverseMap();
        }
    }
}
