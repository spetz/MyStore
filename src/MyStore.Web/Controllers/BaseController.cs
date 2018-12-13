using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services;

namespace MyStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected ActionResult<T> Result<T>(T result)
        {
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        protected Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(c => c.Type == "userId").Value);

        protected async Task SendAsync<T>(T command) where T : ICommand
            => await _dispatcher.SendAsync(command);

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _dispatcher.QueryAsync(query);
    }
}