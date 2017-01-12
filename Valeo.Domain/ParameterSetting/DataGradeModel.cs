using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    [Serializable]
    [PetaPoco.TableName("m_DataGrade")]
    [PetaPoco.PrimaryKey("DataGradeID", autoIncrement = true)]
    public class DataGradeModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long DataGradeID { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string DataGrade { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
    }



    public class DataGradeModelBing
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long DataGradeID { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string DataGrade { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
