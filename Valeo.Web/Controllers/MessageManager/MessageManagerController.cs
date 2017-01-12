using App.Lang;
using Valeo.Domain;
using Valeo.Domain.Enum;
using Valeo.Domain.Message;
using Valeo.Lang;
using Valeo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.MessageManager
{
    public class MessageManagerController : BaseController
    {
        private readonly MessageService _messageService = new MessageService();
        public Valeo.Service.MemberListService Service = new Valeo.Service.MemberListService();
        // GET: MessageManager
        #region 查询处理
        public ActionResult Index()
        {
            //得到权限
            ViewBag.RoleInfo = LoginUser.ListUserAuthVM.Where(o => o.Mod_id == "MessageManager").ToList();
            return View();
        }
        /// <summary>
        /// 初始化消息提醒列表
        /// </summary>
        /// <param name="message"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult InitDataMessge(MessageVM message, long page = 1, long rows = 10, string sort = null, string order = "asc")
        {
            string userId = ViewBag.Uid;
            var messagePage = _messageService.getMessagePage(message, page, rows, userId, sort, order);
            var messagetList = messagePage.Items;
            var result = new
            {
                total = messagePage.TotalItems,
                rows = messagetList
            };
            var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_024;
            addLog(0, 3, msg, VarKey.ServicePage.MessageManager.ToString());
            return Json(result);
        }
        #endregion

        #region 新增处理
        public ActionResult Add()
        {
            return View("_Add");
        }

        /// <summary>
        /// 添加数据到 消息和接收人表
        /// </summary>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public JsonResult AddSave(MessageVM messageVM)
        {
            
            MessageModel messageModel = new MessageModel();
            try
            {
                messageModel.MessageTitle = messageVM.MessageTitle;
                messageModel.Message = messageVM.Message;
                messageModel.SendTime = (DateTime.Now).ToString();
                messageModel.UserID = ViewBag.Uid;
                messageModel.Type = 1;
                messageModel.adduser = ViewBag.Uname;
                messageModel.addtime = (DateTime.Now).ToString();
                string[] arr = messageVM.MemberName.TrimEnd(';').TrimStart(';').Split(';');
                bool flag = false;
                string flagName = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    string temp = arr[i];
                    int count = 0;
                    for (int j = 0; j < arr.Length; j++)
                    {
                        string temp2 = arr[j];
                        //有重复值就count+1 
                        if (temp == temp2)
                        {
                            
                            count++;
                            if (count >= 2)
                            {
                                flagName = temp;
                            }
                        }
                    }
                    //由于中间又一次会跟自己本身比较所有这里要判断count>=2
                    if (count >= 2)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    return Json(new { result = 0, Msg = BaseRes.MML_COL_032 + flagName + BaseRes.MML_COL_033 });//"添加失败!"
                }



                string message = "";
                for (int i = 0; i < arr.Length; i++)
                {
                   bool isMember=_messageService.GetIsMember(arr[i],ref message);
                   if (!isMember)
                   {
                       return Json(new { result = 0, Msg = BaseRes.MML_COL_032 + message + BaseRes.MML_COL_034 });//"添加失败!"
                   }
                }

                //添加数据到消息表
               long messageId = _messageService.AddMessage(messageModel);

              
               
               for (int i = 0; i < arr.Length; i++)
               {
                   RecipientModel recModel = new RecipientModel();
                   recModel.MessageID = messageId.ToString();
                   bool isMember = _messageService.GetIsMember(arr[i], ref message);
                   if (isMember)
                   {
                       recModel.MemberID = message;
                   }
                   
                   //添加数据到接收人
                   long recipientId = _messageService.AddRecipient(recModel);
               }


               var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_029;
               addLog(0, 0, msg, VarKey.ServicePage.MessageManager.ToString());
               
                return Json(new { result = 1, Msg = BaseRes.USE_MSG_013 });//"添加成功!"
            }
            catch (Exception)
            {
                var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_030;
                addLog(0, 0, msg, VarKey.ServicePage.MessageManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.SPS_MSG_003 });//"添加失败!"
            }
          
        }
        #endregion

        #region 修改处理
        public ActionResult Edit(string messageID, bool isEdit = true)
        {
            ViewBag.IsEdit = isEdit;
            MessageVM mvModel = new MessageVM();
            List<MessageVM> lstMessge=_messageService.getMessageList(messageID);
            for (int i = 0; i < lstMessge.Count; i++)
            {
                mvModel = lstMessge[i];
            }
            return View("_Edit", mvModel);
        }
        
        /// <summary>
        /// 修改消息表和接收人表
        /// </summary>
        /// <param name="messgeVM"></param>
        /// <returns></returns>
        public JsonResult EditMessage(MessageVM messgeVM)
        {
            try
            {
                messgeVM.upduser = ViewBag.Uname;
                messgeVM.updtime = (DateTime.Now).ToString();
                string userId = ViewBag.Uid;
                _messageService.EditMessage(messgeVM, userId);
                var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_025;
                addLog(0, 1, msg, VarKey.ServicePage.MessageManager.ToString());
                 return Json(new { result = 1, Msg = BaseRes.USE_MSG_015 });// "修改成功!"
            }
            catch (Exception)
            {
                var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_026;
                addLog(0, 1, msg, VarKey.ServicePage.MessageManager.ToString());
                 return Json(new { result = 0, Msg = BaseRes.USE_MSG_016 });//"错误，请稍后在试!" 
            }
        }
        #endregion

        #region 删除处理
        public JsonResult Deletes(string[] messageIds)
        {
            if (messageIds.Length > 0)
            {
                try
                {
                    _messageService.DeleteMessage(messageIds);
                    var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_028;
                    addLog(0,2, msg, VarKey.ServicePage.MessageManager.ToString());
                    return Json(new { result = 1 });//""
                }
                catch (Exception)
                {
                    var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_027;
                    addLog(0, 2, msg, VarKey.ServicePage.MessageManager.ToString());
                    return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });//"删除失败!" 
                }
            }
            else
            {
                var msg = BaseRes.MML_COL_031 + BaseRes.MGC_CTL_027;
                addLog(0, 2, msg, VarKey.ServicePage.MessageManager.ToString());
                return Json(new { result = 0, Msg = BaseRes.COM_MSG_DEL_FAIL });//""删除失败!
            }
        }
        #endregion

        public ActionResult GetMember(string functionName)
        {
            //枚举多语言前缀
            string strCode = "COM_ENUM_";
            ViewBag.FunctionName = functionName;
            //级别下拉列表
            var listMemberGrade = Service.GetMemberGradeList();
           
            ViewBag.ListMemberGrade = listMemberGrade;

            //是否活跃下拉列表
            var listActivity = new List<SelectListItem>
            {
                new SelectListItem {Text = BaseRes.COM_CTL_SLCTALL, Value = ""}
            };
            listActivity.AddRange(from object item in Enum.GetValues(typeof(EnumCommon.ActivityEnum)) select new SelectListItem { Text = COMKey.COM(strCode + item.ToString()), Value = Convert.ToInt32(item).ToString() });
            ViewBag.ListActivity = listActivity;

            //启用禁用下拉列表
            var listState = new List<SelectListItem> { new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = "" } };
            listState.AddRange(from object item in Enum.GetValues(typeof(EnumCommon.StateEnum)) select new SelectListItem { Text = COMKey.COM(strCode + item.ToString()), Value = Convert.ToInt32(item).ToString() });
            ViewBag.ListState = listState;
            return View("_GetMember");
        }
    }
}