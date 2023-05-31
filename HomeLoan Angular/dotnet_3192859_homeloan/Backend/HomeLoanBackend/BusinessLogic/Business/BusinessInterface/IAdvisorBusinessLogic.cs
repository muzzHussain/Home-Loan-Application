using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    /// <summary>
    /// This interface defines the business logic methods for an advisor entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public interface IAdvisorBusinessLogic<T, P>
    {
        public Task<bool> RegisterTask(T entity);
        public Task<bool> LoginTask(string emailId, string password);
        public Task<bool> UpdatePasswordTask(string emailID, P entity);
    }
}
