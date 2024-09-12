using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class LectureScript
    {

        public string AddLecture => @"
            INSERT INTO Lecture (LctName) 
            VALUES (@lctname)";
        
        public string Get => @"
            SELECT
                LctID,
                LctName
            FROM Lecture
            WHERE LctID = @lctid AND LctisActive = 1
            ;
        ";

        public string GetById => @"
            SELECT
            LctName 
            FROM Lecture
            WHERE LctID = @id
            ;
        ";

        public string GetList = @"
        SELECT 
            LctID, 
            LctName, 
            LctisActive 
        FROM Lecture
        WHERE LctisActive = 1
        ";

        public string DeleteLecture => @"
            UPDATE 
                Lecture
                SET LctisActive = 0
                WHERE LctID = @LctID;;
            ";

        public string IsItInUseStudentLecture => @"SELECT ISNULL(
            (SELECT 
            TOP 1 StlStdId FROM StudentLecture WHERE StlLctId = @lctid ORDER BY StlStdId), 
            0
            ) AS StlStdId
            ";



    }
}

