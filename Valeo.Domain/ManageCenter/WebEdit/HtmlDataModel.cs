using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ManageCenter.WebEdit
{
    [Serializable]
    [PetaPoco.TableName("m_HtmlData")]
    [PetaPoco.PrimaryKey("RecdId")]
    public class HtmlDataModel
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long RecdId { get; set; }

        /// <summary>
        /// 语言Key
        /// </summary>
        public string LangKey { get; set; }

        /// <summary>
        /// 界面id
        /// </summary>
        public string FuncPrentId { get; set; }

        /// <summary>
        /// html内容
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string UpdUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdTime { get; set; }
    }
}
