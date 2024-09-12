namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class StudentLectureScript
    {
        public string InsertByStudentInsert => @"
            INSERT INTO StudentLecture (StlLctId, StlStdId)
            SELECT @LctId_{0}, IDENT_CURRENT('Student')
            ;
        ";

        public string InsertByStudentUpdate => @"
            INSERT INTO StudentLecture (StlLctId, StlStdId)
            VALUES (@LctId_{0}, @StdId)
            ;
        ";

        public string Delete => @"
            DELETE FROM StudentLecture
            WHERE StlStdId = @StdId
            ;
        ";

        public string GetList => @"
            SELECT
                StlLctId, StlStdId
            FROM StudentLecture
            ;
        ";
    }
}
