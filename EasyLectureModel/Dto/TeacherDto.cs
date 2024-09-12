using System.ComponentModel.DataAnnotations;

namespace EasyLectureModel.Dto
{
    public class TeacherDto
    {
        [Key]
        public int TcrId { get; set; }
        public string TcrName { get; set; }
        public int TcrUseId { get; set; }
        public bool TcrIsActive { get; set; }

    }

}
