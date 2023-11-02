using NuGet.Protocol.Plugins;
using TodoApplication.Entities;

namespace TodoApplication.Services.IAuthentication
{
    public interface IAuthenticationService
    {
        Task<dynamic> Register(Register request);
        Task<dynamic> Login(Login request);
        Task<dynamic> RegisterAdmin(Register admin);
    }
}
