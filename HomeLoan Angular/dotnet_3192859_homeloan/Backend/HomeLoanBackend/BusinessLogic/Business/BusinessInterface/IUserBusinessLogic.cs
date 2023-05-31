using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    /// <summary>
    /// This is an interface for User Business Logic which defines methods for user registration, login, and updating password
    /// Also take generic parameters for the user entity in registration and password update, and the login entity for user login. 
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    public interface IUserBusinessLogic<A,C>
    {
        public Task<bool> RegisterTask(A entity);
        public Task<bool> LoginTask(string emailId, string password);
        public Task<bool> UpdatePasswordTask(string emailId, C entity);
    }
}
