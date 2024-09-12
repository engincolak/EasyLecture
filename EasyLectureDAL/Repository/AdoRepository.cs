using EasyLectureDAL.Provider;
using EasyLectureDAL.Repository.Ado.Entity;
using EasyLectureDAL.Repository.Ado.Sql;
using EasyLectureDAL.Repository.Interface;

namespace EasyLectureDAL.Repository
{
    public class AdoRepository : IRepository
    {
        private AdoProvider Provider { get; init; }
        private ScriptManager Script { get; init; }

        public IStudent Student { get; init; }
        public ITeacher Teacher { get; init; }
        public IRole UserType { get; init; }    
        public ILecture Lecture { get; init; }  
        public IUsers Users { get; init; }
        public IStudentLecture StudentLecture { get; init; }    

        public AdoRepository()
        {
            Provider = new AdoProvider("Data Source=DESKTOP-PB6C85K\\MSSQLSERVER01;Initial Catalog=staj;Integrated Security=True");
            
            Script = new ScriptManager();

            Users = new UsersAdo(Provider, Script);
            UserType = new RoleAdo(Provider,Script);
            Student = new StudentAdo(Provider, Script);
            Teacher = new TeacherAdo(Provider, Script);
            Lecture = new LectureAdo(Provider, Script);
            StudentLecture = new StudentLectureAdo(Provider, Script);
        }
    }
}
