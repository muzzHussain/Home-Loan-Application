using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Business.BusinessImplementation
{
    /// <summary>
    /// This Business Logic will help the user to Add Collateral, DeleteCollateral, GetAllCollateral, EditCollateral and to SetCollateral to An Application.
    /// </summary>
    internal class CollateralBusinessLogic:ICollateralBusinessLogic<ApplyCollateralDTO,EditCollateralDTO,DeleteCollateralDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        public CollateralBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
        }

        #region AddCollateralByUserTask
        /// <summary>
        /// The method takes an ApplyCollateralDTO object as input, which it maps to a new Collateral object.
        /// If the User object exists, the method sets the UserId property of the new Collateral object to the Id of the retrieved User object.
        /// then it generates a new Guid for the Collateral's Id property and attempts to add the new Collateral to the data store using the DataAccessLayer's Write method. 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return collateralId</returns>
        public async Task<Guid> AddCollateralByUserTask(string emailId, ApplyCollateralDTO entity)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if(userFromDAL == null)
            {
                throw new ArgumentException("User email id doesn't exist");
            }
            Collateral collateral = _mapper.Map<Collateral>(entity);
            collateral.UserId = userFromDAL.Id;
            collateral.Id = new Guid();
            bool response = await _dataAccessLayer.Write().CollateralWrite().AddTask(collateral);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            if (!response)
            {
                throw new Exception("Collateral is not added");
            }
            return collateral.Id;
        }
        #endregion

        #region DeleteCollateralByUserTask
        /// <summary>
        /// This method Deletes a Collateral object from the data store based on its ID. 
        /// Returns true if the delete operation was successful, false otherwise. If the Collateral object doesn't exist, an ArgumentException is thrown.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return response</returns>
        public async Task<bool> DeleteCollateralByUserTask(string emailId, Guid id)
        {
            Collateral collateral = await _dataAccessLayer.Read().CollateralRead().GetByConditionTask((x) => x.Id == id);
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if(userFromDAL == null)
            {
                throw new ArgumentException("User emailId does not exist");
            }
            if (collateral == null)
            {
                throw new ArgumentException("Collateral for the given Id was not found");
            }
            if(collateral.UserId != userFromDAL.Id)
            {
                throw new AccessViolationException("Current user is not allowed to access this resource");
            }
            bool response = await _dataAccessLayer.Write().CollateralWrite().RemoveTask(collateral); ;
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return response;
        }
        #endregion

        #region GetAllCollateralByUserEmailTask
        /// <summary>
        /// This method Retrieves a list of Collateral objects associated with a User identified by their email from the data store and maps them to a list of ApplyCollateralDTO objects. Returns the list of ApplyCollateralDTO objects. If the User object doesn't exist, an ArgumentException is thrown.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>returns list Of CurrentUserApplyCollateralDTO</returns>
        public async Task<IEnumerable<DeleteCollateralDTO>> GetAllCollateralByUserEmailTask(string emailId)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if(userFromDAL == null)
            {
                throw new ArgumentException("User email id doesn't exist");
            }
            IEnumerable<Collateral> listOfCollateralsByEmailFromDAL = await _dataAccessLayer.Read().CollateralRead().GetAllRecordsByConditionTask((x) => x.UserId == userFromDAL.Id);
            IEnumerable<DeleteCollateralDTO> listOfCurrentUserApplyCollateralDTO = _mapper.Map<IEnumerable<DeleteCollateralDTO>>(listOfCollateralsByEmailFromDAL);            
            return listOfCurrentUserApplyCollateralDTO;
        }
        #endregion

        #region EditCollateralByUserTask
        /// <summary>
        /// This method Edits a Collateral object in the data store based on the unique identifier of the Collateral object and the email of the associated User. 
        /// Returns the ID of the edited Collateral object. 
        /// If the User object doesn't exist or the edit operation fails, an exception is thrown.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return collateralID</returns>
        public async Task<Guid> EditCollateralByUserTask(string emailId, EditCollateralDTO entity)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if(userFromDAL == null)
            {
                throw new ArgumentException("User email id doesn't exist");
            }
            Collateral collateral = await _dataAccessLayer.Read().CollateralRead().GetByConditionTask((x) => x.Id == entity.Id);
            if(collateral == null)
            {
                throw new ArgumentException("Collateral does not exist");
            }
            if(userFromDAL.Id != collateral.UserId)
            {
                throw new AccessViolationException("Current user is forbidden to acces this resource");
            }
            collateral.Share = entity.Share;
            collateral.Value = entity.Value;
            bool respone = await _dataAccessLayer.Write().CollateralWrite().EditTask(collateral);
            respone &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            if(!respone)
            {
                throw new Exception("Collateral is not edited");
            }
            return collateral.Id;
        }
        #endregion

        #region SetCollateralToAnApplicationByUserTask
        /// <summary>
        /// This method Sets a collateral to loan application for a user.
        /// </summary>
        /// <param name="collateralId"></param>
        /// <param name="ApplicationId"></param>
        /// <returns>Return the response</returns>
        public async Task<bool> SetCollateralToAnApplicationByUserTask(string emailID, Guid collateralId, Guid ApplicationId)
        {
            Collateral collateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetByConditionTask((x) => x.Id == collateralId);
            if(collateralFromDAL == null)
            {
                throw new ArgumentException("Collateral Id does not exist");
            }
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == ApplicationId);
            if(loanApplicationFromDAL == null)
            {
                throw new ArgumentException("Application Id does not exist");
            }
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailID);
            if(userFromDAL == null)
            {
                throw new ArgumentException("User email Id does not exist");
            }
            if(collateralFromDAL.UserId != userFromDAL.Id || loanApplicationFromDAL.UserId != userFromDAL.Id)
            {
                throw new AccessViolationException("Current user is not authorized to access this resource");
            }
            CollateralAndLoanApplication collateralAndLoanApplication = new CollateralAndLoanApplication();
            collateralAndLoanApplication.LoanApplicationId = ApplicationId;
            collateralAndLoanApplication.CollateralId = collateralId;
            collateralAndLoanApplication.Id = new Guid();
            bool respone = await _dataAccessLayer.Write().CollateralAndLoanApplicationWrite().AddTask(collateralAndLoanApplication);
            return respone &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
        }
        #endregion
    }
}
