using EasyLectureModel.Dto;
using Microsoft.EntityFrameworkCore;

namespace EasyLectureDAL.Base
{
    internal class EFEntityBase : DbContext
    {
        protected DbSet<UserDto> Users { get; set; }
        protected DbSet<StudentDto> Student { get; set; }
        protected DbSet<TeacherDto> Teacher { get; set; }
        protected DbSet<LectureDto> Lecture { get; set; }
        protected DbSet<StudentLectureDto> StudentLecture { get; set; }
        protected DbSet<RoleDto> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentLectureDto>().HasKey(u => new
            {
                u.StlLctId,
                u.StlStdId
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "Server=DESKTOP-PB6C85K\\MSSQLSERVER01;Database=staj;User ID=sa;Password=123456;TrustServerCertificate=False;Encrypt=False;";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conn);
            }
        }
    }
}
