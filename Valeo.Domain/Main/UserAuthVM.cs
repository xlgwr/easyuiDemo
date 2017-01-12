using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class UserAuthVM
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public string Mod_id { get; set; }
 
        /// <summary>
        /// 操作代码 0:查询 1:新增 2:修改 3:删除  4：审核 5:导入 6:导出
        /// </summary>
        public int Opr_code { get; set; }
 
    }
}
