using AutoMapper;
using HappyCompany.Domain.Data;

namespace HappyCompany.Domain.Mapping.Profiles
{
    public class WarehouseToDto : Profile
    {
        public WarehouseToDto()
        {
            CreateMap<WarehouseDto, Warehouse>().ReverseMap();
        }
    }
}
