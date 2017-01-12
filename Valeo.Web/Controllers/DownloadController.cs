using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Valeo.Common;
using System.Drawing;

namespace Valeo.Controllers
{
    public class DownloadController : Controller
    { 
        [HttpPost]
        public JsonResult Exists(string id)
        {
            var result = new
            {
                type = 0,
                message = "对应文件不存在。",
                value = @"\Content\img\404.png"
            };

            if (!string.IsNullOrEmpty(id))
            {
                var _path = isExit(id);
                if (!string.IsNullOrEmpty(_path))
                {
                    result = new
                    {
                        type = 1,
                        message = "获取文件成功。",
                        value = id
                    };
                }
            }
            return Json(result);
        }
        string isExit(string id)
        {
            string filePathBase = Server.MapPath("/");
            string _path = string.Concat(filePathBase, id);

            if (!System.IO.File.Exists(_path))
            {
                filePathBase = ConfigurationManager.AppSettings["Valeo.SystemFile"] + @"/";
                _path = string.Concat(filePathBase, id);

                if (!System.IO.File.Exists(_path))
                {
                    filePathBase = ConfigurationManager.AppSettings["Valeo.DataFile"] + @"/";
                    _path = string.Concat(filePathBase, id);
                    if (!System.IO.File.Exists(_path))
                    {
                        return "";
                    }
                }
            }
            return _path;
        }
        /// <summary>
        /// 本地图片
        /// </summary>
        /// <param name="id">0:根目录 Server.MapPath，1：web config自定义目录SystemFile,2：web config自定义目录DataFile</param>
        /// <param name="id2">filePath</param>
        /// <returns></returns>
        public FileResult ShowImage(string id)
        {
            var _path = isExit(id);
            if (string.IsNullOrEmpty(_path))
            {
                _path = Server.MapPath("/") + @"\Content\img\404.png";
            }
            FileStream fs = new FileStream(_path, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return File(byData, "image/jpg");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="filePath">flieName</param>
        /// <returns></returns>
        public FileStreamResult Uploads(string filePath, string flieName)
        {
            //var filePath = id.Replace("_", "/");
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }
            //传过来的参数如有+号，要全转为___,这再转回。。
            filePath = System.Web.HttpUtility.HtmlDecode(filePath.Replace("___","+"));
            var tmpfilePath = Encryption.Decode(filePath);
            string absoluFilePath = isExit(tmpfilePath);
            if (string.IsNullOrEmpty(absoluFilePath))
            {
                absoluFilePath = Server.MapPath("/") + @"\Content\img\404.png";
                flieName = "没有找到文件404";
            }

            string contentType = MimeMapping.GetMimeMapping(absoluFilePath);
            try
            {
                //string realPath = Server.MapPath(file);
                //string saveFileName = FileUtil.GetFileName(realPath);

                Response.WriteFile(absoluFilePath);
                Response.Charset = "GB2312";
                Response.ContentEncoding = Encoding.GetEncoding("GB2312");

                //Response.ContentType = "application/ms-excel/msword";
                Response.ContentType = contentType;

                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(flieName + System.IO.Path.GetExtension(absoluFilePath)));
                Response.Flush();
                Response.End();

                //FileInfo file = new FileInfo(absoluFilePath);
                //file.Delete();

                return new FileStreamResult(Response.OutputStream, contentType);//"application/ms-excel/msword"

                // var fileStream = new MemoryStream(fileContents);
                // return File(fileStream, "application/ms-excel", "fileStream.xls");

                //return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(flieName));

            }
            catch (Exception)
            {
                return null;
                //return null;
                //throw;
            }

        }
       
    }
}