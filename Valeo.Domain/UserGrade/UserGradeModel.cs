using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.UserGrade
{
    [Serializable]
    [PetaPoco.TableName("m_UserGrade")]
    [PetaPoco.PrimaryKey("UserGradeID")]
    public class UserGradeModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public long UserGradeID { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string UserGrade { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 添加用户
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string UpdUser { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdTime { get; set; }



    }

    public class UserGradeModelVM : UserGradeModel
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTimeVM { get { return AddTime.ToString("yyyy-MM-dd HH:ss"); } }
    }
}
