using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class StudenLectureEF : EFEntityBase, IStudentLecture
    {
        public List<StudentLectureDto> GetList()
        {
            return StudentLecture.ToList();
        }
    }
}
