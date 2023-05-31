using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Enums
    {

        /// <summary>
        ///     This enum is for Loan Application Status
        /// </summary>
        public enum LoanApplicationStatus
        {
            Created,
            Applied,
            InProgress,
            Accepted,
            Rejected
        }

        /// <summary>
        ///     This enum is to define Promotion State
        /// </summary>
        public enum PromotionState
        {
            Open,
            Closed
        }

        /// <summary>
        ///     This enum is to define collateral type
        /// </summary>
        public enum CollateralType
        {
            Stock,
            Property,
            InsurancePolicy,
            Gold
        }

        /// <summary>
        ///     This enum is to define promotion type
        /// </summary>
        public enum PromotionType
        {
            A,
            B,
            C, 
            D
        }
    }
}
