
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("m_CaseType")]
    [PetaPoco.PrimaryKey("CaseTypeID",autoIncrement=true)]
    public class CaseTypeModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long CaseTypeID { get; set; }

        /// <summary>
        /// 案件编码
        /// </summary>
        public virtual string CaseType { get; set; }
        /// <summary>
        /// 案件类别:民事上訴/傷亡訴訟/民事訴訟/僱員補償案件/刑事案件/破產案件/雜項案件/小額錢債審裁處上訴/建築及仲裁訴訟/裁判法院上訴
        /// </summary>
        public virtual string CaseType_En { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public virtual string CaseType_Cn { get; set; }

        /// <summary>
        /// 法院ID
        /// </summary>
        public virtual long CourtID { get; set; }

        /// <summary>
        /// 法院类型
        /// </summary>
        public virtual long Tykpe { get; set; }
        #endregion

 

    }
}
 