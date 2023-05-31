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
    internal class PromotionsRepo:IWriteOperation<Promotions>, IReadOperation<Promotions>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public PromotionsRepo(AppDbContext appDbContext )
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get list of all Promotions by using expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="<IEnumerable<Promotions>"></returns>
        public async Task<IEnumerable<Promotions>> GetAllRecordsByConditionTask(Expression<Func<Promotions, bool>> expression)
        {
            return await _appDbContext.Promotions.Where(expression).ToListAsync();
        }
        #endregion
        #region  GetAllRecordsTask
        /// <summary>
        /// Get list of all records
        /// </summary>
        /// <returns type="IEnumerable<Promotions>"></returns>
        public async Task<IEnumerable<Promotions>> GetAllRecordsTask()
        {
            return await _appDbContext.Promotions.ToListAsync();
        }
        #endregion

        #region GetByConditionTask
        /// <summary>
        /// Get one particular promotion by using expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="Promotions"></returns>
        public async Task<Promotions> GetByConditionTask(Expression<Func<Promotions, bool>> expression)
        {
            return await _appDbContext.Promotions.FirstOrDefaultAsync(expression);
        }
        #endregion

        #region RemoveTask
        /// <summary>
        /// Remove the Promotion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> RemoveTask(Promotions entity)
        {
            Promotions promotions = await _appDbContext.Promotions.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.Promotions.Remove(promotions);
            return true;
        }
        #endregion

        #region AddTask
        /// <summary>
        /// Add Promotion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> AddTask(Promotions entity)
        {
            await _appDbContext.Promotions.AddAsync(entity);
            return true;
        }
        #endregion

        #region EditTask
        /// <summary>
        /// Edit Promotion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> EditTask(Promotions entity)
        {
            Promotions promotions = await _appDbContext.Promotions.FirstOrDefaultAsync((x) => (x.Id == entity.Id));
            promotions.StartDate = entity.StartDate;
            promotions.EndDate = entity.EndDate;
            promotions.Message = entity.Message;
            promotions.Type = entity.Type;
            return true;
        }
        #endregion
    }
}
