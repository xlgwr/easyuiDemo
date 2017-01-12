using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valeo.Lang;

namespace Valeo.Domain
{
    /// <summary>
    /// 搜索记录明细画面表格
    /// </summary>
    [Serializable]
    public class SearchHistoryVM
    {

        private string _SchDatetime = string.Empty;
        /// <summary>
        /// 搜索  时间+日期
        /// </summary>
        public string SchDatetime
        {
            get
            {
                return _SchDatetime;
            }

            set
            {
                try
                {
                    _SchDatetime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm");
                }
                catch (Exception)
                {

                    _SchDatetime = value;
                }

            }

        }

        /// <summary>
        /// 搜索者
        /// </summary>
        public string SchMemberNm { get; set; }

        private string _SchContent = string.Empty;
        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SchContent { get; set; }
        //{ 
        //    get
        //    {
        //        try
        //        {
        //            if (_SchContent.Substring(0, 1) == ";")
        //            {
        //                return _SchContent.Substring(1, _SchContent.Length - 1);
        //            }
        //            return _SchContent;
        //        }
        //        catch (Exception ex)
        //        {

        //            return _SchContent;
        //        }

        //    }

        //    set
        //    {
        //        _SchContent = value;
        //    }
        //}

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品详细-【产品名称-次数】
        /// </summary>
        public string ProductDetail { get; set; }

        /// <summary>
        /// 搜索结果
        /// </summary>
        public string SchResult { get; set; }


        private string _SchStatus = "0";
        /// <summary>
        /// 搜索状态
        /// </summary>
        public string SchStatus
        {
            get
            {
                string status = string.Empty;
                switch (_SchStatus)
                {
                    case "0":

                        status = BaseRes.COM_CTL_SCHSTATUS_000;
                        break;
                    case "1":
                        status = BaseRes.COM_CTL_SCHSTATUS_001;
                        break;
                    case "2":
                        status = BaseRes.COM_CTL_SCHSTATUS_002;
                        break;
                    case "3":
                        status = BaseRes.COM_CTL_SCHSTATUS_003;
                        break;
                }
                return status;

            }
            set
            {
                _SchStatus = value;
            }

        }


        /// <summary>
        /// 前台备注
        /// </summary>
        public string Remark1 { get; set; }

        /// <summary>
        /// 后台备注
        /// </summary>
        public string Remark2 { get; set; }
    }
}
