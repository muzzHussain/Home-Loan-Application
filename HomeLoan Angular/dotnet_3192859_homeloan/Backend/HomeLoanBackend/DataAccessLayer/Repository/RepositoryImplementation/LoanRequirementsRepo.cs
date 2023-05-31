using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    internal class LoanRequirementsRepo:IReadOperation<LoanRequirements>, IWriteOperation<LoanRequirements>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public LoanRequirementsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region  GetAllRecordsTask
        /// <summary>
        /// Get list of all LoanRequirements
        /// </summary>
        /// <returns type ="IEnumerable<LoanRequirements>"></returns>
        public async Task<IEnumerable<LoanRequirements>> GetAllRecordsTask()
        {
            return await _appDbContext.LoanRequirements.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get Loan Requirement by using the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="LoanRequirements"></returns>
        public async Task<LoanRequirements> GetByConditionTask(Expression<Func<LoanRequirements, bool>> expression)
        {
            return await _appDbContext.LoanRequirements.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get the list of all records by using the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<LoanRequirements>"></returns>
        public async Task<IEnumerable<LoanRequirements>> GetAllRecordsByConditionTask(Expression<Func<LoanRequirements, bool>> expression)
        {
            return await _appDbContext.LoanRequirements.Where(expression).ToListAsync();
        }
        #endregion
        #region AddTask
        /// <summary>
        ///Adding the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> AddTask(LoanRequirements entity)
        {
            await _appDbContext.LoanRequirements.AddAsync(entity);
            return true;
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>

        public async Task<bool> RemoveTask(LoanRequirements entity)
        {
            LoanRequirements loanRequirements = await _appDbContext.LoanRequirements.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _appDbContext.Remove(loanRequirements);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> EditTask(LoanRequirements entity)
        {
            LoanRequirements loanRequirements = await _appDbContext.LoanRequirements.FirstOrDefaultAsync((x) => (x.Id == entity.Id));
            loanRequirements.LoanDuration = entity.LoanDuration;
            loanRequirements.LoanStartDate = entity.LoanStartDate;
            loanRequirements.LoanAmount = entity.LoanAmount;
            return true;
        }
        #endregion
    }
}
