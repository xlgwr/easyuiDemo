using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_Disqualification")]
    [PetaPoco.PrimaryKey("RecordID", autoIncrement = true)]
    public class DisqualificationModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long RecordID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual string CRNo { get; set; }

        /// <summary>
        /// 0:英文 1:简体 2:繁体
        /// </summary>
        public virtual long Language { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string ItemNo { get; set; }

        /// <summary>
        /// 被取消资格人士姓名/法团名称
        /// </summary>
        public virtual string CorporateName { get; set; }

        /// <summary>
        /// 中文姓名/名称
        /// </summary>
        public virtual string ChineseName { get; set; }

        /// <summary>
        /// 香港身份证号码/公司编号
        /// </summary>
        public virtual string IDCard { get; set; }

        /// <summary>
        /// 海外护照号码
        /// </summary>
        public virtual string OverseasPassportID { get; set; }

        /// <summary>
        /// 护照签发国家
        /// </summary>
        public virtual string PassportCountry { get; set; }

        /// <summary>
        /// 相同项目编号
        /// </summary>
        public virtual string SameNo { get; set; }

        /// <summary>
        /// 网页源码id
        /// </summary>
        public virtual long HtmlPageID { get; set; }
       
    }
}
