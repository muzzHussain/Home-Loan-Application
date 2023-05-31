using System;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessInterface
{
    /// <summary>
    /// This is an interface for Promotion Business Logic which defines a single method for adding a new promotion by an advisor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPromotionBusinessLogic<T, P>
    {       
        public Task<Guid> AddNewPromotionByAdvisorTask(T newPromotion);

        public Task<bool> RemovePromotionByAdvisorTask(Guid Id);

        public Task<Guid> EditPromotionByAdvisorTask(P entity);

        public Task<T> GetOpenPromotionByAdvisorTask();
    }
}
