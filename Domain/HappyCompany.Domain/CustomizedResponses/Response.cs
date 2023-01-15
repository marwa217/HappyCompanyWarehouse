using HappyCompany.Domain.Interface.Common;
using HappyCompany.Infra.Constants;

namespace HappyCompany.Domain.CustomizedResponses
{
    public class Response : IResponse
    {
        public dynamic Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public HttpResponse HttpResponse { get; set; }
    }
}
