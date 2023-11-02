using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApplication.Authentication;
using TodoApplication.Entities;
using TodoApplication.Services.IAuthentication;

namespace TodoApplication.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<dynamic> Login(Login request)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return JsonConvert.SerializeObject(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return null;
        }

        public async Task<dynamic> Register(Register request)
        {
            var userExists = await userManager.FindByNameAsync(request.Username);

            if (userExists != null)
            {
                return null;
            };

            User user = new User()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return null;
            }
            return "User Successfully created";

        }

        public async Task<dynamic> RegisterAdmin(Register admin)
        {
            var userExists = await userManager.FindByNameAsync(admin.Username);

            if (userExists != null)
            {
                return null;
            };

            User user = new User()
            {
                Email = admin.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = admin.Username
            };

            var result = await userManager.CreateAsync(user, admin.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRole.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRole.User));
            }

            if (await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRole.Admin);
            }

            return "User created successfully";
        }
    }
}
