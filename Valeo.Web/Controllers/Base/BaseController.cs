using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo;
using log4net;
using Valeo.Domain;
using Valeo.Service;
using System.Globalization;
using System.Threading;
using Valeo.Common;
using Valeo.Domain.Combox;
using System.IO;
using System.Configuration;
using System.Data;
using PetaPoco;
using System.Text.RegularExpressions;
using System.Drawing;
using Valeo.Domain.ParameterSetting;
using Valeo.Service.ParameterSetting;

namespace Valeo.Controllers
{

    [Localization]
    public class BaseController : Controller
    {
        UsersOperationhistoryService logservice = new UsersOperationhistoryService();
        ComParameterService _comPservice = new ComParameterService();
        public UserInfo LoginUser { get; set; }
        public Dictionary<string, ComParameterModel> _CPM { get; set; }

        public LogHelper log;
        public string _logoBaer64 = "";

        public BaseController()
        {
            log = new LogHelper(this.GetType());
            _CPM = _comPservice.getDicComP();
        }


        /// <summary>
        /// 设置多语言
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ActionResult Setlanguage(string lang)
        {

            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

                /// 把设置保存进cookie
                HttpCookie _cookie = new HttpCookie("Valeo.CurrentUICulture2", Thread.CurrentThread.CurrentUICulture.Name);
                _cookie.Expires = DateTime.MaxValue;
                Response.SetCookie(_cookie);
            }

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        /// <summary>
        ///  获取指定权限
        /// </summary>
        /// <param name="key"></param> 
        /// <returns>1:true, 0: false</returns>
        public bool GetRightValue(string key)
        {

            bool retValue = false;

            try
            {
                if (LoginUser == null) return retValue;

                //List<MemberRightVM> lstMemberRight = LoginUser.ListMemberRight.Where(t => t.RightKey.Equals(key)).ToList<MemberRightVM>();
                //if (lstMemberRight != null && lstMemberRight.Count > 0)
                //{
                //    retValue = lstMemberRight[0].RightValue == 1 ? true : false;
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// 执行控制器方法之前先执行该方法
        /// 获取自定义SessionId的值，然后从Memcache中取出
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_CPM.Count < 1)
            {
                _CPM = _comPservice.getDicComP();
            }

            bool isExt = false;

            var cookie = Request.Cookies["ValeoSessionIdback"];

            if (cookie != null)
            {
                string sessionId = cookie.Value;
                cookie.Expires = DateTime.Now.AddMinutes(60);
                Response.SetCookie(cookie);

                if (!string.IsNullOrEmpty(sessionId))
                {
                    var obj = Common.MemberHelper.GetCache(sessionId);
                    if (obj != null)
                    {
                        LoginUser = Valeo.Common.SerializerHelper.DeserializeToObject<UserInfo>(obj.ToString());
                        //设置Cache滑动过期时间为30分钟
                        Common.MemberHelper.SetCache(sessionId, Common.SerializerHelper.SerializerToString(LoginUser), new TimeSpan(0, 0, 60, 0));

                        //用户ID
                        ViewBag.Uid = LoginUser.UserID;
                        //用户名称
                        ViewBag.Uname = LoginUser.UserName;
                        isExt = true;
                    }
                    else
                    {
                        var httpCookie = Request.Cookies["ValeoVerificationIdback"];
                        if (httpCookie != null)
                        {
                            var verificationId = httpCookie.Value;
                            var userInfo = new MemberService().GetLogin(Encryption.Decode(verificationId));
                            if (userInfo != null)
                            {
                                Common.MemberHelper.SetCache(sessionId, Common.SerializerHelper.SerializerToString(userInfo), new TimeSpan(0, 0, 60, 0));
                                LoginUser = userInfo;
                                ViewBag.Uid = LoginUser.UserID;
                                //用户名称
                                ViewBag.Uname = LoginUser.UserName;
                                isExt = true;
                            }
                        }
                    }
                }
            }

            if (!isExt) //用户没登录
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                filterContext.HttpContext.Response.Write("<script> top.document.location.href='/Login/Index'</script>");
                filterContext.HttpContext.Response.End();
            }

            ImgToBase64String(@"/Content/img/logo.png");

            base.OnActionExecuting(filterContext);
        }


        /// <summary>
        ///  缩写信息（名称）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string GetShortName(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;

            try
            {
                retValue = original.Trim() + " ";

                if (convertFormat == Enums.ConvertFormat.Abb2Full)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortNameVM)
                    {
                        retValue = retValue.Replace(item.ShortName + " ", item.LongName + " ");
                    }

                }
                else if (convertFormat == Enums.ConvertFormat.Full2Abb)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortNameVM)
                    {
                        retValue = retValue.Replace(item.LongName + " ", item.ShortName + " ");

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retValue.Trim();
        }

        /// <summary>
        ///  缩写信息（地址）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string GetShortAddr(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;

            try
            {
                retValue = original.Trim() + " ";

                if (convertFormat == Enums.ConvertFormat.Abb2Full)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortAddrVM)
                    {
                        retValue = retValue.Replace(item.ShortName + " ", item.LongName + " ");
                    }

                }
                else if (convertFormat == Enums.ConvertFormat.Full2Abb)
                {
                    foreach (ShortNameVM item in LoginUser.ListShortAddrVM)
                    {
                        retValue = retValue.Replace(item.LongName + " ", item.ShortName + " ");

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retValue.Trim();
        }

        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// 操作分类(0:新建 1:修改 2:删除 3:查询 4:取消等等)
        /// </summary>
        /// <param name="Type">类别(0:用户日志 1:会员日志)</param>
        /// <param name="operateType">操作分类(0:新建 1:修改 2:删除 3:查询  4:禁用 5：启用 6：导出 7：导入 8:审核等等)</param>
        /// <param name="msg">日记说明</param>
        /// <param name="from">模块ID</param>
        public void addLog(int type, int operateType, string msg, string from)
        {
            try
            {
                LogRecordModel tmpLog = new LogRecordModel();
                tmpLog.AddDateTime = DateTime.Now;
                tmpLog.Content = msg;
                tmpLog.Form = from;
                tmpLog.OperateType = operateType;
                tmpLog.Type = type;
                tmpLog.UserID = LoginUser.UserID;
                logservice.Add(tmpLog);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
        /// <summary>
        /// 获取下拉框，内容
        /// </summary>
        /// <param name="e">Enum类型</param>
        /// <returns></returns>
        public List<ComboxListModel> getCombox(EnumCombox e)
        {
            ComboxService ComboxService = new ComboxService();
            var tmpModels = new List<ComboxListModel>();
            try
            {
                tmpModels = ComboxService.getList(e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return tmpModels;
        }
        /// <summary>
        /// 0.默认根文件目录:/Uploads
        /// 1.系统文件目录SystemFile: 
        /// 2.线上数据文件目录DataFile:
        /// 3.其它tmp/
        /// /2016/201608/会员ID/20160812, 
        /// </summary>
        /// <param name="path">会员ID/模块ID</param>
        /// <param name="isSystem">0:根目录 Server.MapPath，1：web config自定义目录SystemFile,2：web config自定义目录DataFile，3：临时文件.</param>
        /// <returns></returns>
        public PathsFile ToCreateDirectory(string path, int isSystem)
        {
            var tmpPath = new PathsFile() { prefixPath = "", jsonPath = "" };
            var tmpPathCreate = "";
            var tmpPathReturn = "";
            var tmpNow = DateTime.Now;
            try
            {
                switch (isSystem)
                {
                    case 0:
                        tmpPathReturn = @"/Uploads/" + tmpNow.ToString("yyyy") + @"/" + tmpNow.ToString("yyyyMM") + @"/" + path + @"/" + tmpNow.ToString("yyyyMMdd");
                        tmpPathCreate = Server.MapPath("/") + tmpPathReturn;
                        break;
                    case 1:
                        tmpPathReturn = @"/" + tmpNow.ToString("yyyy") + @"/" + tmpNow.ToString("yyyyMM") + @"/" + path + @"/" + tmpNow.ToString("yyyyMMdd");
                        tmpPathCreate = ConfigurationManager.AppSettings["Valeo.SystemFile"] + tmpPathReturn;
                        break;
                    case 2:
                        tmpPathReturn = @"/" + tmpNow.ToString("yyyy") + @"/" + tmpNow.ToString("yyyyMM") + @"/" + path + @"/" + tmpNow.ToString("yyyyMMdd");
                        tmpPathCreate = ConfigurationManager.AppSettings["Valeo.DataFile"] + tmpPathReturn;
                        break;
                    case 3:
                        tmpPathReturn = @"/tmp/" + tmpNow.ToString("yyyy") + @"/" + tmpNow.ToString("yyyyMM") + @"/" + path + @"/" + tmpNow.ToString("yyyyMMdd");
                        tmpPathCreate = ConfigurationManager.AppSettings["Valeo.DataFile"] + tmpPathReturn;
                        break;
                    default:
                        break;
                }
                if (!Directory.Exists(tmpPathCreate))
                {
                    Directory.CreateDirectory(tmpPathCreate);
                }
                tmpPath.prefixPath = tmpPathCreate;
                tmpPath.jsonPath = tmpPathReturn;
                return tmpPath;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                tmpPath.jsonPath = @"/Uploads/" + tmpNow.ToString("yyyy") + @"/" + tmpNow.ToString("yyyyMM") + @"/" + path + @"/" + tmpNow.ToString("yyyyMMdd");
                tmpPath.prefixPath = Server.MapPath("/") + tmpPathReturn;
                return tmpPath;
            }
        }


        #region 简繁拼音转换

        /// <summary>
        /// 文字转换（1:简体-拼音；2:简体-繁 3:繁-拼音）
        /// </summary>
        /// <param name="original"></param>
        /// <param name="convertFormat"></param>
        /// <returns></returns>
        public string[] GetWord3(string original, Enums.ConvertFormat convertFormat)
        {

            string retValue;
            string[] retbyte = new string[0];
            try
            {
                if (LoginUser == null)
                {
                    retbyte = new string[1];
                    retbyte[0] = original;
                    return retbyte;
                }
                retValue = original.Trim();
                switch (convertFormat)
                {
                    case Enums.ConvertFormat.Simp2Trad:
                        retbyte = new string[1];
                        //2:简体-繁
                        foreach (WordVM item in LoginUser.ListWord2VM)
                        {
                            retValue = retValue.Replace(item.WordKey, item.WordValue);
                            retbyte[0] = retValue;
                        }
                        break;

                    case Enums.ConvertFormat.Trad2Simp:
                        retbyte = new string[1];
                        //2:简体-繁
                        foreach (WordVM item in LoginUser.ListWord2VM)
                        {
                            retValue = retValue.Replace(item.WordValue, item.WordKey);
                            retbyte[0] = retValue;
                        }
                        break;

                    case Enums.ConvertFormat.Trad2Alph:
                        retbyte = new string[1];
                        //3:繁-拼音
                        foreach (WordVM item in LoginUser.ListWord3VM)
                        {
                            retValue = retValue.Replace(item.WordKey, item.WordValue);
                            retbyte[0] = retValue;
                        }
                        break;

                    case Enums.ConvertFormat.Simp2Alph:
                        retbyte = new string[1];
                        //1:简体-拼音
                        foreach (WordVM item in LoginUser.ListWord1VM)
                        {
                            retValue = retValue.Replace(item.WordKey, item.WordValue);
                            retbyte[0] = retValue;
                        }
                        break;

                    case Enums.ConvertFormat.Trad2AlphSP:
                        retbyte = new string[1];
                        retValue = retValue.Replace(" ", "").Trim();
                        retValue = retValue.Insert(1, " ");
                        retValue = retValue.Insert(retValue.LastIndexOf(retValue.Substring(0, 1)), " ");
                        //3:繁-拼音 X xx
                        foreach (WordVM item in LoginUser.ListWord3VM)
                        {
                            retValue = retValue.Replace(item.WordKey, item.WordValue);
                            retbyte[0] = retValue;
                        }
                        break;

                    case Enums.ConvertFormat.Simp2AlphSp:
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Type");
                        dt.Columns.Add("name");
                        string retValue2 = retValue;
                        foreach (WordVM item in LoginUser.ListWord2VM)
                        {
                            retValue2 = retValue2.Replace(item.WordKey, item.WordValue);
                        }
                        char[] arr = retValue.Trim().ToCharArray();
                        char[] arr2 = retValue2.Trim().ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            foreach (WordVM item in LoginUser.ListWord4VM)
                            {
                                if (arr[i].ToString() == item.WordKey || arr2[i].ToString() == item.WordKey || !Regex.Match(arr[i].ToString(), "[\u4e00-\u9fa5]").Success)
                                {
                                    DataRow dr1 = dt.NewRow();
                                    if (!Regex.Match(arr[i].ToString(), "[\u4e00-\u9fa5]").Success)
                                    {
                                        dr1[0] = i.ToString();
                                        dr1[1] = arr[i].ToString();
                                        dt.Rows.Add(dr1);
                                        break;
                                    }
                                    if (arr[i].ToString() == item.WordKey)
                                    {
                                        dr1[0] = i.ToString();
                                        dr1[1] = arr[i].ToString().Replace(item.WordKey, item.WordValue);
                                    }
                                    else if (arr2[i].ToString() == item.WordKey)
                                    {
                                        dr1[0] = i.ToString();
                                        dr1[1] = arr2[i].ToString().Replace(item.WordKey, item.WordValue);
                                    }
                                    dt.Rows.Add(dr1);
                                }
                            }
                        }
                        string[][] ss = new string[arr.Length][];

                        int xx = 0;
                        foreach (var items in arr)
                        {
                            DataRow[] dr = dt.Select("type='" + xx + "'");
                            int number = arr.Length - 1 > xx ? dr.Count() * 2 : dr.Count();
                            ss[xx] = new string[number];
                            int index = 0;
                            foreach (var item in dr)
                            {
                                ss[xx][index++] = item[1].ToString();
                                if (arr.Length - 1 > xx)
                                {
                                    ss[xx][index++] = item[1].ToString() + " ";
                                }
                            }
                            xx++;
                        }
                        if (dt.Rows.Count > 0)
                        {
                            retbyte = PaiLie(ss);
                        }
                        break;

                }

            }
            catch (Exception)
            {
                throw;
            }

            return retbyte;
        }


        public string[] PaiLie(string[][] array)
        {
            int indesx = 1;
            for (int i = 0; i < array.Length; i++)
            {
                indesx = indesx * array[i].Length;
            }
            string[] sbtye = new string[indesx];
            int num = sbtye.Length;
            int num1 = 1;
            for (int i = 0; i < array.Length; i++)
            {
                num = num / array[i].Length;
                int yx = -1;
                for (int x = 0; x < num1; x++)
                {
                    for (int y = 0; y < array[i].Length; y++)
                    {
                        for (int z = 0; z < num; z++)
                        {
                            yx += 1;
                            sbtye[yx] += array[i][y].ToString();
                        }
                    }
                }
                num1 = num1 * array[i].Length;
            }
            return sbtye;
        }

        #endregion


        /// <summary>
        ///   //图片 转为    base64编码的文本
        ///    ViewBag.logoBaser64 = ImgToBase64String(@"/Content/img/logo.png");
        /// </summary>
        /// <param name="Imagefilename"></param>
        /// <returns></returns>
        public string ImgToBase64String(string Imagefilename)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_logoBaer64))
                {
                    return _logoBaer64;
                }
                string filePathBase = "/";
                if (Server != null)
                {
                    filePathBase = Server.MapPath("/");
                }
                string _path = string.Concat(filePathBase, Imagefilename);
                Bitmap bmp = new Bitmap(_path);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);

                _logoBaer64 = strbaser64;

                return strbaser64;
            }
            catch (Exception ex)
            {
                log.Error(Imagefilename);
                log.Error(ex);
                return "";
            }
        }
        /// <summary>
        /// iType
        /// 
        /// 保存添加图片PDF
        /// 0:图片格式为jpg,png,bmp或PDF、xls、xlsx
        /// 1:PDF
        /// 2:xls、xlsx
        /// 
        /// pathType
        /// 0.默认根文件目录:/Uploads
        /// 1.系统文件目录SystemFile: 
        /// 2.线上数据文件目录DataFile:
        /// 3.其它tmp/
        /// </summary>
        /// <param name="tmpFilePath"></param>
        /// <param name="tmpFilePath"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadsFiles(HttpPostedFileBase postFiles, int iType = 0, int pathType = 3, string msgTitle = "")
        {
            var msg = "";
            try
            {
                HttpPostedFileBase tmpFilePath = postFiles;

                var dicTypes = new Dictionary<int, string[]>();
                var dicTypesMsg = new Dictionary<int, string>();
                string[] imgTypes0 = { "image/pjpeg", "image/jpeg", "image/png", "image/bmp", "application/pdf", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
                string[] imgTypes1 = { "application/pdf" };
                string[] imgTypes2 = { "application/vnd.ms-excel", "application/octet-stream", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };

                dicTypes.Add(0, imgTypes0);
                dicTypesMsg.Add(0, "文件只能是：1.图片格式为jpg,png,bmp \n 2.PDF \n 3.xls、xlsx 格式");
                dicTypes.Add(1, imgTypes1);
                dicTypesMsg.Add(1, "文件只能是：PDF 格式");
                dicTypes.Add(2, imgTypes2);
                dicTypesMsg.Add(2, "文件只能是：xls、xlsx 格式");

                var toCheckTypes = imgTypes0;
                var toCheckTypesMsg = "文件只能是：图片格式为jpg,png,bmp或PDF、xls、xlsx 格式";
                if (dicTypes.ContainsKey(iType))
                {
                    toCheckTypes = dicTypes[iType];
                    toCheckTypesMsg = dicTypesMsg[iType];
                }
                if (!toCheckTypes.Contains(tmpFilePath.ContentType))
                {
                    var json = new { result = 0, msg = toCheckTypesMsg };
                    msg = msgTitle + " 上传出错。Error:" + json.msg;
                    addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(json);
                }

                if (tmpFilePath.ContentLength > 0)
                {
                    //文件path
                    var tmppath = ToCreateDirectory(LoginUser.UserID, 2);
                    //文件名
                    string fileName = string.Format("{0}{1}",
                        Guid.NewGuid().ToString(), Path.GetExtension(tmpFilePath.FileName));
                    string filePath = Path.Combine(tmppath.prefixPath, fileName);
                    tmpFilePath.SaveAs(filePath);
                    var json = new { result = 1, msg = "上传成功", path = tmppath.jsonPath + "/" + fileName };

                    msg = msgTitle + " 上传成功。Path:" + filePath;
                    addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                    return Json(json);
                }
            }
            catch (Exception)
            {
                msg = msgTitle + " 上传出错。";
                addLog(0, 0, msg, VarKey.ServicePage.ParamManager.ToString());
                var json = new { result = 0, msg = "上传出错！" };
                return Json(json);
            }
            var json1 = new { result = 0, msg = "上传出错！" };
            return Json(json1);
        }
    }
}
