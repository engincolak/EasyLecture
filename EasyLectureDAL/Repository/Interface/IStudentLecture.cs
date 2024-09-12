using EasyLectureModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Interface
{
    public interface IStudentLecture
    {
        List<StudentLectureDto> GetList();
    }
}
