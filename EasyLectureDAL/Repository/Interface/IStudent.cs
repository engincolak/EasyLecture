using EasyLectureModel.Dto;

namespace EasyLectureDAL.Repository.Interface
{
    public interface IStudent
    {
        int GetId(int UseId);
        StudentDto Get(int id);
        List<StudentDto> GetList();
        string AddStudentLecture(int stdid, int lctid);
        int GetStudentIdByMail(string mail);
        bool Delete(int Stdid);
        bool UserIDControl(int id);
        int EMailCheck(string email);
        int Save(StudentDto student, UserDto user, List<int> lectureIds);
    }
}
