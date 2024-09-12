using EasyLectureBusiness;
using EasyLectureModel.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;

namespace EasyLectureApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected BusinessManager _businessManager;

        public BaseController(BusinessManager businessManager)
        {
            _businessManager = businessManager;
        }

        protected string GetToken()
        {
            _ = Request.Headers.TryGetValue("Authorization", out StringValues token);

            if (token.Count == 0)
                throw new Exception("Token not found !");

            string tokenValue = token[0];

            if (!tokenValue.StartsWith("Bearer "))
                throw new Exception("Invalid token format !");

            return tokenValue.Replace("Bearer ", "");
        }

        protected void CheckSession(string serviceName)
        {
            string token = GetToken();

            UserDto userDto = _businessManager.UserBusiness.GetSession(token);

            if (userDto == null)
                throw new Exception("Invalid Session !");

            if (!_businessManager.UserBusiness.CheckPermisson(userDto.UseRolId, serviceName))
                throw new Exception("Invalid Permisson !");
        }
    }
}
