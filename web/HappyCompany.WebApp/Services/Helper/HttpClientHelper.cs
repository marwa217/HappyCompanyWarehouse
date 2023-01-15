using System.Net.Http.Headers;

namespace HappyCompany.WebApp.Services.Helper
{
    public class HttpClientHelper
    {
        protected HttpClient _httpClient;
        public UserDto _userDto;
       // protected readonly IJsonUtils _jsonUtils;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _userDto = new UserDto();
        }

        // prepare header function 
        protected void PrepareHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _userDto.token);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        //protected StringContent CreateRequestBody(dynamic content)
        //{
        //    //TODO: check return Json 
        //    var json = JsonConvert.SerializeObject(content);
        //    return new StringContent(json, Encoding.UTF8, "application/json");

        //}
    }
}
