
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_HtmlPageCom")]
    [PetaPoco.PrimaryKey("HtmlID",autoIncrement=true)]
    public partial class HtmlPageComModel
    {
        #region 实体属性
        
        /// <summary>
        /// 
        /// </summary>
        public virtual long HtmlID { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int Language { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string HtmlURL { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string HtmlPage { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime addDate { get; set; }
        
        
        
     
        
        #endregion

 

    }
}
 