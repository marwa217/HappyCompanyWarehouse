namespace HappyCompany.WebApp.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAll(int WarehouseID);
        Task Add(Item item);
    }
}
