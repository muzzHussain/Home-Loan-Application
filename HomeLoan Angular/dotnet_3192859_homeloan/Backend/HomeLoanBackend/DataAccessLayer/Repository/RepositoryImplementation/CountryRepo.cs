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
    internal class CountryRepo: IReadOperation<Country>, IWriteOperation<Country>
    {
        #region PrivateVariables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public CountryRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding task 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> AddTask(Country entity)
        {
            await _appDbContext.Country.AddAsync(entity);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the task
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>

        public async Task<bool> EditTask(Country entity)
        {
            Country country = await _appDbContext.Country.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            country.Name = entity.Name;
            country.Code = entity.Code;
            return true;
        }
        #endregion
        #region  GetAllRecordsByConditionTask
        /// <summary>
        /// Get all records by using following expressions
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="IEnumerable<Country>"></returns>
        public async Task<IEnumerable<Country>> GetAllRecordsByConditionTask(Expression<Func<Country, bool>> expression)
        {
            return await _appDbContext.Country.Where(expression).ToListAsync();
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        /// Getting the list of the country
        /// </summary>
        /// <returns type ="IEnumerable<Country>"></returns>
        public async Task<IEnumerable<Country>> GetAllRecordsTask()
        {
            return await _appDbContext.Country.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Getting the country by using the given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="Country"></returns>
        public async Task<Country> GetByConditionTask(Expression<Func<Country, bool>> expression)
        {
            return await _appDbContext.Country.FirstOrDefaultAsync(expression);
        }
        #endregion

        #region RemoveTask
        /// <summary>
        /// Removing the task 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(Country entity)
        {
            Country country = await _appDbContext.Country.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.Country.Remove(country);
            return true;
        }
        #endregion
    }
}
