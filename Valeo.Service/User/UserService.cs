using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.User;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service.User
{
    public class UserService : BaseService
    {
        /// <summary>
        /// 查询分页用户表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<UserModel> GetPageUsers(long page, long rows, string sort, string order, UserSearchModel condition)
        {


            var sql = new Sql().Append(@"SELECT UserID,UserName,FullName_Cn,FullName_En,FullName_Tm,Status, UserGradeID ,Remark
                    from m_User ");



            if (!string.IsNullOrEmpty(condition.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ",  condition.FullName_Cn+"%");
            }

            if (!string.IsNullOrEmpty(condition.FullName_En))
            {
                sql.Where(" FullName_En  like  @0  ",  condition.FullName_En + "%");
            }

            if (!string.IsNullOrEmpty(condition.FullName_Tm))
            {
                sql.Where(" FullName_Tm  like  @0  ",  condition.FullName_Tm + "%");
            }

            if (!string.IsNullOrEmpty(condition.UserName))
            {
                sql.Where(" UserID  like  @0  ",  condition.UserName + "%");
            }
            if ( condition.UserGradeID!=0)
            {
                sql.Where(" UserGradeID = @0  ", condition.UserGradeID);
            }
            if (!string.IsNullOrEmpty(sort))
            { 
                sql.OrderBy(sort + " " + order); 
            }
            else
            {
                sql.OrderBy(" UserID DESC ");
            }
            return  db.Page<UserModel>(page, rows,sql );
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<MemberModel> GetAllUser()
        {
            return db.Fetch<MemberModel>("select UserID,UserName,FullName_Cn,FullName_En,FullName_Tm,Status from m_User order by UserName");
        }
        /// <summary>
        /// 根据ID得到用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel GetUserModel(string userId)
        {
            return db.Single<UserModel>("WHERE UserId=@0", userId);
        }        

        /// <summary>
        /// 判断是否有这个用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserName(string userName)
        {
            var userModel = db.FirstOrDefault<UserModel>(@"SELECT UserID,UserName,FullName_Cn,FullName_En,FullName_Tm,Status from m_User where UserId=@0", userName);
            if(userModel==null)
            { 
                return false;
            }else
            {
                return true;
            }
        }

        public void Add(UserModel model)
        {
            //model.UserID = model.UserName;
            model.Password = Encryption.Encode(model.Password);
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                model.UserName = model.UserID;
            }
            db.Insert(model);
        }

        public void Edit(UserModel model)
        { 
            model.Password = Encryption.Encode(model.Password);
            db.Update(model);
        }

        public void Deletes(string[] userIds)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in userIds)
	                {
                        db.Delete(new UserModel() { UserID = item });
	                } 
                    scope.Complete();
                     
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}
