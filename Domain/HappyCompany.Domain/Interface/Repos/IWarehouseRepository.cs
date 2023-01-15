using HappyCompany.Domain.Filters;
using System.Linq.Expressions;

namespace HappyCompany.Domain.Interface.Repos
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<PagedResponse> GetWarehousePaged(paginationFilter paginationFilter);
    }
}
