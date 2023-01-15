using HappyCompany.WebApp.Services.Helper;
using HappyCompany.WebApp.Services.Interfaces;
using System.Net.Http.Json;

namespace HappyCompany.WebApp.Services
{
    public class ItemService : HttpClientHelper, IItemService
    {
        public ItemService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<Item>> GetAll(int WarehouseID)
        {
            if (_userDto.token != null)
            {
                return await _httpClient.GetFromJsonAsync<Item[]>($"api/Item/list/{WarehouseID}?PageNumber=1&PageSize=10");
            }
            return
                null;
        }

        public async Task Add(Item item)
        {
            if (_userDto.token != null)
            {
                await _httpClient.PostAsJsonAsync("api/Item/item/add", item);
            }
        }

    }
}
