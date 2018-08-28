using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EHR.AuthorizationServer.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}