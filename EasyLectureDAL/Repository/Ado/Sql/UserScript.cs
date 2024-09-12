namespace EasyLectureDAL.Repository.Ado.Sql
{
    public class UserScript
    {
        public string GetRoleId => @"
            SELECT
               UseRolId
            FROM Users
            WHERE UseID = @UseID
            ;
        ";
        public string GetList => @"
            SELECT
                UseID, UseMail, UsePassword, UseRolId 
            FROM Users
            WHERE UseIsActive = 1
            ;
        ";

        public string Get => @"
            SELECT
                UseID, UseMail, UsePassword, UseRolId 
            FROM Users
            WHERE UseIsActive = 1 AND UseID = @UseId
            ;
        ";

        public string Insert => @"
            INSERT INTO 
            Users 
            (UseMail, UsePassword, UseRolId) 
            VALUES (@mail, @password,@usertype)
            ;
            ";

        public string Update => @"
            UPDATE Users
            SET UseMail = @mail, UsePassword = @password, UseRolId = @usertype
            WHERE UseID = @@IDENTITY;
        ";

        public string Login => @"
            SELECT 
                UseID 
            FROM Users
            WHERE UsePassword = @password and UseMail = @mail ";
        public string CreateToken => @"
            UPDATE 
                Users
            SET 
            UseToken = @token
            WHERE UseID = @UseId;
 
                
        ";
        public string CheckSession => @"
            SELECT 
                UseID,
                UseRolId
            FROM Users
            WHERE UseToken = @token
            ;
            ";
        public string CheckPermisson => @"
        SELECT 
            RpiId
        FROM RolePermission
        WHERE RpiRolId = @roleId AND RpiService = @serviceName
        ;
        ";

    }
}
