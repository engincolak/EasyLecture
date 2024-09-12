using EasyLectureDAL.Base;
using EasyLectureDAL.Provider;
using EasyLectureDAL.Repository.Ado.Sql;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Utils;
using EasyLectureModel.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class LectureAdo : AdoEntityBase, ILecture
    {
        public LectureAdo(AdoProvider provider, ScriptManager script) : base(provider, script)
        {
        }
        public bool Delete(int Lctid)
        {

            List<DbParam> dbParams = new()
            {
                new DbParam("@LctID", Lctid)
            };

            try
            {
                Provider.Execute(Script.Lecture.DeleteLecture, dbParams, false);
                return true; // İşlem başarılı
            }
            catch (Exception ex)
            {

                return false; // İşlem başarısız
            }


        }

        public bool IsItInUseStudentLecture(int id)
        {

            List<DbParam> dbParams = new()
            {
                new DbParam("@lctid", id)
            };
            int count = Provider.ExecuteWithId<int>(Script.Lecture.IsItInUseStudentLecture,dbParams);

            if (count > 0)
            {
                // Eğer kayıtlar kullanılıyorsa true döndür
                return true;
            }
            else
            {
                // Eğer kullanılmıyorsa false döndür
                return false;
            }
        }

        public string Add(string name)
        {
            
            List<DbParam> dbParams = new()
    {
        new DbParam("@lctname", name),

    };
            try
            {
                Provider.Execute(Script.Lecture.AddLecture, dbParams, false);
                return "succ"; // İşlem başarılı
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayabilirsiniz (örneğin: Console.WriteLine(ex.Message))
                return "err"; // İşlem başarısız
            }

        }

        public LectureDto Get(int id)
        {
            LectureDto result = null;

            List<DbParam> dbParams = new()
            {
                new DbParam("@lctid", id)
            };

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                result = new()
                {
                    LctId = reader.GetInt32(0),
                    LctName = reader.GetString(1),
                };
            };

            Provider.GetList(funcs, Script.Lecture.Get, dbParams);

            return result;
        }

        public List<LectureDto> GetList()
        {
            List<LectureDto> resultList = new List<LectureDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new LectureDto()
                {
                    LctId = reader.GetInt32(0),
                    LctName = reader.GetString(1),
                    LctisActive = reader.GetBoolean(2)
                });
            };

            Provider.GetList(funcs, Script.Lecture.GetList);

            return resultList;
        }
    }
    }
