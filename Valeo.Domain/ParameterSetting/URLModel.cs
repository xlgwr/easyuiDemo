using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 采集网址查看
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("m_URL")]
    [PetaPoco.PrimaryKey("URLid", autoIncrement = true)]
    public class URLModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long URLid { get; set; }

        /// <summary>
        /// 网址名称
        /// </summary>
        public string URLname { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
