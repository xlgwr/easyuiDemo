using Valeo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class BankAccountVM 
    {

        #region 实体属性

        /// <summary>
        /// 
        /// </summary>
        public virtual long BankAccountID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Bank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AccountNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? DateOpened { get; set; }





        #endregion

        /// <summary>
        /// 修改日期
        /// </summary>
        public virtual string DateOpenedString
        {
            get
            {
                var dateTime = DateOpened;
                return dateTime != null ? dateTime.Value.ToString("yyyy-MM-dd") : "";
            }
            set
            {
                try
                {
                    if (value != null)
                        DateOpened = Convert.ToDateTime(value);
                }
                catch
                {
                    // ignored
                }
              
            }
        }
    }
}
