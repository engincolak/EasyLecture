namespace EasyLectureModel.Model
{
    public class StudentViewPresetModel
    {
        public StudentEditViewModel Student { get; set; }
        public List<LectureViewModel> Lecture { get; set; }
        public List<RoleViewModel> Role { get; set; }
    }
}
