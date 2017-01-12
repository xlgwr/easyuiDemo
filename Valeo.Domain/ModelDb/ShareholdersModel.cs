using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_Shareholders")]
    [PetaPoco.PrimaryKey("Shareholderid", autoIncrement = true)]
    public class ShareholdersModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Shareholderid { get; set; }

        /// <summary>
        /// Entityid_C公司id
        /// </summary>
        public virtual long Entityid_C { get; set; }

        /// <summary>
        /// Entityid董事id
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 现时持有量
        /// </summary>
        public virtual string CurrentHolding { get; set; }

        /// <summary>
        /// 转让数目
        /// </summary>
        public virtual string TransferredNumber { get; set; }

        /// <summary>
        /// 转让日期
        /// </summary>
        public virtual string TransferredDate { get; set; }

        /// <summary>
        /// 持股百分比
        /// </summary>
        public virtual string Percentage { get; set; }

        /// <summary>
        /// 成为股东日期
        /// </summary>
        public virtual string StartDate { get; set; }

        /// <summary>
        /// 退出股东日期
        /// </summary>
        public virtual string EndDate { get; set; }

        /// <summary>
        /// 备注(变更内容等)
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
