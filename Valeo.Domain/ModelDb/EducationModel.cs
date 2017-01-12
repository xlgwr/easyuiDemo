
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_Education")]
    [PetaPoco.PrimaryKey("Educationid",autoIncrement=true)]
    public class EducationModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Educationid { get; set; }
        
        
        /// <summary>
        /// 个人编号
        /// </summary>
        public virtual long Entityid { get; set; }
        
        /// <summary>
        /// 学校
        /// </summary>
        public virtual string Institute { get; set; }
        
        /// <summary>
        /// 学校类型
        /// </summary>
        public virtual string InstituteType { get; set; }
        
        /// <summary>
        /// 学位
        /// </summary>
        public virtual string Degree { get; set; }
        
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime? FromDate { get; set; }

        
        
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? ToDate { get; set; }

       
        
     
        
        #endregion

 

    }
}
 