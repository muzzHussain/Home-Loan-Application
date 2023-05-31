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
    
    internal class PersonalIncomeRepo : IReadOperation<PersonalIncome>, IWriteOperation<PersonalIncome>
    {
        #region Private Variables
        private AppDbContext _appDbContext;
        #endregion
        #region ctor
        public PersonalIncomeRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region GetAllRecordsByConditionTask
        /// <summary>
        /// Get all records list by using expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="IEnumerable<PersonalIncome>"></returns>
        public async Task<IEnumerable<PersonalIncome>> GetAllRecordsByConditionTask(Expression<Func<PersonalIncome, bool>> expression)
        {
            return await _appDbContext.PersonalIncomes.Where(expression).ToListAsync();
        }
        #endregion

        #region GetByConditionTask
        /// <summary>
        /// Get PersonalIncome by given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns type ="PersonalIncome"></returns>

        public async Task<PersonalIncome> GetByConditionTask(Expression<Func<PersonalIncome, bool>> expression)
        {
            return await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(expression);
        }
        #endregion
        #region RemoveTask
        /// <summary>
        /// Removing the PersonalIncome

        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> RemoveTask(PersonalIncome entity)
        {
            PersonalIncome personalIncome = await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(item => item.Id == entity.Id);
            _appDbContext.PersonalIncomes.Remove(personalIncome);
            return true;
        }
        #endregion
        #region AddTask
        /// <summary>
        /// Adding the PersonalIncome
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> AddTask(PersonalIncome entity)
        {
            await _appDbContext.PersonalIncomes.AddAsync(entity);
            return true;
        }
        #endregion
        #region  EditTask
        /// <summary>
        /// Editing the PersonalIncome
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns type ="bool"></returns>
        public async Task<bool> EditTask(PersonalIncome entity)
        {
            PersonalIncome personalIncome = await _appDbContext.PersonalIncomes.FirstOrDefaultAsync(item => item.Id == entity.Id);
            personalIncome.MonthlyFamilyIncome = entity.MonthlyFamilyIncome;
            personalIncome.OtherIncome = entity.OtherIncome;
            return true;
        }
        #endregion
        #region GetAllRecordsTask
        /// <summary>
        ///Get list of all records of PersonalIncome 
        /// 
        /// </summary>
        /// <returns type ="IEnumerable<PersonalIncome>"></returns>
        public async Task<IEnumerable<PersonalIncome>> GetAllRecordsTask()
        {
            return await _appDbContext.PersonalIncomes.ToListAsync();
        }
        #endregion
    }
}
