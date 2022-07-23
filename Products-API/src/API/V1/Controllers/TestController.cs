using API.Controllers;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/teste")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Value()
        {
            return "I'm V1";
        }
    }
}
