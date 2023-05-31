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
    internal class AdvisorRepo : IReadOperation<Advisor>, IWriteOperation<Advisor>
    {
        #region Private Variables
        private readonly AppDbContext _appDbContext;
        #endregion


        #region ctor
        public AdvisorRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion


        #region GetByConditionTask
        /// <summary>
        /// Get the condition by expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type="Advisor"></returns>



        public async Task<Advisor> GetByConditionTask(Expression<Func<Advisor, bool>> expression)
        {
            return await _appDbContext.Advisors.FirstOrDefaultAsync(expression);
        }
        #endregion




        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get All records by passing expression 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<Advisor>"></returns>



        public async Task<IEnumerable<Advisor>> GetAllRecordsByConditionTask(Expression<Func<Advisor, bool>> expression)
        {
            return await _appDbContext.Advisors.Where(expression).ToListAsync();
        }
        #endregion


        #region GetAllRecordsTask
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns type ="Advisor"></returns>



        public async Task<IEnumerable<Advisor>> GetAllRecordsTask()
        {
            return await _appDbContext.Advisors.ToListAsync();
        }
        #endregion


        #region AddTask
        /// <summary>
        /// Add task by Advisor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>

        public async Task<bool> AddTask(Advisor entity)
        {
            await _appDbContext.Advisors.AddAsync(entity);
            return true;
        }
        #endregion


        #region RemoveTask
        /// <summary>
        /// Remove Task by Advisor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> RemoveTask(Advisor entity) 
        { 
        
            Advisor advisor = await _appDbContext.Advisors.FirstOrDefaultAsync( (x) => x.Id == entity.Id);
            _appDbContext.Advisors.Remove(advisor);
            return true;
        }
        #endregion


        #region EditTask
        /// <summary>
        /// Edit task by Advisor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type="bool"></returns>

        public async Task<bool> EditTask(Advisor entity)
        {
            Advisor advisor = await _appDbContext.Advisors.FirstOrDefaultAsync( (x) => x.Id == entity.Id);
            advisor.EmailId = entity.EmailId;
            advisor.Password = entity.Password;
            return true;
        }
        #endregion
    }
}
