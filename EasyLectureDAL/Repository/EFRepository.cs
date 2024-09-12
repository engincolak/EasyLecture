using EasyLectureDAL.Repository.EF.Entity;
using EasyLectureDAL.Repository.Interface;


namespace EasyLectureDAL.Repository
{
    public class EFRepository : IRepository
    {
        public IUsers Users { get; init; }
        public IStudent Student { get; init; }
        public ITeacher Teacher { get; init; }
        public ILecture Lecture { get; init; }
        public IRole UserType { get; init; }
        public IStudentLecture StudentLecture { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public EFRepository() : base()
        {
            Users = new UserEF();
            Student = new StudentEF();
            Teacher = new TeacherEF();
            UserType = new RoleEF();
        }
    }
}
