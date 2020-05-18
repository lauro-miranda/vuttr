using Microsoft.AspNetCore.Mvc;

namespace LM.VUTTR.Api.Controllers
{
    [ApiController, Route("")]
    public class MeController : Controller
    {
        public IActionResult Get() => Ok("VUTTR");
    }
}