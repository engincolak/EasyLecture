using System;
using System.Data.SqlClient;
using EasyLectureModel.Model;
using System.Runtime.CompilerServices;
using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureDAL.Provider;
using EasyLectureModel.Dto;
using System.Data.Common;
using EasyLectureDAL.Utils;
using EasyLectureDAL.Repository.Ado.Sql;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class StudentEF : EFEntityBase, IStudent
    {
        public string AddStudentLecture(int stdid, int lctid)
        {
            throw new NotImplementedException();
        }

        public int GetId(int UseId)
        {
            var result = Student.FirstOrDefault(x => x.StdUseId == UseId);
            return result.StdID;
        }

        public bool Delete(int Stdid)
        {
            var result = Student.FirstOrDefault(x => x.StdID == Stdid);
            var result2 = base.Users.FirstOrDefault(x => x.UseID == result.StdUseId);
            if (result != null && result2.UseID != null)
            {
                result.StdIsActive = false;
                result2.UseIsActive = false;
                SaveChanges();
                return true;
            }
            return false;
        }

        public int EMailCheck(string email)
        {
            var result = base.Users.FirstOrDefault(x => x.UseMail == email);
            if (result != null)
            {
                return 1;
            }
            return 0;
        }

        public StudentDto Get(int id)
        {
               return Student.FirstOrDefault(x=>x.StdID==id);
        }

        public List<StudentDto> GetList()
        {
            return Student.Where(s => s.StdIsActive).ToList();
        }

        public int GetStudentIdByMail(string mail)
        {
            throw new NotImplementedException();
            
        }

        public int Save(StudentDto student, UserDto user, List<int> lectureIds)
        {
            if (student.StdID > 0)
            {
                var users = base.Users.FirstOrDefault(x => x.UseID == user.UseID);
                if (users != null)
                {
                    var students = Student.FirstOrDefault(x => x.StdUseId == user.UseID);
                    if (students != null)
                    {
                        students.StdName = student.StdName;
                        users.UseMail = user.UseMail;
                        users.UsePassword = user.UsePassword;
                        users.UseRolId = user.UseRolId;

                        var existingLectures = base.StudentLecture.Where(x => x.StlStdId == students.StdID).ToList();

                        StudentLecture.RemoveRange(existingLectures);

                        for (int i = 0; i < lectureIds.Count; i++)
                        {
                            StudentLecture.Add(new StudentLectureDto()
                            {
                                StlStdId = students.StdID,
                                StlLctId = lectureIds[i]
                            });
                        }
                        SaveChanges();
                        return user.UseID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                IDbContextTransaction transaction = Database.BeginTransaction();

                try
                {
                    user.UseIsActive = true;
                    student.StdIsActive = true;
                    base.Users.Add(user);
                    SaveChanges();

                    student.StdUseId = user.UseID;
                    Student.Add(student);
                    
                    SaveChanges(); // Save to generate the StdID
                    
                    for (int i = 0; i < lectureIds.Count; i++)
                    {
                        StudentLecture.Add(new StudentLectureDto()
                        {
                            StlStdId = student.StdID, // Now StdID is generated and can be used
                            StlLctId = lectureIds[i]
                        });
                    }

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




        public bool UserIDControl(int id)
        {
            var result = Student.FirstOrDefault(x=> x.StdID==id);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
