using EasyLectureModel;
using EasyLectureModel.Model.Lecture;
using EasyLectureModel.Model.Student;
using EasyLectureMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyLectureMVC.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (!_sessionManagement.HasToken())
                return RedirectToAction("index", "login");

            RoleType role = _sessionManagement.GetRole();

            if (role == RoleType.Student)
            {
                StudentViewModel data = await GetStudentData(_sessionManagement.GetStudentId());
                return View(data);
            }
            else if (role == RoleType.Teacher)
            {
                return View();
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<StudentViewModel> GetStudentData(int studentId)
        {
            LectureAllResponseModel lectures = await _apiProvider.POST<object, LectureAllResponseModel>(null, "Lecture/getlist", _sessionManagement.GetToken());

            StudentModel student = await _apiProvider.POST<StudentRequestModel, StudentModel>(new StudentRequestModel() { id = studentId }, "Student/get", _sessionManagement.GetToken());

            StudentViewModel result = new StudentViewModel
            {
                UserName = student.Name,
                EMail = student.EMail,
                Lectures = new List<string>() 
            };

            foreach (var lectureId in student.LectureIds)
            {
                var lecture = lectures.Lectures.FirstOrDefault(l => l.LctId == lectureId);
                if (lecture != null)
                {
                    result.Lectures.Add(lecture.LctName);
                }
            }

            return result;
        }

    }
}
