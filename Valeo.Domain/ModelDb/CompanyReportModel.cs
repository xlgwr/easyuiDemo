using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ModelDb
{
    [Serializable]
    [PetaPoco.TableName("t_CompanyReport")]
    [PetaPoco.PrimaryKey("Reportid", autoIncrement = true)]
    public class CompanyReportModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long Reportid { get; set; }

        /// <summary>
        /// 公司Entityid
        /// </summary>
        public virtual long Entityid { get; set; }

        /// <summary>
        /// 报表类型(
        /// 1:周年申报表 
        /// 2:公司章程细则修改通知书 
        /// 3:公司宗旨修改通知书 
        /// 4:原有公司对某些章程细则修改通知书 
        /// 等等20来份,多语言处预留30个)
        /// </summary>
        public virtual long Type { get; set; }

        /// <summary>
        /// 报表名称
        /// </summary>
        public virtual string ReportName { get; set; }

        /// <summary>
        /// 原PDF报表文档路径
        /// </summary>
        public virtual string PathPDF { get; set; }

        /// <summary>
        /// 给客户excel报表文档路径
        /// </summary>
        public virtual string PathExcel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
