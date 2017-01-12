using System;

namespace Valeo.Domain
{
    /// <summary>
    /// 接收人信息实体
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_Recipient")]
    [PetaPoco.PrimaryKey("RecipientID")]
    public class RecipientModel
    {
        /// <summary>
        /// 接收者编号
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// 读取标识
        /// </summary>
        public int IsRead { get; set; }

    
    }
}
