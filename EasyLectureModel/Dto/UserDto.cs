using System.ComponentModel.DataAnnotations;

namespace EasyLectureModel.Dto
{
    public class UserDto
    {
        [Key]
        public int UseID { get; set; }
        public string UseMail { get; set; }
        public string UsePassword { get; set; }
        public int UseRolId { get; set; }
        public string UseToken { get; set; }
        public bool UseIsActive { get; set; }
    }
}
