using App.Lang;
using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.Enum;
using Valeo.Domain.Models;
using Valeo.Lang;
using Valeo.Service;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.Main
{
    public class RegisterController : Controller
    {

        private RegisterService RegisterS;
        private MasterService MasterS;

        #region 【查询处理】

        /// <summary>
        /// 获取所有会员
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMember(int id)
        {
            try
            {
                //if (id==null)
                //{
                //    id = 0;
                //}
                RegisterS = new RegisterService();
                var jsonModel = RegisterS.GetAllMember(id);
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取用户出错"), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Register
        public ActionResult Index()
        {

            MasterS = new MasterService();
            SelectListItem em = new SelectListItem();
            List<SelectListItem> Countryli = new List<SelectListItem>();//国家
            List<SelectListItem> Calledli = new List<SelectListItem>();//称呼
            List<SelectListItem> Paymentli = new List<SelectListItem>(); //支付方式
            List<CountryModel> lim = MasterS.GetCountryList();
            //国家
            for (int i = 0; i < lim.Count(); i++)
            {
                em = new SelectListItem();
                em.Value = lim[i].Countryid.ToString();
                em.Text = COMKey.COM(lim[i].LanguageCode);
                Countryli.Add(em);
            }
            ViewBag.CountryJSON = Countryli;

            //称呼
            foreach (var value in Enum.GetValues(typeof(EnumCommon.CalledEnum)))
            {
                em = new SelectListItem();
                em.Text = value.ToString();
                em.Value =Convert.ToInt32(value).ToString();
                Calledli.Add(em);
            }
            ViewBag.CalledJSON = Calledli;

            //支付方式
            foreach (var value in Enum.GetValues(typeof(EnumCommon.PaymentTypeEnum)))
            {
                string strCode = "COM_CTL_ENUM_PAYMENT_";
                em = new SelectListItem();
                em.Text = COMKey.COM(strCode + value.ToString());
                em.Value = Convert.ToInt32(value).ToString();
                Paymentli.Add(em);
            }
            ViewBag.PaymentJSON = Paymentli;


            return View(ViewBag);
        }
        #endregion

        #region 【新增处理】


        /// <summary>
        /// 新增个人
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddIndividual(string memberJson)
        {

            List<HttpPostedFileBase> ListHPF = new List<HttpPostedFileBase>();
            //int index = -1;//HttpPostedFileBase中取出图片索引
            try
            {
                RegisterS = new RegisterService();
                MemberModel Member = new MemberModel();
                DataTable dtMember = JsonHandler.DataTableJson(memberJson);

                if (string.IsNullOrEmpty(dtMember.Rows[0]["MemberName"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, "账号不能为空"), JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(dtMember.Rows[0]["Password"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, "密码不能为空"), JsonRequestBehavior.AllowGet);
                }

                //判断重复
                if (RegisterS.GetRepeat(dtMember.Rows[0]["MemberName"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_EXIST), JsonRequestBehavior.AllowGet);
                }

                #region 绑定数据到 MemberModel
                for (int i = 0; i < dtMember.Rows.Count; i++)
                {
                    var dtindex = dtMember.Rows[i];
                    Member.MemberName = dtindex["MemberName"].ToString();
                    Member.Password =Encryption.Encode( dtindex["Password"].ToString());
                    //Member.Type = Convert.ToInt32(Enums.MemberTypeEnum.INDIVIDUAL).ToString();
                    Member.Surname = dtindex["Surname"].ToString();
                    Member.GivenNames = dtindex["GivenNames"].ToString();
                    Member.FullName_Tm = dtindex["Name_Tm"].ToString();
                    Member.FullName_Cn = dtindex["Name_Cn"].ToString();
                    Member.SeatNO = dtindex["SeatNO"].ToString();
                    Member.Floor = dtindex["Floor"].ToString();
                    Member.RoomNO = dtindex["RoomNO"].ToString();
                    Member.BuildName = dtindex["BuildName"].ToString();
                    Member.Salutation = dtindex["Salutation"].ToString();
                    Member.StreetNumber = dtindex["StreetNumber"].ToString();
                    Member.Street = dtindex["Street"].ToString();
                    Member.HouseNO = dtindex["HouseNO"].ToString();
                    Member.City = dtindex["City"].ToString();
                    Member.PostalCode = dtindex["PostalCode"].ToString();
                    Member.CountryID = dtindex["CountryID"].ToString();
                    Member.OfficeTel = dtindex["OfficeTel"].ToString();
                    Member.HomeTel = dtindex["HomeTel"].ToString();
                    Member.MobilePhone = dtindex["MobilePhone"].ToString();
                    Member.Email = dtindex["Email"].ToString();
                    Member.PaymentWay = dtindex["PaymentWay"].ToString();
                    //Member.PicPath1 = dtindex["PicPath1"].ToString();
                    //Member.PicPath2 = dtindex["PicPath2"].ToString();
                    Member.Purpose1 = dtindex["Purpose1"].ToString();
                    Member.Purpose2 = dtindex["Purpose2"].ToString();
                    Member.Purpose3 = dtindex["Purpose3"].ToString();
                    Member.Purpose4 = dtindex["Purpose4"].ToString();
                    Member.Purpose5 = dtindex["Purpose5"].ToString();
                    Member.Purpose6 = dtindex["Purpose6"].ToString();
                    Member.Purpose7 = dtindex["Purpose7"].ToString();
                    Member.Pathway1 = dtindex["Pathway1"].ToString();
                    Member.Pathway2 = dtindex["Pathway2"].ToString();
                    Member.Pathway3 = dtindex["Pathway3"].ToString();
                    Member.Pathway4 = dtindex["Pathway4"].ToString();
                    Member.Pathway5 = dtindex["Pathway5"].ToString();
                    Member.Pathway6 = dtindex["Pathway6"].ToString();
                    Member.addtime = DateTime.Now;
                }
                #endregion

                string MemberID;
                if (RegisterS.AddIndividual(Member, out MemberID))
                {
                    return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_SUC), JsonRequestBehavior.AllowGet);
                //    #region 保存图片
                //    HttpPostedFileBase HFile;
                //    List<int> liImageIndex = new List<int>();

                //    //营业执照
                //    if (dtMember.Rows[0]["PicPath1"].ToString() != "")
                //    {
                //        index += 1;
                //        HFile = Request.Files[index];
                //        ListHPF.Add(HFile);
                //        liImageIndex.Add(1);
                //    }
                //    else
                //    {
                //        liImageIndex.Add(0);
                //    }
                //    //其它文件
                //    if (dtMember.Rows[0]["PicPath2"].ToString() != "")
                //    {
                //        index += 1;
                //        HFile = Request.Files[index];
                //        ListHPF.Add(HFile);
                //        liImageIndex.Add(2);
                //    }
                //    FileUpload fu = new FileUpload();
                //    List<string> liImage = fu.UploadFile(ListHPF, 0, MemberID);

                //    #endregion

                //    if (RegisterS.UpdateIndividual(MemberID, liImage, liImageIndex))
                //    {
                //        return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_SUC), JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);
                //    }
                }
                else
                {
                    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddCompany(string memberJson, string companyJson, string contactPersonJson)
        {

            //int index = -1;//HttpPostedFileBase中取出图片索引
            List<HttpPostedFileBase> ListHPF = new List<HttpPostedFileBase>();
            try
            {
                RegisterS = new RegisterService();
                DataTable dtMember = JsonHandler.DataTableJson(memberJson);
                DataTable dtCompany = JsonHandler.DataTableJson(companyJson);
                DataTable dtContactPerson = JsonHandler.DataTableJson(contactPersonJson);

                if (string.IsNullOrEmpty(dtMember.Rows[0]["MemberName"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, "账号不能为空"), JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(dtMember.Rows[0]["Password"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, "密码不能为空"), JsonRequestBehavior.AllowGet);
                }

                //判断重复
                if (RegisterS.GetRepeat(dtMember.Rows[0]["MemberName"].ToString()))
                {
                    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_EXIST), JsonRequestBehavior.AllowGet);
                }

                #region 绑定值
                //会员信息
                MemberModel MBModel = new MemberModel();
                var dtMB=dtMember.Rows[0];
                MBModel.MemberName = dtMB["MemberName"].ToString();
                MBModel.Password = Encryption.Encode(dtMB["Password"].ToString());
                //MBModel.Type = Convert.ToInt32(Enums.MemberTypeEnum.COMPANY).ToString();
                MBModel.OfficeTel = dtMB["OfficeTel"].ToString();
                MBModel.HomeTel = dtMB["HomeTel"].ToString();
                MBModel.MobilePhone = dtMB["MobilePhone"].ToString();
                MBModel.Email = dtMB["Email"].ToString();
                MBModel.PaymentWay = dtMB["PaymentWay"].ToString();
                MBModel.Purpose1 = dtMB["Purpose1"].ToString();
                MBModel.Purpose2 = dtMB["Purpose2"].ToString();
                MBModel.Purpose3 = dtMB["Purpose3"].ToString();
                MBModel.Purpose4 = dtMB["Purpose4"].ToString();
                MBModel.Purpose5 = dtMB["Purpose5"].ToString();
                MBModel.Purpose6 = dtMB["Purpose6"].ToString();
                MBModel.Purpose7 = dtMB["Purpose7"].ToString();
                MBModel.Pathway1 = dtMB["Pathway1"].ToString();
                MBModel.Pathway2 = dtMB["Pathway2"].ToString();
                MBModel.Pathway3 = dtMB["Pathway3"].ToString();
                MBModel.Pathway4 = dtMB["Pathway4"].ToString();
                MBModel.Pathway5 = dtMB["Pathway5"].ToString();
                MBModel.Pathway6 = dtMB["Pathway6"].ToString();
                MBModel.addtime = DateTime.Now;
                //公司信息
                MemberComanyModel MCModel = new MemberComanyModel();
                var dtC = dtCompany.Rows[0];
                MCModel.FullName_En = dtC["CompanyName_En"].ToString();
                MCModel.FullName_Tm = dtC["CompanyName_Tm"].ToString();
                MCModel.FullName_Cn = dtC["CompanyName_Cn"].ToString();
                MCModel.BusinessType = dtC["BusinessType"].ToString();
                MCModel.CIBRNO = dtC["CIBRNO"].ToString();
                MCModel.SeatNO = dtC["SeatNO"].ToString();
                MCModel.Floor = dtC["Floor"].ToString();
                MCModel.RoomNO = dtC["RoomNO"].ToString();
                MCModel.BuildName = dtC["BuildName"].ToString();
                MCModel.StreetNumber = dtC["StreetNumber"].ToString();
                MCModel.Street = dtC["Street"].ToString();
                MCModel.HouseNO = dtC["HouseNO"].ToString();
                MCModel.City = dtC["City"].ToString();
                MCModel.PostalCode = dtC["PostalCode"].ToString();
                MCModel.CountryID = dtC["CountryID"].ToString();
                //MCModel.PicPath1 = dtC["PicPath1"].ToString();
                //MCModel.PicPath2 = dtC["PicPath2"].ToString();
                //联系人信息
                List<ContactPersonModel> liCP = new List<ContactPersonModel>();
                for (int i = 0; i < dtContactPerson.Rows.Count; i++)
                {
                    ContactPersonModel CPModel = new ContactPersonModel();
                    var dtCP = dtContactPerson.Rows[i];
                    CPModel.Surname = dtCP["Surname"].ToString();
                    CPModel.GivenNames = dtCP["GivenNames"].ToString();
                    CPModel.Salutation = dtCP["Salutation"].ToString();
                    CPModel.FullName_Tm = dtCP["Name_Tm"].ToString();
                    CPModel.FullName_Cn = dtCP["Name_Cn"].ToString();
                    CPModel.Department = dtCP["Department"].ToString();
                    CPModel.Position = dtCP["Position"].ToString();
                    CPModel.OfficeTel1 = dtCP["OfficeTel1"].ToString();
                    CPModel.OfficeTel2 = dtCP["OfficeTel2"].ToString();
                    CPModel.MobilePhone = dtCP["MobilePhone"].ToString();
                    CPModel.Fax = dtCP["Fax"].ToString();
                    CPModel.Email = dtCP["Email"].ToString();
                    //CPModel.PicPath1 = dtCP["CPPicPath1"].ToString();
                    //CPModel.PicPath2 = dtCP["CPPicPath2"].ToString();
                    liCP.Add(CPModel);
                }
                #endregion
                string MemberID;
                if (RegisterS.AddCompany(MBModel,MCModel,liCP,out MemberID))
                {
                    return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_SUC), JsonRequestBehavior.AllowGet);
                    //HttpPostedFileBase HFile;
                    //List<int> liImageIndex = new List<int>();
                    List<ContactPersonModel> liCPM = new List<ContactPersonModel>();
                    //#region  保存图片
                    //for (int i = 0; i < dtContactPerson.Rows.Count; i++)
                    //{
                    //    //名片
                    //    if (dtContactPerson.Rows[i]["CPPicPath1"].ToString() != "")
                    //    {
                    //        index += 1;
                    //        HFile = Request.Files[index];
                    //        ListHPF.Add(HFile);
                    //        liImageIndex.Add(1);
                    //    }
                    //    else
                    //    {
                    //        liImageIndex.Add(3);
                    //    }

                    //    //其它文件
                    //    if (dtContactPerson.Rows[i]["CPPicPath2"].ToString() != "")
                    //    {
                    //        index += 1;
                    //        HFile = Request.Files[index];
                    //        ListHPF.Add(HFile);
                    //        liImageIndex.Add(2);
                    //    }
                    //}

                    ////营业执照
                    //if (dtCompany.Rows[0]["PicPath1"].ToString() != "")
                    //{
                    //    index += 1;
                    //    HFile = Request.Files[index];
                    //    ListHPF.Add(HFile);
                    //    liImageIndex.Add(4);
                    //}
                    //else
                    //{
                    //    liImageIndex.Add(6);
                    //}
                    ////其它文件
                    //if (dtCompany.Rows[0]["PicPath2"].ToString() != "")
                    //{
                    //    index += 1;
                    //    HFile = Request.Files[index];
                    //    ListHPF.Add(HFile);
                    //    liImageIndex.Add(5);
                    //}

                    //FileUpload fu = new FileUpload();
                    //List<string> liImage = fu.UploadFile(ListHPF, 0, MemberID);

                    //#endregion

                    //if (RegisterS.UpdateCompany(MemberID, liImage, liImageIndex))
                    //{
                    //    return Json(JsonHandler.CreateMessage(1, BaseRes.COM_MSG_ADD_SUC), JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);
                    //}
                }
                else
                {
                    return Json(JsonHandler.CreateMessage(0, BaseRes.COM_MSG_ADD_FAIL), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region  共通

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="index">图片索引</param>
        /// <param name="FilePath">路径</param>
        /// <param name="imgName">图片名</param>
        /// <returns></returns>
        //public string FileImg(int index,string  imgName)
        //{
        //    try
        //    {// + ".jpg"
        //        List<HttpPostedFileBase> List = new List<HttpPostedFileBase>();
        //        HttpPostedFileBase HFile = Request.Files[index];
        //        int row = new FileUpload().UploadFile(HFile, imgName);
        //        if (row == 0)
        //        {
        //            return "上传图片异常";
        //        }
        //        else if (row == 2)
        //        {
        //            return "上传文件格式错误";
        //        }
        //        else {
        //            return "";
        //        }

        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
        #endregion
    }
}