using System;

namespace Valeo.Domain.Message
{
    /// <summary>
    /// 消息信息实体
    /// </summary>
    [Serializable]
    public class MessageVM
    {
        /// <summary>
        /// 接收者编号
        /// </summary>
        public string MemberID { get; set; }

        /// <summary>
        /// 接收者编号
        /// </summary>
        public string MemberName { get; set; }

      
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageID { get; set; }

        /// <summary>
        /// 发送者
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MessageTitle { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }

        public string SendTimeS { get; set; }

        public string SendTimeE { get; set; }
        /// <summary>
        /// 读取标识
        /// </summary>
        public int IsRead { get; set; }

        /// <summary>
        /// 0:系统（自动发） 1:打折(手工发) 
        /// </summary>
        public long Type { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string addtime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string upduser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string updtime { get; set; }
    }
}
