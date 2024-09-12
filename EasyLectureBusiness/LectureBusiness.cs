using EasyLectureDAL.Repository;
using EasyLectureModel;
using EasyLectureModel.Dto;
using EasyLectureModel.Model.Lecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureBusiness
{
    public class LectureBusiness : BusinessBase
    {
        public LectureBusiness(BusinessManager businessManager) : base(businessManager)
        {
        }

        public bool Add(LectureRequestModel lectureRequestModel)
        {
            IRepository repo = base.GetRepository(RepositoryType.Ado); // EF mi ADO mu buradan seçiyoruz.
            string result = repo.Lecture.Add(lectureRequestModel.LectureName);
            if (result == "succ")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public LectureAllResponseModel GetList()
        {
            List<LectureDto> lectureDtos = GetRepository(RepositoryType.Ado).Lecture.GetList();

            return new LectureAllResponseModel
            {
                Lectures = lectureDtos
            };
        }

        public bool Delete(LectureModel lectureModel)
        {
            IRepository repo = base.GetRepository(RepositoryType.Ado); // EF mi ADO mu buradan seçiyoruz.
            
                //StudentLecture'da kullanılıyor mu kontrol edilsin..
                bool result = repo.Lecture.IsItInUseStudentLecture(lectureModel.Id);

            if (result == false)
            {


                var response = repo.Lecture.Delete(lectureModel.Id);
                if (response == true)
                {
                    return true;
                }


                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }



            
        }

        public LectureModel Get(int id)
        {
            LectureModel result = null;

            if (!(id > 0))
                throw new Exception("Id sıfırdan büyük olmalı !");

            LectureDto lectureDto = GetRepository(RepositoryType.Ado).Lecture.Get(id);

            if (lectureDto != null)
            {
                result = new()
                {
                    Id = lectureDto.LctId,
                    Name = lectureDto.LctName,
                };
            }

            return result;
        }
    }
}
