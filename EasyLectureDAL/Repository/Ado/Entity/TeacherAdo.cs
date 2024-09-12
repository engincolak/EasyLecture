using EasyLectureDAL.Base;
using EasyLectureDAL.Provider;
using EasyLectureDAL.Repository.Ado.Sql;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Utils;
using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class TeacherAdo : AdoEntityBase, ITeacher
    {
        public TeacherAdo(AdoProvider adoProvider, ScriptManager script) : base(adoProvider, script)
        {
            
        }
        public int GetId(int UseId)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@UseId", UseId)
            };
            int TcrId = Provider.ExecuteWithId<int>(Script.Teacher.GetId, dbParams);
            return TcrId;
        }
        public List<TeacherDto> GetList()
        {
            List<TeacherDto> resultList = new List<TeacherDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new TeacherDto()
                {
                    TcrId = reader.GetInt32(0),
                    TcrName = reader.GetString(1),
                    TcrUseId = reader.GetInt32(2)
                });
            };

            Provider.GetList(funcs, Script.Teacher.GetList);

            return resultList;
        }


        public void Delete(int Tcrid)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@TcrID", Tcrid)
            };

            try
            {
                Provider.Execute(Script.Teacher.DeleteTeacher, dbParams, false);
            }
            catch (Exception ex)
            {

            }
        }


        public TeacherDto Get(int id)
        {
            TeacherDto result = null;
            List<DbParam> dbParams = new()
            {
                new DbParam("@id", id)
            };

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new()
                {
                    TcrId = reader.GetInt32(0),
                    TcrName = reader.GetString(1),
                    TcrUseId = reader.GetInt32(2),
                };
            };

            Provider.GetList(funcs, Script.Teacher.GetById, dbParams);

            return result;

        }

        public int Save(TeacherDto teacher, UserDto user)
        {
            string query = string.Empty;

            List<DbParam> dbParams = new()
            {
                new DbParam("@id", teacher.TcrId),
                new DbParam("@name", teacher.TcrName),
                new DbParam("@mail", user.UseMail),
                new DbParam("@password", user.UsePassword),
                new DbParam("@usertype", user.UseRolId)
            };


            if (teacher.TcrId > 0)
            {
                // Update
                dbParams.Add(new DbParam("@TcrID", teacher.TcrId));
                query = Script.Teacher.Uptade + Script.User.Update;
            }
            else
            {
                // Insert
                query = Script.User.Insert + Script.Teacher.Insert;
            }

            return Provider.ExecuteWithId<int>(query, dbParams, true);
        }


    }
}
