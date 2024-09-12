using EasyLectureModel;
using EasyLectureMVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EasyLectureMVC.Classes
{
    public class SessionManagement
    {
        private string _tokenKey = "token";
        private string _UserNameKey = "user-name";
        private string _UserIdKey = "user-id";
        private string _IdKey = "id";
        private string _RoleId = "role-id";
        private Controllers.ControllerBase _controllerBase;

        public SessionManagement(Controllers.ControllerBase controllerBase)
        {
            _controllerBase = controllerBase;
        }
        public void SetSession(int userId, string userName, string token, int id, int roleId)
        {
            _controllerBase.HttpContext.Session.SetInt32(_UserIdKey, userId);
            _controllerBase.HttpContext.Session.SetString(_UserNameKey, userName);
            _controllerBase.HttpContext.Session.SetString(_tokenKey, token);
            _controllerBase.HttpContext.Session.SetInt32(_IdKey, id);
            _controllerBase.HttpContext.Session.SetInt32(_RoleId,roleId);
        }

        public string GetToken()
        {
            return _controllerBase.HttpContext.Session.GetString(_tokenKey);
        }

        public bool HasToken()
        {
            return !string.IsNullOrEmpty(_controllerBase.HttpContext.Session.GetString(_tokenKey));
        }

        public string GetUserName()
        {
            return _controllerBase.HttpContext.Session.GetString("user-name");
        }

        public int GetUserId()
        {
            return _controllerBase.HttpContext.Session.GetInt32("user-id") ?? 0;
        }

        public int GetStudentId()
        {
            return _controllerBase.HttpContext.Session.GetInt32("id") ?? 0;
        }

        public int GetTeacherId()
        {
            return _controllerBase.HttpContext.Session.GetInt32("id") ?? 0;
        }

        public RoleType GetRole()
        {
            return (RoleType)(_controllerBase.HttpContext.Session.GetInt32("role-id") ?? 0);
        }
    }
}
