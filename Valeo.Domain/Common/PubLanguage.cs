using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valeo.Lang;
using Newtonsoft.Json;
using PetaPoco;

namespace Valeo.Domain.Common
{
    public static class PubLanguage
    {
        public static readonly string[] HideColmnsStrings =
        {
            "Tid", "tLang", "tkeyNo", "tIndex", "htmlID", "tStatus",
            "ClientIP", "adduser", "upduser", "addtime", "updtime",
            "PublicID","Show","DataGradeID","Language","HtmlID","Enable",
            "ttype","tname","Remark","InstituteId","classType","Districtarea"
        };
        public static readonly string[] HideColmnsStringsPub =
        {
            "Tid", "tLang", "tkeyNo", "tIndex", "htmlID", "tStatus",
            "ClientIP", "adduser", "upduser", "addtime", "updtime",
            "PublicID","Show","DataGradeID","Language","HtmlID","Enable",
            "ttype","tname","InstituteId","classType","Districtarea"
        };

        public static readonly string[] SelectFiledCn = { "LawyerNameCn", "RegNameCn", "Name_Cn", "MembershipCn", "CompanyNameCn", "Name", "FullName", "CompanyName", "ArchitectsName" };
    
        public static readonly string[] SelectFiledEn = { "LawyerNameEn", "RegNameEn", "Name_En", "MembershipEn", "CompanyNameEn" };
        public static string GetPageJson(Page<dynamic> page,long tableId)
        {
            string strJsonRow = "{\"total\":" + page.TotalItems + ",\"rows\":[";
            foreach (var itemRow in page.Items)
            {
                string strJsonCol = "{";
                var itemCols = itemRow as IEnumerable<KeyValuePair<string, object>>;
                if (itemCols != null)
                    strJsonCol = itemCols.Aggregate(strJsonCol, (current, itemCol) => current + String.Format("\"{0}\":{1},", itemCol.Key, JsonConvert.SerializeObject(itemCol.Value)));
                strJsonCol+="\"TableID\":" + tableId ;
                //strJsonCol = strJsonCol.TrimEnd(',');
                strJsonCol += "},";
                strJsonRow += strJsonCol;
            }
            strJsonRow = strJsonRow.TrimEnd(',');
            strJsonRow += "]}";
            return strJsonRow;
        }
        /// <summary>
        /// 得到多语言
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetTableColumnsLanguage(string tableName, string columnName)
        {
            return PubLanguage.GetBaseResValue(tableName + "_" + columnName);
        }
        /// <summary>
        /// 得到多语言值
        /// </summary>
        /// <param name="baseResKey"></param>
        /// <returns></returns>
        public static string GetBaseResValue(string baseResKey)
        {
            if (string.IsNullOrEmpty(baseResKey)) return "";
            var t = typeof(BaseRes);
            var pis = t.GetProperties();
            foreach (var pi in pis.Where(pi => pi.Name.ToUpper() == baseResKey.ToUpper()))
            {
                return (string)pi.GetValue(pi.Name);
            }
            return baseResKey;
        }
    }
}
