using EasyLectureApi.Controllers;
using EasyLectureBusiness;
using EasyLectureModel.Model.User;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    public UserController(BusinessManager businessManager) : base(businessManager)
    {
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequestModel userModel)
    {
        if (userModel == null)
            return BadRequest("User data is required.");

        UserLoginResponseModel response = base._businessManager.UserBusiness.Login(userModel);

        return Ok(response);
    }
}
