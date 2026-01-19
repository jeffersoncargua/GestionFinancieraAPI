using System.Net;

namespace GestionFinancieraNet.Utils
{
    public class ResponseDto
    {
        public ResponseDto()
        {
            Errors = new List<string>();
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object? Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public List<string> Errors { get; set; }
    }
}
