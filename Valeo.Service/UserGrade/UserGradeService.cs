using Valeo.Domain;
using Valeo.Domain.UserGrade;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service.UserGrade
{
    public class UserGradeService : BaseService
    {
        /// <summary>
        /// 得到用户全部等级
        /// </summary>
        /// <returns></returns>
        public List<UserGradeModelVM> GetAll()
        {
            return db.Fetch<UserGradeModelVM>("select * from m_UserGrade where 1=1");
        }
        /// <summary>
        /// 得到所有等级
        /// </summary>
        /// <returns></returns>
        public List<UserGradeModelVM> GetUserGradeList()
        {
            return GetAll();
        }

        public Dictionary<string, string> GetUserGradeDic()
        {
            var tmpR = new Dictionary<string, string>();
            try
            {
                var tmpall = GetAll();
                foreach (var item in tmpall)
                {
                    var tmpKey = item.UserGradeID.ToString();
                    if (!tmpR.ContainsKey(tmpKey))
                    {
                        tmpR.Add(tmpKey, item.UserGrade);
                    }
                }
            }
            catch (Exception)
            {
            }
            return tmpR;

        }
        /// <summary>
        /// 得到等级名称
        /// </summary>
        /// <param name="UserGrades"></param>
        /// <param name="userGradeId"></param>
        /// <returns></returns>
        public string GetUserGradeName(List<UserGradeModelVM> UserGrades, long userGradeId)
        {
            var userGrade = UserGrades.SingleOrDefault(o => o.UserGradeID == userGradeId);
            if (userGrade == null)
            {
                return userGradeId.ToString();
            }
            else
            {
                return userGrade.UserGrade;
            }
        }

        /// <summary>
        /// 得到全部数据等级
        /// </summary>
        /// <returns></returns>
        public List<DataGradeModel> GetDataGradeList()
        {
            return db.Query<DataGradeModel>("where 1=1").ToList();
        }

        /// <summary>
        /// 获取当前用户级别信息
        /// </summary>
        /// <param name="userGradeId"></param>
        /// <param name="dataGradeIDs">数据级别</param>
        /// <returns></returns>
        public UserGradeModel GetUserGradeModel(long? userGradeId, ref List<UserDataRelationModel> dataGradeIDs)
        {
            string str_sql = string.Format(@"select * from m_UserGrade where userGradeId='{0}'", userGradeId);
            var userGrade = db.FirstOrDefault<UserGradeModel>(str_sql);

            str_sql = string.Format(@"select * from m_UserDataRelation where userGradeId={0} ", userGradeId);
            //当前用户级别对应的数据级别
            dataGradeIDs = db.Query<UserDataRelationModel>(str_sql).ToList();

            return userGrade;
        }


        /// <summary>
        /// 判断是否有这个用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsGradeName(string UserGrade)
        {
            var UserGradeModel = db.FirstOrDefault<UserGradeModel>(@"SELECT UserGradeID,UserGrade,Remark from m_UserGrade where UserGrade=@0", UserGrade);
            if (UserGradeModel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        /// <summary>
        /// 新增级别
        /// </summary>
        /// <param name="model"></param>
        public void Add(UserGradeModel model, int[] DataGradeIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //生成级别ID
                    Sql sql = new Sql();
                    sql.Append(@"select UserGradeID from m_UserGrade order by UserGradeID desc ");
                    string str_UserGradeID = db.FirstOrDefault<string>(sql);

                    model.UserGradeID = long.Parse(str_UserGradeID) + 1;

                    //sql = new Sql();
                    //sql.Append(@" insert into m_UserGrade values(@0,@1,@2,@3,@4,@5,@6,@7)", model.UserGradeID, model.UserGrade, model.Status, model.Remark, model.AddUser, model.UpdUser, model.AddTime, model.UpdTime);
                    //db.Execute(sql);

                    db.Insert("m_UserGrade", "UserGradeID", false, model);

                    //新增对应的数据级别到表m_UserDataRelation(用户&数据级别关系表)
                    foreach (var item in DataGradeIDs)
                    {
                        UserDataRelationModel DataGrademodel = new UserDataRelationModel();
                        DataGrademodel.UserGradeID = model.UserGradeID;
                        DataGrademodel.DataGradeID = item;
                        db.Insert(DataGrademodel);
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 修改级别
        /// </summary>
        public void Edit(UserGradeModel model, int[] DataGradeIDs)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    //删除现有的级别关系表m_UserDataRelation
                    Sql sql = new Sql().Append(@"delete from m_UserDataRelation where UserGradeID=@0", model.UserGradeID);
                    int result = db.Execute(sql);

                    //修改用户级别表
                    result = db.Update(model);

                    //新增对应的数据级别到表m_UserDataRelation(用户&数据级别关系表)
                    foreach (var item in DataGradeIDs)
                    {
                        UserDataRelationModel DataGrademodel = new UserDataRelationModel();
                        DataGrademodel.UserGradeID = model.UserGradeID;
                        DataGrademodel.DataGradeID = item;
                        db.Insert(DataGrademodel);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        /// <summary>
        /// 删除选中级别
        /// 同时删除m_UserDataRelation表中级别与数据权限的对应关系
        /// </summary>
        /// <param name="userGrades"></param>
        public void Deletes(string[] gradeIds)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    foreach (var item in gradeIds)
                    {
                        int result = 0;
                        result = db.Delete(new UserGradeModel() { UserGradeID = long.Parse(item) });

                        //删除对应关系
                        Sql sql = new Sql().Append(@"delete from m_UserDataRelation where UserGradeID=@0", long.Parse(item));
                        result = db.Execute(sql);

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
