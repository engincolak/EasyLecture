using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Interface
{
    public interface ITeacher
    {
        int Save(TeacherDto teacher, UserDto user);
        TeacherDto Get(int id);
        void Delete(int id);
        List<TeacherDto> GetList();
        int GetId(int UseId);





    }
}
