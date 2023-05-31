using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessImplementation
{
    /// <summary>
    /// This Business Logic will help the Advisor to login, Register and Update the Password.
    /// </summary>
    internal class AdvisorBusinessLogic : IAdvisorBusinessLogic<AdvisorRegisterDTO, UpdatePasswordDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        public AdvisorBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
        }

        #region LoginTask

        ///<summary>
        ///The method checks if the provided email exists in the database and if it does, it compares the password provided with the stored password for that email.
        ///</summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>

        public async Task<bool> LoginTask(string emailId, string password)
        {
            Advisor advisorFromDAL = await _dataAccessLayer.Read().AdvisorRead().GetByConditionTask( (x) => x.EmailId == emailId );
            if (advisorFromDAL == null)
            {
                throw new ArgumentException("Advisor Not Found");
            }
            if (password == advisorFromDAL.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region UpdatePasswordTask

        /// <summary>
        /// This method updates the password with the new password provided in the UpdatePasswordDTO.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> UpdatePasswordTask(string emailId, UpdatePasswordDTO entity)
        {            
            Advisor advisorFromDAL = await _dataAccessLayer.Read().AdvisorRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (advisorFromDAL == null)
            {
                throw new ArgumentException("Email Id doesn't exist");
            }
            if (entity.Password == advisorFromDAL.Password)
            {
                advisorFromDAL.Password = entity.NewPassword;
                bool response = await _dataAccessLayer.Write().AdvisorWrite().EditTask(advisorFromDAL);
                response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return response;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region RegisterTask
        /// <summary>
        /// This method will help to register a new Advisor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> RegisterTask(AdvisorRegisterDTO entity)
        {
           
            Advisor advisorFromDAL = await _dataAccessLayer.Read().AdvisorRead().GetByConditionTask((x) => x.EmailId == entity.EmailId);
            if (advisorFromDAL == null)
            {
                Advisor advisor = _mapper.Map<Advisor>(entity);
                advisor.Id = new Guid();
                bool response = await _dataAccessLayer.Write().AdvisorWrite().AddTask(advisor);
                response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return response;
            }
            else
            {
                throw new ArgumentException("Email Already Exists !");
            }
        }
        #endregion
    }
}
