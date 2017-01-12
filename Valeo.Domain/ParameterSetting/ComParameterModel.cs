using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.ParameterSetting
{
    [Serializable]
    [PetaPoco.TableName("m_ComParameter")]
    [PetaPoco.PrimaryKey("Comkey", autoIncrement = false)]
    public class ComParameterModel
    {
        /// <summary>
        /// 参数key
        /// </summary>
        public string Comkey { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Comvalue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public long Comtype { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public long DspNo { get; set; }
    }
}
