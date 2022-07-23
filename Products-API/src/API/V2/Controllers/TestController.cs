using API.Controllers;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Value()
        {
            return "I'm V2";
        }
    }
}
