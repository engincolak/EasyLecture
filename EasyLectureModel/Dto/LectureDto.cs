using System.ComponentModel.DataAnnotations;

namespace EasyLectureModel.Dto
{
    public record LectureDto
    {
        [Key]
        public int LctId { get; set; }      
        public string LctName { get; set; }
        public bool LctisActive { get; set; }
    }

}
