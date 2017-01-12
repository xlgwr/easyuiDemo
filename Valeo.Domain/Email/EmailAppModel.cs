using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    [TableName("t_EmailList")]
    [PrimaryKey("EmailID", autoIncrement = false)]
    [ExplicitColumns]
    public class EmailAppModel
    {
        public long MemberID { get; set; }

        [Column("EmailID")]
        //邮件唯一标识(自动递增)
        public string EmailID { get; set; }

          
        [Column("Subject")]
        //主题
        public string Subject { get; set; }

        [Column("FromMail")]
        //发送人
        public string FromMail { get; set; }

         
        [Column("ToMail")]
        //收件人地址（可以是多个收件人，程序中是以“;"进行区分的）
        public string ToMail { get; set; }

        //内容
        [Column("EmailBody")]
        public string EmailBody { get; set; }

        [Column("ReceiveTime")]
        //发送时间
        public string ReceiveTime { get; set; }

        [Column("AttachPath")]
        //附件路径 本地物理路径， 可添加多个用符号';' 隔开
        public string AttachPath { get; set; }

        public class VarKey
        {
            public const string tablename = "t_EmailList";
            public const string EmailID = "EmailID";
            public const string Subject = "Subject";
            public const string FromMail = "FromMail";
            public const string ToMail = "ToMail";
            public const string EmailBody = "EmailBody";
            public const string ReceiveTime = "ReceiveTime";
            public const string AttachPath = "AttachPath";
        }
    }
}