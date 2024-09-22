using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eTranscript.Models.DomainModels;
using eTranscript.Managers;

namespace eTranscript.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountManagementController : ControllerBase
    {
        private readonly AuthenticationManager _authentication;

        public AccountManagementController(AuthenticationManager authentication)
        {
            _authentication = authentication;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authentication.Login(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _authentication.Register(request);

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("registeradmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequestDto request)
        {
            var response = await _authentication.RegisterAdmin(request);

            return Ok(response);
        }

        //[AllowAnonymous]
        [HttpPost("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var response = await _authentication.ChangePassword(request);

            return Ok(response);
        }
    }


}
