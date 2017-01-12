using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 系统参数设定
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("m_Parameter")]
    [PetaPoco.PrimaryKey("Paramkey", autoIncrement = false)]
    public class SysParameterModel
    {
        /// <summary>
        /// 主建 参数KEY(自编)
        /// </summary>
        public string Paramkey { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Paramvalue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 参数类型 0：系统参数 1:用户参数
        /// </summary>
        public int Paramtype { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DspNo { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }


        /// <summary>
        /// 更新人
        /// </summary>
        public string upduser { get; set; }


        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updtime { get; set; }
    }
}
