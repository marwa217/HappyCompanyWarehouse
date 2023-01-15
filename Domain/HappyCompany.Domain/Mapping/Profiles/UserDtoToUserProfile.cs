using AutoMapper;
using HappyCompany.Domain.Dtos;

namespace HappyCompany.Domain.Mapping.Profiles
{
    public class UserDtoToUserProfile : Profile
    {
        public UserDtoToUserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
