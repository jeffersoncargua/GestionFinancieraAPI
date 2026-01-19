using GestionFinancieraNet.Models.Dtos;
using GestionFinancieraNet.Utils;

namespace GestionFinancieraNet.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUnique(string correo);

        Task<ResponseDto> Register(RegisterDto register);

        Task<LoginResponseDto> Login(LoginDto login);
    }
}
