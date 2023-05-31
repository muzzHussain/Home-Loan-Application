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
    internal class StateRepo: IReadOperation<State>, IWriteOperation<State>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public StateRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding the state
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> AddTask(State entity)
        {
            await _appDbContext.State.AddAsync(entity);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the state
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> EditTask(State entity)
        {
            State state = await _appDbContext.State.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            state.Name = entity.Name;
            state.Code = entity.Code;
            state.CountryId = entity.CountryId;
            return true;
        }
        #endregion
        #region  GetAllRecordsByConditionTask
        /// <summary>
        /// Getting list of all records
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<State>"></returns>
        public async Task<IEnumerable<State>> GetAllRecordsByConditionTask(Expression<Func<State, bool>> expression)
        {
            return await _appDbContext.State.Where(expression).ToListAsync();
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        /// Get all records list
        /// </summary>
        /// <returns type ="IEnumerable<State>"></returns>
        public async Task<IEnumerable<State>> GetAllRecordsTask()
        {
            return await _appDbContext.State.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get the records by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="State"></returns>
        public async Task<State> GetByConditionTask(Expression<Func<State, bool>> expression)
        {
            return await _appDbContext.State.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the state
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(State entity)
        {
            State state = await _appDbContext.State.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.State.Remove(state);
            return true;
        }
        #endregion
    }
}
