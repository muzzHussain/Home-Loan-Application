using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Utility.Enums;

namespace BusinessLogic.Business.BusinessInterface
{
    /// <summary>
    /// This is an interface for Loan Business Logic which defines methods for performing loan application related operations. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    /// <typeparam name="X"></typeparam>
    public interface ILoanBusinessLogic<T, P, X, Y>
    {
        public Task<Guid> ApplyLoanApplicationByUserTask(string emailId, T entity);
        public Task<IEnumerable<P>> FetchAllUserAppliedLoanApplicationByAdvisorTask(string emailId);
        public Task<P> FetchAnAppliedLoanApplicationByAdvisorTask(Guid loanApplicationId);
        public Task<X> FetchAnLoanApplicationByAdvisorTask(Guid loanApplicationId);

        public Task<IEnumerable<X>> FetchAllLoanApplicationByUser(string emailId);
        public Task<Guid> ChangeLoanApplicationStatusByAdvisorTask(Guid applicationId, LoanApplicationStatus status, string? notes);
        public Task<bool> ChangeLoanApplicationStatusByUserTask(string emailId, Guid applicationId);
        public Task<bool> EditLoanApplicationByUserTask(string emailId, Y entity);
        public Task<IEnumerable<AdvisorLoanApplicationDTO>> FetchAllAppliedLoanApplicationByAdvisorTask();
        public Task<IEnumerable<UserLoanApplicationDTO>> FetchAllLoanApplicationByAdvisorTask();


    }
}
