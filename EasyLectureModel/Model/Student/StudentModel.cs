namespace EasyLectureModel.Model.Student
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public List<int> LectureIds { get; set; }
    }
}
