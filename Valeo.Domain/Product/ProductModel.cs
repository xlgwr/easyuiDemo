
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Valeo.Domain.Product
{
    [Serializable]
    [PetaPoco.TableName("m_Product")]
    [PetaPoco.PrimaryKey("ProductId",autoIncrement=false)]
    public class ProductModel
    {
        #region 实体属性
        
        /// <summary>
        /// 产品代号(自己手动编)
        /// </summary>
        public virtual string ProductID { get; set; }
        
        
        /// <summary>
        /// 大分类 0:在线搜索 1:离线搜索 2:自动监察;
        /// </summary>
        public virtual int? BigType { get; set; }
        
        /// <summary>
        /// 小分类 0:法庭 1:公司 2:土地 3:信贷 4:公共
        /// </summary>
        public virtual int? SmallType { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProductName { get; set; }
        
        /// <summary>
        /// 币种
        /// </summary>
        public virtual string Currency { get; set; }
        
        /// <summary>
        /// 单价 80元/次
        /// </summary>
        public virtual double Price { get; set; }
        /// <summary>
        /// 折扣(如80%填80)
        /// </summary>
        public virtual int Discount { get; set; }
        /// <summary>
        /// 折扣单价
        /// </summary>
        public virtual double DiscountPrice { get; set; }
        
        /// <summary>
        /// 有效期(几个月)
        /// </summary>
        public virtual int Validity { get; set; }
        
        /// <summary>
        /// 所有年限)  只针对法庭有效
        /// </summary>
        public virtual int AgeLimit { get; set; }
        
        /// <summary>
        /// 人数(自动监察会用到,监察人员上限)
        /// </summary>
        public virtual int PeopleCount { get; set; }
        
        /// <summary>
        /// 查询类别(1:土地查册 2:注册摘要 3;注册摘要表格 4:其他)(离线土地会用到)
        /// </summary>
        public virtual int SeachType { get; set; }
        
        /// <summary>
        /// 记录类别(1:全部记录 2:现实记录)(离线土地会用到)
        /// </summary>
        public virtual int RecordType { get; set; }
        
        /// <summary>
        /// 报告类别：(离线公司会用到)公司:(0:最近公司年报，1:组织章程大纲及章程细则，2:公司注册证书，3:公司年报-特别指明年份（需手动输入），4:抵押（需注明年份），5:有效地商业/分行登记证核证副本，6:其他，7:商业登记册内资料摘录的核证本，8:商业登记册内资料摘录的电子摘录，9:组织规章大纲及章程细则 10:有效地商业/分行登记证核证副本，11:公司强制性清盘案记录查册） 人个:(12:破产案查册，13:有限公司董事查册，14:其他)
        /// </summary>
        public virtual string ReportType { get; set; }
        /// <summary>
        /// 前端是否显示  0 不显示，1, 显示
        /// </summary>
        public virtual int Show { get; set; }


        [ResultColumn]
        public virtual int [] IntReportTypes { get; set; }
        /// <summary>
        /// 财务:0/证券:1
        /// </summary>
        public virtual int LenderType { get; set; }
        
        #endregion

 

    }

}
 