using AutoMapper;
using GestionFinancieraNet.Data;
using GestionFinancieraNet.Models.Dtos;
using GestionFinancieraNet.Models.Entity;
using GestionFinancieraNet.Repository.IRepository;
using GestionFinancieraNet.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestionFinancieraNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        private readonly IMapper _mapper;
        private string secretKey;
        internal ResponseDto _response;
        
        public UserRepository(ApplicationDbContext db, UserManager<UserIdentity> userManager, RoleManager<IdentityRole> rolManager, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _rolManager = rolManager;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("JwtSettings:Secret");
            this._response = new ();
        }
        
        public async Task<bool> IsUnique(string correo)
        {
            var userExist = await _userManager.FindByEmailAsync(correo);

            if (userExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            var userExist = await _userManager.FindByEmailAsync(login.Email);

            if (userExist == null)
            {
                LoginResponseDto loginResponseDto = new()
                {
                    User = null,
                    Token = null,
                    Message = "No se encontro al usuario",
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                };

                return loginResponseDto;
            }

            var isValid = await _userManager.CheckPasswordAsync(userExist, login.Password);
            if (!isValid)
            {
                LoginResponseDto loginResponseDto = new()
                {
                    User = null,
                    Token = null,
                    Message = "Contraseña incorrecta",
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                };

                return loginResponseDto;
            }

            // Si el usuario y contraseña son validos
            var roles = await _userManager.GetRolesAsync(userExist);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userExist.Email!),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponse = new()
            {
                User = _mapper.Map<UserDto>(userExist),
                Token = tokenHandler.WriteToken(token),
                Message = "Login exitoso",
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            return loginResponse;
            
        }

        public async Task<ResponseDto> Register(RegisterDto register)
        {
            var userExist = await _userManager.FindByEmailAsync(register.Email);

            if (userExist != null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Result = null;
                _response.Message = "Ya existe un registro con ese correo";

                return _response;
            }

            UserIdentity user = new()
            {
                Email = register.Email,
                UserName = register.Email,
                Name = register.Name,
                Phone = register.Phone,
                NormalizedEmail = register.Email.ToUpper(),
            };

            try
            {
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    if (!await _rolManager.RoleExistsAsync(register.Role) && !string.IsNullOrEmpty(register.Role))
                    {
                        await _rolManager.CreateAsync(new IdentityRole(register.Role));
                    }
                    else
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.Result = null;
                        _response.Message = "No existe el rol de usuario";

                        return _response;
                    }

                    await _userManager.AddToRoleAsync(user, register.Role);

                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.Result = null;
                    _response.Message = "El registro se realizo con exito";

                    return _response;

                }

                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Result = null;
                _response.Message = result.Errors.FirstOrDefault().ToString();

                return _response;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Result = null;
                _response.Message = $"Se produjo una excepcion {ex.Message}";

                return _response;
            }
        }
    }
}
