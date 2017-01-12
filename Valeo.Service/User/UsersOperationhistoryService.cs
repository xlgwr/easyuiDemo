using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.User;
using Valeo.Lang;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Valeo.Service
{
    /// <summary>
    /// 用户操作记录
    /// </summary>
    public class UsersOperationhistoryService : BaseService
    {


        /// <summary>
        /// 查询分页操作记录表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<LogRecordSearchModel> GetListPages(long page, long rows, string sort, string order, LogRecordSearchModel condition)
        {
            List<Object> listPara = new List<object>();

            //var sql = new Sql().Append(@"select * from (select y.UserGrade,x.* from (SELECT a.Type,");
            //sql.Append(@" (case a.Type when 1 then CAST(a.MemberID as nvarchar(32)) else a.UserID end) as UserID,");
            //sql.Append(@" (case a.Type when 1 then c.MemberName else b.UserName end) as UserName,");
            //sql.Append(@" (case a.Type when 1 then c.MemberGradeId else b.UserGradeID end) as UserGradeID,");
            //sql.Append(@" (case a.OperateType when 0 then '新建' when 1 then '修改' when 2 then '删除' when 3 then '查询'else '其他' end) as OperateTypeDesc,");
            //sql.Append(@" a.Form,a.OperateType,a.Content,a.AddDateTime from t_LogRecord a LEFT JOIN m_User b on a.UserID=b.UserID ");
            //sql.Append(@" left JOIN m_Member c on a.MemberID=c.MemberID) x left JOIN m_UserGrade y on x.UserGradeID=y.UserGradeID) m ");

            var sql = new Sql().Append(@"select * from (select y.UserGrade,x.* from (SELECT a.LogID,a.Type,");
            sql.Append(@" a.UserID,");
            sql.Append(@" b.UserName,");
            sql.Append(@" b.UserGradeID,");
            sql.Append(@" (case a.OperateType when 0 then '新增' when 1 then '修改' when 2 then '删除' when 3 then '查询'else '其他' end) as OperateTypeDesc,");
            sql.Append(@" a.Form,a.OperateType,a.Content,a.AddDateTime from t_LogRecord a LEFT JOIN m_User b on a.UserID=b.UserID ");
            sql.Append(@" ) x left JOIN m_UserGrade y on x.UserGradeID=y.UserGradeID) m ");

            sql.Append(" Where 1=1 and m.Type=0 ");
            //用户名称
            if (!string.IsNullOrEmpty(condition.UserName))
            {
                sql.Append(string.Format(" and m.UserName  =  '{0}'", condition.UserName));

            }
            //用户级别
            if (condition.UserGradeID > 0)
            {
                sql.Append(string.Format(" and m.UserGradeID  =  '{0}'  ", condition.UserGradeID));
            }
            //访问页面
            if (!string.IsNullOrEmpty(condition.Form) && !condition.Form.Equals("0"))
            {
                sql.Append(string.Format(" and m.Form  =  '{0}'  ", condition.Form));
            }

            if (!string.IsNullOrEmpty(condition.AddDateTimeB))
            {
                sql.Append(string.Format(" and  m.AddDateTime>='{0}'  ", condition.AddDateTimeB));
            }

            if (!string.IsNullOrEmpty(condition.AddDateTimeE))
            {
                var td = DateTime.Parse(condition.AddDateTimeE);
                sql.Append(string.Format(" and m.AddDateTime<= '{0}'  ",td.ToString("yyyy/MM/dd 23:59:59")));
            }
            //默认显示三天内的使用记录
            if (string.IsNullOrEmpty(condition.AddDateTimeB) && string.IsNullOrEmpty(condition.AddDateTimeE))
            {
                sql.Append(string.Format(" and  m.AddDateTime>= '{0}'  ", DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd HH:mm:ss")));
            }


            if (!string.IsNullOrEmpty(sort))
            {
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" m.AddDateTime DESC ");
            }

            return db.Page<LogRecordSearchModel>(page, rows, sql);
        }

        public void Add(LogRecordModel model)
        {
            db.Insert("t_LogRecord", "LogID", true, model);
        }

        /// <summary>
        /// 类别(0:用户日志 1:会员日志)
        /// 操作分类(0:新建 1:修改 2:删除 3:查询等等)
        /// </summary>
        /// <param name="Type">类别(0:用户日志 1:会员日志)</param>
        /// <param name="operateType">操作分类(0:新建 1:修改 2:删除 3:查询等等)</param>
        /// <param name="msg">日记说明</param>
        /// <param name="from">模块ID</param>
        public void addLog(string UserID, int type, int operateType, string msg, string from)
        {
            try
            {
                LogRecordModel tmpLog = new LogRecordModel();
                tmpLog.AddDateTime = DateTime.Now;
                tmpLog.Content = msg;
                tmpLog.Form = from;
                tmpLog.OperateType = operateType;
                tmpLog.Type = type;
                tmpLog.UserID = UserID;
                Add(tmpLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取所有后台界面
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> getServicePageSelectList()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = VarKey.ServicePage.All.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LGN_CTL_008, Value = VarKey.ServicePage.Login.ToString() });
            //valeo
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_200, Value = VarKey.ServicePage.DeliveryManage.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_300, Value = VarKey.ServicePage.DeliveryStatusManage.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_500, Value = VarKey.ServicePage.MaterialTraceability.ToString() });

            //selectItems.Add(new SelectListItem { Text = BaseRes.MLO_COL_019, Value = VarKey.ServicePage.Register.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_008, Value = VarKey.ServicePage.MemberManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_009, Value = VarKey.ServicePage.OffineSearch.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_010, Value = VarKey.ServicePage.OrderManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_011, Value = VarKey.ServicePage.TaskManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_012, Value = VarKey.ServicePage.ProductManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_013, Value = VarKey.ServicePage.ReportManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_014, Value = VarKey.ServicePage.FinanceManager.ToString() });
            //新增
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_110, Value = VarKey.ServicePage.FinanceReport.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_015, Value = VarKey.ServicePage.UserInfoManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_016, Value = VarKey.ServicePage.CollectData.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_017, Value = VarKey.ServicePage.OnlineData.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_018, Value = VarKey.ServicePage.EmailManager.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_019, Value = VarKey.ServicePage.ParamManager.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_020, Value = VarKey.ServicePage.EditWeb.ToString() });
            //selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_021, Value = VarKey.ServicePage.MessageManager.ToString() });
            selectItems.Add(new SelectListItem { Text = BaseRes.LOG_SEA_022, Value = VarKey.ServicePage.Exit.ToString() });

            return selectItems;
        }
        /// <summary>
        /// 删除多个id
        /// </summary>
        /// <param name="id">id1,id2,id3</param>
        /// <returns></returns>
        public bool Deletes(string id)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //删除
                    Sql sql = new Sql().Append(string.Format(@"delete from t_LogRecord where LogID in ({0})", id));
                    var result = db.Execute(sql);

                    scope.Complete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }



    }
}
