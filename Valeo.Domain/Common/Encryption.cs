using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace Valeo.Common
{
    public static class Encryption
    {
        const string KEY_64 = "EMMSVV01";

        /// <summary>
       /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encode(string data)
        {

            if (data == null || string.IsNullOrEmpty(data)) return "";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            int i = cryptoProvider.KeySize;

            MemoryStream ms = new MemoryStream();

            ICryptoTransform ff = cryptoProvider.CreateEncryptor(byKey, byIV);

            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            //DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            //int i = cryptoProvider.KeySize;

            //MemoryStream ms = new MemoryStream();
            //CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            //StreamWriter sw = new StreamWriter(cst);
            //sw.Write(data);
            //sw.Flush();
            //cst.FlushFinalBlock();
            //sw.Flush();
            //return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key64"></param>
        /// <param name="iv64"></param>
        /// <returns></returns>
        public static string Encode(string data, string key64, string iv64)
        {

            if (data == null || string.IsNullOrEmpty(data)) return "";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            int i = cryptoProvider.KeySize;

            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decode(string data)
        {

            if (data == null || string.IsNullOrEmpty(data)) return "";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        public static string Decode(string data, string key64, string iv64)
        {

            if (data == null || string.IsNullOrEmpty(data)) return "";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }
}

