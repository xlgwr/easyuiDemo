using Valeo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class UserInfo
    {
        /// <summary>
        /// 用户id(自编,用于登录)
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public string UserGradeID { get; set; }

        /// <summary>
        /// 密码
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

        /// <summary>
        /// 公司邮箱
        /// </summary> 
        public SetEmailVM SetEmailVM = new SetEmailVM();

        /// <summary>
        /// 权限信息
        /// </summary>
        public List<UserAuthVM> ListUserAuthVM = new List<UserAuthVM>();
 
        /// <summary>
        /// 缩写信息（名称）
        /// </summary>
        public List<ShortNameVM> ListShortNameVM = new List<ShortNameVM>();

        /// <summary>
        /// 缩写信息（地址）
        /// </summary>
        public List<ShortNameVM> ListShortAddrVM = new List<ShortNameVM>();

        /// <summary>
        /// 文字转换（1:简体-拼音）
        /// </summary>
        public List<WordVM> ListWord1VM = new List<WordVM>();
    
        /// <summary>
        /// 文字转换（2:简体-繁）
        /// </summary>
        public List<WordVM> ListWord2VM = new List<WordVM>();

        /// <summary>
        /// 文字转换（3:繁-拼音）
        /// </summary>
        public List<WordVM> ListWord3VM = new List<WordVM>();

        /// <summary>
        /// 文字转换（1 3:汉字-拼音）
        /// </summary>
        public List<WordVM> ListWord4VM = new List<WordVM>();
    }
}
