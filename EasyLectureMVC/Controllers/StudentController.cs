using EasyLectureModel.Model;
using EasyLectureModel.Model.Student;
using EasyLectureMVC.Classes;
using EasyLectureMVC.Models.Student;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class StudentController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            var StudentData = await GetListStudent();

            if (!_sessionManagement.HasToken())
                return RedirectToAction("index", "login");

            return View(StudentData);
        }

        public async Task<StudentResponseModel> GetListStudent()
        {

            StudentResponseModel StudentResponse = await _apiProvider.POST<object, StudentResponseModel>(null, "Student/getlist", _sessionManagement.GetToken());

            return StudentResponse;
        }

        public async Task<IActionResult> EditStudent(int id)
        {
            if (!_sessionManagement.HasToken())
                return RedirectToAction("Index", "Login");

                var lectureData = await GetPreset(id);
                return View(lectureData);
        }

        [HttpPost]
        public async Task<StudentViewPresetModel> GetPreset(int id)
        {
            StudentViewPresetModel result = new();

            var studentRequest = new StudentRequestModel
            {
                id = id,
            };

            StudentPresetModel studentPresetModel = await _apiProvider.POST<StudentRequestModel, StudentPresetModel>(studentRequest, "Student/getpreset", _sessionManagement.GetToken());

            if (id > 0)
            {
                result.Student = new StudentEditViewModel()
                {
                    Id = studentPresetModel.Student.Id,
                    EMail = studentPresetModel.Student.EMail,
                    Name = studentPresetModel.Student.Name,
                    Password = studentPresetModel.Student.Password,
                    RoleId = studentPresetModel.Student.RoleId,
                    LectureIds = studentPresetModel.Student.LectureIds,
                };
            }
            else
            {
                result.Student = new StudentEditViewModel()
                {
                    Id = 0,
                    EMail = " ",
                    Name = " ",
                    Password = " ",
                    RoleId = 1,
                    LectureIds = new List<int>(),
                };
            }

            result.Lecture = studentPresetModel.Lecture.Select(x => new LectureViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            result.Role = studentPresetModel.Role.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Save(StudentEditViewModel studentEditViewModel)
        {
            var studentRequest = new StudentEditRequestViewModel
            {
                id = studentEditViewModel.Id,
                name = studentEditViewModel.Name,
                eMail = studentEditViewModel.EMail, 
                password = studentEditViewModel.Password,
                roleId = studentEditViewModel.RoleId,
                lectureIds = studentEditViewModel.LectureIds,
            };

            await _apiProvider.POST<StudentEditRequestViewModel, StudentEditViewModel>(studentRequest, "Student/save", _sessionManagement.GetToken());
            return Redirect("/Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var studentRequest = new StudentViewRequestModel
            {
                id = id
            };

            await _apiProvider.POST<StudentViewRequestModel, StudentEditViewModel>(studentRequest, "Student/delete", _sessionManagement.GetToken());
            return RedirectToAction("Index");
        }



    }
}
