using HappyCompany.Domain.Data;
using HappyCompany.Domain.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HappyCompany.Application.Repos
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) :base(context)
        {

        }

        public async Task<PagedResponse> GetItemPaged(paginationFilter paginationFilter, Expression<Func<Item, bool>> predicate)
        {
            var itemsList = context_
                .Items
                .Where(predicate)
                .Include(i => i.warehouse);

            var totalItems = itemsList.Count();

            var filteredItems =
                await itemsList
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .ToListAsync();

            var returnedObjs = filteredItems.Count < 1 ? null : filteredItems;

            return new PagedResponse()
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize,
                TotalRecords = totalItems,
                Data = returnedObjs
            }; 
        }
    }
}
