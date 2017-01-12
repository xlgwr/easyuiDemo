using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// y用户权限
    /// </summary>
    public class SysModuleVM
    {
        /// <summary>
        /// 画面ID
        /// </summary>
        public string Mod_id { get; set; }

        /// <summary>
        /// 画面名称
        /// </summary>
        public string Mod_nm { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        public int Search { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public int Create { get; set; }

        /// <summary>
        /// 修改
        /// </summary>
        public int Edit { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public int Delete { get; set; }

        /// <summary>
        /// 审核
        /// </summary>
        public int Check { get; set; }

        /// <summary>
        /// 导入
        /// </summary>
        public int Import { get; set; }

        /// <summary>
        /// 导出
        /// </summary>
        public int Export { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

    }

    /// <summary>
    /// 功能信息表
    /// </summary>
    public class SysModule
    {
        public string Mod_id { get; set; }
        public string Mod_nm { get; set; }
        public int Status { get; set; }

    }

    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserAuthority
    {
        public long UserGradeID { get; set; }
        public string Mod_id { get; set; }
        public int Opr_code { get; set; }
    }

    /// <summary>
    /// 功能操作明细表
    /// 操作代码Opr_code 0:查询 1:新增 2:修改 3:删除 4:查询 5：审核 6:导入 7:导出
    /// </summary>
    public class SysModuleDetail 
    {
        public string Mod_id { get; set; }
        public int Opr_code { get; set; }
        /// <summary>
        /// check: 0,1
        /// </summary>
        public int Status { get; set; }

    }
}
