using EasyLectureDAL.Repository;
using EasyLectureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLectureBusiness
{
    public class BusinessManager
    {
        public Dictionary<RepositoryType, IRepository> Repository {  get; init; }

        public StudentBusiness StudentBusiness { get; init; }
        public TeacherBusiness TeacherBusiness { get; init; }
        public LectureBusiness LectureBusiness { get; init; }
        public UserBusiness UserBusiness { get; init; }

        public BusinessManager()
        {
            Repository = new();

            LoadRepositories();

            StudentBusiness = new StudentBusiness(this);
            TeacherBusiness = new TeacherBusiness(this);
            LectureBusiness = new LectureBusiness(this);
            UserBusiness = new UserBusiness(this);
        }

        private void LoadRepositories()
        {
            _ = Repository.TryAdd(RepositoryType.Ado, new AdoRepository());
            _ = Repository.TryAdd(RepositoryType.EF, new EFRepository());

        }
    }
}
