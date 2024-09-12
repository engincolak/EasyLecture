using Azure;
using EasyLectureApi.Controllers;
using EasyLectureBusiness;
using EasyLectureModel.Model.Lecture;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class LectureController : BaseController
{
    public LectureController(BusinessManager businessManager) : base(businessManager)
    {
    }
    [HttpPost("add")]
    public IActionResult Add([FromBody] LectureRequestModel lecture)
    {
        CheckSession("Lecture/add");

        bool result = _businessManager.LectureBusiness.Add(lecture);
        if (result == true)
        {
            return Ok(new
            {
                message = "Lecture saved successfully!",
            });
        }
        else
        {
            return Ok(new
            {
                message = "Lecture not be saved.",
            });
        }

    }

    [HttpPost("getlist")]
    public IActionResult GetList()
    {
        CheckSession("Lecture/getlist");

        LectureAllResponseModel result = _businessManager.LectureBusiness.GetList();

        return Ok(result);

    }


    [HttpPost("delete")]
    public IActionResult Delete(LectureModel lectureModel)
    {
        CheckSession("Lecture/delete");

        var result = _businessManager.LectureBusiness.Delete(lectureModel);
        if (result == true)
        {
            return Ok(new { message = "Lecture deleted successfully!" });
        }
        else
        {
            return Ok(new { message = "Lecture could not be deleted!" });
        }
    }

    [HttpPost("get")]
    public IActionResult Get(int id)
    {

        CheckSession("Lecture/get");

        var result = _businessManager.LectureBusiness.Get(id);
        if (result == null)
        {
            return Ok(new
            {
                message = "Lecture not retrieved.",
            });
        }
        else
        {
            return Ok(new
            {
                message = "Lecture retrieved successfully!",
                name = result
            });
        }
    }

}

