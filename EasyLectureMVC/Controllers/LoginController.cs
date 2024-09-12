using EasyLectureModel.Model.User;
using EasyLectureMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class LoginController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel request)
        {
            
            UserLoginRequestModel loginRequest = new()
            {
                Email = request?.UserName ?? "",
                Password = request?.Password  ?? "",
            };

            UserLoginResponseModel loginResponse = await _apiProvider.POST<UserLoginRequestModel, UserLoginResponseModel>(loginRequest, "User/login", _sessionManagement.GetToken());
            
            if (!string.IsNullOrEmpty(loginResponse?.Token ?? ""))
            {
                if (loginResponse.RoleId == 1)
                {
                    _sessionManagement.SetSession(loginResponse.Id, loginResponse.Email, loginResponse.Token,loginResponse.StudentId, loginResponse.RoleId);
                }
                else if (loginResponse.RoleId == 2)
                {
                    _sessionManagement.SetSession(loginResponse.Id, loginResponse.Email, loginResponse.Token, loginResponse.TeacherId, loginResponse.RoleId);
                }

                return RedirectToAction("index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect username or password. Please try again.";
            }

            return RedirectToAction("Index" , "Login");
        }
    }
}
