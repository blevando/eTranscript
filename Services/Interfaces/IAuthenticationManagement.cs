using System.IdentityModel.Tokens.Jwt;
using eTranscript.Models.DomainModels;

namespace eTranscript.Services.Repositories
{
    public interface IAuthenticationManagement
    {
        Task<string> Register(RegisterRequestDto request);
        Task<string> RegisterAdmin(RegisterRequestDto request);
        Task<string> Login(LoginRequestDto request);
        Task<string> ChangePassword(ChangePasswordRequestDto request);
    }
}

