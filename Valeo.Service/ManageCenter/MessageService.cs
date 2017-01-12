using System;
using System.Collections.Generic;
using Valeo.Domain;
using PetaPoco;
using Valeo.Domain.Message;
using Valeo.Common;

namespace Valeo.Service
{
    /// <summary>
    /// 消息信息服务类
    /// </summary>
    public class MessageService : BaseService
    {
      
        #region 【查询处理】

        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<MessageVM> GetMessageList(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                condition = "1 = 1";
            }

            Sql sql =new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID 
                    WHERE {0} ORDER BY MSG.SendTime DESC", condition);

            List<MessageVM> list = db.Fetch<MessageVM>(sql);

            return list;
        }

        public bool GetIsMember(string memberName,ref string message)
        {
            Sql sql = new Sql();
            sql.Append(@"SELECT MemberID from m_Member where MemberName=@0", memberName);
            List<MemberModel> memberList = db.Fetch<MemberModel>(sql);
            if (memberList.Count!=0)
            {
                if (memberList.Count>1)
                {
                    message = memberName;
                    return false;
                }
                else
                {
                    message = memberList[0].MemberID;
                    return true;
                }
                
            }
            else
            {
                message = memberName;
                return false;
            }

        } 

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MessageVM> GetPageMessageList(GridPager pager, string fromdate = ""
                                                    , string todate = "", string msgType = "")
        {
             

            Sql sql = new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID ");


            if (!string.IsNullOrEmpty(fromdate))
            {
                sql.Where(" MSG.SendTime >= @0  ", fromdate);
            }

            if (!string.IsNullOrEmpty(todate))
            {
                sql.Where(" MSG.SendTime <= @0  ", todate);
            }

            if (!string.IsNullOrEmpty(msgType) && !msgType.Equals(VarKey.Comon.ItemAll))
            {
                //sql.Where(" MSG.SendTime <= @0  ", todate);
            }
  
            sql.Append(" ORDER BY MSG.SendTime DESC ");

            var list = db.Page<MessageVM>(pager.page, pager.pageSize, sql); 
            return list;
        }

        /// <summary>
        /// 按条件取得消息信息列表(分页数据)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MessageVM> GetPageMessageList(int startIndex, int pageSize, string condition = "")
        {


            Sql sql = new Sql().Append(@" 
                    SELECT   REC.MemberID, REC.MessageID, REC.IsRead, MSG.UserID, MSG.MessageTitle, MSG.Message, MSG.SendTime
                    FROM    dbo.t_Recipient AS REC LEFT OUTER JOIN
                            dbo.t_Message AS MSG ON REC.MessageID = MSG.MessageID " );
 
            sql.Append(" ORDER BY MSG.SendTime DESC ");

            var list = db.Page<MessageVM>(startIndex, pageSize, sql);
             
            return list;
        }


        /// <summary>
        /// 查询并分页消息表
        /// </summary>
        /// <param name="messageModel"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Page<MessageVM> getMessagePage(MessageVM messageModel, long page, long rows, string userName, string sort, string order)
        {
            try
            {
                var sql = new Sql().Append(@"SELECT m_Member.MemberID,m_Member.MemberName,
                t_Message.MessageID,t_Message.MessageTitle,t_Message.Message, CONVERT(varchar(20), t_Message.SendTime, 120) AS SendTime FROM t_Recipient 
                RIGHT JOIN m_Member on m_Member.MemberID=t_Recipient.MemberID
                RIGHT JOIN t_Message on t_Message.MessageID=t_Recipient.MessageID");
                //if(!string.IsNullOrEmpty(userName)){
                //     sql.Where("t_Message.UserID=@0",userName);
                //}
                if (!string.IsNullOrEmpty(messageModel.SendTimeS))
                {
                    sql.Where("t_Message.SendTime>@0 ", messageModel.SendTimeS + " 00:00:00:000");
                }
                if (!string.IsNullOrEmpty(messageModel.SendTimeE))
                {
                    sql.Where("t_Message.SendTime<@0 ", messageModel.SendTimeE + " 23:59:59:999");
                }
                if (!string.IsNullOrEmpty(messageModel.MessageTitle))
                {
                    sql.Where("  t_Message.MessageTitle like @0",  messageModel.MessageTitle + "%");
                }

                //if(!string.IsNullOrEmpty(messageModel.SendTime)){
                //    sql.Append(" OR t_Message.SendTime=@0",messageModel.SendTime);
                //}
                if (string.IsNullOrWhiteSpace(sort))
                {
                    sql.Append("ORDER BY t_Message.SendTime DESC");
                }
                else
                {
                    sql.OrderBy(sort + " " + order);
                }
                
                return db.Page<MessageVM>(page, rows, sql);
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        /// <summary>
        /// 查询消息表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public List<MessageVM> getMessageList(string messageId)
        {
            List<MessageVM> lstMessge = new List<MessageVM>();
            try
            {
                Sql sql = new Sql().Append(@"SELECT m_Member.MemberID,m_Member.MemberName,
                        t_Message.MessageID,t_Message.MessageTitle,t_Message.Message,t_Message.SendTime,
                        t_Message.Type,t_Message.adduser,t_Message.addtime FROM t_Recipient 
                        RIGHT JOIN m_Member on m_Member.MemberID=t_Recipient.MemberID
                        RIGHT JOIN t_Message on t_Message.MessageID=t_Recipient.MessageID
                        where t_Message.MessageID=@0", messageId);
                lstMessge= db.Fetch<MessageVM>(sql);
                return lstMessge;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        
        #endregion

        #region 【新增处理】

        /// <summary>
        /// 新增消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long AddMessage(MessageModel model)
        {

            long rtnValue = -1;

            using (var scope = db.GetTransaction())
            {
                try
                {
                    //查看该消息是否存在
                    var result = db.Fetch<MessageModel>(string.Format(@"
                        SELECT * FROM t_Message WHERE MessageTitle='{0}'", model.MessageTitle));

                    if (result.Count > 0)
                    {
                        rtnValue = 0;
                    }
                    else
                    {
                      rtnValue= DataConvert.Value2long(db.Insert("t_Message", "MessageID", model)); 
                      scope.Complete();

                      
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }
        /// <summary>
        /// 新增接收人
        /// </summary>
        /// <param name="recModel"></param>
        /// <returns></returns>
        public long AddRecipient(RecipientModel recModel)
        {
            long rtnValue = -1;
            using (var scope = db.GetTransaction())
            {
                try
                {
                    rtnValue =DataConvert.Value2long(db.Insert("t_Recipient", "RecipientID", recModel));
                    scope.Complete();
                  
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return rtnValue ;
          
        }
        #endregion

        #region 【修改处理】

        /// <summary>
        /// 修改消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void EditMessage(MessageVM model, string userId)
        {
            MessageModel mModel = new MessageModel();
            mModel.MessageTitle = model.MessageTitle;
            mModel.Message = model.Message;
            mModel.SendTime = model.SendTime;
            mModel.UserID = userId;
            mModel.upduser = model.upduser;
            mModel.updtime = model.updtime;
            RecipientModel recModel = new RecipientModel();
            recModel.MemberID = model.MemberID;
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Update("t_Message", "MessageID", mModel, model.MessageID);
                    db.Update("t_Recipient", "MessageID", recModel, model.MessageID);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

          
        }

        #endregion

        #region 【删除处理】

        /// <summary>
        /// 删除消息信息
        /// </summary>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        public void DeleteMessage(string[] messageIds)
        {
           

            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var messgeId in messageIds)
                    {
                        db.Delete<MessageModel>("Where MessageID=@0", messgeId);
                        db.Delete<RecipientModel>("Where MessageID=@0", messgeId);
                    }
                  //  db.Delete("t_Message", "MessageID", null, MessageID);
                    scope.Complete(); 
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

           
        }

        #endregion
    }
}