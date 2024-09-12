using EasyLectureModel.Model;
using EasyLectureMVC.Models;
using EasyLectureMVC.Models.Student;
using EasyLectureMVC.Models.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class TeacherController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            var TeacherData = await GetListTeacher();

            if (!_sessionManagement.HasToken())
                return Redirect("/login");

            return View(TeacherData);
        }

        public async Task<TeacherViewResponseModel> GetListTeacher()
        {

            TeacherViewResponseModel TeacherResponse = await _apiProvider.POST<object, TeacherViewResponseModel>(null, "Teacher/getlist", _sessionManagement.GetToken());

            return TeacherResponse;
        }

        public async Task<IActionResult> EditTeacher(int id)
        {
            if (!_sessionManagement.HasToken())
                return RedirectToAction("Index", "Login");

            var teacherData = await Get(id);
            return View(teacherData);
        }

        [HttpPost]
        public async Task<TeacherViewModel> Get(int id)
        {
            TeacherViewModel result = new();

            var teacherRequest = new TeacherViewRequestModel 
            {
                id = id,
            };
            if (id > 0)
            {
                TeacherViewModel teacherModel = await _apiProvider.POST<TeacherViewRequestModel, TeacherViewModel>(teacherRequest, "Teacher/get", _sessionManagement.GetToken());

            
                result.Id = teacherModel.Id;
                result.Name = teacherModel.Name;
                result.EMail = teacherModel.EMail;
                result.Password = teacherModel.Password;
            }
            else
            {
                result.Id = 0;
                result.Name = " ";
                result.EMail = " ";
                result.Password = " ";
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Save(TeacherViewModel teacherViewModel)
        {
            var studentRequest = new StudentEditRequestViewModel
            {
                id = teacherViewModel.Id,
                name = teacherViewModel.Name,
                eMail = teacherViewModel.EMail,
                password = teacherViewModel.Password,
                roleId = 2,
            };

            await _apiProvider.POST<StudentEditRequestViewModel, StudentEditViewModel>(studentRequest, "Teacher/save", _sessionManagement.GetToken());
            return Redirect("/Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var studentRequest = new StudentViewRequestModel
            {
                id = id
            };

            await _apiProvider.POST<StudentViewRequestModel, StudentEditViewModel>(studentRequest, "Teacher/delete", _sessionManagement.GetToken());
            return RedirectToAction("Index");
        }




    }
}
