using EasyLectureMVC.Classes;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected ApiProvider _apiProvider;
        protected SessionManagement _sessionManagement;

        public ControllerBase()
        {
            _apiProvider = new ApiProvider("https://localhost:7107");
            _sessionManagement = new(this);
        }
    }
}
