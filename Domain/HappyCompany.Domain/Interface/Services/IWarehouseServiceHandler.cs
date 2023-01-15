

using HappyCompany.Domain.Data;
using HappyCompany.Domain.Dtos;

namespace HappyCompany.Domain.Interface.Services
{
    public interface IWarehouseServiceHandler
    {
        Task<Response> GetWarehouseListPage(PaginationQueryDto paginationQuery);

        Task<Response> GetWarehouseDetails(int Id);
        Task<Response> AddWarehouse(WarehouseDto entity);
    }
}
