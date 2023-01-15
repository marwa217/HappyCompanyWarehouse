using HappyCompany.Domain.Data;

namespace HappyCompany.WebApp.Services.Interfaces
{
    public interface IWareHouseService
    {
        Task<IEnumerable<Warehouse>> GetWareHouses();
        Task Add(WarehouseDto warehouseDto);
    }
}
