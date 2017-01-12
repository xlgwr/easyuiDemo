using Valeo.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo
{
    public class Enums
    {

        /// <summary>
        /// 数据格式转换
        /// </summary>
        public enum ConvertFormat
        {
            /// <summary>
            ///简称-》全称
            /// </summary>
            Abb2Full = 0,

            /// <summary>
            ///全称-》简称
            /// </summary>
            Full2Abb = 1,

            /// <summary>
            ///简体-》繁体
            /// </summary>
            Simp2Trad = 10,

            /// <summary>
            ///繁体-》简体
            /// </summary>
            Trad2Simp = 11,

            /// <summary>
            ///简体-》拼音
            /// </summary>
            Simp2Alph = 12,

            /// <summary>
            ///繁体-》拼音
            /// </summary>
            Trad2Alph = 13,

            /// <summary>
            /// 简体-》拼音 姓后加空格 X MM
            /// </summary>
            Simp2AlphSp = 14,

            /// <summary>
            /// 繁体-》拼音 姓后加空格 X MM
            /// </summary>
            Trad2AlphSP = 15,

        }

        /// <summary>
        /// 数据格式类型
        /// </summary>
        public enum DateTimeFormat
        {

            /// <summary>
            /// 时间
            /// </summary>
            DateTime = 0,

            /// <summary>
            /// 日期
            /// </summary>
            Date = 1,

            /// <summary>
            /// 时间
            /// </summary>
            Time = 2,
        }

        /// <summary>
        ///查询方式： 0:在线搜索 1:离线搜索 2:自动监察; 
        /// </summary>
        public static string GetSearchMayText(int searchMay)
        {
            switch ((SearchMay)searchMay)
            {
                case SearchMay.Online:
                    return BaseRes.CBX_CTL_001;

                case SearchMay.Offline:
                    return BaseRes.CBX_CTL_002;

                default:
                    return BaseRes.CBX_CTL_003;
            }

            return "";
        }
         
        /// <summary>
        ///查询方式： 0:在线搜索 1:离线搜索 2:自动监察; 
        /// </summary>
        public enum SearchMay
        {
            /// <summary>
            ///  -1,所有
            /// </summary>
            All = -1,
            /// <summary>
            ///  0:在线搜索
            /// </summary>
            Online = 0,

            /// <summary>
            /// 1:离线搜索
            /// </summary>
            Offline = 1,

            /// <summary>
            /// 2:自动监察
            /// </summary>
            AutoMonintor = 2,

        }

        /// <summary>
        ///产品类型  0:法庭 1:公司 2:土地 3:信贷 4:公共 5:其他;
        /// </summary>
        public enum ProductType
        {
            /// <summary>
            ///  0:法庭
            /// </summary>
            Court = 0,

            /// <summary>
            /// 1:公司
            /// </summary>
            Company = 1,

            /// <summary>
            /// 2:土地
            /// </summary>
            Land = 2,

            /// <summary>
            /// 3:信贷
            /// </summary>
            Credit = 3,

            /// <summary>
            /// 4:公共
            /// </summary>
            Comon = 4,

            /// <summary>
            /// 5:其他
            /// </summary>
            Other = 5,

            /// <summary>
            /// 6.全部数据
            /// </summary>
            All=6
        }







        public enum TextBoxKeyEnum
        {
            /// <summary>
            /// 被告EN
            /// </summary>
            DEN = 1,

            /// <summary>
            /// 被告Cn
            /// </summary>
            DCN = 2,

            /// <summary>
            /// 被告En
            /// </summary>
            PEN = 3,

            /// <summary>
            /// 被告Cn
            /// </summary>
            PCN = 4,

            /// <summary>
            /// 案件编号
            /// </summary>
            CNO = 5,

            /// <summary>
            /// 代表
            /// </summary>
            RPT = 6,

            /// <summary>
            /// 地址1
            /// </summary>
            ADD1 = 7,

            /// <summary>
            /// 地址2
            /// </summary>
            ADD2 = 8,
        }



        /// <summary>
        /// 会员类型 0：个人  1：公司
        /// </summary>
        public enum CaseSortEnum
        {
            /// <summary>
            /// 个人
            /// </summary>
            CaseTime = 0,
            /// <summary>
            /// 公司
            /// </summary>
            CaseCode = 1
        }


        /// <summary>
        /// 权限一级菜单  多语言key=ID
        /// </summary>
        public enum RightMenuEnum
        {
            
            /// <summary>
            /// 套餐购买
            /// </summary>
            PUB_CTL_143 = 1,

            /// <summary>
            /// 在线搜索
            /// </summary>
            PUB_CTL_144 = 2,

            /// <summary>
            /// 自动监察
            /// </summary>
            PUB_CTL_145 = 3,

            /// <summary>
            /// 离线搜索
            /// </summary>
            PUB_CTL_146 = 4,

            /// <summary>
            /// 管理中心
            /// </summary>
            PUB_CTL_147 = 9,
        }



        /// <summary>
        /// 离线搜索，二级菜单  多语言key=ID，父节点为4
        /// </summary>
        public enum RightSecMenuEnum
        {

            /// <summary>
            /// 法庭查册
            /// </summary>
            MEU_CTL_AUTOM_COURT = 5,

            /// <summary>
            /// 土地查册
            /// </summary>
            OSP_CTL_105 = 6,

            /// <summary>
            /// 公司查册
            /// </summary>
            TSK_CTL_040 = 7,

            /// <summary>
            /// 其他服务
            /// </summary>
            TSK_CTL_042 = 8,

        }
    }
}
