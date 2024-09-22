using eTranscript.Models.DomainModels;
using eTranscript.Services.Repositories;

namespace eTranscript.Managers
{
    public class AuthenticationManager
    {
        private readonly IAuthenticationManagement _authentication;
        public AuthenticationManager( IAuthenticationManagement authentication)
        {
            _authentication = authentication;
        }
       public  async Task<string> Register(RegisterRequestDto request)
        {
           return await _authentication.Register(request);
        }

        public async Task<string> RegisterAdmin(RegisterRequestDto request)
        {
            return await _authentication.RegisterAdmin(request);
        }

        public async   Task<string> Login(LoginRequestDto request)
        {
            return await _authentication.Login(request);
        }

       public async Task<string> ChangePassword(ChangePasswordRequestDto request)
        {
            return await _authentication.ChangePassword(request);
        }
    }
}
