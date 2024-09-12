using Azure;
using EasyLectureApi.Controllers;
using EasyLectureBusiness;
using EasyLectureModel.Model.Student;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentController : BaseController
{
    public StudentController(BusinessManager businessManager) : base(businessManager)
    {
    }

    [HttpPost("save")]
    public IActionResult Save([FromBody] StudentModel student)
    {
        CheckSession("Student/save");

        if (student == null)
        {
            return BadRequest("Student data is required.");
        }

        var result = _businessManager.StudentBusiness.Save(student); 

        if (string.IsNullOrEmpty(result.Message))
        {
            return BadRequest(new
            {
                message = result.Message,
                student = result
            });
        }

        return Ok(new
        {
            message = "Student saved successfully!",
            student = result
        });
    }

    [HttpPost("getlist")]
    public IActionResult GetList()
    {
        CheckSession("Student/getlist");

        var result = _businessManager.StudentBusiness.GetList();


        return Ok(new
        {
            students = result
        });
    }


    [HttpPost("delete")]
    public IActionResult Delete(StudentRequestModel studentRequestModel)
    {
        CheckSession("Student/delete");

        _businessManager.StudentBusiness.Delete(studentRequestModel.id);

        // Silme işlemi burada yapılır
        return Ok(new { message = "Student deleted successfully!" });  
    }

    [HttpPost("get")]
    public IActionResult Get(StudentRequestModel requestModel)
    {
        CheckSession("Student/get");

        StudentModel result = _businessManager.StudentBusiness.Get(requestModel);
        if (result == null)
        {
            return Ok(new
            {
                message = "Student not retrieved.",
            });
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPost("getpreset")]
    public IActionResult GetPreset(StudentRequestModel requestModel)
    {
        CheckSession("Student/getpreset");

        var result = _businessManager.StudentBusiness.GetPreset(requestModel);
        if (result == null)
        {
            return Ok(new
            {
                message = "Student not retrieved.",
            });
        }
        else
        {
            return Ok(result);
        }
    }
}
