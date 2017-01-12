using Valeo.Common;
using Valeo.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    public class RegisterService : BaseService
    {
        private Sql _sql;

        #region 【查询处理】

        public MemberModel getMemberModel(string nameOrId)
        {
            if (string.IsNullOrEmpty(nameOrId))
            {
                return null;
            }
            try
            {
                var _sql = new Sql();
                long tmpLID = 0;
                if (long.TryParse(nameOrId, out tmpLID))
                {
                    _sql = new Sql("select  * from m_Member m where (m.MemberID=@0)", nameOrId);
                }
                else
                {
                    _sql = new Sql("select  * from m_Member m where (m.MemberName=@0)", nameOrId);
                }
                return db.FirstOrDefault<MemberModel>(_sql);
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 获取所有Member
        /// </summary>
        /// <returns></returns>
        public List<MemberModel> GetAllMember(int id)
        {
            var dblist = db.Fetch<MemberModel>("select MemberID,MemberName,Surname,GivenNames,FullName_Cn,FullName_Tm from m_Member order by MemberName");
            if (id == -1)
            {
                dblist.Add(new MemberModel() { MemberID = "-1", MemberName = "All", Surname = "All" });
            }

            return dblist;
        }
        #endregion

        #region 【新增操作】
        /// <summary>
        /// 新增个人
        /// </summary>
        /// <returns></returns>
        public bool AddIndividual(MemberModel memberM, out string MemberID)
        {
            bool rtnValue = false;
            try
            {
                MemberID = db.Insert("m_Member", "MemberID", memberM).ToString();

                rtnValue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <returns></returns>
        public bool AddCompany(MemberModel MBModel, MemberComanyModel MCModel, List<ContactPersonModel> liCP, out string MemberID)
        {
            bool rtnValue = false;
            MemberID = "0";
            using (var scope = db.GetTransaction())
            {
                try
                {
                    object MemberComanyID = db.Insert("m_MemberComany", "MemberComanyID", MCModel);

                    //公司联系人
                    for (int i = 0; i < liCP.Count; i++)
                    {
                        ContactPersonModel CPModel = new ContactPersonModel();
                        CPModel.MemberComanyID = MemberComanyID.ToString();
                        CPModel.Surname = liCP[i].Surname;
                        CPModel.GivenNames = liCP[i].GivenNames;
                        CPModel.Salutation = liCP[i].Salutation;
                        CPModel.FullName_Tm = liCP[i].FullName_Tm;
                        CPModel.FullName_Cn = liCP[i].FullName_Cn;
                        CPModel.Department = liCP[i].Department;
                        CPModel.Position = liCP[i].Position;
                        CPModel.OfficeTel1 = liCP[i].OfficeTel1;
                        CPModel.OfficeTel2 = liCP[i].OfficeTel2;
                        CPModel.MobilePhone = liCP[i].MobilePhone;
                        CPModel.Fax = liCP[i].Fax;
                        CPModel.Email = liCP[i].Email;
                        CPModel.PicPath1 = liCP[i].PicPath1;
                        CPModel.PicPath2 = liCP[i].PicPath2;
                        db.Insert("m_ContactPerson", "ContactPersonID", true, CPModel).ToString();
                    }
                    MBModel.MemberComanyID = MemberComanyID.ToString();
                    MemberID = db.Insert("m_Member", "MemberID", MBModel).ToString();
                    scope.Complete();
                    rtnValue = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rtnValue;
        }
        #endregion


        #region  【修改处理】

        /// <summary>
        /// 修改个人
        /// </summary>
        /// <returns></returns>
        public bool UpdateIndividual(string MemberID, List<string> liImgPath, List<int> liIndex)
        {
            MemberModel MB = new MemberModel();

            List<string> columnsMCM = new List<string>();
            columnsMCM.Add(MemberModel.VarKey.picpath1);
            columnsMCM.Add(MemberModel.VarKey.picpath2);
            bool rtnValue = false;
            try
            {
                #region ===取出文件路径
                int index = -1;
                for (int i = 0; i < liIndex.Count; i++)
                {
                    if (liIndex[i] == 1)
                    {
                        index += 1;
                        MB.PicPath1 = liImgPath[index];
                        if (liIndex[i + 1] == 2)
                        {
                            index += 1;
                            MB.PicPath2 = liImgPath[index];
                        }
                    }
                    else if (liIndex[i] == 0)
                    {
                        if (liIndex[i] == 2)
                        {
                            index += 1;
                            MB.PicPath2 = liImgPath[index];
                        }
                    }
                }

                #endregion
                int x = db.Update("m_Member", "MemberID", MB, MemberID, columnsMCM);
                rtnValue = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }

        /// <summary>
        /// 修改公司
        /// </summary>
        /// <returns></returns>
        public bool UpdateCompany(string MemberID, List<string> liImgPath, List<int> liIndex)
        {
            List<ContactPersonModel> liCpm = new List<ContactPersonModel>();
            ContactPersonModel cpm = new ContactPersonModel();
            List<MemberComanyModel> liMCM = new List<MemberComanyModel>();
            MemberComanyModel MCM = new MemberComanyModel();

            List<string> columnsMCM = new List<string>();
            columnsMCM.Add(MemberModel.VarKey.picpath1);
            columnsMCM.Add(MemberModel.VarKey.picpath2);
            _sql = new Sql().Append(@"select MemberComanyID from m_member where memberid=@0", MemberID);
            List<MemberModel> liMB = db.Fetch<MemberModel>(_sql);
            bool rtnValue = false;
            try
            {
                #region ===取出文件路径
                int index = -1;
                MCM = new MemberComanyModel();
                for (int i = 0; i < liIndex.Count; i++)
                {
                    cpm = new ContactPersonModel();
                    if (liIndex[i] == 1)
                    {
                        index += 1;
                        cpm.PicPath1 = liImgPath[index];
                        if (liIndex[i + 1] == 2)
                        {
                            index += 1;
                            cpm.PicPath2 = liImgPath[index];
                        }
                        liCpm.Add(cpm);
                    }
                    else if (liIndex[i] == 3)
                    {
                        if (liIndex[i] == 2)
                        {
                            index += 1;
                            cpm.PicPath2 = liImgPath[index];
                        }
                        liCpm.Add(cpm);
                    }

                    else if (liIndex[i] == 4)
                    {
                        index += 1;
                        MCM.PicPath1 = liImgPath[index];
                        if (liIndex[i + 1] == 5)
                        {
                            index += 1;
                            MCM.PicPath2 = liImgPath[index];
                        }
                    }
                    else if (liIndex[i] == 6)
                    {
                        if (liIndex[i] == 5)
                        {
                            index += 1;
                            MCM.PicPath2 = liImgPath[index];
                        }
                    }
                }

                #endregion

                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        int x = db.Update("m_MemberComany", "MemberComanyID", MCM, liMB[0].MemberComanyID, columnsMCM);
                        for (int i = 0; i < liCpm.Count; i++)
                        {
                            Sql sql = new Sql().Append(@" UPDATE [m_ContactPerson]
                                                        SET [PicPath1] = @0
                                                           ,[PicPath2] = @1
                                                      WHERE [MemberComanyID] = @2", liCpm[i].PicPath1, liCpm[i].PicPath2, liMB[0].MemberComanyID);
                            int u = db.Execute(sql);
                        }
                        scope.Complete();
                        rtnValue = true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }
        #endregion


        /// <summary>
        /// 判断是否重复
        /// </summary>
        /// <param name="fIlter"></param>
        /// <returns></returns>
        public bool GetRepeat(string MemberName)
        {
            bool isExsit = false;

            Sql sql = new PetaPoco.Sql()
                   .Append("SELECT COUNT(MemberName) ")
                   .Append("FROM m_Member ")
                   .Append(" WHERE MemberName=@0 ", MemberName);

            if (db.ExecuteScalar<int>(sql) > 0) isExsit = true;

            return isExsit;
        }
    }
}
