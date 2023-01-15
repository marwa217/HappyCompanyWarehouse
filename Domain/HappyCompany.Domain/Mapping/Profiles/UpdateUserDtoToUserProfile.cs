using AutoMapper;
using HappyCompany.Domain.Dtos;

namespace HappyCompany.Domain.Mapping.Profiles
{
    public class UpdateUserDtoToUserProfile : Profile
    {
        public UpdateUserDtoToUserProfile()
        {
            CreateMap<EditUserDto, User>().ReverseMap();
        }
    }
}

