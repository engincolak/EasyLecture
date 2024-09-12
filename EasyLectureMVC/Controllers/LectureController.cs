using EasyLectureModel.Model.Lecture;
using EasyLectureMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Controllers
{
    public class LectureController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            var LectureData = await GetListLecture();

            if (!_sessionManagement.HasToken())
                return Redirect("/login");

            return View(LectureData);
        }

        public IActionResult LectureAdd()
        {
            if (!_sessionManagement.HasToken())
                return Redirect("/login");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LectureModel lectureModel)
        {
            LectureModel LectureResponse = await _apiProvider.POST<LectureModel, LectureModel>(lectureModel, "Lecture/delete", _sessionManagement.GetToken());

            return Redirect("/Lecture");
        }

        [HttpPost]
        public async Task<IActionResult> Add(LectureModel lectureModel)
        {
            LectureRequestModel lectureRequest = new()
            {
                LectureName = lectureModel?.Name
            };

            LectureModel LectureResponse = await _apiProvider.POST<LectureRequestModel, LectureModel>(lectureRequest, "Lecture/add", _sessionManagement.GetToken());

            return Redirect("/Lecture");
        }

        public async Task<LectureAllResponseModel> GetListLecture()
        {

            LectureAllResponseModel LectureResponse = await _apiProvider.POST<object, LectureAllResponseModel>(null, "Lecture/getlist", _sessionManagement.GetToken());

            return LectureResponse;
        }


    }
}
