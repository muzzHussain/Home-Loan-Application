using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.RepositoryImplementation
{
    public class PropertyRepo : IReadOperation<Property>, IWriteOperation<Property>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public PropertyRepo(AppDbContext context)
        {
            _appDbContext = context;
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        /// Getting the list of all Property
        /// </summary>
        /// <returns type="IEnumerable<Property>"></returns>
        public async Task<IEnumerable<Property>> GetAllRecordsTask()
        {
            return await _appDbContext.Properties.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Getting the Property provided by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="Property"></returns>
        public async Task<Property> GetByConditionTask(Expression<Func<Property, bool>> expression)
        {
            return await _appDbContext.Properties.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region  GetAllRecordsByConditionTask
        /// <summary>
        /// Getting the list of Property
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<Property>"></returns>
        public async Task<IEnumerable<Property>> GetAllRecordsByConditionTask(Expression<Func<Property, bool>> expression)
        {
            return await _appDbContext.Properties.Where(expression).ToListAsync();
        }
        #endregion
        #region  AddTask
        /// <summary>
        /// Adding the Property
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> AddTask(Property entity)
        {
            await _appDbContext.Properties.AddAsync(entity);
            return true; ;
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the property
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> RemoveTask(Property entity)
        {
            Property property = await _appDbContext.Properties.FirstOrDefaultAsync(item => item.Id == entity.Id);
            _appDbContext.Properties.Remove(property);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the property
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> EditTask(Property entity)
        {
            Property property = await _appDbContext.Properties.FirstOrDefaultAsync(item => item.Id == entity.Id);
            property.Address = entity.Address;
            property.Size = entity.Size;
            property.Cost = entity.Cost;
            property.RegistrationCost = entity.RegistrationCost;
            return true;
        }
        #endregion
    }
}
