using Microsoft.AspNetCore.Mvc;
using EasyLectureBusiness;
using EasyLectureApi.Controllers;
using EasyLectureModel.Model.Teacher;

[ApiController]
[Route("[controller]")]
public class TeacherController : BaseController
{
    public TeacherController(BusinessManager businessManager) : base(businessManager)
    {
    }

    [HttpPost("save")]
    public IActionResult Save([FromBody] TeacherModel teacher)
    {
        CheckSession("Teacher/save");

        var result = _businessManager.TeacherBusiness.Save(teacher);

        if (!string.IsNullOrEmpty(result.Message))
        {
            return Ok(new
            {
                Teacher = result
            });
        }
        else
        {
                
        }

        return null;
    }

    [HttpPost("get")]
    public IActionResult Get(TeacherRequestModel teacherRequestModel)
    {
        CheckSession("Teacher/get");

        var result = _businessManager.TeacherBusiness.Get(teacherRequestModel.id);
        if (result == null)
        {
            return Ok(new
            {
                message = "Teacher not retrieved.",
            });
        }
        else
        {
            return Ok(result);
        }
    }


    [HttpPost("delete")]
    public IActionResult Delete(TeacherRequestModel teacherRequestModel)
    {
        CheckSession("Teacher/delete");

        _businessManager.TeacherBusiness.Delete(teacherRequestModel.id);

        // Silme işlemi burada yapılır
        return Ok(new { message = "Student deleted successfully!" });
    }


    [HttpPost("getlist")]
    public IActionResult GetList()
    {
        CheckSession("Teacher/getlist");

        var result = _businessManager.TeacherBusiness.GetList();


        return Ok(new { Teacher = result });
    }

}
