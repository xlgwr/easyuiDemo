using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 繁简体设定
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("m_Word")]
    [PetaPoco.PrimaryKey("WordId", autoIncrement = true)]
    public class WordModel
    {
        /// <summary>
        /// 主建 参数KEY(自编)
        /// </summary>
        public long WordId { get; set; }

        /// <summary>
        /// 类别(1:简体-拼音 2:简体-繁体 3:繁体-拼音 4:简体-字码 5-繁体-字码)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 参数Key
        /// </summary>
        public string WordKey { get; set; }
        /// <summary>
        /// 参数Value
        /// </summary>
        public string WordValue { get; set; }

    }
}
