
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.Models
{
    [Serializable]
    [PetaPoco.TableName("t_Case")]
    [PetaPoco.PrimaryKey("Caseid",autoIncrement=true)]
    public class CaseModel
    {
        #region 实体属性
        
        /// <summary>
        /// 唯一标识(自动递增)
        /// </summary>
        public virtual long CaseId { get; set; }
        
        
        /// <summary>
        /// 案件编号(不是GUID,而是法院定的编号,不可以修改,系统内部关系用)
        /// </summary>
        public virtual string CaseNo { get; set; }
        
        /// <summary>
        /// 案件编号(默认与CaseNo是同一个，但这个可以人工修正,显示时都显示这个编号)
        /// </summary>
        public virtual string CaseNoNew { get; set; }
        
        /// <summary>
        /// 中文法庭编号 如(民事訴訟 001/2014) 是案件上解析出来的
        /// </summary>
        public virtual string CaseNo_Cn { get; set; }
        
        /// <summary>
        /// 次数[2/4]#
        /// </summary>
        public virtual string NumberTimes { get; set; }
        
        /// <summary>
        /// 法院id
        /// </summary>
        public virtual long CourtID { get; set; }
        
        /// <summary>
        /// 案件类别:民事上訴/傷亡訴訟/民事訴訟/僱員補償案件/刑事案件/破產案件/雜項案件/小額錢債審裁處上訴/建築及仲裁訴訟/裁判法院上訴
        /// </summary>
        public virtual long CaseTypeID { get; set; }
        
        /// <summary>
        /// 案件年份2015
        /// </summary>
        public virtual string Year { get; set; }
        
        /// <summary>
        /// 序号001
        /// </summary>
        public virtual string SerialNo { get; set; }
        
        /// <summary>
        /// 开庭日期
        /// </summary>
        public virtual string CourtDay { get; set; }
        
        /// <summary>
        /// 当事人各方（原告，被告的原始记录)
        /// </summary>
        public virtual string Parties { get; set; }
        
        /// <summary>
        /// 原告
        /// </summary>
        public virtual string Plaintiff { get; set; }
        
        /// <summary>
        /// 原告地址
        /// </summary>
        public virtual string P_Address { get; set; }
        
        /// <summary>
        /// 被告
        /// </summary>
        public virtual string Defendant { get; set; }
        
        /// <summary>
        /// 被告地址
        /// </summary>
        public virtual string D_Address { get; set; }
        
        /// <summary>
        /// 原因/性质
        /// </summary>
        public virtual string Nature { get; set; }
        
        /// <summary>
        /// 法官
        /// </summary>
        public virtual string Judge { get; set; }
        
        /// <summary>
        /// 应讯代表
        /// </summary>
        public virtual string Representation { get; set; }
        
        /// <summary>
        /// 原告代表
        /// </summary>
        public virtual string Representation_P { get; set; }
        
        /// <summary>
        /// 被告代表
        /// </summary>
        public virtual string Representation_D { get; set; }
        
        /// <summary>
        /// 聆讯
        /// </summary>
        public virtual string Hearing { get; set; }
        
        /// <summary>
        /// 币别(excel上有)
        /// </summary>
        public virtual string Currency { get; set; }
        
        /// <summary>
        /// 总金额(excel上有)
        /// </summary>
        public virtual int Amount { get; set; }
        
        /// <summary>
        /// 其他
        /// </summary>
        public virtual string Other { get; set; }
        
        /// <summary>
        /// 其他1
        /// </summary>
        public virtual string Other1 { get; set; }
        
        /// <summary>
        /// 有无判决书(0:无 1:有)
        /// </summary>
        public virtual int Judgement { get; set; }
        
        /// <summary>
        /// HtmlID
        /// </summary>
        public virtual long HtmlID { get; set; }
        
        /// <summary>
        /// 1：线下可见（未审） 2:线上可见（已审）
        /// </summary>
        public virtual int Show { get; set; }
        
        /// <summary>
        /// 数据级别(0:公开 1:内部人员可见 2:主管可见 3:超级用户可见)
        /// </summary>
        public virtual string DataGradeID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string AddUser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? AddDatetime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string UpdUser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? UpdDatetime { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 0:禁用 1:启用
        /// </summary>
        public virtual int Enable { get; set; }
        
        #endregion

 

    }
}
 