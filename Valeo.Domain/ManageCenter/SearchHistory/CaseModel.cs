using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [PetaPoco.TableName("t_Case")]
    [PetaPoco.PrimaryKey("Caseid")]
    public class CaseModel
    {
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public long Caseid { get; set; }

        /// <summary>
        /// 案件编号(不是GUID,而是法院定的编号,不可以修改,系统内部关系用)
        /// </summary>
        public string CaseNo { get; set; }

        /// <summary>
        /// 案件编号(默认与CaseNo是同一个，但这个可以人工修正,显示时都显示这个编号)
        /// </summary>
        public string CaseNoNew { get; set; }

        /// <summary>
        /// 中文法庭编号 如(民事訴訟 001/2014) 是案件上解析出来的
        /// </summary>
        public string CaseNo_Cn { get; set; }

        /// <summary>
        /// 次数[2/4]#
        /// </summary>
        public string NumberTimes { get; set; }

        /// <summary>
        /// 法院id
        /// </summary>
        public long CourtID { get; set; }

        /// <summary>
        /// 案件类别:民事上訴/傷亡訴訟/民事訴訟/僱員補償案件/刑事案件/破產案件/雜項案件/小額錢債審裁處上訴/建築及仲裁訴訟/裁判法院上訴
        /// </summary>
        public long CaseTypeID { get; set; }

        /// <summary>
        /// 原告英文名   
        /// </summary>
        public string P_PerName_En { get; set; }

        /// <summary>
        /// 案件年份2015
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 序号001
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 开庭日期
        /// </summary>
        public string CourtDay { get; set; }

        /// <summary>
        /// 原告
        /// </summary>
        public string Plaintiff { get; set; }

        /// <summary>
        /// 被告
        /// </summary>
        public string Defendant { get; set; }

        /// <summary>
        /// 原因/性质
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// 性质(不要了,与Cause是同一个意思)
        /// </summary>
        public string Nature { get; set; }

        /// <summary>
        /// 法官
        /// </summary>
        public string Judge { get; set; }

        /// <summary>
        /// 应讯代表
        /// </summary>
        public string Representation { get; set; }

        /// <summary>
        /// 聆讯
        /// </summary>
        public string Hearing { get; set; }

        /// <summary>
        /// 币别(excel上有)
        /// </summary>
        public string Currency { get; set; }
  
        /// <summary>
        /// 总金额(excel上有)
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        ///HtmlID
        /// </summary>
        public long HtmlID { get; set; }

        /// <summary>
        ///1：线下可见（未审） 2:线上可见（已审）
        /// </summary>
        public int Show { get; set; }

        /// <summary>
        ///数据级别(0:公开 1:内部人员可见 2:主管可见 3:超级用户可见)
        /// </summary>
        public long DataGradeID { get; set; }

        /// <summary>
        /// 添加者
        /// </summary>
        public string adduser { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string upduser { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? addtime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updtime { get; set; }

    }
}