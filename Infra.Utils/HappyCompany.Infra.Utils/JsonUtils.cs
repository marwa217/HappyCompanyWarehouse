using Newtonsoft.Json;
using HappyCompany.Infra.Utils.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace HappyCompany.Infra.Utils
{
    public class JsonUtils : IJsonUtils
    {
        private readonly ILogger _logger;


        public T DeserializeObject<T>(dynamic value)
        {
            if (value == null)
                return (T)Activator.CreateInstance(typeof(T));

            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var objects = JsonConvert.DeserializeObject<T>(value.ToString(), settings);

                if (objects == null)
                    return (T)Activator.CreateInstance(typeof(T));

                return objects;

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<JArray> GetJArrayData(HttpContent content, string level = null)
        {
            var stringData = await content.ReadAsStringAsync();

            try
            {
                if (!string.IsNullOrEmpty(level))
                {
                    JObject jObect = JObject.Parse(stringData);
                    return JArray.Parse(jObect[$"{level}"].ToString());
                }

                var jsonData = JArray.Parse(stringData);

                return jsonData;
            }
            catch (Newtonsoft.Json.JsonReaderException jsonExp)
            {
                _logger.LogError(
                        $"Content is not valid JSON {jsonExp.Message} > {jsonExp.ToString()}"
                        );
                return null;
            }
        }

        public async Task<JObject> GetJObjectData(HttpContent content)
        {
            var stringData = await content.ReadAsStringAsync();
            try
            {
                var jsonData = JObject.Parse(stringData);

                return jsonData;
            }
            catch (Newtonsoft.Json.JsonReaderException jsonExp)
            {
                _logger.LogError(
                        $"Content is not valid JSON {jsonExp.Message} > {jsonExp.ToString()}"
                        );
                return null;
            }
        }

        public string SerializeObject(dynamic value)
        {
            if (value == null)
                return "";

            try
            {
                var result = JsonConvert.SerializeObject(value);

                if (string.IsNullOrEmpty(result) || result.Equals("null"))
                    return "";

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
