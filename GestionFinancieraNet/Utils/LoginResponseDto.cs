using GestionFinancieraNet.Models.Dtos;
using System.Net;

namespace GestionFinancieraNet.Utils
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }

        public UserDto? User { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
