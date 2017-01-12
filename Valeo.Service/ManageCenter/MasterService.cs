
using Valeo.Domain;
using Valeo.Domain.Models;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Service
{
    public class MasterService : BaseService
    {
        private Sql _sql;

        #region 【查询处理】

        /// <summary>
        /// 回显详情
        /// </summary>
        /// <returns></returns>
        public List<MasterVM> GetEchoList(long MemberID)
        {
            List<MasterVM> _lss = new List<MasterVM>();

            Sql _sql = new Sql();

            try
            {
                _sql.Append(" select MC.MemberComanyID,MC.FullName_En AS CompanyName_En,MC.FullName_Tm AS CompanyName_Tm,MC.FullName_Cn AS CompanyName_Cn,MC.BusinessType,CIBRNO,MC.SeatNO,")
                    .Append(" MC.Floor,MC.RoomNO,MC.BuildName,MC.StreetNumber,MC.Street,MC.HouseNO,MC.City,MC.PostalCode,MC.CountryID,CP.Surname,CP.GivenNames,CP.Salutation")
                    .Append(" ,CP.FullName_Tm AS Name_Tm,CP.FullName_Cn AS Name_Cm,CP.Department,CP.Position,CP.OfficeTel1,CP.OfficeTel2,CP.MobilePhone,CP.Fax,CP.Email,CP.PicPath1 AS CPPicPath1")
                    .Append(" ,CP.PicPath1 AS CPPicPath2,MB.PaymentWay,MC.PicPath1 AS MCPicPath1,MC.PicPath2 AS MCPicPath2")
                    .Append(" ,MB.Surname AS GSurname,MB.GivenNames AS GGivenNames,MB.Salutation AS GSalutation,MB.FullName_Tm AS GName_Tm,MB.FullName_Cn AS GName_Cn")
                    .Append(" ,MB.SeatNO AS GSeatNO,MB.Floor AS GFloor,MB.RoomNO AS GRoomNO,MB.BuildName AS GBuildName,MB.StreetNumber AS StreetNumber,MB.Street AS GStreet")
                    .Append(" ,MB.HouseNO AS GHouseNO,MB.City AS GCity,MB.PostalCode AS GPostalCode,MB.CountryID AS GCountryID,MB.PicPath1 AS GPicPath1,MB.PicPath2 AS GPicPath2")
                    .Append(" ,MB.HomeTel AS GHomeTel,MB.OfficeTel AS GOfficeTel,MB.MobilePhone AS GMobilePhone,MB.Email AS GEmail,MB.Type")
                    .Append(" ,Purpose1,Purpose2,Purpose3,Purpose4,Purpose5,Purpose6")
                    .Append(" ,Purpose7,Pathway1,Pathway2,Pathway3,Pathway4,Pathway5,Pathway6")
                    .Append(" from m_Member AS MB")
                    .Append(" left join m_MemberComany AS MC on MC.MemberComanyID=MB.MemberComanyID")
                    .Append(" left join m_ContactPerson AS CP on CP.MemberComanyID=MC.MemberComanyID")
                    .Append(" where MB.MemberID=@0", MemberID);

                _lss = db.Fetch<MasterVM>(_sql);
                return _lss;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取国家集合
        /// </summary>
        /// <returns></returns>
        public List<CountryModel> GetCountryList()
        {
            try
            {
                Sql sql = new Sql().Append(@" SELECT * FROM m_country ORDER BY sort ASC");
                List<CountryModel> list = db.Fetch<CountryModel>(sql);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 【新增操作】
        /// <summary>
        /// 新增个人
        /// </summary>
        /// <returns></returns>
        public bool AddIndividual(MemberModel memberM)
        {
            List<string> columnsMB = new List<string>();
            columnsMB.Add(MemberModel.VarKey.membername);
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
            columnsMB.Add(MemberModel.VarKey.updtime);
            columnsMB.Add(MemberModel.VarKey.upduser);
            bool rtnValue = false;
            try
            {
                db.Update("m_Member", "MemberID", memberM,memberM.MemberID,columnsMB);

                rtnValue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnValue;
        }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <returns></returns>
        public bool EditCompany(MemberModel MBModel, MemberComanyModel MCModel, List<ContactPersonModel> liCP)
        {
            bool rtnValue = false;

            List<string> columnsMC = new List<string>();
            columnsMC.Add(MemberComanyModel.VarKey.fullname_en);
            columnsMC.Add(MemberComanyModel.VarKey.fullname_tm);
            columnsMC.Add(MemberComanyModel.VarKey.fullname_cn);
            columnsMC.Add(MemberComanyModel.VarKey.businesstype);
            columnsMC.Add(MemberComanyModel.VarKey.cibrno);
            columnsMC.Add(MemberComanyModel.VarKey.seatno);
            columnsMC.Add(MemberComanyModel.VarKey.floor);
            columnsMC.Add(MemberComanyModel.VarKey.roomno);
            columnsMC.Add(MemberComanyModel.VarKey.buildname);
            columnsMC.Add(MemberComanyModel.VarKey.streetnumber);
            columnsMC.Add(MemberComanyModel.VarKey.street);
            columnsMC.Add(MemberComanyModel.VarKey.houseno);
            columnsMC.Add(MemberComanyModel.VarKey.city);
            columnsMC.Add(MemberComanyModel.VarKey.postalcode);
            columnsMC.Add(MemberComanyModel.VarKey.countryid);
            columnsMC.Add(MemberComanyModel.VarKey.picpath1);
            columnsMC.Add(MemberComanyModel.VarKey.picpath2);

            List<string> columnsMB = new List<string>();
            columnsMB.Add(MemberModel.VarKey.paymentway);
            columnsMB.Add(MemberModel.VarKey.upduser);
            columnsMB.Add(MemberModel.VarKey.updtime);

            using (var scope = db.GetTransaction())
            {
                int row = 0;
                try
                {
                    row = db.Update("m_MemberComany", "MemberComanyID", MCModel, MCModel.MemberComanyID, columnsMC);
                    if (row > 0)
                    {
                        db.Delete("m_ContactPerson", "MemberComanyID", null, MCModel.MemberComanyID);
                        for (int i = 0; i < liCP.Count; i++)
                        {
                            ContactPersonModel CPModel = new ContactPersonModel();
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
                            db.Insert("m_ContactPerson", "ContactPersonID", true, CPModel); 
                        }
                        if (row > 0)
                        {
                            row = db.Update("m_Member", "MemberID", MBModel, MBModel.MemberID, columnsMB);
                        }
                    }
                    scope.Complete();
                    rtnValue = row > 0 ? true : false;
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
