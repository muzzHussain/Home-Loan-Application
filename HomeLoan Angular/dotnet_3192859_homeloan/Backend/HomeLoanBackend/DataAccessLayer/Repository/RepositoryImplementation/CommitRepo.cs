using DataAccessLayer.Data;
using DataAccessLayer.Repository.RepositoryInterface;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    internal class CommitRepo: ICommitOperation
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public CommitRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region SaveChanges
        /// <summary>
        /// For tracking and Saving the changes in dbcontext
        /// </summary>
        /// <returns type="bool"></returns>
        public async Task<bool> SaveChanges()
        {
            return await _appDbContext.SaveChangesAsync() >= 0;
        }
        #endregion
    }
}
