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
    public class UserRepo : IReadOperation<User>, IWriteOperation<User>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion
        #region ctor
        public UserRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get the list of all recordsof users by given condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="IEnumerable<User>"></returns>
        public async Task<IEnumerable<User>> GetAllRecordsByConditionTask(Expression<Func<User, bool>> expression)
        {
            return await _appDbContext.Users.Where(expression).ToListAsync();
        }
        #endregion

        #region  GetAllRecordsTask
        /// <summary>
        /// Get list of all records of user 
        /// </summary>
        /// <returns type="IEnumerable<User>"></returns>
        public async Task<IEnumerable<User>> GetAllRecordsTask()
        {
            return await _appDbContext.Users.ToListAsync();
        }
        #endregion
        #region GetByConditionTask
        /// <summary>
        /// Get records of user by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="User"></returns>
        public async Task<User> GetByConditionTask(Expression<Func<User, bool>> expression)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> RemoveTask(User entity)
        {
            User userEntity = await _appDbContext.Users.FirstOrDefaultAsync((entity) => entity.Id == entity.Id);
            _appDbContext.Users.Remove(userEntity);
            return true;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding the user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> AddTask(User entity)
        {
            await _appDbContext.Users.AddAsync(entity);
            return true;
        }
        #endregion
        #region EditTask
        /// <summary>
        /// Editing the user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>
        public async Task<bool> EditTask(User entity)
        {
            User user = await _appDbContext.Users.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            user.EmailId = entity.EmailId;
            user.Password = entity.Password;
            user.CountryCode = entity.CountryCode;
            user.CityCode = entity.CityCode;
            user.StateCode = entity.StateCode;
            user.CountryCode = entity.CountryCode;
            return true;
        }
        #endregion
    }
}
