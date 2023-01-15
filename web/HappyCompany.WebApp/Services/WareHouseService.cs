using HappyCompany.Domain.Data;
using HappyCompany.WebApp.Services.Helper;
using HappyCompany.WebApp.Services.Interfaces;
using System.Net.Http.Json;

namespace HappyCompany.WebApp.Services
{
    public class WareHouseService : HttpClientHelper, IWareHouseService
    {
        public WareHouseService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<Warehouse>> GetWareHouses()
        {
            if (_userDto.token != null)
            {
                return await _httpClient.GetFromJsonAsync<Warehouse[]>("list?PageNumber=1&PageSize=10");
            }
            return null;
       
        }
       

        public async Task Add(WarehouseDto warehouseDto)
        {
            if (_userDto.token != null)
            {
                await _httpClient.PostAsJsonAsync("api/Warehouse/warehouse/Add", warehouseDto);
            }
        }
    }
}
