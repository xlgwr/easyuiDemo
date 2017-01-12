using Valeo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class IDDocumentsVM 
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long IDDocumentID { get; set; }


        /// <summary>
        /// 公司或人的编号
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 证件类别(0:身份证 1:护照)
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 国籍(Countryid)
        /// </summary>
        public virtual int Countryid { get; set; }

        /// <summary>
        /// 证件编号
        /// </summary>
        public virtual string Number { get; set; }

        /// <summary>
        /// 办证日期
        /// </summary>
        public virtual System.DateTime? CertificateDate { get; set; }


        /// <summary>
        /// 办证日期
        /// </summary>
        public virtual string CertificateDateString
        {
            get
            {
                var dateTime = CertificateDate;
                return dateTime != null ? dateTime.Value.ToString("yyyy-MM-dd") : "";
            }
            set
            {
                try
                {
                    if (value != null)
                        CertificateDate = Convert.ToDateTime(value);
                }
                catch
                {
                    // ignored
                }

            }
        }

        public virtual string ComboxListName { get; set; }
    }
}
