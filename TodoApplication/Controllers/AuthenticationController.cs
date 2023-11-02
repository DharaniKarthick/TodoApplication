using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApplication.Authentication;
using TodoApplication.Entities;
using TodoApplication.Services.IAuthentication;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _auth;

        public AuthenticationController(IAuthenticationService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            try
            {
                var result = await _auth.Login(model);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized();
            }
            catch(Exception ex) 
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {

            try
            {
                var result = await _auth.Register(model);
                if (result != null)
                {
                    return Ok(result);
                }
                Response response = new Response { Status = "Error", Message ="Cannot register the user" };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            try
            {
                var result = await _auth.RegisterAdmin(model);
                if (result != null)
                {
                    return Ok(result);
                }
                Response response = new Response { Status = "Error", Message = "Cannot register the admin" };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                Response response = new Response { Status = "Error", Message = ex.Message };
                return new JsonResult(response);
            }

        }
    }
}
