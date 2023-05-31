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
    internal class CityRepo : IReadOperation<City>, IWriteOperation<City>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
# region ctor
        public CityRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding City
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool></returns>
        public async Task<bool> AddTask(City entity)
        {
            await _appDbContext.City.AddAsync(entity);
            return true;
        }
        #endregion

        #region EditTask
        /// <summary>
        /// Editing the city 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> EditTask(City entity)
        {
            City city = await _appDbContext.City.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            city.Name = entity.Name;
            city.StateId = entity.StateId;
            city.Code = entity.Code;
            return true;
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get All records by using expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<City>"></returns>

        public async Task<IEnumerable<City>> GetAllRecordsByConditionTask(Expression<Func<City, bool>> expression)
        {
            return await _appDbContext.City.Where(expression).ToListAsync();
        }
        #endregion
        #region GetAllRecordTask
        /// <summary>
        /// Get all Records
        /// </summary>
        /// <returns type="IEnumerable<City>"></returns>
        public async Task<IEnumerable<City>> GetAllRecordsTask()
        {
            return await _appDbContext.City.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get task by given condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="City"></returns>
        public async Task<City> GetByConditionTask(Expression<Func<City, bool>> expression)
        {
            return await _appDbContext.City.FirstOrDefaultAsync(expression);
        }
        #endregion
# region RemoveTask
        /// <summary>
        /// Remove the given task
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(City entity)
        {
            City city = await _appDbContext.City.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _appDbContext.City.Remove(city);
            return true;
        }
        #endregion
    }
}
