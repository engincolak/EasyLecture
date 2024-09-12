using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLectureModel.Dto
{
    public class StudentDto
    {
        [Key]
        public int StdID { get; set; }
        public string StdName { get; set; }
        public int StdUseId { get; set; }     
        public bool StdIsActive { get; set; }
    }
}
