using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyStore.Services;

namespace MyStore.Web.Controllers
{
    [Route("api")]
    public class HomeController : BaseController
    {
        private readonly IOptions<AppOptions> _appOptions;

        public HomeController(IDispatcher dispatcher,
            IOptions<AppOptions> appOptions) : base(dispatcher)
        {
            _appOptions = appOptions;
        }

        public ActionResult<string> Get() => _appOptions.Value.Name;
    }
}