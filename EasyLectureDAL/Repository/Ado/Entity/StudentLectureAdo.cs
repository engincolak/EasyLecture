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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Entity
{
    public class StudentLectureAdo : AdoEntityBase, IStudentLecture
    {
        public StudentLectureAdo(AdoProvider provider, ScriptManager script) : base(provider, script)
        {
        }

        public List<StudentLectureDto> GetList()
        {
            List<StudentLectureDto> resultList = new List<StudentLectureDto>();

            Action<SqlDataReader> funcs = delegate (SqlDataReader reader)
            {
                resultList.Add(new StudentLectureDto()
                {
                    StlLctId = reader.GetInt32(0),
                    StlStdId = reader.GetInt32(1),
                });
            };

            Provider.GetList(funcs, Script.StudentLecture.GetList);

            return resultList;
        }
    }
}



