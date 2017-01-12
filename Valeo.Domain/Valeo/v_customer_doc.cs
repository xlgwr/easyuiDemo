using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Valeo
{
    /// <summary>
    /// 客户文档
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("v_customer_doc")]
    [PetaPoco.PrimaryKey("docID", autoIncrement = true)]
    public class v_customer_doc : vBase
    {
        #region Model
        /// <summary>
        /// key
        /// </summary>
        public double docID
        {
            set;
            get;
        }
        /// <summary>
        /// key
        /// </summary>
        public string customerNo
        {
            set;
            get;
        }
        /// <summary>
        /// 文档名称
        /// </summary>
        public string docName
        {
            set;
            get;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int sortNo
        {
            set;
            get;
        }
        #endregion Model
    }
}
