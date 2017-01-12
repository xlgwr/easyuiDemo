
using Valeo.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lang
{

    /// <summary>
    /// 多语言Key
    /// </summary>
    public class LangKey
    {

        /// <summary>
        /// 验证数据最大长度
        /// </summary>
        public const string MAX_LENGTH = "COM_MSG_MAX_LENGTH";

        /// <summary>
        /// 必须为正整数
        /// </summary>
        public const string MUST_POSITIVE = "COM_MSG_MST_POSITIVE";
        /// <summary>
        /// 正整数
        /// </summary>
        public const string POSITIVE = "PRO_MSG_001";
        /// <summary>
        /// 最小公差为负整数
        /// </summary>
        public const string MINDIFF = "MTS_MSG_009";
        /// <summary>
        /// 最大公差为正整数
        /// </summary>
        public const string MAXDIFF = "MTS_MSG_010";
        /// <summary>
        /// 标准值不能小于1的正整数
        /// </summary>
        public const string MTSTDREG = "MTS_MSG_001";
        /// <summary>
        /// 公差为正整数
        /// </summary>
        public const string MTDIFFREG = "MTS_MSG_014";
        /// <summary>
        /// 必须为整数
        /// </summary>
        public const string MTDINTEGER = "MTS_MSG_015";
           /// <summary>
        /// 必须字符开头
        /// </summary>
        public const string HSBREG = "HSB_CTL_009";
        /// <summary>
        /// 必须字符开头
        /// </summary>
        public const string POSITIVSORT = "CLI_MSG_004";
    }

    /// <summary>
    /// 返回多语言
    /// </summary>
    public static class COMKey
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Valeo.Lang.BaseRes", typeof(BaseRes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        public static string COM(string xx)
        {
            return ResourceManager.GetString(xx);
        }
    }
}
