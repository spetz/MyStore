using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Controllers;
using Xunit;

namespace MyStore.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public void given_valid_user_get_should_return_username()
        {
            var controller = new AccountController(null, null);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new List<ClaimsIdentity>()
                    {
                        new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, "user")
                        })
                    })
                }
            };

            var username = controller.Get();

            username.Should().NotBeNull();
        }
    }
}