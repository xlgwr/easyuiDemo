using App.Lang;
using Valeo.Domain.Enum;
using Valeo.Domain.Member;
using Valeo.Domain.MemberGrade;
using Valeo.Domain.MemberGradeRight;
using Valeo.Domain.Models;
using Valeo.Domain.MemberRight;
using Valeo.Domain.User;
using Valeo.Lang;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Valeo.Common;
using Valeo.Domain;
using MemberModel = Valeo.Domain.Member.MemberModel;
using Valeo.Domain.Combox;

namespace Valeo.Service
{
    /// <summary>
    /// 会员信息查看
    /// </summary>
    public class MemberListService : BaseService
    {
        #region 【查询处理】

        /// <summary>
        /// 查看级别列表
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public List<SelectListItem> GetMemberGradeList()
        {

            List<SelectListItem> selectItems = new List<SelectListItem>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT MemberGradeID,MemberGrade FROM m_MemberGrade ORDER BY addtime ASC");

                List<MemberGradeModel> ListMemberGrade = db.Fetch<MemberGradeModel>(sql);
                foreach (MemberGradeModel item in ListMemberGrade)
                {
                    selectItems.Add(new SelectListItem { Text = item.MemberGrade, Value = item.MemberGradeID.ToString() });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }

        /// <summary>
        /// 获取下拉
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCountryList(int comboxid)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            try
            {
                Sql sql = new Sql().Append(@" 
                    select m_ComboxList.* from m_Combox
                    inner join m_ComboxList on m_ComboxList.comboxid=m_Combox.comboxid
                    where m_Combox.comboxid=@0 order by m_ComboxList.DspNo", comboxid);
                List<ComboxListVM> ListCountry = db.Fetch<ComboxListVM>(sql);
                foreach (ComboxListVM item in ListCountry)
                {
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        string Ltext = string.IsNullOrEmpty(COMKey.COM(item.LanguageCode)) == true ? item.ComboxListName : COMKey.COM(item.LanguageCode);
                        selectItems.Add(new SelectListItem { Text = Ltext, Value = item.ComboxListKey.ToString() });
                    }
                    else
                    {
                        selectItems.Add(new SelectListItem { Text = item.ComboxListName, Value = item.ComboxListKey.ToString() });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }

        /// <summary>
        /// 级别权限列表
        /// </summary>
        /// <returns></returns>
        public List<MemberGradeRightVM> GetMemberGradeRightList()
        {
            List<MemberGradeRightVM> selectItems = new List<MemberGradeRightVM>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT * from m_RightKey WHERE Type=1 ORDER BY DspNo ASC");
                List<MemberGradeRightVM> ListMemberGradeRight = db.Fetch<MemberGradeRightVM>(sql);
                foreach (MemberGradeRightVM item in ListMemberGradeRight)
                {
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        selectItems.Add(new MemberGradeRightVM
                        {
                            RightKey = item.RightKey,
                            LanguageCode = item.LanguageCode,
                            LanguageText = COMKey.COM(item.LanguageCode.ToString())
                        });
                    }
                    else
                    {
                        selectItems.Add(new MemberGradeRightVM
                        {
                            RightKey = item.RightKey
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }


        /// <summary>
        /// 级别权限列表
        /// </summary>
        /// <returns></returns>
        public List<MemberRightKeyModel> GetMemberGradeRightKeyList()
        {
            List<MemberRightKeyModel> selectItems = new List<MemberRightKeyModel>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT * from m_RightKey WHERE Type=1 ORDER BY DspNo ASC");
                List<MemberRightKeyModel> ListMemberGradeRight = db.Fetch<MemberRightKeyModel>(sql);
                foreach (MemberRightKeyModel item in ListMemberGradeRight)
                {
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightKey = item.RightKey,
                            LanguageCode = item.LanguageCode,
                            LanguageText = COMKey.COM(item.LanguageCode.ToString()),
                            ParentKey = item.ParentKey
                        });
                    }
                    else
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightKey = item.RightKey,
                            ParentKey = item.ParentKey
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }


        /// <summary>
        /// 级别权限列表
        /// </summary>
        /// <returns></returns>
        public List<MemberRightKeyModel> GetMemberGradeKeyList(string memberGradeID)
        {
            List<MemberRightKeyModel> selectItems = new List<MemberRightKeyModel>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT mrk.*,mr.RightValue from m_RightKey mrk
INNER JOIN m_MemberGradeRight as mr on mr.RightKey=mrk.RightKey
WHERE mr.MemberGradeID=@0  ", memberGradeID);
                List<MemberRightKeyModel> ListMemberGradeRight = db.Fetch<MemberRightKeyModel>(sql);
                sql = new Sql().Append(@" SELECT mrk.* from m_RightKey mrk WHERE mrk.Type=1");
                List<MemberRightKeyModel> ListRight = db.Fetch<MemberRightKeyModel>(sql);

                foreach (MemberRightKeyModel item in ListRight)
                {
                    foreach (MemberRightKeyModel _ListMemberGradeRight in ListMemberGradeRight)
                    {
                        if (_ListMemberGradeRight.RightKey == item.RightKey)
                        {
                            item.RightValue = _ListMemberGradeRight.RightValue;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightKey = item.RightKey,
                            DspNo = item.DspNo,
                            LanguageCode = item.LanguageCode,
                            LanguageText = COMKey.COM(item.LanguageCode.ToString()),
                            ParentKey = item.ParentKey,
                            RightValue = item.RightValue
                        });
                    }
                    else
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightKey = item.RightKey,
                            ParentKey = item.ParentKey
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }

        /// <summary>
        /// 获取级别权限KEY
        /// </summary>
        /// <param name="memberGradeID"></param>
        /// <returns></returns>
        public List<RightKeyVM> GetRightKeyList(string memberGradeID)
        {
            List<RightKeyVM> list = new List<RightKeyVM>();
            Sql sql = new Sql().Append(@"SELECT
                            m_MemberGradeRight.RightKey
                            FROM
                            m_MemberGrade
                            INNER JOIN m_MemberGradeRight ON m_MemberGradeRight.MemberGradeID = m_MemberGrade.MemberGradeID
                            where m_MemberGrade.MemberGradeID=@0 and m_MemberGradeRight.RightValue=1", memberGradeID);
            list = db.Fetch<RightKeyVM>(sql);
            return list;
        }
        public List<MemberRightModel> GetMemberGradeRightChk(string MemberGradeID)
        {
            List<MemberRightModel> selectItems = new List<MemberRightModel>();
            try
            {
                Sql sql = new Sql().Append(@"select * from m_MemberRight where MemberID=@0", MemberGradeID);
                List<MemberRightModel> ListMemberGradeRight = db.Fetch<MemberRightModel>(sql);
                foreach (MemberRightModel item in ListMemberGradeRight)
                {
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        selectItems.Add(new MemberRightModel
                        {
                            RightKey = item.RightKey,
                            RightValue = item.RightValue,
                            LanguageCode = item.LanguageCode,
                            LanguageText = COMKey.COM(item.LanguageCode.ToString())
                        });
                    }
                    else
                    {
                        selectItems.Add(new MemberRightModel
                        {
                            RightValue = item.RightValue,
                            RightKey = item.RightKey
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }

        public List<MemberRightKeyModel> GetMemberGradeRightKeyChk(string MemberID)
        {
            List<MemberRightKeyModel> selectItems = new List<MemberRightKeyModel>();
            try
            {
                Sql sql = new Sql().Append(@"select mm.*,mr.ParentKey from m_MemberRight as mm INNER JOIN m_RightKey as mr on mr.RightKey=mm.RightKey where mm.MemberID=@0  and mr.Type=1", MemberID);
                List<MemberRightKeyModel> ListMemberGradeRight = db.Fetch<MemberRightKeyModel>(sql);
                sql = new Sql().Append(@" SELECT mrk.RightKey,mrk.LanguageCode,mrk.remark,mrk.ParentKey from m_RightKey mrk WHERE mrk.Type=1");
                List<MemberRightKeyModel> ListMemberGradeRightS = db.Fetch<MemberRightKeyModel>(sql);
                ListMemberGradeRight.AddRange(ListMemberGradeRightS);
                for (int i = 0; i < ListMemberGradeRight.Count; i++)  //外循环是循环的次数
                {
                    for (int j = ListMemberGradeRight.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
                    {

                        if (ListMemberGradeRight[i].RightKey == ListMemberGradeRight[j].RightKey)
                        {
                            ListMemberGradeRight.RemoveAt(j);
                        }

                    }
                }
                foreach (MemberRightKeyModel item in ListMemberGradeRight)
                {
                    if (!string.IsNullOrEmpty(item.LanguageCode))
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightKey = item.RightKey,
                            RightValue = item.RightValue,
                            LanguageCode = item.LanguageCode,
                            LanguageText = COMKey.COM(item.LanguageCode.ToString()),
                            ParentKey = item.ParentKey
                        });
                    }
                    else
                    {
                        selectItems.Add(new MemberRightKeyModel
                        {
                            RightValue = item.RightValue,
                            RightKey = item.RightKey,
                            ParentKey = item.ParentKey
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return selectItems;
        }
        /// <summary>
        /// 查个人会员列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MemberListVM> GetPagePMember(long page, long rows, string sort, string order, MemberListVM condition)
        {

            int Day = GetActiveDay();
            var sql = new Sql().Append(@"
                            select " + Day + @" as ActiveDay,M.addtime,M.MemberID,MemberName,m.HomeTel,m.OfficeTel,m.MobilePhone,m.Salutation,
                                    Surname,GivenNames,FullName_Tm,
                                    FullName_Cn,Email,LastSeachTime,
                                    M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID 
                                    from m_member AS M
                                   left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID");
            sql.Where(" M.Type=@0", Convert.ToInt32(EnumCommon.MemberTypeEnum.INDIVIDUAL).ToString());
            if (!string.IsNullOrEmpty(condition.MemberName))
            {
                sql.Where(" MemberName  like  @0  ", condition.MemberName + "%");
            }
            if (!string.IsNullOrEmpty(condition.Surname))
            {
                sql.Where(" Surname  like  @0  ", condition.Surname + "%");
            }
            if (!string.IsNullOrEmpty(condition.GivenNames))
            {
                sql.Where(" GivenNames  like  @0  ", condition.GivenNames + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Tm))
            {
                sql.Where(" FullName_Tm  like  @0  ", condition.FullName_Tm + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", condition.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(condition.Email))
            {
                sql.Where(" Email  like  @0  ", condition.Email + "%");
            }
            if (!string.IsNullOrEmpty(condition.MemberGradeID))
            {
                sql.Where(" MG.MemberGradeID  = @0  ", condition.MemberGradeID);
            }
            if (condition.SLastSeachTime == "1")
            {
                sql.Where(" LastSeachTime >  @0  ", DateTime.Now.AddDays(-Day));
            }
            else if (condition.SLastSeachTime == "0")
            {
                sql.Where(" LastSeachTime <  @0   or LastSeachTime is null ", DateTime.Now.AddDays(-Day));
            }

            if (condition.SEnable == "1")
            {
                sql.Where(" Enable  =  1  ");
            }
            else if (condition.SEnable == "0")
            {
                sql.Where(" Enable  =  0  or enable is null  ");
            }

            if (!string.IsNullOrEmpty(condition.addtime))
            {
                sql.Where(" M.addtime  >  @0  ", condition.addtime + " 00:00:00:000");
            }
            if (!string.IsNullOrEmpty(condition.EndTime))
            {
                sql.Where(" M.addtime  < @0", condition.EndTime + " 23:59:59:999");
            }
            if (condition.IsRemainingSum == 0)
            {
                sql.Where(" M.RemainingSum  =  0  ");
            }
            else if (condition.IsRemainingSum == 1)
            {
                sql.Where(" M.RemainingSum  >  0  ");
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.Equals("MemberGradeID"))
                {
                    sort = "MG.MemberGrade";
                }
                else
                {
                    sort = "M." + sort;
                }
                sort = sort.Replace("String", "");
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" M.addtime desc");
            }
            //sql.Append("  order by M.addtime desc");

            return db.Page<MemberListVM>(page, rows, sql);
        }

        /// <summary>
        /// 查个人会员详情
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public MemberModel GetPagePMember(string MemberID, int start)
        {
            MemberModel ListMB = new MemberModel();
            try
            {
                int Day = GetActiveDay();
                var sql = new Sql().Append(@"select " + Day + @" as ActiveDay,* from m_member where memberid=@0", MemberID);
                ListMB = db.FirstOrDefault<MemberModel>(sql);
                sql = new Sql().Append(@"select * from m_MemberRight where memberid=@0", MemberID);
                ListMB.MemberRightModel = db.Fetch<MemberRightModel>(sql);
                if (start == 1)
                {
                    sql = new Sql().Append(@"select * from m_MemberComany where MemberComanyID=@0", ListMB.MemberComanyID);
                    ListMB.MemberComanyModel = db.FirstOrDefault<Valeo.Domain.MemberComanyModel>(sql);
                    sql = new Sql().Append(@"select * from m_ContactPerson where MemberComanyID=@0  order by [Default] desc", ListMB.MemberComanyID);
                    ListMB.ContactPersonModel = db.Fetch<Valeo.Domain.ContactPersonModel>(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ListMB;
        }

        /// <summary>
        /// 查公司会员列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<MemberListVM> GetPageCMember(long page, long rows, string sort, string order, MemberListVM condition)
        {
            int Day = GetActiveDay();
            var sql = new Sql().Append(@"
                            select " + Day + @" as ActiveDay, M.addtime,M.MemberID,MemberName,M.HomeTel,M.OfficeTel,M.MobilePhone,M.Salutation,
                                   MC.FullName_En,MC.FullName_Tm,
                                   MC.FullName_Cn,CP.Email,LastSeachTime,FinancialLicenseNo,SecuritiesLicenceNo,
                                   M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID  from m_MemberComany AS MC
                            right join m_member AS m on m.MemberComanyID = MC.MemberComanyID
                            left join   m_ContactPerson AS CP on CP.MemberComanyID=MC.MemberComanyID
                            left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID");
            sql.Where(" M.Type=@0  and [Default]=1", Convert.ToInt32(EnumCommon.MemberTypeEnum.COMPANY).ToString());//and CP.[Default]=0
            if (!string.IsNullOrEmpty(condition.MemberName))
            {
                sql.Where(" MemberName  like  @0  ", condition.MemberName + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_En))
            {
                sql.Where(" MC.FullName_En  like  @0  ", condition.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Tm))
            {
                sql.Where(" MC.FullName_Tm  like  @0  ", condition.FullName_Tm + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Cn))
            {
                sql.Where(" MC.FullName_Cn  like  @0  ", condition.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(condition.Email))
            {
                sql.Where(" m.MemberComanyID in (SELECT MemberComanyID FROM  m_ContactPerson WHERE Email  like  @0 ) ", condition.Email + "%");
            }
            if (!string.IsNullOrEmpty(condition.MemberGradeID))
            {
                sql.Where(" MG.MemberGradeID  = @0  ", condition.MemberGradeID);
            }
            if (condition.SLastSeachTime == "1")
            {
                sql.Where(" LastSeachTime >  @0", DateTime.Now.AddDays(-Day));
            }
            else if (condition.SLastSeachTime == "0")
            {
                sql.Where(" LastSeachTime <  @0  or LastSeachTime is null ", DateTime.Now.AddDays(-Day));
            }

            if (condition.SEnable == "1")
            {
                sql.Where(" Enable  =  1  ");
            }
            else if (condition.SEnable == "0")
            {
                sql.Where(" Enable  =  0  or enable is null  ");
            }

            if (!string.IsNullOrEmpty(condition.addtime))
            {
                sql.Where(" M.addtime  >  @0  ", condition.addtime + " 00:00:00:000");
            }
            if (!string.IsNullOrEmpty(condition.EndTime))
            {
                sql.Where(" M.addtime  < @0", condition.EndTime + " 23:59:59:999");
            }
            if (condition.IsRemainingSum == 0)
            {
                sql.Where(" M.RemainingSum  =  0  ");
            }
            else if (condition.IsRemainingSum == 1)
            {
                sql.Where(" M.RemainingSum  >  0  ");
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.Equals("MemberGradeID"))
                {
                    sort = "MG.MemberGrade";
                }
                else if (sort.Equals("FullName_En"))
                {
                    sort = "MC.FullName_En";
                }
                else if (sort.Equals("FullName_Tm"))
                {
                    sort = "MC.FullName_Tm";
                }
                else if (sort.Equals("FullName_Cn"))
                {
                    sort = "MC.FullName_Cn";
                }
                else if (sort.Equals("SecuritiesLicenceNo"))
                {

                }
                else if (sort.Equals("FinancialLicenseNo"))
                {

                }
                else
                {
                    sort = "M." + sort;
                }
                sort = sort.Replace("String", "");
                sql.OrderBy(sort + " " + order);
            }
            else
            {
                sql.OrderBy(" M.addtime desc");
            }

            return db.Page<MemberListVM>(page, rows, sql);
        }

        /// <summary>
        /// 导出个人
        /// </summary>
        /// <returns></returns>
        public List<MemberListVM> GetExportPMember(MemberListVM condition)
        {

            int Day = GetActiveDay();
            var sql = new Sql().Append(@"
                            select " + Day + @" as ActiveDay,M.addtime,M.MemberID,MemberName,M.HomeTel,M.OfficeTel,M.MobilePhone,M.Salutation,
                                    Surname,GivenNames,FullName_Tm,
                                    FullName_Cn,Email,LastSeachTime,
                                    M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID 
                                    from m_member AS M
                                   left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID");
            sql.Where(" M.Type=@0", Convert.ToInt32(EnumCommon.MemberTypeEnum.INDIVIDUAL).ToString());
            if (!string.IsNullOrEmpty(condition.MemberName))
            {
                sql.Where(" MemberName  like  @0  ", condition.MemberName + "%");
            }
            if (!string.IsNullOrEmpty(condition.Surname))
            {
                sql.Where(" Surname  like  @0  ", condition.Surname + "%");
            }
            if (!string.IsNullOrEmpty(condition.GivenNames))
            {
                sql.Where(" GivenNames  like  @0  ", condition.GivenNames + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Tm))
            {
                sql.Where(" FullName_Tm  like  @0  ", condition.FullName_Tm + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", condition.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(condition.Email))
            {
                sql.Where(" Email  like  @0  ", condition.Email + "%");
            }
            if (!string.IsNullOrEmpty(condition.MemberGradeID))
            {
                sql.Where(" MG.MemberGradeID  = @0  ", condition.MemberGradeID);
            }
            if (condition.SLastSeachTime == "1")
            {
                sql.Where(" LastSeachTime >  @0  ", DateTime.Now.AddDays(-Day));
            }
            else if (condition.SLastSeachTime == "0")
            {
                sql.Where(" LastSeachTime <  @0   or LastSeachTime is null ", DateTime.Now.AddDays(-Day));
            }

            if (condition.SEnable == "1")
            {
                sql.Where(" Enable  =  1  ");
            }
            else if (condition.SEnable == "0")
            {
                sql.Where(" Enable  =  0  or enable is null  ");
            }

            if (!string.IsNullOrEmpty(condition.addtime))
            {
                sql.Where(" M.addtime  >  @0  ", condition.addtime + " 00:00:00:000");
            }
            if (!string.IsNullOrEmpty(condition.EndTime))
            {
                sql.Where(" M.addtime  < @0", condition.EndTime + " 23:59:59:999");
            }

            sql.OrderBy(" M.addtime desc");
            return db.Fetch<MemberListVM>(sql);
        }

        /// <summary>
        /// 导出公司
        /// </summary>
        /// <returns></returns>
        public List<MemberListVM> GetExportCMember(MemberListVM condition)
        {

            int Day = GetActiveDay();
            var sql = new Sql().Append(@"
                            select " + Day + @" as ActiveDay, M.addtime,M.MemberID,MemberName,M.HomeTel,M.OfficeTel,M.MobilePhone,M.Salutation,
                                   MC.FullName_En,MC.FullName_Tm,
                                   MC.FullName_Cn,CP.Email,LastSeachTime,FinancialLicenseNo,SecuritiesLicenceNo,
                                   M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID  from m_MemberComany AS MC
                            right join m_member AS m on m.MemberComanyID = MC.MemberComanyID
                            left join   m_ContactPerson AS CP on CP.MemberComanyID=MC.MemberComanyID
                            left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID");
            sql.Where(" M.Type=@0  and [Default]=1", Convert.ToInt32(EnumCommon.MemberTypeEnum.COMPANY).ToString());//and CP.[Default]=0
            //sql.Where(" M.Type=@0", Convert.ToInt32(EnumCommon.MemberTypeEnum.COMPANY).ToString());
            if (!string.IsNullOrEmpty(condition.MemberName))
            {
                sql.Where(" MemberName  like  @0  ", condition.MemberName + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_En))
            {
                sql.Where(" MC.FullName_En  like  @0  ", condition.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Tm))
            {
                sql.Where(" MC.FullName_Tm  like  @0  ", condition.FullName_Tm + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullName_Cn))
            {
                sql.Where(" MC.FullName_Cn  like  @0  ", condition.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(condition.Email))
            {
                sql.Where(" CP.Email  like  @0  ", condition.Email + "%");
            }
            if (!string.IsNullOrEmpty(condition.MemberGradeID))
            {
                sql.Where(" MG.MemberGradeID  = @0  ", condition.MemberGradeID);
            }
            if (condition.SLastSeachTime == "1")
            {
                sql.Where(" LastSeachTime >  @0  ", DateTime.Now.AddDays(-Day));
            }
            else if (condition.SLastSeachTime == "0")
            {
                sql.Where(" LastSeachTime <  @0   or LastSeachTime is null ", DateTime.Now.AddDays(-Day));
            }

            if (condition.SEnable == "1")
            {
                sql.Where(" Enable  =  1  ");
            }
            else if (condition.SEnable == "0")
            {
                sql.Where(" Enable  =  0  or enable is null  ");
            }

            if (!string.IsNullOrEmpty(condition.addtime))
            {
                sql.Where(" M.addtime  >  @0  ", condition.addtime + " 00:00:00:000");
            }
            if (!string.IsNullOrEmpty(condition.EndTime))
            {
                sql.Where(" M.addtime  < @0", condition.EndTime + " 23:59:59:999");
            }

            sql.OrderBy(" M.addtime desc");
            return db.Fetch<MemberListVM>(sql);
        }


        public MemberListVM getOnePC(MemberListVM condition)
        {
            int Day = GetActiveDay();
            var sql = new Sql().Append(@"
                            select  " + Day + @" as ActiveDay, * from (select M.Type,M.addtime,M.MemberID,MemberName,m.HomeTel,m.OfficeTel,m.MobilePhone,m.Salutation,
                                    CONCAT(Surname,' ',GivenNames) as FullName_En,FullName_Tm,
                                    FullName_Cn,Email,LastSeachTime,'' as FinancialLicenseNo,'' as SecuritiesLicenceNo,
                                    M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID 
                                    from m_member AS M
                                   left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID
                            WHERE ( M.Type=0)
                            AND ( Enable  =  1  )
                            UNION
                            SELECT M.Type,M.addtime,M.MemberID,MemberName,M.HomeTel,M.OfficeTel,M.MobilePhone,M.Salutation,
                                                               MC.FullName_En,MC.FullName_Tm,
                                                               MC.FullName_Cn,CP.Email,LastSeachTime,FinancialLicenseNo,SecuritiesLicenceNo,
                                                               M.RemainingSum,Enable,MG.MemberGrade AS MemberGradeID  from m_MemberComany AS MC
                                                        right join m_member AS m on m.MemberComanyID = MC.MemberComanyID
                                                        left join (SELECT  * FROM  m_ContactPerson WHERE [Default]=1 ) AS CP on CP.MemberComanyID=MC.MemberComanyID
                                                        left join m_MemberGrade AS MG on MG.MemberGradeID=M.MemberGradeID
                            WHERE ( M.Type=1)) x");

            if (!string.IsNullOrEmpty(condition.MemberName))
            {
                sql.Where(" x.MemberName  =  @0  ", condition.MemberName);
            }
            if (!string.IsNullOrEmpty(condition.MemberID))
            {
                sql.Where(" x.MemberID  =  @0  ", condition.MemberID);
            }
            
            return db.FirstOrDefault<MemberListVM>(sql);
        }
        public int GetActiveDay()
        {
            int ActiveDay = 0;
            try
            {
                Sql sql = new Sql().Append(@" select paramvalue from m_Parameter where ParamKey='ActiveDay'");
                ActiveDay = db.FirstOrDefault<int>(sql);
            }
            catch (Exception)
            {
                throw;
            }
            return ActiveDay;
        }

        #endregion

        #region 【新增操作】
        /// <summary>
        /// 新增个人
        /// </summary>
        /// <returns></returns>
        public bool AddIndividual(MemberModel memberM, List<MemberRightModel> LiMR, out string MemberID)
        {
            bool rtnValue = false;
            try
            {
                using (var scope = db.GetTransaction())
                {
                    MemberID = db.Insert("m_Member", "MemberID", memberM).ToString();
                    for (int i = 0; i < LiMR.Count; i++)
                    {
                        LiMR[i].MemberID = MemberID;
                        LiMR[i].DspNo = i;
                        db.Insert("m_MemberRight", "MemberRightID", LiMR[i]).ToString();
                    }
                    scope.Complete();
                    rtnValue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberM"></param>
        /// <param name="type">0:个人，1公司</param>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool AddIndividual(MemberModel memberM, int type, out string MemberID)
        {
            bool rtnValue = false;
            try
            {
                memberM.Addtime = DateTime.Now;
                MemberID = "";
                #region  个人
                if (type == 0)
                {
                    using (var scope = db.GetTransaction())
                    {
                        MemberID = db.Insert("m_Member", "MemberID", memberM).ToString();
                        for (int i = 0; i < memberM.MemberRightModel.Count; i++)
                        {
                            memberM.MemberRightModel[i].MemberID = MemberID;
                            db.Insert("m_MemberRight", "MemberRightID", memberM.MemberRightModel[i]).ToString();
                        }
                        scope.Complete();
                        rtnValue = true;
                    }
                }
                #endregion
                #region 公司
                else if (type == 1)
                {
                    using (var scope = db.GetTransaction())
                    {
                        string MemberComanyModelID = db.Insert("m_MemberComany", "MemberComanyID", memberM.MemberComanyModel).ToString();
                        memberM.MemberComanyID = Convert.ToInt32(MemberComanyModelID);
                        for (int i = 0; i < memberM.ContactPersonModel.Count; i++)
                        {
                            memberM.ContactPersonModel[i].MemberComanyID = MemberComanyModelID;
                            db.Insert("m_ContactPerson", "ContactPersonID", memberM.ContactPersonModel[i]).ToString();
                        }

                        MemberID = db.Insert("m_Member", "MemberID", memberM).ToString();
                        for (int i = 0; i < memberM.MemberRightModel.Count; i++)
                        {
                            memberM.MemberRightModel[i].MemberID = MemberID;
                            db.Insert("m_MemberRight", "MemberRightID", memberM.MemberRightModel[i]).ToString();
                        }
                        scope.Complete();
                        rtnValue = true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }
        #endregion

        #region【修改处理】
        public bool Edit(MemberModel model, int start)
        {
            bool b = false;

            if (start == 0)
            {

                #region
                List<string> columnsMB = new List<string>();
                columnsMB.Add(MemberModel.VarKey.membername);
                columnsMB.Add(MemberModel.VarKey.password);
                columnsMB.Add(MemberModel.VarKey.invoicedate);
                columnsMB.Add(MemberModel.VarKey.membergradeid);
                columnsMB.Add(MemberModel.VarKey.enable);
                columnsMB.Add(MemberModel.VarKey.surname);
                columnsMB.Add(MemberModel.VarKey.givennames);
                columnsMB.Add(MemberModel.VarKey.fullname_tm);
                columnsMB.Add(MemberModel.VarKey.fullname_cn);
                columnsMB.Add(MemberModel.VarKey.seatno);
                columnsMB.Add(MemberModel.VarKey.floor);
                columnsMB.Add(MemberModel.VarKey.roomno);
                columnsMB.Add(MemberModel.VarKey.buildname);
                columnsMB.Add(MemberModel.VarKey.salutation);
                columnsMB.Add(MemberModel.VarKey.streetnumber);
                columnsMB.Add(MemberModel.VarKey.street);
                columnsMB.Add(MemberModel.VarKey.houseno);
                columnsMB.Add(MemberModel.VarKey.city);
                columnsMB.Add(MemberModel.VarKey.postalcode);
                columnsMB.Add(MemberModel.VarKey.countryid);
                columnsMB.Add(MemberModel.VarKey.officetel);
                columnsMB.Add(MemberModel.VarKey.hometel);
                columnsMB.Add(MemberModel.VarKey.mobilephone);
                columnsMB.Add(MemberModel.VarKey.email);
                columnsMB.Add(MemberModel.VarKey.paymentway);
                columnsMB.Add(MemberModel.VarKey.picpath1);
                columnsMB.Add(MemberModel.VarKey.picpath2);
                columnsMB.Add(MemberModel.VarKey.picpath3);
                columnsMB.Add(MemberModel.VarKey.updtime);
                columnsMB.Add(MemberModel.VarKey.upduser);
                columnsMB.Add(MemberModel.VarKey.purpose1);
                columnsMB.Add(MemberModel.VarKey.purpose2);
                columnsMB.Add(MemberModel.VarKey.purpose3);
                columnsMB.Add(MemberModel.VarKey.purpose4);
                columnsMB.Add(MemberModel.VarKey.purpose5);
                columnsMB.Add(MemberModel.VarKey.purpose6);
                columnsMB.Add(MemberModel.VarKey.purpose7);
                columnsMB.Add(MemberModel.VarKey.pathway1);
                columnsMB.Add(MemberModel.VarKey.pathway2);
                columnsMB.Add(MemberModel.VarKey.pathway3);
                columnsMB.Add(MemberModel.VarKey.pathway4);
                columnsMB.Add(MemberModel.VarKey.pathway5);
                columnsMB.Add(MemberModel.VarKey.pathway6);
                columnsMB.Add(MemberModel.VarKey.pathway6remark);
                columnsMB.Add(MemberModel.VarKey.purpose7remark);

                columnsMB.Add(MemberModel.VarKey.areacodehometel);
                columnsMB.Add(MemberModel.VarKey.areacodeofficetel);
                columnsMB.Add(MemberModel.VarKey.areacodemobilephone);
                #endregion
                using (var scope = db.GetTransaction())
                {
                    db.Update("m_Member", "MemberID", model, model.MemberID, columnsMB);

                    for (int i = 0; i < model.MemberRightModel.Count; i++)
                    {
                        Sql _sql = new Sql();
                        _sql.Append(@"select * from m_MemberRight where RightKey=@0 and memberid=@1", model.MemberRightModel[i].RightKey, model.MemberID);
                        List<MemberRightKeyModel> ListMemberGradeRightS = db.Fetch<MemberRightKeyModel>(_sql);
                        if (ListMemberGradeRightS.Count > 0)
                        {
                            _sql = new Sql();
                            _sql.Append(@"update m_MemberRight set RightValue=@0 where MemberID=@1 and RightKey=@2"
                                , model.MemberRightModel[i].RightValue, model.MemberID, model.MemberRightModel[i].RightKey);
                        }
                        else
                        {
                            _sql = new Sql();
                            _sql.Append(@"INSERT INTO [m_MemberRight]([MemberID],[RightKey],[RightValue],[LanguageCode])
                                           VALUES(@0,@1,@2,@3)", model.MemberID, model.MemberRightModel[i].RightKey, model.MemberRightModel[i].RightValue
                                                              , model.MemberRightModel[i].LanguageCode);
                        }
                        db.Execute(_sql);
                    }
                    scope.Complete();
                }
                b = true;
            }
            else if (start == 1)
            {
                #region
                List<string> columnsMC = new List<string>();
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.fullname_en);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.fullname_tm);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.fullname_cn);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.businesstype);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.cibrno);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.seatno);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.floor);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.roomno);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.buildname);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.streetnumber);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.street);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.houseno);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.city);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.postalcode);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.countryid);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath1);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath2);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath3);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath4);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.fvaliditydate);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.svaliditydate);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.financiallicenseno);
                columnsMC.Add(Valeo.Domain.MemberComanyModel.VarKey.securitieslicenceno);

                List<string> columnsMB = new List<string>();
                columnsMB.Add(MemberModel.VarKey.paymentway);
                columnsMB.Add(MemberModel.VarKey.remainingsum);
                columnsMB.Add(MemberModel.VarKey.invoicedate);
                columnsMB.Add(MemberModel.VarKey.membergradeid);
                columnsMB.Add(MemberModel.VarKey.password);
                columnsMB.Add(MemberModel.VarKey.enable);
                columnsMB.Add(MemberModel.VarKey.upduser);
                columnsMB.Add(MemberModel.VarKey.updtime);
                columnsMB.Add(MemberModel.VarKey.purpose1);
                columnsMB.Add(MemberModel.VarKey.purpose2);
                columnsMB.Add(MemberModel.VarKey.purpose3);
                columnsMB.Add(MemberModel.VarKey.purpose4);
                columnsMB.Add(MemberModel.VarKey.purpose5);
                columnsMB.Add(MemberModel.VarKey.purpose6);
                columnsMB.Add(MemberModel.VarKey.purpose7);
                columnsMB.Add(MemberModel.VarKey.pathway1);
                columnsMB.Add(MemberModel.VarKey.pathway2);
                columnsMB.Add(MemberModel.VarKey.pathway3);
                columnsMB.Add(MemberModel.VarKey.pathway4);
                columnsMB.Add(MemberModel.VarKey.pathway5);
                columnsMB.Add(MemberModel.VarKey.pathway6);
                columnsMB.Add(MemberModel.VarKey.picpath3);
                columnsMB.Add(MemberModel.VarKey.pathway6remark);
                columnsMB.Add(MemberModel.VarKey.purpose7remark);

                List<string> columnsCP = new List<string>();
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.surname);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.givennames);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.salutation);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.fullname_tm);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.fullname_cn);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.department);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.position);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.officetel1);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.officetel2);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.mobilephone);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.fax);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.email);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.picpath1);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.picpath2);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.Default);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.areacodeofficetel1);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.areacodeofficetel2);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.areacodefax);
                columnsCP.Add(Valeo.Domain.ContactPersonModel.VarKey.areacodemobilephone);
                #endregion
                Sql sql = new Sql().Append(@"
                                        select MC.MemberComanyID from m_MemberComany AS MC
                                        inner join m_Member AS M on M.MemberComanyID=MC.MemberComanyID
                                        where M.MemberID=@0", model.MemberID);
                List<Valeo.Domain.MemberComanyModel> ListMemberComany = db.Fetch<Valeo.Domain.MemberComanyModel>(sql);
                sql = new Sql().Append(@" select ContactPersonID from m_ContactPerson where MemberComanyID=@0 ORDER BY ContactPersonID ASC", ListMemberComany[0].MemberComanyID);
                List<Valeo.Domain.ContactPersonModel> list = db.Fetch<Valeo.Domain.ContactPersonModel>(sql);
                model.MemberComanyID = Convert.ToInt32(ListMemberComany[0].MemberComanyID);
                using (var scope = db.GetTransaction())
                {
                    db.Update("m_MemberComany", "MemberComanyID", model.MemberComanyModel, model.MemberComanyID, columnsMC);
                    db.Update("m_Member", "MemberID", model, model.MemberID, columnsMB);
                    for (int z = 0; z < list.Count; z++)
                    {
                        if (z + 1 <= model.ContactPersonModel.Count)
                        {
                            db.Update("m_ContactPerson", "ContactPersonID", model.ContactPersonModel[z], list[z].ContactPersonID, columnsCP);
                        }
                        else
                        {
                            db.Delete("m_ContactPerson", "ContactPersonID", null, list[z].ContactPersonID);
                        }
                    }
                    if (list.Count < model.ContactPersonModel.Count)
                    {
                        int count = model.ContactPersonModel.Count;
                        for (int i = count; i > list.Count; i--)
                        {
                            model.ContactPersonModel[i - 1].MemberComanyID = model.MemberComanyID.ToString();
                            db.Insert("m_ContactPerson", "ContactPersonID", model.ContactPersonModel[i - 1]);
                        }
                    }
                    for (int i = 0; i < model.MemberRightModel.Count; i++)
                    {
                        Sql _sql = new Sql();
                        _sql.Append(@"select * from m_MemberRight where RightKey=@0 and memberid=@1", model.MemberRightModel[i].RightKey, model.MemberID);
                        List<MemberRightKeyModel> ListMemberGradeRightS = db.Fetch<MemberRightKeyModel>(_sql);
                        if (ListMemberGradeRightS.Count > 0)
                        {
                            _sql = new Sql();
                            _sql.Append(@"update m_MemberRight set RightValue=@0 where MemberID=@1 and RightKey=@2"
                                , model.MemberRightModel[i].RightValue, model.MemberID, model.MemberRightModel[i].RightKey);
                        }
                        else
                        {
                            _sql = new Sql();
                            _sql.Append(@"INSERT INTO [m_MemberRight]([MemberID],[RightKey],[RightValue],[LanguageCode])
                                           VALUES(@0,@1,@2,@3)", model.MemberID, model.MemberRightModel[i].RightKey, model.MemberRightModel[i].RightValue
                                                              , model.MemberRightModel[i].LanguageCode);
                        }
                        db.Execute(_sql);
                    }
                    scope.Complete();
                    b = true;
                }
            }
            return b;
        }


        /// <summary>
        /// 修改公司
        /// </summary>
        /// <returns></returns>
        public bool UpdateCompany(string MemberID, List<string> liImgPath, List<FileVM> liIndex)
        {
            //联系人
            List<Valeo.Domain.ContactPersonModel> liCpm = new List<Valeo.Domain.ContactPersonModel>();
            Valeo.Domain.ContactPersonModel cpm = new Valeo.Domain.ContactPersonModel();
            //
            List<Valeo.Domain.MemberComanyModel> liMCM = new List<Valeo.Domain.MemberComanyModel>();
            Valeo.Domain.MemberComanyModel MCM = new Valeo.Domain.MemberComanyModel();

            List<string> columnsMCM = new List<string>();
            columnsMCM.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath1);
            columnsMCM.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath2);
            columnsMCM.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath3);
            columnsMCM.Add(Valeo.Domain.MemberComanyModel.VarKey.picpath4);
            Sql _sql = new Sql().Append(@"select MemberComanyID from m_member where memberid=@0", MemberID);
            List<MemberModel> liMB = db.Fetch<MemberModel>(_sql);
            _sql = new Sql().Append(@"select ContactPersonID from m_ContactPerson where MemberComanyId=@0 ORDER BY ContactPersonID ASC", liMB[0].MemberComanyID);
            List<Valeo.Domain.ContactPersonModel> liCP = db.Fetch<Valeo.Domain.ContactPersonModel>(_sql);
            bool rtnValue = false;
            try
            {
                #region ===取出文件路径
                int index = -1;
                MCM = new Valeo.Domain.MemberComanyModel();
                for (int i = 0; i < liIndex.Count; i++)
                {
                    cpm = new Valeo.Domain.ContactPersonModel();
                    if (liIndex[i].PicPath1 == 1 || liIndex[i].PicPath2 == 1)
                    {
                        if (liIndex[i].PicPath1 == 1)
                        {
                            index += 1;
                            cpm.PicPath1 = liImgPath[index];
                        }
                        if (liIndex[i].PicPath2 == 1)
                        {
                            index += 1;
                            cpm.PicPath2 = liImgPath[index];
                        }
                        liCpm.Add(cpm);
                    }

                    if (liIndex[i].PicPath1 == 2)
                    {
                        index += 1;
                        MCM.PicPath1 = liImgPath[index];
                    }

                    if (liIndex[i].PicPath2 == 2)
                    {
                        index += 1;
                        MCM.PicPath2 = liImgPath[index];
                    }

                    if (liIndex[i].PicPath3 == 2)
                    {
                        index += 1;
                        MCM.PicPath3 = liImgPath[index];
                    }

                    if (liIndex[i].PicPath4 == 2)
                    {
                        index += 1;
                        MCM.PicPath4 = liImgPath[index];
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
                                                      WHERE [ContactPersonID] = @2", liCpm[i].PicPath1, liCpm[i].PicPath2, liCP[i].ContactPersonID);
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


        /// <summary>
        /// 修改个人
        /// </summary>
        /// <returns></returns>
        public bool UpdateIndividual(string MemberID, List<string> liImgPath, List<FileVM> liIndex)
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
                    if (liIndex[i].PicPath1 == 1)
                    {
                        index += 1;
                        MB.PicPath1 = liImgPath[index];
                    }
                    if (liIndex[i].PicPath2 == 1)
                    {
                        index += 1;
                        MB.PicPath2 = liImgPath[index];
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
        #endregion


        #region【删除处理】
        public bool Deletes(string ModelID, int start)
        {
            var orderModel = db.FirstOrDefault<OrderModel>("Where MemberID=@0", ModelID);
            if (orderModel != null)
            {
                return false;
            }
            Sql sql;
            bool b = false;
            try
            {
                using (var scope = db.GetTransaction())
                {
                    if (start == 1)
                    {
                        sql = new Sql().Append(@"
                                        select MC.MemberComanyID from m_MemberComany AS MC
                                        inner join m_Member AS M on M.MemberComanyID=MC.MemberComanyID
                                        where M.MemberID=@0", ModelID);
                        List<Valeo.Domain.MemberComanyModel> ListMemberComany =
                            db.Fetch<Valeo.Domain.MemberComanyModel>(sql);

                        if (ListMemberComany.Count > 0)
                        {
                            db.Delete("m_ContactPerson", "MemberComanyID",
                                new Valeo.Domain.MemberComanyModel().MemberComanyID,
                                ListMemberComany[0].MemberComanyID);
                            db.Delete("m_MemberComany", "MemberComanyID",
                                new Valeo.Domain.MemberComanyModel().MemberComanyID,
                                ListMemberComany[0].MemberComanyID);
                            db.Delete("m_member", "MemberID", new MemberModel().MemberID, ModelID);
                        }
                    }
                    db.Delete(new MemberModel() { MemberID = Convert.ToInt64(ModelID) });
                    db.Execute("DELETE FROM m_MemberRight WHERE MemberID=@0", ModelID);
                    scope.Complete();
                    b = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }
        #endregion

        #region【修改处理】
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


        /// <summary>
        /// 验证邮箱是否重复
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="type">0:个人，1公司</param>
        /// <returns></returns>
        public bool GetEmailChk(string email,string memberid)
        {
            try
            {
                //是否存在
                bool Isexistence = false;
                Sql sql = new Sql().Append(@"select * from (
                      select cp.email,memberid from m_ContactPerson as cp
                      left join m_MemberComany as mc on mc.memberComanyid=cp.memberComanyid
                      left join m_Member as mb on mb.memberComanyid=mc.memberComanyid
	                  UNION
	                  select email,memberid from m_Member
                ) as test where Email =@0 ", email);
                if (!string.IsNullOrWhiteSpace(memberid))
                {
                    sql.Append(" and MemberID !=@0", memberid);
                }
                List<MemberModel> ss = db.Fetch<MemberModel>(sql);
                if (ss.Count > 0)
                {
                    Isexistence = true;
                }
                return Isexistence;

            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public MemberModel GetMemberModel(string memberName)
        {
            return db.FirstOrDefault<MemberModel>("Where MemberName=@0", @memberName);
        }

        public long GetDefaultGradeId()
        {
            var memberGrade =
                db.FirstOrDefault<MemberGradeRightModel>("SELECT MemberGradeID from  m_MemberGrade  where SetDefault=1 ");
            return memberGrade != null ? memberGrade.MemberGradeID : 1;
        }
    }
}
