namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class RoleScript
    {
        public string GetList => @"
             SELECT
                RolId, RolName 
            FROM Role
            WHERE RolIsActive = 1
            ;
        ";

        public string Get => @"
            SELECT
                RolId, RolName
            FROM Role
            WHERE RolIsActive = 1 AND RolId = @UstId
            ;
        ";
    }
}
