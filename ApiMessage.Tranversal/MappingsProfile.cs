using ApiMessage.Application.DTO.UserDTO;
using ApiMessage.Models;
using AutoMapper;

namespace ApiMessage.Tranversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<UserInfoME, UserInfoDTO>().ReverseMap();
        }
    }
}
