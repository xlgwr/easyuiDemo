using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    /// <summary>
    /// 密码修改
    /// </summary>
    public class ChangeMemberPsw : BaseService
    {

        #region 【查询处理】

        /// <summary>
        /// 按条件取得消息信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<MemberModel> GetMemberList(string  MemberID)
        {

            Sql sql = new Sql().Append(@" 
                    SELECT   *
                    FROM    dbo.m_Member 
                    WHERE MemberID =@0", MemberID);

            List<MemberModel> list = db.Fetch<MemberModel>(sql);

            return list;
        }



        #endregion

        #region 【修改处理】

        /// <summary>
        /// 修改消息信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Int16 ModifyMember(MemberModel model)
        {
            Int16 rtnValue = -1;

            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Update(MemberModel.VarKey.tablename, MemberModel.VarKey.memberid, model, model.MemberID);
                    scope.Complete();

                    rtnValue = 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }

        #endregion


    }
}
