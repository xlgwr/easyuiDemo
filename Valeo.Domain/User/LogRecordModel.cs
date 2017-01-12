using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    [Serializable]
    [PetaPoco.TableName("t_LogRecord")]
    [PetaPoco.PrimaryKey("LogID", autoIncrement = true)]
    public class LogRecordModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long LogID { get; set; }

        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public long MemberID { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 界面id
        /// </summary>
        public string Form { get; set; }
        /// <summary>
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        public int OperateType { get; set; }
        
        /// <summary>
        /// 日志说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime AddDateTime { get; set; }



 

    }
}
