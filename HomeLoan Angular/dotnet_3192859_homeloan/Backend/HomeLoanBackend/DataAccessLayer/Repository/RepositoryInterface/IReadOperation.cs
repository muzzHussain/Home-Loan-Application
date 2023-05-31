using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.RepositoryInterface
{
    public interface IReadOperation<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllRecordsTask();
        public Task<T> GetByConditionTask(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllRecordsByConditionTask(Expression<Func<T, bool>> expression);
    }
}
