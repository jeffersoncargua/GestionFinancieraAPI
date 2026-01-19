using GestionFinancieraNet.Models.Dtos;
using GestionFinancieraNet.Repository.IRepository;
using GestionFinancieraNet.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinancieraNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public AuthenticationController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseDto>> Register([FromBody] RegisterDto register)
        {
            var _response = await _userRepo.Register(register);

            return StatusCode((int)_response.StatusCode, _response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto login)
        {
            var _response = await _userRepo.Login(login);

            return StatusCode((int)_response.StatusCode, _response);
        }
    }
}
