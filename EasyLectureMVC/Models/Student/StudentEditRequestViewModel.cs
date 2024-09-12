namespace EasyLectureMVC.Models.Student
{
    public class StudentEditRequestViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public List<int> lectureIds { get; set; }
    }
}
