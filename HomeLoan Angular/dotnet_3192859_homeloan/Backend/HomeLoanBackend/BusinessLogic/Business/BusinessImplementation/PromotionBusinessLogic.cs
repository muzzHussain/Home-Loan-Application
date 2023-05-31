using AutoMapper;
using BusinessLogic.Business.BusinessInterface;
using BusinessLogic.DTO;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Threading.Tasks;
using static Utility.Enums;

namespace BusinessLogic.Business.BusinessImplementation
{
    /// <summary>
    /// This Business Logic will help to Add Promotions.
    /// </summary>
    internal class PromotionBusinessLogic : IPromotionBusinessLogic<PromotionsDTO, EditPromotionDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        public PromotionBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
        }

        #region AddNewPromotionByAdvisorTask
        /// <summary>
        /// This method adds a new promotion and closes any active promotions..
        /// </summary>
        /// <param name="newPromotion"></param>
        /// <returns>return promotion added by Advisor</returns>
        public async Task<Guid> AddNewPromotionByAdvisorTask(PromotionsDTO newPromotion)
        {
            bool response;
            Promotions activeLatestPromotion = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Status == PromotionState.Open);
            if (activeLatestPromotion != null)
            {
                activeLatestPromotion.Status = PromotionState.Closed;
                response = await _dataAccessLayer.Write().PromotionsWrite().EditTask(activeLatestPromotion);
            }
            if (newPromotion.StartDate.Date > newPromotion.EndDate.Date)
            {
                throw new ArgumentException("Start date should be smaller or equal to end date");
            }
            Promotions promotions = _mapper.Map<Promotions>(newPromotion);
            promotions.Id = new Guid();
            response = await _dataAccessLayer.Write().PromotionsWrite().AddTask(promotions);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            //return _mapper.Map<PromotionsDTO>(promotions);
            return promotions.Id;
        }
        #endregion

        #region RemovePromotionByAdvisorTask
        /// <summary>
        ///     This method adds a new promotion and closes any active promotions..
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>return promotion added by Advisor</returns>
        public async Task<bool> RemovePromotionByAdvisorTask(Guid Id)
        {
            Promotions promotions = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Id == Id);
            if (promotions == null)
            {
                throw new ArgumentException("Promotions for given id is not exist");
            }
            bool response = await _dataAccessLayer.Write().PromotionsWrite().RemoveTask(promotions);
            response &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return response;
        }
        #endregion

        #region EditPromotionByAdvisorTask
        /// <summary>
        ///     This method adds a new promotion and closes any active promotions..
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return promotion added by Advisor</returns>
        public async Task<Guid> EditPromotionByAdvisorTask(EditPromotionDTO entity)
        {
            Promotions promotionFromDAL = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Id == entity.Id);
            if (promotionFromDAL == null)
            {
                throw new ArgumentException("Promotion doesn't exist");
            }
            Promotions promotions = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Id == entity.Id);
            promotions.StartDate = entity.StartDate;
            promotions.EndDate = entity.EndDate;
            promotions.Type = (PromotionType)Enum.Parse(typeof(PromotionType), entity.Type);
            promotions.Message = entity.Message;
            bool respone = await _dataAccessLayer.Write().PromotionsWrite().EditTask(promotions);
            respone &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            if (!respone)
            {
                throw new Exception("Collateral is not edited");
            }
            return promotions.Id;
        }
        #endregion

        public async Task<PromotionsDTO> GetOpenPromotionByAdvisorTask()
        {
            Promotions promotions = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Status == 0);
            if (promotions == null)
            {
                throw new ArgumentException("There is no active promotion exist");
            }
            Promotions promotionFromDAL = await _dataAccessLayer.Read().PromotionsRead().GetByConditionTask((x) => x.Status == promotions.Status);
            PromotionsDTO promotionDTO = _mapper.Map<PromotionsDTO>(promotionFromDAL);
            
            return promotionDTO;
        }
    }
}
