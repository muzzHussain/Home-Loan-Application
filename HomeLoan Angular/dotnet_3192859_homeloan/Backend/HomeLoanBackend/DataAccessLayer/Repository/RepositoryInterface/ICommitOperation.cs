using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface ICommitOperation
    {
        public Task<bool> SaveChanges();
    }
}
