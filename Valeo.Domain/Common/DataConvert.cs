using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Valeo.Common
{
    public partial class DataConvert
    {
        /// <summary>
        ///  Url转码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
        /// <summary>
        /// 获取Guid处理
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 检测 GuidValue 是否包含有效的值，默认值Guid.Empty
        /// </summary>
        /// <param name="GuidValue">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值Guid.Empty</returns>
        public static Guid GetGuid(string GuidValue)
        {
            try
            {
                return new Guid(GuidValue);
            }
            catch { return Guid.Empty; }
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="DateTime1">开始时间</param>
        /// <param name="DateTime2">结束时间</param>
        /// <param name="dayText">多语言(日)</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2, string dayText)
        {
            string dateDiff = null;

            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                string hours = ts.Hours.ToString(), minutes = ts.Minutes.ToString(), seconds = ts.Seconds.ToString();



                if (ts.Hours < 10)
                {
                    hours = "0" + ts.Hours.ToString();
                }
                if (ts.Minutes < 10)
                {
                    minutes = "0" + ts.Minutes.ToString();
                }
                if (ts.Seconds < 10)
                {
                    seconds = "0" + ts.Seconds.ToString();
                }

                if (ts.Days <= 0)
                {
                    dateDiff = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
                }
                else
                {
                    dateDiff = string.Format("{0}{1} {2}:{3}:{4}", ts.Days, dayText, hours, minutes, seconds);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return dateDiff;
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="DateTime1">开始时间</param>
        /// <param name="DateTime2">结束时间</param>
        /// <returns></returns>
        public static long DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            long dateDiff = 0;

            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                dateDiff = ts.Milliseconds;
            }
            catch (Exception)
            {

                throw;
            }

            return dateDiff;
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="DateTime1">开始时间</param>
        /// <param name="DateTime2">结束时间</param>
        /// <returns></returns>
        public static int DateDiffDays(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;

            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                dateDiff = ts.Days;
            }
            catch (Exception)
            {

                throw;
            }

            return dateDiff;
        }


        /// <summary>
        /// 时间时分转换为分钟处理
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static int GetMinuteByDateTime(DateTime dt)
        {

            try
            {
                return dt.Hour * 60 + dt.Minute;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 时间格式化处理
        /// </summary>
        /// <param name="obj">时间</param>
        /// <returns></returns>
        public static string Value2dateFormat(object obj, string format)
        {

            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.ToString())) return "";

                DateTime dt;

                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt.ToString(format);
                }

                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 时间格式化处理
        /// </summary>
        /// <param name="obj">时间</param>
        /// <returns></returns>
        public static DateTime Value2DateTime(string obj)
        {

            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.ToString())) return DateTime.Now;

                DateTime dt;

                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }

                return DateTime.Now;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 区域时间转化
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string Value2DateCultureInfo(string ds, bool isDate = true)
        {
            try
            {
                if (string.IsNullOrEmpty(ds))
                {
                    return ds;
                }

                var dd = DateTime.Now;
                if (ds.IndexOf('/') > -1)
                {
                    dd = DateTime.ParseExact(ds, "M/d/yy", new System.Globalization.CultureInfo("en-us"));
                }
                else
                {
                    dd = DateTime.ParseExact(ds, "dd-MMM-yyyy", new System.Globalization.CultureInfo("en-us"));
                }

                if (isDate)
                {
                    return dd.ToString("yyyy-MM-dd");
                }
                else
                {
                    return dd.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception)
            {
                return ds;
            }
        }
        /// <summary>
        /// 时间格式化处理
        /// </summary>
        /// <param name="obj">时间</param>
        /// <returns></returns>
        public static string Value2OverDateTime(string obj)
        {
            DateTime dtOver;
            string OverTime = "";

            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.ToString())) return OverTime;

                if (DateTime.TryParse(obj.ToString(), out dtOver))
                {
                    OverTime = dtOver.ToString("yyyy-MM-dd 23:59:59");
                }

                return OverTime;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 时间格式化处理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public static bool Value2DateTime(string obj, out DateTime dtValue)
        {
            bool isSuc = false;
            dtValue = DateTime.Now;

            try
            {
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    if (DateTime.TryParse(obj.ToString(), out dtValue))
                    {
                        isSuc = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return isSuc;
        }

        /// <summary>
        /// 对象转换为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Value2string(object obj)
        {

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return string.Empty;

            return obj.ToString();
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal Value2decimal(object obj)
        {

            decimal dec;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            decimal.TryParse(obj.ToString().Trim(), out dec);

            dec = decimal.Parse(dec.ToString("###########0.000000"));
            return dec;
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float Value2float(object obj)
        {

            float dec;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            float.TryParse(obj.ToString().Trim(), out dec);

            dec = float.Parse(dec.ToString("###########0.000000"));
            return dec;
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Value2int(object obj)
        {
            decimal dec;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            decimal.TryParse(obj.ToString().Trim(), out dec);

            dec = decimal.Parse(dec.ToString("###########0"));

            return (int)dec;
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Value2int(object obj, out int decValue)
        {

            bool isSuc = false;
            decValue = 0;
            decimal dec;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return isSuc;

            if (decimal.TryParse(obj.ToString().Trim(), out dec))
            {
                isSuc = true;
            }

            dec = decimal.Parse(dec.ToString("###########0"));
            decValue = (int)dec;

            return isSuc;
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long Value2long(object obj)
        {
            long lng;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            long.TryParse(obj.ToString().Trim(), out lng);

            return lng;
        }

        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal Value2decimal(object obj, int pos)
        {

            decimal dec;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            decimal.TryParse(obj.ToString().Trim(), out dec);

            dec = Math.Round(dec, pos);

            dec = decimal.Parse(dec.ToString(GetFormatString(pos)));
            return dec;
        }


        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double Value2double(object obj)
        {

            double dub;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            double.TryParse(obj.ToString().Trim(), out dub);

            dub = double.Parse(dub.ToString("###########0.000000"));
            return dub;
        }


        /// <summary>
        /// 对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double Value2double(object obj, int pos)
        {

            double dub;

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return 0;

            double.TryParse(obj.ToString().Trim(), out dub);

            dub = Math.Round(dub, pos);

            dub = double.Parse(dub.ToString(GetFormatString(pos)));
            return dub;
        }

        /// <summary>
        /// 对象格式转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Value2Format(object obj, int pos)
        {

            decimal dec;
            double dub;
            string strDec = "";

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return strDec;

            if (decimal.TryParse(obj.ToString().Trim(), out dec))
            {
                dec = Math.Round(dec, pos);

                strDec = dec.ToString(GetFormatString(pos));
            }
            else
            {
                if (double.TryParse(obj.ToString().Trim(), out dub))
                {
                    dub = Math.Round(dub, pos);

                    strDec = dub.ToString(GetFormatString(pos));
                }
            }

            return strDec;
        }

        /// <summary>
        /// 对象格式转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tempUnit">温度单位区分</param>
        /// <returns></returns>
        public static string Value2Format(object obj, string tempUnit, int pos)
        {

            decimal dec;
            double dub;
            string strDec = "";

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return strDec;

            if (decimal.TryParse(obj.ToString().Trim(), out dec))
            {
                dec = GetC2Fvalue(dec, tempUnit);

                dec = Math.Round(dec, pos);

                strDec = dec.ToString(GetFormatString(pos));
            }
            else
            {
                if (double.TryParse(obj.ToString().Trim(), out dub))
                {
                    dub = GetC2Fvalue(dub, tempUnit);

                    dub = Math.Round(dub, pos);

                    strDec = dub.ToString(GetFormatString(pos));
                }
            }

            return strDec;
        }

        /// <summary>
        /// 摄氏温度转换值
        /// </summary>
        /// <returns></returns>
        private static decimal GetC2Fvalue(decimal readData, string tempUnit)
        {
            if (tempUnit.Equals("0")) return readData;

            //华氏度 = 摄氏度* 1.8 +32
            return readData * (decimal)1.8 + 32;
        }

        /// <summary>
        /// 摄氏温度转换值
        /// </summary>
        /// <returns></returns>
        private static double GetC2Fvalue(double readData, string tempUnit)
        {
            if (tempUnit.Equals("0")) return readData;

            //华氏度 = 摄氏度* 1.8 +32
            return readData * 1.8 + 32;
        }

        /// <summary>
        /// 对象格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTime2Format(object obj, Enums.DateTimeFormat format)
        {

            DateTime dtValue;
            string strDec = "";

            if (obj == null || string.IsNullOrEmpty(obj.ToString())) return strDec;

            if (DateTime.TryParse(obj.ToString().Trim(), out dtValue))
            {
                switch (format)
                {

                    case Enums.DateTimeFormat.DateTime:

                        strDec = dtValue.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");

                        break;

                    case Enums.DateTimeFormat.Time:

                        strDec = dtValue.ToString("HH:mm:ss");

                        break;

                    case Enums.DateTimeFormat.Date:

                        strDec = dtValue.ToString("yyyy-MM-dd").Replace("/", "-");

                        break;
                }
            }
            else
            {
                strDec = obj.ToString().Trim();
            }

            return strDec;
        }

        /// <summary>
        /// 对象格式化
        /// </summary>
        /// <param name="dtValue"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTime2Format(DateTime dtValue, Enums.DateTimeFormat format)
        {

            string strDec = "";

            switch (format)
            {

                case Enums.DateTimeFormat.DateTime:

                    strDec = dtValue.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");

                    break;

                case Enums.DateTimeFormat.Time:

                    strDec = dtValue.ToString("HH:mm:ss");

                    break;

                case Enums.DateTimeFormat.Date:

                    strDec = dtValue.ToString("yyyy-MM-dd").Replace("/", "-");

                    break;
            }

            return strDec;
        }

        /// <summary>
        /// Byte转换为KByte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal byte2Kbyte(long value)
        {

            try
            {
                return Value2decimal((double)value / 1024, 2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Byte转换为MByte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal byte2Mbyte(long value)
        {

            try
            {

                return Value2decimal((double)value / 1024 / 1024, 2);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 转化一个DataTable  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable List2DataTable(IList list)
        {

            DataTable result = new DataTable();

            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }

            return result;
        }

        #region 获取数字格式化串

        /// <summary>
        /// 获取数字格式化串
        /// </summary>
        /// <returns></returns>
        public static string GetFormatString(int pos)
        {

            string FormatString = "#0.00";

            if (pos > 0)
            {
                FormatString = "#0." + "".PadLeft(pos, '0');
            }
            else
            {
                FormatString = "#0";
            }

            return FormatString;
        }

        #endregion

        /// <summary>
        /// 是否包括汉字
        /// @"[\u4e00-\u9fbb]"
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool zhContains(string input, string patt = @"[\u4e00-\u9fbb]")
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return false;
                }
                Regex rgx = new Regex(patt, RegexOptions.IgnoreCase);

                return rgx.IsMatch(input);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        static string ReplaceReg(string patt, string input, string totxt)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return "";
                }
                Regex rgx = new Regex(patt, RegexOptions.IgnoreCase);
                return rgx.Replace(input.ToString(), totxt).Trim();
            }
            catch (Exception ex)
            {

                return input;
            }
        }
    }
}
