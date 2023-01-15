using Newtonsoft.Json.Linq;

namespace HappyCompany.Infra.Utils.Interfaces
{
    public interface IJsonUtils
    {
        T DeserializeObject<T>(dynamic value);
        Task<JArray> GetJArrayData(HttpContent content, string level = null);
        Task<JObject> GetJObjectData(HttpContent content);
        string SerializeObject(dynamic value);
    }
}
