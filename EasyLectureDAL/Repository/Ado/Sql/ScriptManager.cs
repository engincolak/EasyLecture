using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class ScriptManager
    {
        public StudentScript Student {  get; init; }
        public UserScript User { get; init; }
        public TeacherScript Teacher { get; init; } 
        public LectureScript Lecture { get; init; }
        public StudentLectureScript StudentLecture { get; init; }
        public RoleScript Role { get; init; }


        public ScriptManager()
        {
            Role = new RoleScript();
            StudentLecture = new StudentLectureScript();
            Teacher = new TeacherScript();
            Lecture = new LectureScript();  
            User = new UserScript();
            Student = new StudentScript();
        }
    }
}
