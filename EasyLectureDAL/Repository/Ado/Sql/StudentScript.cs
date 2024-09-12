namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class StudentScript
    {

        public string GetId => @"
            SELECT 
            StdID 
            FROM Student 
            WHERE StdUseId = @UseId AND StdIsActive = 1 
             ;
        ";
        
        public string Insert => @"
            INSERT INTO Student (StdName, StdUseID) 
            VALUES (@name, @@IDENTITY);
            SELECT SCOPE_IDENTITY()
         ;
        ";

        public string Update => @"
            UPDATE Student
            SET StdName = @name
            WHERE StdID = @id
            ;
            SELECT @id;
        ";

        public string GetList => @"
            SELECT
                StdId, StdName, StdUseId 
            FROM Student
            WHERE StdIsActive = 1
            ;
        ";

        public string GetById => @"
            SELECT
                StdId, StdName, StdUseId 
            FROM Student
            WHERE StdId = @id AND StdIsActive = 1
            ;
        ";

        public string AddStudentLecture => @"
                INSERT INTO StudentLecture (stdID, lctID) 
                VALUES (@stdid, @lctid);";

        public string DeleteStudent => @"
        
            UPDATE Student
            SET StdIsActive = 0
            WHERE StdID = @StdID;

            UPDATE Users
            SET UseIsActive = 0
            WHERE UseID = (SELECT StdUseID FROM Student WHERE StdID = @StdID);

        ";
        public string UserIdControl => @"SELECT COUNT(*) FROM Users WHERE UseID = @id";

        public string MailControl => @"
            SELECT 
                COUNT(UseId) 
            FROM Users 
            WHERE UseMail = @mail
            ;
        ";
    }
}
