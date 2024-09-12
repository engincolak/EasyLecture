using EasyLectureModel.Dto;
using EasyLectureModel.Model.User;

namespace EasyLectureDAL.Repository.Interface
{
    public interface IUsers
    {
        UserDto Save (UserDto userDto);
        int EMailCheck(string email);
        int Login(UserLoginRequestModel userModel);
        List<UserDto> GetList();
        UserDto Get(int id);
        void CreateToken(int id, string token);
        bool Delete(int Useid);
        UserDto CheckSession(string token);
        bool CheckPermisson(int roleId, string serviceName);
        int GetRoleId(int UseID);
    }
}
