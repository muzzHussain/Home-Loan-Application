using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IWriteOperation<T> where T : class
    {
        public Task<bool> AddTask(T entity);
        public Task<bool> RemoveTask(T entity);
        public Task<bool> EditTask(T entity);
    }
}
