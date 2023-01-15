using HappyCompany.Domain.Filters;
using System.Linq.Expressions;

namespace HappyCompany.Domain.Interface.Repos
{
    public interface IItemRepository: IRepository<Item>
    {
        Task<PagedResponse> GetItemPaged(paginationFilter paginationFilter, Expression<Func<Item, bool>> predicate);
    }
}
