using EasyLectureDAL.Repository;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel;
using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using EasyLectureModel.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureBusiness
{
    public class TeacherBusiness : BusinessBase
    {
        public TeacherBusiness(BusinessManager businessManager) : base(businessManager)
        {
        }


        public List<TeacherModel> GetList()
        {
            List<TeacherModel> result = new();

            List<TeacherDto> teacherDtos = GetRepository(RepositoryType.Ado).Teacher.GetList();
            List<UserDto> userDtos = GetRepository(RepositoryType.EF).Users.GetList();
            List<RoleDto> userTypeDtos = GetRepository(RepositoryType.EF).UserType.GetList();

            Dictionary<int, string> userTypeDictionary = userTypeDtos
                .ToDictionary(x => x.RolID, x => x.RolName);

            foreach (var item in teacherDtos)
            {
                TeacherModel res = new()
                {
                    Id = item.TcrId,
                    Name = item.TcrName,
                    EMail = null,
                    UserType = 2
                };

                UserDto userDto = userDtos.FirstOrDefault(x => x.UseID == item.TcrUseId);
                if (userDto != null)
                {
                    res.EMail = userDto.UseMail;
                }
                    result.Add(res);
            }

            return result;
        }


        public void Delete(int id)
        {
            IRepository repo = base.GetRepository(RepositoryType.Ado); // EF mi ADO mu buradan seçiyoruz.

                repo.Teacher.Delete(id);
        }


        public TeacherModel Get(int id)
        {
            if (!(id > 0))
                throw new Exception("User Id sıfırdan büyük olmalı !");


            TeacherDto teacherDto = GetRepository(RepositoryType.Ado).Teacher.Get(id);
            UserDto userDtos = GetRepository(RepositoryType.Ado).Users.Get(teacherDto.TcrUseId);

            if (teacherDto  != null)
            {
                return new()
                {
                    Id = teacherDto.TcrId,
                    Name = teacherDto.TcrName,
                    EMail = userDtos.UseMail,
                    Password = userDtos.UsePassword,
                };
            }

            return null;
        }
        public SaveResponseModel Save(TeacherModel teacher)
        {
            IRepository repo = base.GetRepository(RepositoryType.Ado);
            SaveResponseModel result = new();

            TeacherDto teacherDto = new()
            {
                TcrId = teacher.Id,
                TcrName = teacher.Name
            };

            UserDto userDto = new()
            {
                UseMail = teacher.EMail,
                UsePassword = teacher.Password,
                UseRolId = teacher.UserType
            };

            int userId = repo.Teacher.Save(teacherDto, userDto);

            if (userId > 0)
            {
                result.Id = userId;
                result.Message = "OK";
            }
            else
            {
                result.Message = "Database işlemleri başarısız oldu.";
            }

            return result;



        }
    }
}
