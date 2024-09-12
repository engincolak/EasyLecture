using EasyLectureMVC.Classes;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class LogoutController : ControllerBase
    {
        public IActionResult Index()
        {

            _sessionManagement.SetSession(0,"", "", 0, 0);
            return RedirectToAction("index", "login");

        }
    }
}
