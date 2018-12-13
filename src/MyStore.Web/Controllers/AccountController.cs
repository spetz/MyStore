using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.Infrastructure.Auth;
using MyStore.Services;

namespace MyStore.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IJwtService _jwtService;

        public AccountController(IDispatcher dispatcher,
            IJwtService jwtService) : base(dispatcher)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> Get()
        {
            return HttpContext.User.Identity.Name;
        }

        [HttpPost("login")]
        public async Task<ActionResult<JsonWebToken>> Login()
        {
            var jwt = _jwtService.Create(Guid.NewGuid().ToString(), "user1", "user", null);

            return await Task.FromResult(jwt);
        }
    }
}