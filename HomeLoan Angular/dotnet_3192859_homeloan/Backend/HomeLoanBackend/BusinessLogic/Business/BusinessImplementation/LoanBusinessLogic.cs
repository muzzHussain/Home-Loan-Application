using AutoMapper;
using BusinessLogic.Algorithms;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using static Utility.Enums;

namespace BusinessLogic.Business.BusinessImplementation
{
    /// <summary>
    /// This Business Logic will help the user and Advisor to ApplyLoan, CalculateLoanEligibility, FetchAllUserAppliedLoanApplicationByAdvisor, 
    /// FetchAnLoanApplication, FetchAllLoanApplicationByUser, ChangeLoanApplicationStatusByAdvisor and ChangeLoanApplicationStatusByUserTask.
    /// </summary>
    internal class LoanBusinessLogic : ILoanBusinessLogic<ApplyLoanDTO, AdvisorLoanApplicationDTO, UserLoanApplicationDTO, EditLoanApplicationDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        public LoanBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
        }

        #region ApplyLoanApplicationByUserTask
        /// <summary>
        /// This method creates a new loan application by first validating the user's email, adding property, loan requirements and personal income records to the database, 
        /// and finally creating a new loan application record with the user's ID, and the IDs of the added property, loan requirements and personal income records
        /// </summary>
        /// <param name="loanDTO"></param>
        /// <returns>the ID of the newly created loan application.</returns>
        public async Task<Guid> ApplyLoanApplicationByUserTask(string emailId, ApplyLoanDTO loanDTO)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("Email Id doesn't exist");
            }
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.UserId == userFromDAL.Id && (x.Status == LoanApplicationStatus.Created || x.Status == LoanApplicationStatus.Applied));
            if (loanApplicationFromDAL != null)
            {
                throw new ArgumentException("User already has open application");
            }
            Property property = _mapper.Map<Property>(loanDTO);
            property.Id = new Guid();
            bool response = await _dataAccessLayer.Write().PropertyWrite().AddTask(property);

            LoanRequirements loanRequirements = _mapper.Map<LoanRequirements>(loanDTO);
            loanRequirements.Id = new Guid();
            response &= await _dataAccessLayer.Write().LoanRequirementsWrite().AddTask(loanRequirements);


            PersonalIncome personalIncome = _mapper.Map<PersonalIncome>(loanDTO);
            personalIncome.Id = new Guid();
            response &= await _dataAccessLayer.Write().PersonalIncomeWrite().AddTask(personalIncome);


            LoanApplication application = new LoanApplication();
            application.Id = new Guid();
            application.PersonalIncomeId = personalIncome.Id;
            application.LoanRequirementsId = loanRequirements.Id;
            application.PropertyId = property.Id;
            application.UserId = userFromDAL.Id;
            application.Status = LoanApplicationStatus.Created;
            response &= await _dataAccessLayer.Write().LoanApplicationWrite().AddTask(application);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            if (!response)
            {
                throw new Exception("Unable to create loan application");
            }
            return application.Id;
        }
        #endregion

        #region CalculateLoanEligibility
        /// <summary>
        /// This method calculates the loan eligibility based on the collateral and loan requirements of a loan application. It first checks if the collateral and loan application exist,
        /// then calculates the eligibility using a CollateralCalculator class and returns the eligibility as an integer.
        /// </summary>
        /// <param name="collateralId"></param>
        /// <param name="applicationId"></param>
        /// <returns>integer value for eligibility</returns>
        public async Task<int> CalculateLoanEligibility(Guid collateralId, Guid applicationId)
        {
            Collateral collateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetByConditionTask((x) => x.Id == collateralId);
            if (collateralFromDAL == null)
            {
                throw new ArgumentException("Collateral Id doesn't exist");
            }
            LoanApplication applicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == applicationId);
            if (applicationFromDAL == null)
            {
                throw new ArgumentException("Application Id doesn't exist");
            }
            LoanRequirements loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetByConditionTask((x) => x.Id == applicationFromDAL.LoanRequirementsId);
            if (loanRequirementsFromDAL == null)
            {
                throw new ArgumentException("Loan Requirements Id doesn't exist");
            }
            CollateralCalculator _collateralCalculator = new CollateralCalculator(collateralFromDAL.Type.ToString(),
                collateralFromDAL.Value,
                collateralFromDAL.Share);
            int eligibility = _collateralCalculator.CalculateEligibility(loanRequirementsFromDAL.LoanAmount);
            return eligibility;
        }
        #endregion

        #region FetchAllUserAppliedLoanApplicationByAdvisorTask
        /// <summary>
        /// This method fetches all loan applications applied by a user for an advisor, along with associated collateral, 
        /// loan requirements, personal income, and property details. It also calculates the loan eligibility and returns the result as a list of AdvisorLoanApplicationDTO objects.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>will return list Loan Application</returns>
        public async Task<IEnumerable<AdvisorLoanApplicationDTO>> FetchAllUserAppliedLoanApplicationByAdvisorTask(string emailId)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("User emailId does not exist");
            }
            IEnumerable<LoanApplication> allUserLoanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetAllRecordsByConditionTask((x) => x.UserId == userFromDAL.Id && x.Status == LoanApplicationStatus.Applied);
            if (allUserLoanApplicationFromDAL == null)
            {
                throw new ArgumentException($"There are no loan application for {emailId}");
            }
            IList<AdvisorLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = new List<AdvisorLoanApplicationDTO>();

            IEnumerable<CollateralAndLoanApplication> userCollateralAndLoanApplicationFromDAL = await _dataAccessLayer.Read().CollateralAndLoanApplicationRead().GetAllRecordsTask();
            IEnumerable<Collateral> userCollateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetAllRecordsTask();
            IEnumerable<LoanRequirements> userLoanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetAllRecordsTask();
            IEnumerable<PersonalIncome> userPersonalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetAllRecordsTask();
            IEnumerable<Property> userPropertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetAllRecordsTask();

            Dictionary<Guid, Collateral> collateralDict = new Dictionary<Guid, Collateral>();
            Dictionary<Guid, LoanRequirements> loanRequirementsDict = new Dictionary<Guid, LoanRequirements>();
            Dictionary<Guid, PersonalIncome> personalIncomeDict = new Dictionary<Guid, PersonalIncome>();
            Dictionary<Guid, Property> propertyDict = new Dictionary<Guid, Property>();
            Dictionary<Guid, Guid> collateralAndLoanDict = new Dictionary<Guid, Guid>();

            foreach (LoanRequirements loanRequirements in userLoanRequirementsFromDAL)
            {
                loanRequirementsDict.Add(loanRequirements.Id, loanRequirements);
            }
            foreach (CollateralAndLoanApplication collateralAndLoan in userCollateralAndLoanApplicationFromDAL)
            {
                collateralAndLoanDict.Add(collateralAndLoan.LoanApplicationId, collateralAndLoan.CollateralId);
            }
            foreach (PersonalIncome personalIncome in userPersonalIncomeFromDAL)
            {
                personalIncomeDict.Add(personalIncome.Id, personalIncome);
            }
            foreach (Property property in userPropertyFromDAL)
            {
                propertyDict.Add(property.Id, property);
            }
            foreach (Collateral collateral in userCollateralFromDAL)
            {
                collateralDict.Add(collateral.Id, collateral);
            }
            foreach (LoanApplication loanApplication in allUserLoanApplicationFromDAL)
            {
                AdvisorLoanApplicationDTO advisorLoanApplicationDTO = new AdvisorLoanApplicationDTO();
                advisorLoanApplicationDTO.LoanDuration = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanDuration;
                advisorLoanApplicationDTO.LoanAmount = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanAmount;
                advisorLoanApplicationDTO.LoanStartDate = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanStartDate;

                advisorLoanApplicationDTO.MonthlyFamilyIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;
                advisorLoanApplicationDTO.OtherIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;

                advisorLoanApplicationDTO.Address = propertyDict[loanApplication.PropertyId].Address;
                advisorLoanApplicationDTO.Size = propertyDict[loanApplication.PropertyId].Size;
                advisorLoanApplicationDTO.Cost = propertyDict[loanApplication.PropertyId].Cost;
                advisorLoanApplicationDTO.RegistrationCost = propertyDict[loanApplication.PropertyId].RegistrationCost;

                advisorLoanApplicationDTO.EmailId = emailId;

                advisorLoanApplicationDTO.Type = collateralDict[collateralAndLoanDict[loanApplication.Id]].Type.ToString();
                advisorLoanApplicationDTO.Value = collateralDict[collateralAndLoanDict[loanApplication.Id]].Value;
                advisorLoanApplicationDTO.Share = collateralDict[collateralAndLoanDict[loanApplication.Id]].Share;

                int eligibilityResult = await CalculateLoanEligibility(collateralAndLoanDict[loanApplication.Id], loanApplication.Id);
                if (eligibilityResult == 0)
                {
                    advisorLoanApplicationDTO.Eligibility = "Red";
                }
                else if (eligibilityResult == 1)
                {
                    advisorLoanApplicationDTO.Eligibility = "Yellow";
                }
                else
                {
                    advisorLoanApplicationDTO.Eligibility = "Green";
                }
                listOfAdvisorLoanApplicationDTO.Add(advisorLoanApplicationDTO);

            }
            if (listOfAdvisorLoanApplicationDTO.Count() == 0)
            {
                throw new ArgumentException($"There are no applied loan application for {emailId}");
            }
            return listOfAdvisorLoanApplicationDTO;
        }
        #endregion



        public async Task<IEnumerable<AdvisorLoanApplicationDTO>> FetchAllAppliedLoanApplicationByAdvisorTask()
        {
            IEnumerable<LoanApplication> allLoanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetAllRecordsByConditionTask((x) => x.Status == LoanApplicationStatus.Applied 
            || x.Status==LoanApplicationStatus.Accepted || x.Status==LoanApplicationStatus.InProgress || x.Status==LoanApplicationStatus.Rejected);
            if (allLoanApplicationFromDAL == null)
            {
                throw new ArgumentException($"There are no Applied loan application");
            }
            IList<AdvisorLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = new List<AdvisorLoanApplicationDTO>();

            IEnumerable<CollateralAndLoanApplication> collateralAndLoanApplicationFromDAL = await _dataAccessLayer.Read().CollateralAndLoanApplicationRead().GetAllRecordsTask();
            IEnumerable<Collateral> collateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetAllRecordsTask();
            IEnumerable<LoanRequirements> loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetAllRecordsTask();
            IEnumerable<PersonalIncome> personalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetAllRecordsTask();
            IEnumerable<Property> propertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetAllRecordsTask();
            IEnumerable<User> userFromDAL = await _dataAccessLayer.Read().UserRead().GetAllRecordsTask();

            Dictionary<Guid, Collateral> collateralDict = new Dictionary<Guid, Collateral>();
            Dictionary<Guid, LoanRequirements> loanRequirementsDict = new Dictionary<Guid, LoanRequirements>();
            Dictionary<Guid, PersonalIncome> personalIncomeDict = new Dictionary<Guid, PersonalIncome>();
            Dictionary<Guid, Property> propertyDict = new Dictionary<Guid, Property>();
            Dictionary<Guid, Guid> collateralAndLoanDict = new Dictionary<Guid, Guid>();
            Dictionary<Guid, User> userDict = new Dictionary<Guid, User>();

            foreach (LoanRequirements loanRequirements in loanRequirementsFromDAL)
            {
                loanRequirementsDict.Add(loanRequirements.Id, loanRequirements);
            }
            foreach (CollateralAndLoanApplication collateralAndLoan in collateralAndLoanApplicationFromDAL)
            {
                collateralAndLoanDict.Add(collateralAndLoan.LoanApplicationId, collateralAndLoan.CollateralId);
            }
            foreach (PersonalIncome personalIncome in personalIncomeFromDAL)
            {
                personalIncomeDict.Add(personalIncome.Id, personalIncome);
            }
            foreach (Property property in propertyFromDAL)
            {
                propertyDict.Add(property.Id, property);
            }
            foreach (Collateral collateral in collateralFromDAL)
            {
                collateralDict.Add(collateral.Id, collateral);
            }
            foreach (User user in userFromDAL)
            {
                userDict.Add(user.Id, user);
            }
            foreach (LoanApplication loanApplication in allLoanApplicationFromDAL)
            {
                AdvisorLoanApplicationDTO advisorLoanApplicationDTO = new AdvisorLoanApplicationDTO();
                advisorLoanApplicationDTO.LoanDuration = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanDuration;
                advisorLoanApplicationDTO.LoanAmount = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanAmount;
                advisorLoanApplicationDTO.LoanStartDate = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanStartDate;

                advisorLoanApplicationDTO.MonthlyFamilyIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;
                advisorLoanApplicationDTO.OtherIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;

                advisorLoanApplicationDTO.Address = propertyDict[loanApplication.PropertyId].Address;
                advisorLoanApplicationDTO.Size = propertyDict[loanApplication.PropertyId].Size;
                advisorLoanApplicationDTO.Cost = propertyDict[loanApplication.PropertyId].Cost;
                advisorLoanApplicationDTO.RegistrationCost = propertyDict[loanApplication.PropertyId].RegistrationCost;

                advisorLoanApplicationDTO.EmailId = userDict[loanApplication.UserId].EmailId;
                advisorLoanApplicationDTO.Id = loanApplication.Id;
                advisorLoanApplicationDTO.Status = loanApplication.Status.ToString();
                advisorLoanApplicationDTO.Type = collateralDict[collateralAndLoanDict[loanApplication.Id]].Type.ToString();
                advisorLoanApplicationDTO.Value = collateralDict[collateralAndLoanDict[loanApplication.Id]].Value;
                advisorLoanApplicationDTO.Share = collateralDict[collateralAndLoanDict[loanApplication.Id]].Share;

                int eligibilityResult = await CalculateLoanEligibility(collateralAndLoanDict[loanApplication.Id], loanApplication.Id);
                if (eligibilityResult == 0)
                {
                    advisorLoanApplicationDTO.Eligibility = "Red";
                }
                else if (eligibilityResult == 1)
                {
                    advisorLoanApplicationDTO.Eligibility = "Yellow";
                }
                else
                {
                    advisorLoanApplicationDTO.Eligibility = "Green";
                }
                listOfAdvisorLoanApplicationDTO.Add(advisorLoanApplicationDTO);

            }
            if (listOfAdvisorLoanApplicationDTO.Count() == 0)
            {
                throw new ArgumentException($"There are no applied loan application");
            }
            return listOfAdvisorLoanApplicationDTO;
        }



        public async Task<IEnumerable<UserLoanApplicationDTO>> FetchAllLoanApplicationByAdvisorTask()
        {
            IEnumerable<LoanApplication> allLoanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetAllRecordsTask();
            if (allLoanApplicationFromDAL == null)
            {
                throw new ArgumentException($"There are no Applied loan application");
            }
            IList<UserLoanApplicationDTO> listOfAdvisorLoanApplicationDTO = new List<UserLoanApplicationDTO>();

            IEnumerable<CollateralAndLoanApplication> collateralAndLoanApplicationFromDAL = await _dataAccessLayer.Read().CollateralAndLoanApplicationRead().GetAllRecordsTask();
            IEnumerable<Collateral> collateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetAllRecordsTask();
            IEnumerable<LoanRequirements> loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetAllRecordsTask();
            IEnumerable<PersonalIncome> personalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetAllRecordsTask();
            IEnumerable<Property> propertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetAllRecordsTask();
            IEnumerable<User> userFromDAL = await _dataAccessLayer.Read().UserRead().GetAllRecordsTask();

            Dictionary<Guid, Collateral> collateralDict = new Dictionary<Guid, Collateral>();
            Dictionary<Guid, LoanRequirements> loanRequirementsDict = new Dictionary<Guid, LoanRequirements>();
            Dictionary<Guid, PersonalIncome> personalIncomeDict = new Dictionary<Guid, PersonalIncome>();
            Dictionary<Guid, Property> propertyDict = new Dictionary<Guid, Property>();
            Dictionary<Guid, Guid> collateralAndLoanDict = new Dictionary<Guid, Guid>();
            Dictionary<Guid, User> userDict = new Dictionary<Guid, User>();

            foreach (LoanRequirements loanRequirements in loanRequirementsFromDAL)
            {
                loanRequirementsDict.Add(loanRequirements.Id, loanRequirements);
            }
            foreach (CollateralAndLoanApplication collateralAndLoan in collateralAndLoanApplicationFromDAL)
            {
                collateralAndLoanDict.Add(collateralAndLoan.LoanApplicationId, collateralAndLoan.CollateralId);
            }
            foreach (PersonalIncome personalIncome in personalIncomeFromDAL)
            {
                personalIncomeDict.Add(personalIncome.Id, personalIncome);
            }
            foreach (Property property in propertyFromDAL)
            {
                propertyDict.Add(property.Id, property);
            }
            foreach (Collateral collateral in collateralFromDAL)
            {
                collateralDict.Add(collateral.Id, collateral);
            }
            foreach (User user in userFromDAL)
            {
                userDict.Add(user.Id, user);
            }
            foreach (LoanApplication loanApplication in allLoanApplicationFromDAL)
            {
                UserLoanApplicationDTO advisorLoanApplicationDTO = new UserLoanApplicationDTO();
                advisorLoanApplicationDTO.LoanDuration = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanDuration;
                advisorLoanApplicationDTO.LoanAmount = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanAmount;
                advisorLoanApplicationDTO.LoanStartDate = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanStartDate;

                advisorLoanApplicationDTO.MonthlyFamilyIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;
                advisorLoanApplicationDTO.OtherIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;

                advisorLoanApplicationDTO.Address = propertyDict[loanApplication.PropertyId].Address;
                advisorLoanApplicationDTO.Size = propertyDict[loanApplication.PropertyId].Size;
                advisorLoanApplicationDTO.Cost = propertyDict[loanApplication.PropertyId].Cost;
                advisorLoanApplicationDTO.RegistrationCost = propertyDict[loanApplication.PropertyId].RegistrationCost;
                advisorLoanApplicationDTO.Id = loanApplication.Id;
                advisorLoanApplicationDTO.EmailId = userDict[loanApplication.UserId].EmailId;
                advisorLoanApplicationDTO.Status = loanApplication.Status.ToString();
                
                listOfAdvisorLoanApplicationDTO.Add(advisorLoanApplicationDTO);

            }
            if (listOfAdvisorLoanApplicationDTO.Count() == 0)
            {
                throw new ArgumentException($"There are no applied loan application");
            }
            return listOfAdvisorLoanApplicationDTO;
        }


        #region FetchAnAppliedLoanApplicationByAdvisorTask
        /// <summary>
        /// This method fetches a loan application by its ID and retrieves relevant information about the application and return a status code.
        /// </summary>
        /// <param name="loanApplicationId"></param>
        /// <returns>fetch a Loan Application</returns>
        public async Task<AdvisorLoanApplicationDTO> FetchAnAppliedLoanApplicationByAdvisorTask(Guid loanApplicationId)
        {
            CollateralAndLoanApplication collateralAndLoanApplicationFromDAL = await _dataAccessLayer.Read().CollateralAndLoanApplicationRead().GetByConditionTask((x) => x.LoanApplicationId == loanApplicationId);
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == loanApplicationId);
            if (loanApplicationFromDAL.Status != LoanApplicationStatus.Applied)
            {
                throw new ArgumentException("Given application is not applied");
            }
            Collateral collateralFromDAL = await _dataAccessLayer.Read().CollateralRead().GetByConditionTask((x) => x.Id == collateralAndLoanApplicationFromDAL.CollateralId);
            LoanRequirements loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.LoanRequirementsId);
            PersonalIncome personalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.PersonalIncomeId);
            Property propertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.PropertyId);
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.UserId);

            AdvisorLoanApplicationDTO advisorLoanApplicationDTO = new AdvisorLoanApplicationDTO();
            advisorLoanApplicationDTO.Id = loanApplicationId;
            advisorLoanApplicationDTO.LoanDuration = loanRequirementsFromDAL.LoanDuration;
            advisorLoanApplicationDTO.LoanAmount = loanRequirementsFromDAL.LoanAmount;
            advisorLoanApplicationDTO.LoanStartDate = loanRequirementsFromDAL.LoanStartDate;

            advisorLoanApplicationDTO.MonthlyFamilyIncome = personalIncomeFromDAL.MonthlyFamilyIncome;
            advisorLoanApplicationDTO.OtherIncome = personalIncomeFromDAL.OtherIncome;

            advisorLoanApplicationDTO.Address = propertyFromDAL.Address;
            advisorLoanApplicationDTO.Size = propertyFromDAL.Size;
            advisorLoanApplicationDTO.Cost = propertyFromDAL.Cost;
            advisorLoanApplicationDTO.RegistrationCost = propertyFromDAL.RegistrationCost;

            advisorLoanApplicationDTO.EmailId = userFromDAL.EmailId;
            
            advisorLoanApplicationDTO.Type = collateralFromDAL.Type.ToString();
            advisorLoanApplicationDTO.Value = collateralFromDAL.Value;
            advisorLoanApplicationDTO.Share = collateralFromDAL.Share;
            advisorLoanApplicationDTO.Status = loanApplicationFromDAL.Status.ToString();

            int eligibilityResult = await CalculateLoanEligibility(collateralFromDAL.Id, loanApplicationFromDAL.Id);
            if (eligibilityResult == 0)
            {
                advisorLoanApplicationDTO.Eligibility = "Red";
            }
            else if (eligibilityResult == 1)
            {
                advisorLoanApplicationDTO.Eligibility = "Yellow";
            }
            else
            {
                advisorLoanApplicationDTO.Eligibility = "Green";
            }
            return advisorLoanApplicationDTO;
        }
        #endregion

        public async Task<UserLoanApplicationDTO> FetchAnLoanApplicationByAdvisorTask(Guid loanApplicationId)
        {
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == loanApplicationId);

            LoanRequirements loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.LoanRequirementsId);
            PersonalIncome personalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.PersonalIncomeId);
            Property propertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.PropertyId);
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == loanApplicationFromDAL.UserId);

            UserLoanApplicationDTO userLoanApplicationDTO = new UserLoanApplicationDTO();
            userLoanApplicationDTO.LoanDuration = loanRequirementsFromDAL.LoanDuration;
            userLoanApplicationDTO.LoanAmount = loanRequirementsFromDAL.LoanAmount;
            userLoanApplicationDTO.LoanStartDate = loanRequirementsFromDAL.LoanStartDate;

            userLoanApplicationDTO.MonthlyFamilyIncome = personalIncomeFromDAL.MonthlyFamilyIncome;
            userLoanApplicationDTO.OtherIncome = personalIncomeFromDAL.OtherIncome;

            userLoanApplicationDTO.Address = propertyFromDAL.Address;
            userLoanApplicationDTO.Size = propertyFromDAL.Size;
            userLoanApplicationDTO.Cost = propertyFromDAL.Cost;
            userLoanApplicationDTO.RegistrationCost = propertyFromDAL.RegistrationCost;

            userLoanApplicationDTO.EmailId = userFromDAL.EmailId;
            userLoanApplicationDTO.Status = loanApplicationFromDAL.Status.ToString();
            userLoanApplicationDTO.Id = loanApplicationId;
            return userLoanApplicationDTO;
        }


        #region FetchAllLoanApplicationByUser
        /// <summary>
        /// Fetches all loan applications of a user identified by their email ID.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>IEnumerable<UserLoanApplicationDTO> containing information about each loan application of the user</returns>
        public async Task<IEnumerable<UserLoanApplicationDTO>> FetchAllLoanApplicationByUser(string emailId)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("User email Id does not exist");
            }
            IEnumerable<LoanApplication> listOfUserLoanApplication = await _dataAccessLayer.Read().LoanApplicationRead().GetAllRecordsByConditionTask(x => (x.UserId == userFromDAL.Id));

            if (listOfUserLoanApplication == null)
            {
                throw new ArgumentException("User has not applied for any loan application");
            }

            IEnumerable<LoanRequirements> listOfLoanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetAllRecordsTask();
            IEnumerable<PersonalIncome> listOfPersonalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetAllRecordsTask();
            IEnumerable<Property> listOfPropertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetAllRecordsTask();

            Dictionary<Guid, LoanRequirements> loanRequirementsDict = new Dictionary<Guid, LoanRequirements>();
            Dictionary<Guid, PersonalIncome> personalIncomeDict = new Dictionary<Guid, PersonalIncome>();
            Dictionary<Guid, Property> propertyDict = new Dictionary<Guid, Property>();

            foreach (LoanRequirements loanRequirements in listOfLoanRequirementsFromDAL)
            {
                loanRequirementsDict.Add(loanRequirements.Id, loanRequirements);
            }
            foreach (PersonalIncome personalIncome in listOfPersonalIncomeFromDAL)
            {
                personalIncomeDict.Add(personalIncome.Id, personalIncome);
            }
            foreach (Property property in listOfPropertyFromDAL)
            {
                propertyDict.Add(property.Id, property);
            }
            IList<UserLoanApplicationDTO> listOfUserLoanApplicationDTO = new List<UserLoanApplicationDTO>();
            foreach (LoanApplication loanApplication in listOfUserLoanApplication)
            {
                UserLoanApplicationDTO userApplicationDTO = new UserLoanApplicationDTO();
                userApplicationDTO.Id = loanApplication.Id;
                userApplicationDTO.LoanDuration = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanDuration;
                userApplicationDTO.LoanAmount = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanAmount;
                userApplicationDTO.LoanStartDate = loanRequirementsDict[loanApplication.LoanRequirementsId].LoanStartDate;

                userApplicationDTO.MonthlyFamilyIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;
                userApplicationDTO.OtherIncome = personalIncomeDict[loanApplication.PersonalIncomeId].MonthlyFamilyIncome;

                userApplicationDTO.Address = propertyDict[loanApplication.PropertyId].Address;
                userApplicationDTO.Size = propertyDict[loanApplication.PropertyId].Size;
                userApplicationDTO.Cost = propertyDict[loanApplication.PropertyId].Cost;
                userApplicationDTO.RegistrationCost = propertyDict[loanApplication.PropertyId].RegistrationCost;
                userApplicationDTO.EmailId = emailId;
                userApplicationDTO.Status = loanApplication.Status.ToString();
                userApplicationDTO.Notes = loanApplication.Notes;
                
                listOfUserLoanApplicationDTO.Add(userApplicationDTO);
            }
            return listOfUserLoanApplicationDTO;
        }
        #endregion

        #region ChangeLoanApplicationStatusByAdvisorTask
        /// <summary>
        /// This method Changes the status of a loan application by an advisor.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="status"></param>
        /// <returns>The ID of the updated loan application.</returns>
        public async Task<Guid> ChangeLoanApplicationStatusByAdvisorTask(Guid applicationId, LoanApplicationStatus status, string? notes)
        {
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == applicationId);
            if (loanApplicationFromDAL == null)
            {

                throw new ArgumentException("Loan Application Not Found");
            }
            if (loanApplicationFromDAL.Status == LoanApplicationStatus.Applied || loanApplicationFromDAL.Status==LoanApplicationStatus.Accepted
                ||loanApplicationFromDAL.Status==LoanApplicationStatus.InProgress
                ||loanApplicationFromDAL.Status==LoanApplicationStatus.Rejected)
            {
                loanApplicationFromDAL.Status = status;
                loanApplicationFromDAL.Notes = notes;
                bool response = await _dataAccessLayer.Write().LoanApplicationWrite().EditTask(loanApplicationFromDAL);
                response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                if (!response)
                {
                    throw new Exception("Loan Status Cannot be Changed");
                }
                return loanApplicationFromDAL.Id;
            }
            else
            {
                throw new ArgumentException("Loan Status Cannot be Changed");
            }


        }
        #endregion

        #region ChangeLoanApplicationStatusByUserTask
        /// <summary>
        /// This method Changes the status of a loan application identified by applicationId to "Applied".
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> ChangeLoanApplicationStatusByUserTask(string emailId, Guid applicationId)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("User email Id does not exist");
            }
            LoanApplication loanApplicationFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == applicationId);
            if (loanApplicationFromDAL == null)
            {
                throw new ArgumentException("Loan application does not exist");
            }
            if (loanApplicationFromDAL.UserId != userFromDAL.Id)
            {
                throw new AccessViolationException("Current user is not authorized to access this resource");
            }
            CollateralAndLoanApplication collateralAndLoan = await _dataAccessLayer.Read().CollateralAndLoanApplicationRead().GetByConditionTask(x => (x.LoanApplicationId == applicationId));
            if (collateralAndLoan == null)
            {
                throw new ArgumentException("Application Id Doesn't have a Collateral ");
            }
            LoanApplication loanApplication = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask(x => x.Id == applicationId);
            loanApplication.Status = Enums.LoanApplicationStatus.Applied;
            bool response = await _dataAccessLayer.Write().LoanApplicationWrite().EditTask(loanApplication);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return response;
        }
        #endregion

        #region EditLoanApplicationByUserTask
        /// <summary>
        /// This method edits the loan application .
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> A boolean value indicating if the operation was successful.</returns>
        public async Task<bool> EditLoanApplicationByUserTask(string emailId, EditLoanApplicationDTO entity)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.EmailId == emailId);
            if (userFromDAL == null)
            {
                throw new ArgumentException("User email Id does not exist");
            }
            LoanApplication loanApplictionFromDAL = await _dataAccessLayer.Read().LoanApplicationRead().GetByConditionTask((x) => x.Id == entity.Id);
            if (loanApplictionFromDAL == null)
            {
                throw new ArgumentException("No loan application found for the given Id");
            }
            if (loanApplictionFromDAL.UserId != userFromDAL.Id)
            {
                throw new AccessViolationException("Current user is not authorized to access this resource");
            }
            if (loanApplictionFromDAL.Status != LoanApplicationStatus.Created)
            {
                throw new ArgumentException("Applied Loan application cannot be edited");
            }

            LoanRequirements loanRequirementsFromDAL = await _dataAccessLayer.Read().LoanRequirementsRead().GetByConditionTask((x) => x.Id == loanApplictionFromDAL.LoanRequirementsId);
            PersonalIncome personalIncomeFromDAL = await _dataAccessLayer.Read().PersonalIncomeRead().GetByConditionTask((x) => x.Id == loanApplictionFromDAL.PersonalIncomeId);
            Property propertyFromDAL = await _dataAccessLayer.Read().PropertyRead().GetByConditionTask((x) => x.Id == loanApplictionFromDAL.PropertyId);

            loanRequirementsFromDAL.LoanDuration = entity.LoanDuration;
            loanRequirementsFromDAL.LoanStartDate = entity.LoanStartDate;
            loanRequirementsFromDAL.LoanAmount = entity.LoanAmount;

            personalIncomeFromDAL.OtherIncome = entity.OtherIncome;
            personalIncomeFromDAL.MonthlyFamilyIncome = entity.MonthlyFamilyIncome;

            propertyFromDAL.Address = entity.Address;
            propertyFromDAL.RegistrationCost = entity.RegistrationCost;
            propertyFromDAL.Cost = entity.Cost;
            propertyFromDAL.Size = entity.Size;

            bool response = await _dataAccessLayer.Write().LoanRequirementsWrite().EditTask(loanRequirementsFromDAL);
            response &= await _dataAccessLayer.Write().PersonalIncomeWrite().EditTask(personalIncomeFromDAL);
            response &= await _dataAccessLayer.Write().PropertyWrite().EditTask(propertyFromDAL);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return response;
        }
        #endregion

    }
}


