
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Loan")]
    [PetaPoco.PrimaryKey("LoanID", autoIncrement = true)]
    public partial class LoanModel
    {
        #region 实体属性

        /// <summary>
        /// 
        /// </summary>
        public virtual long LoanID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual int LenderType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LenderNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Borrower_En { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Borrower_Cn { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string LoanNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoanType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Currencies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoanPrincipal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicatinAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApprovalDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrincipalOS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalOS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastPaidDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int OverdueDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias_En { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias_Cn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Birthdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Marital { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address_En { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address_Cn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LenghtTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelType6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelNo6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Employer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServicesTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecuredProperty1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecuredProperty2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MortgagedSecurity1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StockCode1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareAmount1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalValue1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MortgagedSecurity2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StockCode2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareAmount2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalValue2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MortgagedSecurity3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StockCode3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareAmount3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalValue3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MortgagedSecurity4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StockCode4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareAmount4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalValue4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MortgagedSecurity5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StockCode5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareAmount5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalValue5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareTotalValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        static int _MarginFinancingPCheck = 0;
        /// <summary>
        /// 
        /// </summary>
        public string MarginFinancingPValue { get; set; }
        public virtual int MarginFinancingPCheck
        {
            get
            {
                return _MarginFinancingPCheck;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(MarginFinancingPValue))
                {
                    _MarginFinancingPCheck = 1;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public string MarginFinancingTValue { get; set; }
        int _MarginFinancingTCheck = 0;
        /// <summary>
        /// 
        /// </summary>
        public virtual int MarginFinancingTCheck {

            get
            {
                return _MarginFinancingTCheck;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(MarginFinancingTValue))
                {
                    _MarginFinancingTCheck = 1;
                }
            }
        
        }



        /// <summary>
        /// 
        /// </summary>
        public string QuotaValue { get; set; }
        int _QuotaCheck = 0;
        /// <summary>
        /// 
        /// </summary>
        public virtual int QuotaCheck {
            get
            {
                return _QuotaCheck;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(QuotaValue))
                {
                    _QuotaCheck = 1;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string PurchasePowerValue { get; set; }
        int _PurchasePowerCheck = 0;
        /// <summary>
        /// 
        /// </summary>
        public virtual int PurchasePowerCheck
        {
            get
            {
                return _PurchasePowerCheck;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(PurchasePowerValue))
                {
                    _PurchasePowerCheck = 1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int T2Check { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CashCheck { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int NettingCheck { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Show { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int DataGradeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? AddDatetime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpdUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? UpdDatetime { get; set; }





        #endregion



    }
}
