using EasyLectureDAL.Base;
using EasyLectureDAL.Repository.Interface;
using EasyLectureModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.EF.Entity
{
    internal class LectureEF : EFEntityBase, ILecture
    {
        public string Add(string name)
        {
            var newLecture = new LectureDto
            {
                LctName = name
            };
            Lecture.Add(newLecture);
            SaveChanges();
            return "succ";
        }

        public bool Delete(int Lctid)
        {
            var result = Lecture.FirstOrDefault(x => x.LctId == Lctid);
            if (result != null)
            {
                result.LctisActive = false;
                SaveChanges();
                return true;
            }
            return false;
        }

        public LectureDto Get(int id)
        {
            var result = Lecture.FirstOrDefault(x=>x.LctId == id);
            if (result != null) 
            {
                var lectureDto = new LectureDto
                {
                    LctId = result.LctId,
                    LctName = result.LctName,
                    LctisActive = result.LctisActive
                };
                return lectureDto;

            }
            return null;

        }

        public List<LectureDto> GetList()
        {
            return Lecture.ToList();
        }

        public bool IsItInUseStudentLecture(int id)
        {
            var result = base.StudentLecture
                                    .Where(sl => sl.StlLctId == id)
                                    .OrderBy(sl => sl.StlStdId)
                                    .Select(sl => sl.StlStdId)
                                    .FirstOrDefault(); // Eğer sonuç yoksa 0 dönecek

            var stlStdId = result != 0 ? result : 0;
            return true;
        }
    }
}
