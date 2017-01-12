using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.User
{
    public class UserSearchModel
    {

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public long UserGradeID { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string FullName_En { get; set; }

        /// <summary>
        /// 中文名字
        /// </summary>
        public string FullName_Cn { get; set; }
        /// <summary>
        /// 繁体名字
        /// </summary>
        public string FullName_Tm { get; set; }

        //ming xi 
        public List<UserModel> Name { get; set; }
    }
}
