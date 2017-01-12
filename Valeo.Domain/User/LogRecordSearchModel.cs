using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.User
{
    public class LogRecordSearchModel
    {
        public long LogID { get; set; }
        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserID { get; set; }
        public string UserName { get; set; }

        /// <summary>
        /// 用户级别ID
        /// </summary>
        public int UserGradeID { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public string UserGrade { get; set; }

        /// <summary>
        /// 访问页面
        /// </summary>
        public string Form { get; set; }
        public string FormVM { get; set; }


        public string AddDateTime { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public string AddDateTimeB { get; set; }
        public string AddDateTimeE { get; set; }


        /// <summary>
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        public int OperateType { get; set; }
        public string OperateTypeDesc { get; set; }

        /// <summary>
        /// 日志说明
        /// </summary>
        public string Content { get; set; }
    }
}
