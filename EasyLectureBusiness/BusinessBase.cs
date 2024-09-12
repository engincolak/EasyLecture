using EasyLectureDAL.Repository;
using EasyLectureModel;

namespace EasyLectureBusiness
{
    public class BusinessBase
    {
        protected BusinessManager BusinessManager { get; init; }

        public BusinessBase(BusinessManager businessManager)
        {
            BusinessManager = businessManager;
        }

        protected IRepository GetRepository(RepositoryType repositoryType)
        {
            if (BusinessManager.Repository.TryGetValue(repositoryType, out IRepository repository))
                return repository;
            else
                throw new Exception($"Repository not found. Type : {repositoryType} ");
        }
    }
}
