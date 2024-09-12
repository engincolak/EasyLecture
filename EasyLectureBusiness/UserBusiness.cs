using EasyLectureModel;
using EasyLectureModel.Dto;
using EasyLectureModel.Model.User;

namespace EasyLectureBusiness
{
    public class UserBusiness : BusinessBase
    {
        public UserBusiness(BusinessManager businessManager) : base(businessManager)
        {
            
        }

        public UserLoginResponseModel Login(UserLoginRequestModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.Email) ||
                string.IsNullOrEmpty(userModel.Password))
            { return null; }

            else
            {
                int userId = GetRepository(RepositoryType.Ado).Users.Login(userModel);
                
                if (userId > 0)
                {
                    string token = Guid.NewGuid().ToString();
                    
                    GetRepository(RepositoryType.Ado).Users.CreateToken(userId, token);

                    int StdID = 0, TcrID = 0;

                    int RoleId = GetRepository(RepositoryType.EF).Users.GetRoleId(userId);
                    if (RoleId == 1)
                    {
                        // student get
                        StdID = GetRepository(RepositoryType.Ado).Student.GetId(userId); 
                    }
                    else if (RoleId == 2)
                    {
                        // teacher get
                        TcrID = GetRepository(RepositoryType.Ado).Teacher.GetId(userId);
                    }

                    return new UserLoginResponseModel()
                    {
                        Id = userId,
                        Email = userModel.Email,
                        Token = token,
                        RoleId =  RoleId,
                        TeacherId = TcrID,
                        StudentId = StdID
                    };                    
                }                
            }

            return null;
        }

        public UserDto GetSession(string token)
        {
            return GetRepository(RepositoryType.Ado).Users.CheckSession(token);
        }

        public bool CheckPermisson(int roleId, string serviceName)
        {
            bool result = GetRepository(RepositoryType.Ado).Users.CheckPermisson(roleId, serviceName);
            return result;
        }
    }
}
