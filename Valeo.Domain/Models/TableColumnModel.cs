using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    public class TableColumnModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public int IsNullable { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int ColumnLength { get; set; }
    }
}
