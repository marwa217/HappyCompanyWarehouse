using HappyCompany.Domain.Data;
using HappyCompany.Domain.Filters;


namespace HappyCompany.Application.Repos
{
    public class WarehouseRepository : Repository<Warehouse> , IWarehouseRepository
    {
        public WarehouseRepository(ApplicationDbContext context):base(context)
        {
                
        }

        public async Task<PagedResponse> GetWarehousePaged(paginationFilter paginationFilter)
        {
            var warehousesList = context_.Warehouses;
                 

            var totalrecords = warehousesList.Count();

            var filteredWarehousesData =
                 warehousesList
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .ToList();

            var returnedObjs = filteredWarehousesData.Count < 1 ? null : filteredWarehousesData;

            return new PagedResponse()
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize,
                TotalRecords = totalrecords,
                Data = returnedObjs
            };
        }
    }
}
