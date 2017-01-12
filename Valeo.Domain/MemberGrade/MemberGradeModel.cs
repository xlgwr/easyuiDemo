
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.MemberGrade
{
    [Serializable]
    [PetaPoco.TableName("m_MemberGrade")]
    [PetaPoco.PrimaryKey("MemberGradeId",autoIncrement=false)]
    public class MemberGradeModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual long MemberGradeID { get; set; }
        
        
        /// <summary>
        /// 会员级别名称（A）
        /// </summary>
        public virtual string MemberGrade { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 添加用户
        /// </summary>
        public virtual string Adduser { get; set; }
        
        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual string Addtime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public virtual string Upduser { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual string Updtime { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 