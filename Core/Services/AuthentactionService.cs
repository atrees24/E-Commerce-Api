using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Abstraction;
using Shared;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthentactionService(UserManager<User> _userManager,IOptions<JwtOptions> options) : IAuthentactionService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) throw new UnAthuraizedException("Email Doesn't Exsist");

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result) throw new UnAthuraizedException();

            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user)
                );
        }

        public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel)
        {
           
            var user = new User()
            {
                DisplayName = registerModel.DisplayName,
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                PhoneNumber = registerModel.PhoneNumber

            };


            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDTO(
               user.DisplayName,
               user.Email!,
               await CreateTokenAsync(user)
               );

        }



        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };


            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

            audience: jwtOptions.Audience,

            issuer: jwtOptions.Issure,

            expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),

            claims: authClaims,

            signingCredentials: signingCreds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
