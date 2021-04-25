using Microsoft.AspNetCore.Mvc;

namespace BankingChallenge.Api.Controllers
{
    [Route("")]
    [ApiController]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Cloud Engineer Banking Challenge V1.1";
        }
    }
}
