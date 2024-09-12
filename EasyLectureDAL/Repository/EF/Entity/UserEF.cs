using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel.Dto;
using EasyLectureModel.Model.User;
using System.Runtime.CompilerServices;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class UserEF : EFEntityBase, IUsers
    {
        public UserDto Save(UserDto userDto) //burası saibeli 
        {

            if (userDto.UseID == 0)
            {

                var newUser = new UserDto
                {
                    UseMail = userDto.UseMail,
                    UsePassword = userDto.UsePassword,
                    UseRolId = userDto.UseRolId
                };
                Users.Add(newUser);
                return userDto;
            }
            else
            {
                var User = Users.FirstOrDefault(x => x.UseID == userDto.UseID);
                if (User == null)
                {
                    User.UseMail = userDto.UseMail;
                    User.UsePassword = userDto.UsePassword;
                    User.UseRolId = userDto.UseRolId;
                }
                SaveChanges();

                return new UserDto
                {
                    UseID = userDto.UseID,
                    UseMail = userDto.UseMail,
                    UsePassword = userDto.UsePassword,
                    UseRolId = userDto.UseRolId
                };
            }

        }

        public void CreateToken(int id, string token)
        {
            var user = Users.FirstOrDefault(x => x.UseID == id);
            if (user != null)
            {
                user.UseToken = token;
                SaveChanges();
            }
        }
        public int EMailCheck(string email)
        {
            var user = Users.FirstOrDefault(x=> x.UseMail == email);
            return user.UseID;
        }
        public UserDto Get(int id)
        {
            return Users.FirstOrDefault(x => x.UseID == id);
        }
        public List<UserDto> GetList()
        {
            return Users
                .Where(u => u.UseIsActive == true)
                .Select(u => new UserDto
                {
                    UseID = u.UseID,
                    UseMail = u.UseMail,
                    UseRolId = u.UseRolId
                })
                .ToList();
        }

        public int Login(UserLoginRequestModel userModel)
        {
            return Users.FirstOrDefault(x=> x.UseMail == userModel.Email && x.UsePassword == userModel.Password)?.UseID ?? 0;
        }
        public bool Delete(int Useid)
        {
            var result = Users.FirstOrDefault(x => x.UseID == Useid);
            if (result != null)
            {
                result.UseIsActive = false;
                SaveChanges();
            }
            return false;
        }

        public UserDto CheckSession(string token)
        {
            var result = Users.FirstOrDefault(x=> x.UseToken == token);
            return result;
        }

        public int GetRoleId(int UseID)
        {
            var result = Users.FirstOrDefault(x=> x.UseID==UseID);
            return result.UseRolId;
        }

        public bool CheckPermisson(int roleId, string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}
