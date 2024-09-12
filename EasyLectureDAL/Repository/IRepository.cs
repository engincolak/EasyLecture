using EasyLectureDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository
{
    public interface IRepository
    {
        public IStudent Student { get; init; }
        public ITeacher Teacher { get; init; }
        public ILecture Lecture { get; init; }
        public IRole UserType { get; init; }
        public IUsers Users { get; init; }
        public IStudentLecture StudentLecture { get; init; }    
    }
}
