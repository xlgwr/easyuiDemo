using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.User
{
    [Serializable]
    [PetaPoco.TableName("m_User")]
    [PetaPoco.PrimaryKey("UserID",autoIncrement=false)]
    public class UserModel
    {
        /// <summary>
        /// 主建
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public long UserGradeID { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
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
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }
        
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IDDocumentType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDDocumentNo { get; set; }
        /// <summary>
        /// 状态 默认1可用，0不可用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

 

    }
}
