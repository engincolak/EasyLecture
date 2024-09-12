using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Provider;
using EasyLectureModel.Dto;
using System.Data.Common;
using EasyLectureDAL.Utils;
using EasyLectureDAL.Repository.Ado.Sql;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EasyLectureModel.Model.Student;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class StudentAdo : AdoEntityBase, IStudent
    {
        public StudentAdo(AdoProvider adoProvider, ScriptManager script) : base(adoProvider, script)
        {

        }

        public int GetId(int UseId)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@UseId", UseId)
            };
            int StdId = Provider.ExecuteWithId<int>(Script.Student.GetId, dbParams);
            return StdId;
        }

        public StudentPresetModel GetPreset(int StdID)
        {
            StudentPresetModel studentPresetModel = new StudentPresetModel();
            return studentPresetModel;
        }

        public List<StudentDto> GetList()
        {
            List<StudentDto> resultList = new List<StudentDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new StudentDto()
                {
                    StdID = reader.GetInt32(0),
                    StdName = reader.GetString(1),
                    StdUseId = reader.GetInt32(2)
                });
            };

            Provider.GetList(funcs, Script.Student.GetList);

            return resultList;
        }

        public StudentDto Get(int id)
        {
            StudentDto result = null;

            List<DbParam> dbParams = new()
            {
                new DbParam("@id", id)
            };

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new()
                {
                    StdID = reader.GetInt32(0),
                    StdName = reader.GetString(1),
                    StdUseId = reader.GetInt32(2)
                };
            };

            Provider.GetList(funcs, Script.Student.GetById, dbParams);

            return result;
        }
        public bool AddStudentLecture(int stdid, int lctid)
        {

            List<DbParam> dbParams = new()
    {
        new DbParam("@stdid", stdid),
        new DbParam("@lctid", lctid)
    };

            try
            {
                Provider.Execute(Script.Student.AddStudentLecture, dbParams, false);
                return true; // İşlem başarılı
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayabilirsiniz (örneğin: Console.WriteLine(ex.Message))
                return false; // İşlem başarısız
            }
        }

        public int GetStudentIdByMail(string mail)
        {
            // İlk olarak, Mail'e göre UserID'yi alıyoruz
            string queryUser = "SELECT UseID FROM Users WHERE UseMail = @Mail AND UseIsActive = true ;";
            List<DbParam> dbParams1 = new()
            {
                new DbParam("@Mail", mail)
            };


            int userId = Provider.ExecuteWithId<int>(queryUser, dbParams1);

            // Şimdi, UserID'ye göre Student tablosundan stdID'yi alıyoruz
            string queryStudent = "SELECT StdID FROM Student WHERE StdUseID = @UserID;";
            List<DbParam> dbParams2 = new()
            {
                new DbParam("@UserID", userId)
            };


            int studentId = Provider.ExecuteWithId<int>(queryStudent, dbParams2);

            return Convert.ToInt32(studentId);
        }
        public bool Delete(int Stdid)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@StdID", Stdid)
            };

            try
            {
                Provider.Execute(Script.Student.DeleteStudent, dbParams, true);
                return true; // İşlem başarılı
            }
            catch (Exception ex)
            {
                
                return false; // İşlem başarısız
            }


        }
        public bool UserIDControl(int id)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@id", id)
            };

            int count = Provider.GetValue<int>(Script.Student.UserIdControl, dbParams);
            if (count > 0)
            {
                return true; //sonuc varsa true
            }
            else
            {
                return false; // sonuc yoksa false
            }

        }
        public int EMailCheck(string email)
        {
            List<DbParam> dbParams = new()
            {
                new DbParam("@mail", email)
            };

            return Provider.ExecuteWithId<int>(Script.Student.MailControl, dbParams);
        }
        public int Save(StudentDto student, UserDto user, List<int> lectureIds)
        {
            string query = string.Empty;

            List<DbParam> dbParams = new()
            {
                new DbParam("@id", student.StdID),
                new DbParam("@name", student.StdName),
                new DbParam("@mail", user.UseMail),
                new DbParam("@password", user.UsePassword),
                new DbParam("@usertype", user.UseRolId)
            };

            string lectureQuery = string.Empty;

            if (student.StdID > 0)
            {
                // Update
                for (int i = 0; i < lectureIds.Count; i++)
                {
                    lectureQuery += string.Format(Script.StudentLecture.InsertByStudentUpdate, i);
                    dbParams.Add(new DbParam($"@LctId_{i}", lectureIds[i]));
                }
                
                dbParams.Add(new DbParam("@StdId", student.StdID));
                query = Script.Student.Update + Script.User.Update + Script.StudentLecture.Delete + lectureQuery;
            }
            else
            {
                // Insert
                for (int i = 0; i < lectureIds.Count; i++)
                {
                    lectureQuery += string.Format(Script.StudentLecture.InsertByStudentInsert, i);
                    dbParams.Add(new DbParam($"@LctId_{i}", lectureIds[i]));
                }

                query = Script.User.Insert + Script.Student.Insert + lectureQuery;
            }

            return Provider.ExecuteWithId<int>(query, dbParams, false);
        }


        string IStudent.AddStudentLecture(int stdid, int lctid)
        {
            throw new NotImplementedException();
        }
    }
}
