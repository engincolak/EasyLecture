using System.ComponentModel.DataAnnotations;

namespace EasyLectureModel.Dto
{
    public class RoleDto
    {
        [Key]
        public int RolID { get; set; }
        public string RolName { get; set; }
        public bool RolIsActive { get; set; }
    }
}