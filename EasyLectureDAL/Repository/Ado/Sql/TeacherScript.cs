using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class TeacherScript
    {
        public string GetId => @"
            SELECT 
            TcrID 
            FROM Teacher
            WHERE TcrUseId = @UseId
             ;
        ";

        public string Uptade => @"
            UPDATE Teacher
            SET TcrName = @name
            WHERE TcrID = @id
            ;
            SELECT @id;
        ";

        public string Insert => @"
            INSERT INTO Teacher (TcrName, TcrUseID) 
            VALUES (@name, @@IDENTITY);
            SELECT SCOPE_IDENTITY();
        ";

        public string GetList => @"
            SELECT
                TcrId, TcrName, TcrUseId 
            FROM Teacher
            WHERE TcrIsActive = 1
            ;
        ";

        public string SaveTeacher => @"INSERT INTO Teacher (TcrName, TcrUseID) VALUES (@name, @id)";
        public string GetById => @"
            SELECT
                TcrId, TcrName, TcrUseId, TcrIsActive 
            FROM Teacher
            WHERE TcrId = @id
            ;
        ";

        public string DeleteTeacher => @"

            UPDATE Teacher
            SET TcrIsActive = 0
            WHERE TcrID = @TcrID;

            UPDATE Users
            SET UseIsActive = 0
            WHERE UseID = (SELECT TcrUseID FROM Teacher WHERE TcrID = @TcrID);

        ";

    }
}
