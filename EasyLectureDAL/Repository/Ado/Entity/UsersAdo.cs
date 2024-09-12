using System.Data.SqlClient;
using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Provider;
using EasyLectureModel.Dto;
using EasyLectureDAL.Utils;
using EasyLectureDAL.Repository.Ado.Sql;
using EasyLectureModel.Model.User;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class UsersAdo : AdoEntityBase, IUsers
    {
        public UsersAdo(AdoProvider adoProvider, ScriptManager script) : base(adoProvider, script)
        {

        }
        public UserDto CheckSession(string token)
        {
                List<DbParam> param = new()
        {
            new DbParam("@token", token)
        };

            UserDto result = null;

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new UserDto()
                {
                    UseID = reader.GetInt32(0),
                    UseRolId = reader.GetInt32(1)
                };
            };

            Provider.GetSingle(funcs, Script.User.CheckSession, param);

            return result;
        }

        public void CreateToken(int id, string token)
        {
            List<DbParam> param = new()
            { 
                new DbParam("@UseId", id),
                new DbParam("@token", token)
            };

            Provider.Execute(Script.User.CreateToken, param);
        }

        public int Login(UserLoginRequestModel userModel)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@password", userModel.Password),
                new DbParam("@mail", userModel.Email),
            };

            return Provider.ExecuteWithId<int>(Script.User.Login, dbParams);
        }


        public UserDto Get(int id)
        {
            UserDto result = null;

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new UserDto()
                {
                    UseID = reader.GetInt32(0),
                    UseMail = reader.GetString(1),
                    UsePassword = reader.GetString(2),
                    UseRolId = reader.GetInt32(3)
                };
            };

            List<DbParam> dbParams = new()
            {
                new DbParam("@UseId", id)
            };

            Provider.GetSingle(funcs, Script.User.Get, dbParams);

            return result;
        }

        public List<UserDto> GetList()
        {
            List<UserDto> resultList = new List<UserDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new UserDto()
                {
                    UseID = reader.GetInt32(0),
                    UseMail = reader.GetString(1),
                    UsePassword = reader.GetString(2),
                    UseRolId = reader.GetInt32(3)
                });
            };

            Provider.GetList(funcs, Script.User.GetList);

            return resultList;
        }

        public int GetRoleId(int UseID)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@UseID", UseID)
            };
            int result = Provider.ExecuteWithId<int>(Script.User.GetRoleId, dbParams); 
            return result;
        }

        public UserDto Save(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public int EMailCheck(string email)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Useid)
        {
            throw new NotImplementedException();
        }

        public bool CheckPermisson(int roleId, string serviceName)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@roleId", roleId),
                new DbParam("@serviceName", serviceName)
            };
            int result = Provider.ExecuteWithId<int>(Script.User.CheckPermisson, dbParams);
            if (result >0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
