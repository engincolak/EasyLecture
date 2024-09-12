using EasyLectureModel.Model.Lecture;
using EasyLectureModel.Model.Role;

namespace EasyLectureModel.Model.Student
{
    public class StudentPresetModel
    {
        public StudentModel Student { get; set; }
        public List<LectureModel> Lecture { get; set; }
        public List<RoleModel> Role { get; set; }
    }
}
