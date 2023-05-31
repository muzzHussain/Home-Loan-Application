using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Threading.Tasks;

using BusinessLogic.DataValidation;

namespace BusinessLogic.Business.BusinessImplementation
{
    /// <summary>
    /// This Business Logic will help the User to login, Register and Update the Password.
    /// </summary>
    internal class UserBusinessLogic : IUserBusinessLogic<RegisterUserDTO, UpdatePasswordDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly ICountryStateCityCodeValidation _countryStateCityCodeValidation;
        public UserBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer, ICountryStateCityCodeValidation countryStateCityCodeValidation)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
            _countryStateCityCodeValidation = countryStateCityCodeValidation;
        }

        #region RegisterTask
        /// <summary>
        /// This method registers a new user by creating a User object from a RegisterUserDTO,
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> RegisterTask(RegisterUserDTO entity)
        {
            if (!_countryStateCityCodeValidation.CheckCityCodeTask(entity.CityCode).Result)
            {
                throw new ArgumentException("This city code is not valid");
            }
            if (!_countryStateCityCodeValidation.CheckCountryCodeTask(entity.CountryCode).Result)
            {
                throw new ArgumentException("This country code is not valid");
            }
            if (!_countryStateCityCodeValidation.CheckStateCodeTask(entity.StateCode).Result)
            {
                throw new ArgumentException("This state code is not valid");
            }
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == entity.EmailId);
            if(userFromDAL == null)
            {
                User user = _mapper.Map<User>(entity);
                user.Id = new Guid();
                bool response  = await _dataAccessLayer.Write().UserWrite().AddTask(user);
                response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return response;
            }
            else
            {
                throw new ArgumentException("Email Already Exists !");
            }
            
        }
        #endregion


        #region LoginTask
        /// <summary>
        /// This method helps a the user to login by the provided email address, and comparing the user's password with the one provided while registering
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> LoginTask(string emailId, string password)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                return false;
            }
            if (password == userFromDAL.Password)
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
        /// This method updates a user's password by retrieving a User object from the data store based on the provided email address, and comparing the user's current password with the one provided earlier.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> UpdatePasswordTask(string emailId, UpdatePasswordDTO entity)
        {


            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("User Do not Exist");
            }
            if (entity.Password == userFromDAL.Password)
            {
                userFromDAL.Password = entity.NewPassword;
                bool response = await _dataAccessLayer.Write().UserWrite().EditTask(userFromDAL);
                response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return response;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
