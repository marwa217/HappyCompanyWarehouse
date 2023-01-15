using HappyCompany.Domain.Dtos;

namespace HappyCompany.Domain.Interface.Services
{
    public interface IItemServiceHandler
    {
        Task<Response> GetItemListPage(PaginationQueryDto paginationQuery, int WarehouseId);

        Task<Response> AddItem(Item entity);
    }
}
