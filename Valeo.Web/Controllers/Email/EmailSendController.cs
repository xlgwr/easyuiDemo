using App.Lang;
using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.Models;
using Valeo.Domain.ParameterSetting;
using Valeo.Lang;
using Valeo.Service;
using Valeo.Service.ParameterSetting;
using HtmlAgilityPack;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Valeo.Controllers.Email
{
    public class EmailSendController : BaseController
    {

        //业务处理对象
        EmailService Service = new EmailService();
        static bool isReport = false;//报告中点击发送邮件
        static int m_BigType;
        static int m_SmallType;
        static long m_ReportID;
        static long m_TaskID;

        //
        // GET: /EmialSend/
        [ValidateInput(false)]
        public ActionResult Index(EmailAppModel EmailAppModel, string functionName = "", string retId = "")
        {
            ViewBag.RetId = retId;
            ViewBag.FunctionName = functionName;
            //系统电邮
            EmailAppModel.FromMail = Service.GetEmai().getSysEmail();
            return View("Index", EmailAppModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMailDialogPost(EmailAppModel EmailAppModel, long memberID, string functionName = "", string retId = "")
        {
            ViewBag.RetId = retId;
            ViewBag.FunctionName = functionName;
            //系统电邮
            EmailAppModel.FromMail = Service.GetEmai().getSysEmail();
            EmailAppModel.MemberID = memberID;

            return View("Index", EmailAppModel);
        }

        [HttpPost]
        public JsonResult SendMail(string Subject, string FromMail, string ToMail, string EmailBody, string AttachPath)
        {

            //EmailBody= HttpUtility.UrlEncode(EmailBody);
            EmailBody = HttpUtility.UrlDecode(EmailBody);


            bool isSuc = false;
            try
            {


                //邮件发送
                isSuc = Service.DoSendMail(FromMail, ToMail, EmailBody, Subject, AttachPath);
                if (!isSuc) return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);

                //邮件发送成功，在数据库插入发送记录信息
                var emailAppModels = Service.DoInserRecod(LoginUser.UserID, ToMail, EmailBody, Subject, AttachPath);

            
                //日志   
                var msg = "邮件发送:" + "发送成功："+ToMail;
                addLog(0, 0, msg, VarKey.ServicePage.EmailManager.ToString());

                return Json(new
                {
                    type = 1,
                    message = BaseRes.COM_MSG_ADD_SUC,
                    data = new
                    {
                        EmailId = emailAppModels[0].EmailID
                    }
                });
                //return Json(  JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_SUC), JsonRequestBehavior.AllowGet};

            }
            catch (Exception ex)
            {
                log.Error(ex);
                //日志   
                var msg = "邮件发送:" + "发送失败："+ToMail;
                addLog(0, 0, msg, VarKey.ServicePage.EmailManager.ToString());
                return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult UserListPage(string serch, long page = 1, long rows = 10)
        {

            Page<MemberModel> list = Service.GetMenberList(page, rows, serch);
            var emailRecod = list.Items;
            var emailRecodlist = from recod in emailRecod
                                 select new
                                 {
                                     recod.MemberID,
                                     recod.MemberName,
                                     recod.Email,

                                 };


            var result = new
            {
                total = list.TotalItems,
                rows = emailRecodlist
            };
            return Json(result);
        }


        public JsonResult UserListPage2(long memberID, string serch, long page = 1, long rows = 10)
        {

            Page<MemberModel> list = Service.GetDefaultMemberList(memberID, page, rows, serch);
            var emailRecod = list.Items;
            var emailRecodlist = from recod in emailRecod
                                 select new
                                 {
                                     recod.MemberID,
                                     recod.MemberName,
                                     recod.Email,

                                 };


            var result = new
            {
                total = list.TotalItems,
                rows = emailRecodlist
            };
            return Json(result);
        }

        /// <summary>
        /// 计划读取
        /// </summary>
        [HttpPost]
        public JsonResult UploadFile()
        {
            try
            {




                //string mpath = Path.Combine(Server.MapPath("~/App_Data/Uploads/") );
                //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(mpath);
                //int fileNum = dir.GetFiles().Length; // 该目录下的文件数量。。


                //将选择文件复制到~/App_Data/Uploads/保存
                HttpPostedFileBase file = Request.Files["fileToUpload"];
                var tmppath = ToCreateDirectory(LoginUser.UserID, 1);
                //创建文件夹
                string NewPath = Path.Combine(tmppath.prefixPath);

                //文件存放路径
                string filePath = Path.Combine(tmppath.prefixPath, Path.GetFileName(file.FileName));


                //不存在则创建
                if (Directory.Exists(NewPath))
                {
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(NewPath);
                    directoryInfo.Create();
                }


                //保存临时文件
                file.SaveAs(filePath);


                return Json(new
                {
                    status = 1,
                    message = filePath,
                }, "text/html");

                //return Json(JsonHandler.GetJsonValue(1, filePath), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonHandler.GetJsonValue(0, ""), JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

        /// <summary>
        /// 计划读取
        /// </summary>
        [HttpPost]
        public JsonResult UploadFile2(string path)
        {
            try
            {


                string destPath = path;

                var tmppath = ToCreateDirectory(LoginUser.UserID, 1);
                //创建文件夹
                string NewPath = Path.Combine(tmppath.prefixPath);

                //文件存放路径
                string filePath = Path.Combine(tmppath.prefixPath, Path.GetFileName(destPath));


                System.IO.File.Copy(destPath, filePath);


                //string mpath = Path.Combine(Server.MapPath("~/App_Data/Uploads/") );
                //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(mpath);
                //int fileNum = dir.GetFiles().Length; // 该目录下的文件数量。。


                //将选择文件复制到~/App_Data/Uploads/保存
                HttpPostedFileBase file = Request.Files["fileToUpload"];

                //var tmppath = ToCreateDirectory(LoginUser.UserID, 1);
                ////创建文件夹
                //string NewPath = Path.Combine(tmppath.prefixPath);

                ////文件存放路径
                //string filePath = Path.Combine(tmppath.prefixPath, Path.GetFileName(file.FileName));


                ////不存在则创建
                //if (Directory.Exists(NewPath))
                //{
                //}
                //else
                //{
                //    DirectoryInfo directoryInfo = new DirectoryInfo(NewPath);
                //    directoryInfo.Create();
                //}


                ////保存临时文件
                //file.SaveAs(filePath);

                var result = new
                {
                    status = 1,
                    Path = filePath,
                    value = filePath

                };
                return Json(result, JsonRequestBehavior.AllowGet);
                //return Json(JsonHandler.GetJsonValue(1, filePath), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonHandler.GetJsonValue(0, ""), JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

        /// <summary>
        /// 上传附件到服务器
        /// </summary>
        [HttpPost]
        public JsonResult UploadService(string ctlName)
        {
            try
            {




                //string mpath = Path.Combine(Server.MapPath("~/App_Data/Uploads/") );
                //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(mpath);
                //int fileNum = dir.GetFiles().Length; // 该目录下的文件数量。。


                //将选择文件复制到~/App_Data/Uploads/保存
                HttpPostedFileBase file = Request.Files[ctlName];
                var tmppath = ToCreateDirectory(LoginUser.UserID, 1);
                //创建文件夹
                string NewPath = Path.Combine(tmppath.prefixPath);

                //文件存放路径
                string filePath = Path.Combine(tmppath.prefixPath, Path.GetFileName(file.FileName));


                //不存在则创建
                if (Directory.Exists(NewPath))
                {
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(NewPath);
                    directoryInfo.Create();
                }


                //保存临时文件
                file.SaveAs(filePath);

                var result = new
                {
                    status = 1,
                    filePath = file.FileName,

                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = 0,


                };
                return Json(result, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }


        /// <summary>
        /// 保存添加图片PDF
        /// </summary>
        /// <param name="tmpFilePath"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddImageOrPDF()
        {
            var msg = "";
            try
            {
                HttpPostedFileBase tmpFilePath = Request.Files["fileToUpload"];

                string[] imgTypes = { "image/pjpeg", "image/jpeg", "image/png", "image/bmp", "application/pdf", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
                if (!imgTypes.Contains(tmpFilePath.ContentType))
                {
                    var json = new { result = 0, msg = "图片格式为jpg,png,bmp或PDF、xls、xlsx" };
                    msg = "公司:" + "-->上传出错。Error:" + json.msg;
                    addLog(0, 0, msg, VarKey.ServicePage.OnlineData.ToString());
                    return Json(json);
                }

                if (tmpFilePath.ContentLength > 0)
                {
                    //文件path
                    var tmppath = ToCreateDirectory(LoginUser.UserID, 1);
                    //文件名
                    string fileName = string.Format("{0}{1}",
                        Guid.NewGuid().ToString(), Path.GetExtension(tmpFilePath.FileName));
                    string filePath = Path.Combine(tmppath.prefixPath, fileName);
                    tmpFilePath.SaveAs(filePath);
                    var json = new { result = 1, msg = "上传成功", path = tmppath.jsonPath + "/" + fileName };

                    msg = "公司:" + "-->上传成功。Path:" + filePath;
                    addLog(0, 0, msg, VarKey.ServicePage.OnlineData.ToString());
                    return Json(json);
                }
            }
            catch (Exception)
            {
                msg = "公司:" + "-->上传出错。";
                addLog(0, 0, msg, VarKey.ServicePage.OnlineData.ToString());
                var json = new { result = 0, msg = "上传出错！" };
                return Json(json);
            }
            var json1 = new { result = 0, msg = "上传出错！" };
            return Json(json1);
        }

       public string getHtml(string html, string xpach = "//div[@class='divbody']")
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNode node = doc.DocumentNode;
                HtmlNode div = node.SelectNodes(xpach)[0];

                return div.OuterHtml;
            }
            catch (Exception ex)
            {
                return html;
            }

        }    
    }
}