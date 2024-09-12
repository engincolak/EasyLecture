using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLectureModel.Dto
{
    public record StudentLectureDto
    {
        //  [Key, Column(Order = 0)]
        public int StlLctId { get; set; }

        //  [Key, Column(Order = 1)]
        public int StlStdId { get; set; }

    }

}
