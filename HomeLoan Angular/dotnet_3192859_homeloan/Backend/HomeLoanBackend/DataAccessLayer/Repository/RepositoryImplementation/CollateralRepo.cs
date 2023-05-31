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
    internal class CollateralRepo : IReadOperation<Collateral>,IWriteOperation<Collateral>
    {
        #region private variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public CollateralRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region  GetAllRecordsByConditionTask
        /// <summary>
        /// Get all records by given condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="IEnumerable<Collateral>"></returns>
        public async Task<IEnumerable<Collateral>> GetAllRecordsByConditionTask(Expression<Func<Collateral, bool>> expression)
        {
            return await _appDbContext.Collaterals.Where(expression).ToListAsync();
        }
        #endregion
        #region  GetAllRecordsTask
        /// <summary>
        /// Get list of all the records of collateral
        /// </summary>
        /// <returns type ="IEnumerable<Collateral>"></returns>
        public async Task<IEnumerable<Collateral>> GetAllRecordsTask()
        {
            return await _appDbContext.Collaterals.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get all collateral by given condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="Collateral"></returns>
        public async Task<Collateral> GetByConditionTask(Expression<Func<Collateral, bool>> expression)
        {
            return await _appDbContext.Collaterals.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Remove task 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(Collateral entity)
        {
            Collateral collateral = await _appDbContext.Collaterals.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.Collaterals.Remove(collateral);
            return true;
        }
        #endregion
        #region  AddTask
        /// <summary>
        /// Adding the task
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool" ></returns>

        public async Task<bool> AddTask(Collateral entity)
        {
            await _appDbContext.Collaterals.AddAsync(entity);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the given task
        /// </summary>
        /// <param name="entity"></param>
        /// <returns  type="bool"></returns>
        public async Task<bool> EditTask(Collateral entity)
        {
            Collateral collateral = await _appDbContext.Collaterals.FirstOrDefaultAsync( (x) => (x.Id == entity.Id));
            collateral.Share = entity.Share;
            collateral.Value = entity.Value;
            collateral.UserId = entity.UserId;
            collateral.Type = entity.Type;
            return true;
        }
        #endregion
    }
}
