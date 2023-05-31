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
    internal class CollateralAndLoanApplicationRepo : IReadOperation<CollateralAndLoanApplication>, IWriteOperation<CollateralAndLoanApplication>
    {
        AppDbContext _appDbContext;
        #region ctor
        public CollateralAndLoanApplicationRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding Colleteral and Loan Application entity by adding loan application id inside colleteral
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> AddTask(CollateralAndLoanApplication entity)
        {
            await _appDbContext.CollateralsAndLoanApplications.AddAsync(entity);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the entity by matching the loanapplication id and the collateral id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> EditTask(CollateralAndLoanApplication entity)
        {
            CollateralAndLoanApplication collateralAndLoanApplication = await _appDbContext.CollateralsAndLoanApplications.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            collateralAndLoanApplication.LoanApplicationId = entity.LoanApplicationId;
            collateralAndLoanApplication.CollateralId = entity.CollateralId;
            return true;
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get all records by expression provided
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="<IEnumerable<CollateralAndLoanApplication>"></returns>
        public async Task<IEnumerable<CollateralAndLoanApplication>> GetAllRecordsByConditionTask(Expression<Func<CollateralAndLoanApplication, bool>> expression)
        {
            return await _appDbContext.CollateralsAndLoanApplications.Where(expression).ToListAsync();
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        /// Get all the records list
        /// </summary>
        /// <returns type="IEnumerable<CollateralAndLoanApplication>"></returns>
        public async Task<IEnumerable<CollateralAndLoanApplication>> GetAllRecordsTask()
        {
            return await _appDbContext.CollateralsAndLoanApplications.ToListAsync();
        }
        #endregion
        #region  GetByConditionTask
        /// <summary>
        /// Get the task by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="CollateralAndLoanApplication"></returns>
        public async Task<CollateralAndLoanApplication> GetByConditionTask(Expression<Func<CollateralAndLoanApplication, bool>> expression)
        {
            return await _appDbContext.CollateralsAndLoanApplications.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region  RemoveTask
        /// <summary>
        /// Removing the loan application from collateral
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(CollateralAndLoanApplication entity)
        {
            CollateralAndLoanApplication collateralAndLoanApplication = await _appDbContext.CollateralsAndLoanApplications.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.CollateralsAndLoanApplications.Remove(collateralAndLoanApplication);
            return true;
        }
        #endregion
    }
}
