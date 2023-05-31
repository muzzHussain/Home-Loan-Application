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
    internal class LoanApplicationRepo : IReadOperation<LoanApplication>, IWriteOperation<LoanApplication>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor

        public LoanApplicationRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get the LoanApplication by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="LoanApplication"></returns>
        public async Task<LoanApplication> GetByConditionTask(Expression<Func<LoanApplication, bool>> expression)
        {
            return await _appDbContext.LoanApplication.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get list of all records by given condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="IEnumerable<LoanApplication>"></returns>

        public async Task<IEnumerable<LoanApplication>> GetAllRecordsByConditionTask(Expression<Func<LoanApplication, bool>> expression)
        {
            return await _appDbContext.LoanApplication.Where(expression).ToListAsync();
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the LoanApplication
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> RemoveTask(LoanApplication entity)
        {
            LoanApplication loanApplication = await _appDbContext.LoanApplication.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.LoanApplication.Remove(loanApplication);
            return true;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding the LoanApplication 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> AddTask(LoanApplication entity)
        {
            await _appDbContext.LoanApplication.AddAsync(entity);
            return true;
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        /// Get list of all records of loanapplication
        /// </summary>
        /// <returns type ="IEnumerable<LoanApplication>"></returns>
        public async Task<IEnumerable<LoanApplication>> GetAllRecordsTask()
        {
            return await _appDbContext.LoanApplication.ToListAsync();
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the LoanApplication
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> EditTask(LoanApplication entity)
        {
            LoanApplication loanApplication = await _appDbContext.LoanApplication.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            loanApplication.Notes = entity.Notes;
            loanApplication.Status = entity.Status;
            loanApplication.UserId = entity.UserId;
            loanApplication.LoanRequirementsId = entity.LoanRequirementsId;
            loanApplication.PersonalIncomeId = entity.PersonalIncomeId;
            loanApplication.PropertyId = entity.PropertyId;
            return true;
        }
        #endregion
    }
}
