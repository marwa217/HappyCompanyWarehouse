using AutoMapper;
using HappyCompany.Domain.Dtos;
using HappyCompany.Domain.Filters;

namespace HappyCompany.Domain.Mapping.Profiles
{
    public class PaginationQueryToPaginationFilterProfile: Profile
    {
        public PaginationQueryToPaginationFilterProfile()
        {
            CreateMap<PaginationQueryDto, paginationFilter>().ReverseMap();
        }
    }
}
