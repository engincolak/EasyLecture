using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Ado.Entity;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel.Dto;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class TeacherEF : EFEntityBase, ITeacher
    {
        public int GetId(int UseId)
        {
            var result = Teacher.FirstOrDefault(x => x.TcrUseId == UseId);
            return result.TcrId;
        }
        public void Delete(int id)
        {
           var teacher = Teacher.FirstOrDefault(x=> x.TcrUseId == id);
            teacher.TcrIsActive = false;
        }

        public TeacherDto Get(int id)
        {
            var teacher = Teacher.FirstOrDefault(x => x.TcrUseId == id);
            var teacherDto = new TeacherDto
            {
                TcrId = teacher.TcrId,
                TcrName = teacher.TcrName,
                TcrUseId = teacher.TcrUseId,
            };
            return teacher;
        }

        public List<TeacherDto> GetList()
        {
            return Teacher.ToList();
        }

        public int Save(TeacherDto teacher, UserDto user)
        {
            if (teacher.TcrUseId != 0)
            {
                var users = base.Users.FirstOrDefault(x => x.UseID == user.UseID);
                var teachers = Teacher.FirstOrDefault(x => x.TcrUseId == user.UseID);

                //uptade
                var result = Teacher.FirstOrDefault(x=> x.TcrUseId == teacher.TcrUseId);
                teachers.TcrName = teacher.TcrName;
                users.UseMail = user.UseMail;
                users.UsePassword = user.UsePassword;
                users.UseRolId = user.UseRolId;
                SaveChanges(); 
                return user.UseID;
            }
            else
            {
                //ınsert
                IDbContextTransaction transaction = Database.BeginTransaction();

                base.Users.Add(user);

                SaveChanges();
                teacher.TcrUseId = user.UseID;

                Teacher.Add(teacher);

                try
                {

                    SaveChanges();
                    transaction.Commit();
                    return user.UseID;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
