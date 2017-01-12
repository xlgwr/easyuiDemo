using Valeo.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Valeo.Common
{
    public class JsonHandler
    {

        public static JsonMessage CreateMessage(int ptype, string pmessage, string pvalue)
        {
            JsonMessage json = new JsonMessage()
            {
                type = ptype,
                message = pmessage,
                value = pvalue
            };
            return json;
        }
        public static JsonMessage CreateMessage(int ptype, string pmessage)
        {
            JsonMessage json = new JsonMessage()
            {
                type = ptype,
                message = pmessage,
            };
            return json;
        }

        public static JsonValue GetJsonValue(int status, string resultMsg)
        {
            JsonValue json = new JsonValue()
            {
                status = status,
                message = resultMsg,
            };

            return json;
        }

        /// <summary>
        /// 解析Json 返回 DataTable
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static DataTable  DataTableJson(string jsonString)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, typeof(DataTable)) as DataTable;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }



    public class JsonMessage
    {
        public int type { get; set; }
        public string message { get; set; }
        public string value { get; set; }
    }

    public class JsonValue
    {
        /// <summary>
        /// 1 成功，0 失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
    


    }
}


