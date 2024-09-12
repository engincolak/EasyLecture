namespace EasyLectureModel.Model.User
{
    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }

    }
}
