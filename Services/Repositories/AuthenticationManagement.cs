using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eTranscript.Models.DomainModels;
using eTranscript.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eTranscript.Services.Repositories
{

    public class AuthenticationManagement : IAuthenticationManagement
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManagement(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // Register a User
        public async Task<string> Register(RegisterRequestDto request)
        {
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            var userByUsername = await _userManager.FindByNameAsync(request.Username);
            if (userByEmail is not null || userByUsername is not null)
            {
                throw new ArgumentException($"User with email {request.Email} or username {request.Username} already exists.");
            }

            User user = new()
            {
                Email = request.Email,
                UserName = request.Username,
                MatricNumber= request.MatricNumber,
                PhoneNumber = request.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

      
            
            var result = await _userManager.CreateAsync(user, request.Password);            
            

            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
            }

            // Automatically login 
            return await Login(new LoginRequestDto { Username = request.Email, Password = request.Password });
        }

        // Register Admin

        public async Task<string> RegisterAdmin(RegisterRequestDto request)
        {
            // Check if user with the same email or username already exists
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            var userByUsername = await _userManager.FindByNameAsync(request.Username);
            if (userByEmail is not null || userByUsername is not null)
            {
                throw new ArgumentException($"User with email {request.Email} or username {request.Username} already exists.");
            }

            // Create new user object
            User user = new()
            {
                Email = request.Email,
                UserName = request.Username,
                MatricNumber = request.MatricNumber,
                PhoneNumber = request.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Create the user in the identity system
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
            }

            // Assign the 'Admin' role to the newly created user
            if (await _roleManager.RoleExistsAsync("Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                // Optionally, create the 'Admin' role if it doesn't exist
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (roleResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                  
                }
                else
                {
                    throw new Exception($"Failed to create 'Admin' role: {GetErrorsText(roleResult.Errors)}");
                }
            }

            // Automatically log in the newly registered user
            return await Login(new LoginRequestDto { Username = request.Email, Password = request.Password });
        }

        public async Task<string> Login(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(request.Username);
            }

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new ArgumentException($"Unable to authenticate user {request.Username}");
            }

            var authClaims = new List<Claim>
            {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
             

            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> ChangePassword(ChangePasswordRequestDto request)
        {
            // Retrieve the user by their username
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new ArgumentException($"User with username {request.Username} does not exist.");
            }

            // Change the password using the provided current and new passwords
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to change password: {GetErrorsText(result.Errors)}");
            }

            return "Password has been changed successfully.";
        }


        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        private string GetErrorsText(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}
