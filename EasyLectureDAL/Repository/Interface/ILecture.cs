using EasyLectureModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Interface
{
    public interface ILecture
    {
        LectureDto Get(int id);
        List<LectureDto> GetList();

        bool Delete(int Lctid);

        string Add(string name);
        bool IsItInUseStudentLecture(int id);
    }
}
