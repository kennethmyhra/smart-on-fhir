using Microsoft.AspNetCore.Mvc;

namespace EHR.SmartApp.Controllers.Launch
{
    public class LaunchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string code, string state)
        {
            ViewBag.code = code;
            ViewBag.state = state;

            return View();
        }
    }
}