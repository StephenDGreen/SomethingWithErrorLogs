using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Something.Security;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ISomethingUserManager userManager;
        private readonly ILogger logger;

        public HomeController(ISomethingUserManager userManager, ILogger logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [AllowAnonymous]
        [Route("home/authenticate")]
        public ActionResult Authenticate()
        {
            var token = userManager.GetUserToken();
            return Ok(new { access_token = token});
        }
    }
}
