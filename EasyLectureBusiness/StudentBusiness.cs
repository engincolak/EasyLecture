using EasyLectureDAL.Repository;
using EasyLectureDAL.Repository.Ado.Entity;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel;
using EasyLectureModel.Dto;
using EasyLectureModel.Model;
using EasyLectureModel.Model.Student;
using System.Runtime.CompilerServices;

namespace EasyLectureBusiness
{
    public class StudentBusiness : BusinessBase
    {
        public StudentBusiness(BusinessManager businessManager): base(businessManager)
        {
        }

        public List<StudentListModel> GetList()
        {
            List<StudentListModel> result = new();

            List<StudentDto> studentDtos = GetRepository(RepositoryType.EF).Student.GetList();
            List<UserDto> userDtos = GetRepository(RepositoryType.EF).Users.GetList();
            List<RoleDto> userTypeDtos = GetRepository(RepositoryType.EF).UserType.GetList(); // BU Sİ HATAYI ÇÖZMEK İÇİN KONTROL EDİN

            Dictionary<int, string> userTypeDictionary = userTypeDtos
                .ToDictionary(x => x.RolID, x => x.RolName);

            foreach (var item in studentDtos)
            {
                StudentListModel res = new()
                {
                    Id = item.StdID,
                    Name = item.StdName,
                    EMail = null,
                    IsActive = item.StdIsActive,
                    UserType = "unknown" 
                };

                UserDto userDto = userDtos.FirstOrDefault(x => x.UseID == item.StdUseId);
                if (userDto != null)
                {
                    res.EMail = userDto.UseMail;

                    if (userTypeDictionary.TryGetValue(userDto.UseRolId, out string userTypeName))
                        res.UserType = userTypeName;
                }

                if (item.StdIsActive)
                {
                    result.Add(res);
                }
            }

            return result;
        }


        public StudentPresetModel GetPreset(StudentRequestModel requestModel)
        {
            StudentPresetModel result = new()
            {
                Student = Get(requestModel),
                Lecture = new(),
                Role = new()
            };

            List<LectureDto> lectureList = GetRepository(RepositoryType.Ado).Lecture.GetList();

            foreach (var item in lectureList)
            {
                result.Lecture.Add(new EasyLectureModel.Model.Lecture.LectureModel()
                {
                    Id = item.LctId,
                    Name = item.LctName
                });
            }

            List<RoleDto> role = GetRepository(RepositoryType.Ado).UserType.GetList();

            foreach (var item in role)
            {
                result.Role.Add(new EasyLectureModel.Model.Role.RoleModel()
                {
                    Id = item.RolID,
                    Name = item.RolName
                });
            }

            return result;
        }

        public StudentModel Get(StudentRequestModel requestModel)
        {
            if (!(requestModel.id > 0))
                return null; //throw new Exception("User Id sıfırdan büyük olmalı !");

            StudentDto studentDto = GetRepository(RepositoryType.Ado).Student.Get(requestModel.id);

            if (studentDto == null)
                return null; // throw new Exception("öğrenci bulunamadı !");

            UserDto userDto = GetRepository(RepositoryType.Ado).Users.Get(studentDto.StdUseId);

            if (userDto == null)
                return null; //throw new Exception("User bulunamadı !");

            List<StudentLectureDto> studentlectureDtos = GetRepository(RepositoryType.Ado).StudentLecture.GetList();

            // Student'ın derslerini bulmak için ilgili LectureDto'ları filtreleyin
            List<int> lectureIds = studentlectureDtos
                .Where(x => x.StlStdId == requestModel.id) // LectureStudentId ile StudentDto'daki Id'yi eşleştiriyoruz
                .Select(x => x.StlLctId)
                .ToList();

            if (studentDto != null)
            {
                return new()
                {
                    Id = studentDto.StdID,
                    Name = studentDto.StdName,
                    EMail = userDto.UseMail,
                    Password = userDto.UsePassword,
                    RoleId = userDto.UseRolId,
                    LectureIds = lectureIds.Count > 0 ? lectureIds : new()
                };
            }

            return null;
        }

        public void Delete(int id)
        {
            base.GetRepository(RepositoryType.Ado).Student.Delete(id);
        }

        public SaveResponseModel Save(StudentModel student)
        {
            SaveResponseModel result = new();

            // Hata kontrolü
            if (string.IsNullOrEmpty(student.Name) ||
                string.IsNullOrEmpty(student.EMail) ||
                string.IsNullOrEmpty(student.Password))
            {
                result.Message = "Hata: Tüm alanlar doldurulmalıdır.";
                return result;
            }

            if (!(student.RoleId == 1))
            {
                result.Message = "Hata: UserType seçimi doğru değil.";
                return result;
            }

            IRepository repo = base.GetRepository(RepositoryType.Ado); // EF mi ADO mu buradan seçiyoruz.

            int eMailCount = repo.Student.EMailCheck(student.EMail);

            if (eMailCount > (student.Id > 0 ? 1 : 0))
            {
                result.Message = "Error : E-Mail already exists";
                return result;
            }

            StudentDto studentDto = new()
            {
                StdID = student.Id,
                StdName = student.Name,
                StdIsActive = true
            };

            UserDto userDto = new()
            {
                UseMail = student.EMail,
                UsePassword = student.Password,
                UseRolId = student.RoleId,
                UseIsActive = true
            };

            int userId = repo.Student.Save(studentDto, userDto, student.LectureIds);

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
