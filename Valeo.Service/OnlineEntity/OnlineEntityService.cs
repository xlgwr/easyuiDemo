using App.Lang;
using Valeo.Domain.Combox;
using Valeo.Domain.ModelDb;
using Valeo.Domain.Models;
using Valeo.Domain.OnlineEntity;
using Valeo.Lang;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Valeo.Service.OnlineEntity
{
    public class OnlineEntityService : BaseService
    {
        #region 查询处理
        /// <summary>
        /// 获取国家集合
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCountryList(long type)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT * from m_ComboxList WHERE ComboxId=1  ORDER BY DspNo ASC");
                List<ComboxListModel> ListCountry = db.Fetch<ComboxListModel>(sql);
                if (type == 0)
                {
                    selectItems.Add(new SelectListItem { Text = BaseRes.COM_CTL_SLCTALL, Value = "-1" });
                }
                foreach (ComboxListModel item in ListCountry)
                {
                    string text = "";
                    try
                    {

                        text = COMKey.COM(item.LanguageCode);
                        if (text != null)
                        {
                            selectItems.Add(new SelectListItem { Text = text, Value = item.ComboxListKey.ToString() });
                        }

                    }
                    catch (Exception)
                    {
                        text = "";
                        throw;
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
        /// 分页公司查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public Page<CompanyModel> GetPageCompany(long page, long rows, CompanySearchVM company, string sort, string order)
        {
            var sql = new Sql().Append(@"select AddDatetime,Entityid,CRNo,CIBRNO,FullName_Cn,
                                FullName_En,CompanyType,IncorporationDate,AuthorizedCapital,
                                PlaceRegistration,Areas,Listed,IssuedShares,ActiveStatus,WindingUpMode,
                                DissolutionDate,RegisterOfCharges,ImportantNote,Remarks from s_Company");
            if (!string.IsNullOrEmpty(company.CRNo))
            {
                sql.Where(" CRNo  like  @0  ", company.CRNo + "%");
            }
            if (!string.IsNullOrEmpty(company.FullName_En))
            {
                sql.Where(" FullName_En  like  @0  ", company.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(company.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", company.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(company.CIBRNO))
            {
                sql.Where(" CIBRNO  like  @0  ", company.CIBRNO + "%");
            }
            if (!string.IsNullOrEmpty(company.IncorporationDateStart))
            {
                sql.Where(" IncorporationDate>= @0  ", company.IncorporationDateStart);
            }

            if (!string.IsNullOrEmpty(company.IncorporationDateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(company.IncorporationDateEnd);
                    sql.Where(" IncorporationDate<= @0  ", (tmpdate.AddHours(23)).ToString("YYYY-MM-DD"));
                }
                catch (Exception)
                {
                    sql.Where("  IncorporationDate<= @0  ", company.IncorporationDateEnd);

                }

            }
            if (!string.IsNullOrEmpty(company.DissolutionDateStart))
            {
                sql.Where("convert(date, DissolutionDate)>= @0  ", company.DissolutionDateStart);
            }

            if (!string.IsNullOrEmpty(company.DissolutionDateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(company.DissolutionDateEnd);
                    sql.Where("convert(date, DissolutionDate)<= @0  ", tmpdate.AddHours(23).ToString());
                }
                catch (Exception)
                {

                    sql.Where("convert(date, DissolutionDate)<= @0  ", company.DissolutionDateEnd);
                }

            }
            if (company.Listed != -1)
            {
                sql.Where(" Listed  =  @0  ", company.Listed);
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sql.OrderBy(sort + " " + order);
            }
            return db.Page<CompanyModel>(page, rows, sql);
        }

        /// <summary>
        /// 个人查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public Page<PersonModelVM> GetPagePerson(long page, long rows, PersonSearchVM person, string sort, string order)
        {
            var sql = new Sql().Append(@"select Entityid,FullName_En,FullName_Cn,Alias_En,Alias_Cn,
                                    Nickname,Gender,Birthdate,MaritalStatus,CountryName,
                                    PlaceBirth,ChineseCode from s_Person
LEFT   JOIN m_Country on m_Country.Countryid=s_Person.Nationality ");
            if (!string.IsNullOrEmpty(person.EntityId))
            {
                sql.Where(" EntityId  =  @0  ", person.EntityId);
            }
            if (!string.IsNullOrEmpty(person.FullName_En))
            {
                sql.Where(" FullName_En  like  @0  ", person.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(person.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", person.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(person.Surname))
            {
                sql.Where(" Surname  like  @0  ", person.Surname + "%");
            }
            if (!string.IsNullOrEmpty(person.GivenNames))
            {
                sql.Where(" GivenNames  like  @0  ", person.GivenNames + "%");
            }
            if (!string.IsNullOrEmpty(person.Alias_En))
            {
                sql.Where(" Alias_En  like  @0  ", person.Alias_En + "%");
            }
            if (!string.IsNullOrEmpty(person.Alias_Cn))
            {
                sql.Where(" Alias_Cn  like  @0  ", person.Alias_Cn + "%");
            }
            if (!string.IsNullOrEmpty(person.Nickname))
            {
                sql.Where(" Nickname  like  @0  ", person.Nickname + "%");
            }
            if (person.Gender != -1)
            {
                sql.Where(" Gender  = @0  ", person.Gender);
            }
            if (!string.IsNullOrEmpty(person.BirthdateStart))
            {
                sql.Where("Birthdate>=@0  ", person.BirthdateStart);
            }

            if (!string.IsNullOrEmpty(person.BirthdateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(person.BirthdateEnd);
                    sql.Where(" convert(date, Birthdate)<= @0  ", tmpdate.AddHours(23));
                }
                catch (Exception)
                {
                    sql.Where(" convert(date, Birthdate)<= @0  ", person.BirthdateEnd);
                }

            }
            if (person.MaritalStatus != -1)
            {
                sql.Where(" MaritalStatus  = @0  ", person.MaritalStatus);
            }
            if (person.Nationality != -1)
            {
                sql.Where(" Nationality  = @0  ", person.Nationality);
            }
            if (!string.IsNullOrEmpty(person.PlaceBirth))
            {
                sql.Where(" PlaceBirth  like  @0  ", person.PlaceBirth + "%");
            }
            if (!string.IsNullOrEmpty(person.ChineseCode))
            {
                sql.Where(" ChineseCode  like  @0  ", person.ChineseCode + "%");
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                if (sort.Equals("BirthdateString"))
                {
                    sort = "Birthdate";
                }
                sql.OrderBy(sort + " " + order);
            }
            return db.Page<PersonModelVM>(page, rows, sql);
        }
        #endregion

        #region 删除处理
        public Int32 Deletes(string[] Entityids, int start)
        {

            Int32 result = -1;

            using (var scope = db.GetTransaction())
            {

                try
                {
                    foreach (var item in Entityids)
                    {
                        #region 删除处理

                        //查询EntityId是否已经启用 true 未启用，false 已启用
                        bool isUsing = true;

                        //土地关系
                        var ORMresult = db.Fetch<OwnerRelationModel>(string.Format(@"
                            select Entityid from t_OwnerRelation where Entityid={0}", item));
                        if (ORMresult.Count > 0)
                        {
                            isUsing = false;
                            result = 0;
                            return result;
                        }
                        else
                        {
                            //信贷关系
                            var LRMresult = db.Fetch<LoanrRelationModel>(string.Format(@"
                            SELECT Entityid from t_LoanrRelation where Entityid={0}", item));
                            if (LRMresult.Count > 0)
                            {
                                isUsing = false;
                                result = 0;
                                return result;
                            }
                            else
                            {
                                //公共数据关系
                                var PRMresult = db.Fetch<PublicRelationModel>(string.Format(@"
                            SELECT Entityid from t_PublicRelation where Entityid={0}", item));
                                if (PRMresult.Count > 0)
                                {
                                    isUsing = false;
                                    result = 0;
                                    return result;
                                }
                                else
                                {

                                    //法庭数据
                                    var PM = db.Fetch<PlaintiffModel>(string.Format(@"
                                SELECT Entityid from t_Plaintiff where Entityid={0}", item));
                                    var DM = db.Fetch<DefendantModel>(string.Format(@"
                                SELECT Entityid from t_Defendant where Entityid={0}", item));
                                    var JM = db.Fetch<JudgeModel>(string.Format(@"
                                SELECT Entityid from t_Judge where Entityid={0}", item));
                                    if (PM.Count > 0 || DM.Count > 0 || JM.Count > 0)
                                    {
                                        isUsing = false;
                                        result = 0;
                                        return result;
                                    }
                                    else
                                    {
                                        //公司数据
                                        var SM = db.Fetch<SecretaryModel>(string.Format(@"
                                    SELECT Entityid from t_Secretary where Entityid={0}", item));
                                        var DirM = db.Fetch<DirectorsModel>(string.Format(@"
                                    SELECT Entityid from t_Directors where Entityid={0}", item));
                                        var ShareM = db.Fetch<ShareholdersModel>(string.Format(@"
                                    SELECT Entityid from t_Shareholders where Entityid={0}", item));
                                        var DFM = db.Fetch<DisqualificationModel>(string.Format(@"
                                    SELECT Entityid from t_Disqualification where Entityid={0}", item));
                                        var CCM = db.Fetch<CompanyChangeModel>(string.Format(@"
                                    SELECT Entityid from t_CompanyChange where Entityid={0}", item));
                                        var CRM = db.Fetch<CompanyReportModel>(string.Format(@"
                                    SELECT Entityid from t_CompanyReport where Entityid={0}", item));
                                        if (SM.Count > 0 || DirM.Count > 0 || ShareM.Count > 0 || DFM.Count > 0 || CCM.Count > 0 || CRM.Count > 0)
                                        {
                                            isUsing = false;
                                            result = 0;
                                            return result;
                                        }
                                        else
                                        {
                                            isUsing = true;
                                        }

                                    }

                                }
                            }
                        }
                        if (isUsing)
                        {
                            if (start == 1)//个人
                            {
                                //教育
                                db.Delete("t_Education", "Entityid", null, item);
                                //工作经验
                                db.Delete("t_Education", "Entityid", null, item);
                                db.Delete("s_Person", "Entityid", null, item);
                            }
                            else if (start == 2)//公司
                            {
                                db.Delete("s_Company", "Entityid", null, item);
                            }
                            //公共数据
                            db.Delete("t_Telephones", "Entityid", null, item);
                            db.Delete("t_Address", "Entityid", null, item);
                            db.Delete("t_Emails", "Entityid", null, item);
                            db.Delete("t_BankAccount", "Entityid", null, item);
                            db.Delete("t_IDDocuments", "Entityid", null, item);
                            db.Delete("t_SocialNetwork", "Entityid", null, item);
                            db.Delete("t_MediaReport", "Entityid", null, item);
                            db.Delete("s_Entity", "Entityid", null, item);
                            result = 1;
                        }
                        #endregion
                    }
                    scope.Complete();

                }
                catch (Exception)
                {
                    result = -1;
                    throw;
                }
            }
            return result;
        }
        #endregion

        #region 新增处理
        public void AddSave(EntityFormVM model, UserInfo loginUser)
        {
            using (var scope = db.GetTransaction())
            {
                #region 公共数据
                model.entityInfo.Flag = model.entityInfo.Flag;
                model.entityInfo.Param1 = model.entityInfo.Param1;
                model.entityInfo.Type = model.Types;
                EntityModel EM = new EntityModel();

                if (model.Types == 1)
                {
                    //个人
                    model.entityInfo.Type = 1;
                }
                else if (model.Types == 2)
                {
                    //公司
                    model.entityInfo.Type = 2;

                }
                else
                {
                    //未知
                    model.entityInfo.Type = 3;
                }
                db.Insert(model.entityInfo);
                if (model.Types == 1)
                {
                    //个人
                    model.personInfo.Entityid = model.entityInfo.Entityid;
                    model.personInfo.Show = 2;
                    db.Insert(model.personInfo);
                }
                else if (model.Types == 2)
                {
                    //公司
                    model.companyInfo.Entityid = model.entityInfo.Entityid;
                    model.companyInfo.AddDatetime = DateTime.Now;
                    model.companyInfo.AddUser = loginUser.UserName;
                    model.companyInfo.Enable = 1;
                    model.companyInfo.Show = 2;
                    db.Insert(model.companyInfo);
                }


                #endregion

                #region 关系
                foreach (var item in model.relationModel)
                {
                    item.Entityid_M = model.entityInfo.Entityid;
                    var relat = new Domain.Models.RelationshipsModel
                    {
                        Entityid_M = item.Entityid_M,
                        Entityid_C = item.Entityid_C,
                        Relation = item.Relation,
                        Verify = item.Verify
                    };
                    db.Insert(relat);
                }
                #endregion

                #region 电话
                foreach (var item in model.telephonesModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var phone = new Domain.Models.TelephonesModel
                    {
                        Entityid = item.Entityid,
                        PhoneType = item.PhoneType,
                        PhoneNumber = item.PhoneNumber
                    };

                    db.Insert(phone);
                }
                #endregion

                #region 地址
                foreach (var item in model.addressModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var address = new Domain.Models.AddressModel
                    {

                        Entityid = item.Entityid,
                        AddressType = item.AddressType,
                        Address = item.Address,
                        BuildName = item.BuildName,
                        SeatNO = item.SeatNO,
                        HouseNO = item.HouseNO,
                        RoomNO = item.RoomNO,
                        Floor = item.Floor,
                        Street = item.Street,
                        StreetNumber = item.StreetNumber,
                        POBox = item.POBox,
                        PostalCode = item.PostalCode,
                        Area = item.Area,
                        City = item.City,
                        Province = item.Province,
                        Country = item.Country,
                        LotType = item.LotType,
                        LotNo = item.LotNo,
                        Section = item.Section,
                        Subsection = item.Subsection,
                        LenghtTime = item.LenghtTime,
                        OwnerShip = item.OwnerShip
                    };
                    db.Insert(address);
                }
                #endregion

                #region Email
                foreach (var item in model.emailsModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var email = new Domain.Models.EmailsModel
                    {
                        Entityid = item.Entityid,
                        Emailid = item.Emailid
                    };
                    db.Insert(email);
                }
                #endregion

                #region 银行账户
                foreach (var item in model.bankAccountModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var bank = new Domain.Models.BankAccountModel
                    {
                        Entityid = item.Entityid,
                        Bank = item.Bank,
                        AccountType = item.AccountType,
                        AccountName = item.AccountName,
                        AccountNumber = item.AccountNumber

                    };
                    if (!string.IsNullOrEmpty(item.DateOpenedString))
                    {
                        bank.DateOpened = Convert.ToDateTime(item.DateOpenedString);
                    }

                    db.Insert(bank);
                }
                #endregion

                #region 证件
                foreach (var item in model.IDdocumentsModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var idd = new Domain.Models.IDDocumentsModel
                    {
                        Entityid = item.Entityid,
                        Type = item.Type,
                        Countryid = item.Countryid,
                        Number = item.Number
                    };
                    if (!string.IsNullOrEmpty(item.CertificateDateString))
                    {
                        idd.CertificateDate = Convert.ToDateTime(item.CertificateDateString);
                    }

                    db.Insert(idd);
                }
                #endregion

                #region 社交账户
                foreach (var item in model.socialNetworkModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var netWork = new Domain.Models.SocialNetworkModel
                    {
                        Entityid = item.Entityid,
                        NetName = item.NetName,
                        URL = item.URL,
                        SocialNumber = item.SocialNumber,
                        Pic = item.Pic,
                        SocialDate = item.SocialDate
                    };

                    db.Insert(netWork);
                }
                #endregion

                #region 媒体报导
                foreach (var item in model.mediaReportModel)
                {
                    item.Entityid = model.entityInfo.Entityid;
                    var medid = new Domain.Models.MediaReportModel
                    {
                        Entityid = item.Entityid,
                        Name = item.Name,
                        Subject = item.Subject,
                        Content = item.Content
                    };
                    if (!string.IsNullOrEmpty(item.ReportDateString))
                    {
                        medid.ReportDate = Convert.ToDateTime(item.ReportDateString);
                    }
                    db.Insert(medid);
                }
                #endregion

                if (model.Types == 1)
                {
                    #region 个人数据
                    #region 教育

                    foreach (var item in model.educationModel)
                    {
                        item.Entityid = model.entityInfo.Entityid;
                        var educa = new Domain.ModelDb.EducationModel
                        {
                            Entityid = item.Entityid,
                            Institute = item.Institute,
                            InstituteType = item.InstituteType,
                            Degree = item.Degree

                        };

                        if (!string.IsNullOrEmpty(item.FromDateString))
                        {
                            educa.FromDate = Convert.ToDateTime(item.FromDateString);
                        }

                        if (!string.IsNullOrEmpty(item.ToDateString))
                        {
                            educa.ToDate = Convert.ToDateTime(item.ToDateString);
                        }


                        db.Insert(educa);
                    }
                    #endregion

                    #region 工作经验
                    foreach (var item in model.employmentHistoryModel)
                    {
                        item.Entityid_P = model.entityInfo.Entityid;
                        var employ = new Domain.ModelDb.EmploymentHistoryModel
                        {
                            Entityid_P = item.Entityid_P,
                            Entityid = item.Entityid,
                            Employer_En = item.Employer_En,
                            Employer_Cn = item.Employer_Cn,
                            Department = item.Department,
                            Position = item.Position,
                            JoinedDate = item.JoinedDate,
                            LenghtServices = item.LenghtServices,
                            OfficeAddress = item.OfficeAddress,
                            Tel1 = item.Tel1,
                            Tel2 = item.Tel2
                        };
                        db.Insert(employ);
                    }

                    #endregion
                    #endregion
                }
                scope.Complete();
            }
        }
        #endregion

        #region 修改处理
        public EntityFormVM GetEntityFormVM(long entityId, int type)
        {
            #region 公共数据
            var relat = db.Query<RelationshipsVM>(@"select distinct 
            t1.Relationid,
            t1.Entityid_M,
            t1.Entityid_C,
            t1.Relation,
            t1.Verify,
            t2.Type,
            t3.FullName_En,
            t3.FullName_Cn,
            t4.ComboxListName
             from 
            t_Relationships t1 INNER JOIN s_Entity t2 on  (t1.Entityid_C=t2.Entityid) 
            INNER JOIN m_ComboxList t4 on t1.Relation=t4.ComboxListKey 
            INNER JOIN 
            (SELECT   Entityid,FullName_En,FullName_Cn from s_Person
            UNION all
            SELECT   Entityid,FullName_En,FullName_Cn from s_Company) t3

            on t1.Entityid_C=t3.Entityid
            where (t1.Entityid_M=@0)  and t4.ComboxId=2", entityId).ToList();

            var relat2 = db.Query<RelationshipsVM>(@"select distinct 
            t1.Relationid,
            t1.Entityid_M as 'Entityid_C',
            t1.Entityid_C as 'Entityid_M',
            t1.Relation,
            t1.Verify,
            t2.Type,
            t3.FullName_En,
            t3.FullName_Cn,
            t4.ComboxListName
             from 
            t_Relationships t1 INNER JOIN s_Entity t2 on  (t1.Entityid_M=t2.Entityid) 
            INNER JOIN m_ComboxList t4 on t1.Relation=t4.ComboxListKey 
            INNER JOIN 
            (SELECT   Entityid,FullName_En,FullName_Cn from s_Person
            UNION all
            SELECT   Entityid,FullName_En,FullName_Cn from s_Company) t3

            on t1.Entityid_M=t3.Entityid
            where (t1.Entityid_C=@0)  and t4.ComboxId=2", entityId).ToList();
            try
            {
                relat.AddRange(relat2);
            }
            catch (Exception)
            {

            }

            var phoneModel = db.Query<TelephonesVM>(@"SELECT PhoneID,Entityid,PhoneType,PhoneNumber FROM t_Telephones  where Entityid=@0", entityId).ToList();
            var addressModel = db.Query<AddressModel>("Where Entityid=@0", entityId).ToList();
            var emailModel = db.Query<EmailsModel>("Where Entityid=@0", entityId).ToList();
            var iddModel = db.Query<IDDocumentsVM>(@"select IDDocumentID,Entityid,Type,Countryid,Number,CertificateDate,ComboxListName 
from t_IDDocuments as t INNER JOIN m_ComboxList t1 
on t.Countryid=t1.ComboxListKey where t1.ComboxId=1 and Entityid=@0", entityId).ToList();
            var sociaModel = db.Query<SocialNetworkModel>("Where Entityid=@0", entityId).ToList();
            var media = db.Query<MediaReportVM>("SELECT MediaReportID,Entityid,Name,Subject,Content,ReportDate from t_MediaReport Where Entityid=@0", entityId).ToList();
            var bankAccount = db.Query<BankAccountVM>("SELECT BankAccountID,Entityid,Bank,AccountType,AccountName,AccountNumber,DateOpened FROM t_BankAccount Where Entityid=@0", entityId).ToList();
            var entityInfo = db.FirstOrDefault<EntityModel>("Where Entityid=@0", entityId);

            type = entityInfo.Type;
            #endregion

            if (type.Equals(1))
            {
                var educaModel = db.Query<EducationVM>("SELECT Educationid,Entityid,Institute,InstituteType,Degree,FromDate,ToDate FROM t_Education Where Entityid=@0", entityId).ToList();
                var emloyModel = db.Query<EmploymentHistoryModel>("Where Entityid_P=@0", entityId).ToList();
                var personModel = db.FirstOrDefault<PersonModel>("Where Entityid=@0", entityId);
                #region 个人数据

                return new EntityFormVM()
                {
                    entityInfo = entityInfo,
                    personInfo = personModel,
                    relationVM = relat,
                    telephonesVM = phoneModel,
                    addressModel = addressModel,
                    emailsModel = emailModel,
                    IDdocumentsVM = iddModel,
                    bankAccountModel = bankAccount,
                    socialNetworkModel = sociaModel,
                    mediaReportModel = media,
                    educationModel = educaModel,
                    employmentHistoryModel = emloyModel
                };
                #endregion
            }
            else
            {
                #region 公司数据
                var companyModel = db.FirstOrDefault<CompanyModel>("Where Entityid=@0", entityId);
                return new EntityFormVM()
                {
                    entityInfo = entityInfo,
                    companyInfo = companyModel,
                    relationVM = relat,
                    bankAccountModel = bankAccount,
                    telephonesVM = phoneModel,
                    addressModel = addressModel,
                    emailsModel = emailModel,
                    IDdocumentsVM = iddModel,
                    socialNetworkModel = sociaModel,
                    mediaReportModel = media
                };
                #endregion
            }

        }

        public void EditSave(EntityFormVM model, UserInfo loginUser)
        {
            var oldEntityFormVm = GetEntityFormVM(model.entityInfo.Entityid, model.Types);
            model.Types = oldEntityFormVm.entityInfo.Type;
            oldEntityFormVm.entityInfo.Flag = model.entityInfo.Flag;
            oldEntityFormVm.entityInfo.Param1 = model.entityInfo.Param1;
            if (model.Types == 1)
            {
                oldEntityFormVm.personInfo.Entityid = model.entityInfo.Entityid;
                oldEntityFormVm.personInfo.FullName_En = model.personInfo.FullName_En;
                oldEntityFormVm.personInfo.FullName_Cn = model.personInfo.FullName_Cn;
                oldEntityFormVm.personInfo.Birthdate = model.personInfo.Birthdate;
                oldEntityFormVm.personInfo.ChineseCode = model.personInfo.ChineseCode;
                oldEntityFormVm.personInfo.Alias_Cn = model.personInfo.Alias_Cn;
                oldEntityFormVm.personInfo.Alias_En = model.personInfo.Alias_En;
                oldEntityFormVm.personInfo.Gender = model.personInfo.Gender;
                oldEntityFormVm.personInfo.MaritalStatus = model.personInfo.MaritalStatus;
                oldEntityFormVm.personInfo.Nationality = model.personInfo.Nationality;
                oldEntityFormVm.personInfo.Nickname = model.personInfo.Nickname;
                oldEntityFormVm.personInfo.PlaceBirth = model.personInfo.PlaceBirth;
                oldEntityFormVm.personInfo.DataGradeID = model.personInfo.DataGradeID;
                oldEntityFormVm.personInfo.Show = 2;
            }
            else
            {
                oldEntityFormVm.companyInfo.Entityid = model.entityInfo.Entityid;
                oldEntityFormVm.companyInfo.ActiveStatus = model.companyInfo.ActiveStatus;
                oldEntityFormVm.companyInfo.AddDatetime = oldEntityFormVm.companyInfo.AddDatetime;
                oldEntityFormVm.companyInfo.AddUser = oldEntityFormVm.companyInfo.AddUser;
                oldEntityFormVm.companyInfo.Areas = model.companyInfo.Areas;
                oldEntityFormVm.companyInfo.AuthorizedCapital = model.companyInfo.AuthorizedCapital;
                oldEntityFormVm.companyInfo.CIBRNO = model.companyInfo.CIBRNO;
                oldEntityFormVm.companyInfo.CompanyType = model.companyInfo.CompanyType;
                oldEntityFormVm.companyInfo.CountryID = model.companyInfo.CountryID;
                oldEntityFormVm.companyInfo.CRNo = model.companyInfo.CRNo;
                oldEntityFormVm.companyInfo.DissolutionDate = model.companyInfo.DissolutionDate;
                oldEntityFormVm.companyInfo.FullName_Cn = model.companyInfo.FullName_Cn;
                oldEntityFormVm.companyInfo.FullName_En = model.companyInfo.FullName_En;
                oldEntityFormVm.companyInfo.ImportantNote = model.companyInfo.ImportantNote;
                oldEntityFormVm.companyInfo.IncorporationDate = model.companyInfo.IncorporationDate;
                oldEntityFormVm.companyInfo.IssuedShares = model.companyInfo.IssuedShares;
                oldEntityFormVm.companyInfo.Language = model.companyInfo.Language;
                oldEntityFormVm.companyInfo.Listed = model.companyInfo.Listed;
                oldEntityFormVm.companyInfo.PlaceRegistration = model.companyInfo.PlaceRegistration;
                oldEntityFormVm.companyInfo.RegisterOfCharges = model.companyInfo.RegisterOfCharges;
                oldEntityFormVm.companyInfo.Remarks = model.companyInfo.Remarks;
                oldEntityFormVm.companyInfo.UpdDatetime = DateTime.Now;
                oldEntityFormVm.companyInfo.UpdUser = loginUser.UserName;
                oldEntityFormVm.companyInfo.WindingUpMode = model.companyInfo.WindingUpMode;
                oldEntityFormVm.companyInfo.DataGradeID = model.companyInfo.DataGradeID;
                oldEntityFormVm.companyInfo.Show = 2;
                oldEntityFormVm.companyInfo.Enable = 1;
            }

            using (var scope = db.GetTransaction())
            {
                try
                {
                    if (model.Types == 1)
                    {
                        db.Update(oldEntityFormVm.personInfo);


                        foreach (var item in model.educationModel)
                        {
                            var isItem = oldEntityFormVm.educationModel.FirstOrDefault(o => o.Educationid == item.Educationid);
                            item.Entityid = model.entityInfo.Entityid;
                            var relat = new Domain.ModelDb.EducationModel
                            {
                                Educationid = item.Educationid,
                                Entityid = item.Entityid,
                                Institute = item.Institute,
                                InstituteType = item.InstituteType,
                                Degree = item.Degree
                            };

                            if (!string.IsNullOrEmpty(item.FromDateString))
                            {
                                relat.FromDate = Convert.ToDateTime(item.FromDateString);
                            }

                            if (!string.IsNullOrEmpty(item.ToDateString))
                            {
                                relat.ToDate = Convert.ToDateTime(item.ToDateString);
                            }
                            if (isItem == null)
                            {

                                db.Insert(relat);
                            }
                            else
                            {

                                db.Update(relat);
                            }
                        }
                        foreach (var item in from item in oldEntityFormVm.educationModel let isItem = model.educationModel.FirstOrDefault(o => o.Educationid == item.Educationid) where isItem == null select item)
                        {
                            db.Delete(new Domain.ModelDb.EducationModel { Educationid = item.Educationid });
                        }

                        foreach (var item in model.employmentHistoryModel)
                        {
                            var isItem = oldEntityFormVm.employmentHistoryModel.FirstOrDefault(o => o.Employmentid == item.Employmentid);
                            item.Entityid_P = model.entityInfo.Entityid;
                            var relat = new Domain.ModelDb.EmploymentHistoryModel
                            {
                                Employmentid = item.Employmentid,
                                Entityid_P = item.Entityid_P,
                                Entityid = item.Entityid,
                                Employer_En = item.Employer_En,
                                Employer_Cn = item.Employer_Cn,
                                Department = item.Department,
                                JoinedDate = item.JoinedDate,
                                LenghtServices = item.LenghtServices,
                                OfficeAddress = item.OfficeAddress,
                                Position = item.Position,
                                Tel1 = item.Tel1,
                                Tel2 = item.Tel2
                            };
                            if (isItem == null)
                            {

                                db.Insert(relat);
                            }
                            else
                            {

                                db.Update(relat);
                            }
                        }
                        foreach (var item in from item in oldEntityFormVm.employmentHistoryModel let isItem = model.employmentHistoryModel.FirstOrDefault(o => o.Employmentid == item.Employmentid) where isItem == null select item)
                        {
                            db.Delete(new Domain.ModelDb.EmploymentHistoryModel { Employmentid = item.Employmentid });
                        }

                    }
                    else
                    {
                        db.Update(oldEntityFormVm.companyInfo);
                    }
                    db.Update(oldEntityFormVm.entityInfo);
                    #region 关系

                    foreach (var item in model.relationModel)
                    {
                        var isItem = oldEntityFormVm.relationVM.FirstOrDefault(o => o.Relationid == item.Relationid);
                        item.Entityid_M = model.entityInfo.Entityid;
                        var relat = new Domain.Models.RelationshipsModel
                        {
                            Relation = item.Relation,
                            Entityid_M = item.Entityid_M,
                            Entityid_C = item.Entityid_C,
                            Relationid = item.Relationid,
                            Verify = item.Verify
                        };
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.relationVM let isItem = model.relationModel.FirstOrDefault(o => o.Relationid == item.Relationid) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.RelationshipsModel { Relationid = item.Relationid });
                    }
                    #endregion

                    #region 电话
                    foreach (var item in model.telephonesModel)
                    {
                        var isItem = oldEntityFormVm.telephonesVM.FirstOrDefault(o => o.PhoneID == item.PhoneID);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.TelephonesModel
                        {
                            PhoneID = item.PhoneID,
                            Entityid = item.Entityid,
                            PhoneType = item.PhoneType,
                            PhoneNumber = item.PhoneNumber
                        };
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.telephonesVM let isItem = model.telephonesModel.FirstOrDefault(o => o.PhoneID == item.PhoneID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.TelephonesModel { PhoneID = item.PhoneID });
                    }
                    #endregion

                    #region 地址
                    foreach (var item in model.addressModel)
                    {
                        var isItem = oldEntityFormVm.addressModel.FirstOrDefault(o => o.AddressID == item.AddressID);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.AddressModel
                        {
                            AddressID = item.AddressID,
                            Entityid = item.Entityid,
                            Address = item.Address,
                            BuildName = item.BuildName,
                            Street = item.Street,
                            StreetNumber = item.StreetNumber,
                            SeatNO = item.SeatNO,
                            Floor = item.Floor,
                            RoomNO = item.RoomNO,
                            HouseNO = item.HouseNO,
                            Area = item.Area,
                            City = item.City,
                            Province = item.Province,
                            Country = item.Country,
                            LotType = item.LotType,
                            LotNo = item.LotNo,
                            Section = item.Section,
                            Subsection = item.Subsection,
                            POBox = item.POBox,
                            PostalCode = item.PostalCode,
                            AddressType = item.AddressType,
                            OwnerShip = item.OwnerShip,
                            LenghtTime = item.LenghtTime
                        };
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.addressModel let isItem = model.addressModel.FirstOrDefault(o => o.AddressID == item.AddressID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.AddressModel { AddressID = item.AddressID });
                    }
                    #endregion

                    #region email
                    foreach (var item in model.emailsModel)
                    {
                        var isItem = oldEntityFormVm.emailsModel.FirstOrDefault(o => o.Emailid == item.Emailid);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.EmailsModel
                        {
                            Emailid = item.Emailid,
                            Entityid = item.Entityid,
                            Email = item.Email
                        };
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.emailsModel let isItem = model.emailsModel.FirstOrDefault(o => o.Emailid == item.Emailid) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.EmailsModel { Emailid = item.Emailid });
                    }

                    #endregion

                    #region 银行账号
                    foreach (var item in model.bankAccountModel)
                    {
                        var isItem = oldEntityFormVm.bankAccountModel.FirstOrDefault(o => o.BankAccountID == item.BankAccountID);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.BankAccountModel
                        {
                            BankAccountID = item.BankAccountID,
                            Entityid = item.Entityid,
                            Bank = item.Bank,
                            AccountType = item.AccountType,
                            AccountName = item.AccountName,
                            AccountNumber = item.AccountNumber


                        };
                        if (!string.IsNullOrEmpty(item.DateOpenedString))
                        {
                            relat.DateOpened = Convert.ToDateTime(item.DateOpenedString);
                        }
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.bankAccountModel let isItem = model.bankAccountModel.FirstOrDefault(o => o.BankAccountID == item.BankAccountID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.BankAccountModel { BankAccountID = item.BankAccountID });
                    }
                    #endregion

                    #region 身份
                    foreach (var item in model.IDdocumentsModel)
                    {
                        var isItem = oldEntityFormVm.IDdocumentsVM.FirstOrDefault(o => o.IDDocumentID == item.IDDocumentID);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.IDDocumentsModel
                        {
                            IDDocumentID = item.IDDocumentID,
                            Entityid = item.Entityid,
                            Type = item.Type,
                            Countryid = item.Countryid,
                            Number = item.Number

                        };
                        if (!string.IsNullOrEmpty(item.CertificateDateString))
                        {
                            relat.CertificateDate = Convert.ToDateTime(item.CertificateDateString);
                        }
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.IDdocumentsVM let isItem = model.IDdocumentsModel.FirstOrDefault(o => o.IDDocumentID == item.IDDocumentID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.IDDocumentsModel { IDDocumentID = item.IDDocumentID });
                    }
                    #endregion

                    #region 社交账号
                    foreach (var item in model.socialNetworkModel)
                    {
                        var isItem = oldEntityFormVm.socialNetworkModel.FirstOrDefault(o => o.SocialNetworkID == item.SocialNetworkID);
                        item.Entityid = model.entityInfo.Entityid;


                        var relat = new Domain.Models.SocialNetworkModel
                        {
                            SocialNetworkID = item.SocialNetworkID,
                            Entityid = item.Entityid,
                            NetName = item.NetName,
                            URL = item.URL,
                            SocialNumber = item.SocialNumber,
                            SocialDate = item.SocialDate,
                            Pic = item.Pic
                        };

                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.socialNetworkModel let isItem = model.socialNetworkModel.FirstOrDefault(o => o.SocialNetworkID == item.SocialNetworkID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.SocialNetworkModel { SocialNetworkID = item.SocialNetworkID });
                    }
                    #endregion

                    #region 媒体播报
                    foreach (var item in model.mediaReportModel)
                    {
                        var isItem = oldEntityFormVm.mediaReportModel.FirstOrDefault(o => o.MediaReportID == item.MediaReportID);
                        item.Entityid = model.entityInfo.Entityid;
                        var relat = new Domain.Models.MediaReportModel
                        {
                            MediaReportID = item.MediaReportID,
                            Entityid = item.Entityid,
                            Name = item.Name,
                            Subject = item.Subject,
                            Content = item.Content
                        };
                        if (!string.IsNullOrEmpty(item.ReportDateString))
                        {
                            relat.ReportDate = Convert.ToDateTime(item.ReportDateString);
                        }
                        if (isItem == null)
                        {

                            db.Insert(relat);
                        }
                        else
                        {

                            db.Update(relat);
                        }
                    }
                    foreach (var item in from item in oldEntityFormVm.mediaReportModel let isItem = model.mediaReportModel.FirstOrDefault(o => o.MediaReportID == item.MediaReportID) where isItem == null select item)
                    {
                        db.Delete(new Domain.Models.MediaReportModel { MediaReportID = item.MediaReportID });
                    }

                    #endregion

                    scope.Complete();
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }


        public EntityFormVM GetEntityId(long entityId)
        {
            var entityInfo = db.Single<EntityModel>("Where Entityid=@0", entityId);
            return new EntityFormVM()
            {
                entityInfo = entityInfo
            };
        }
        #endregion

        #region 导出处理

        /// <summary>
        /// 个人导出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PersonModelVM> ExportPerson(string id)
        {
            Sql sql = new Sql();
            sql.Append(@"SELECT Entityid,FullName_En,FullName_Cn,Alias_En,
                Alias_Cn,Nickname,Gender,
                CASE Gender WHEN '0' THEN '男'
                WHEN '1' THEN '女'
                WHEN '2' THEN '其他'
                END as GenderVM,
                Birthdate,MaritalStatus, 
                CASE MaritalStatus 
                WHEN '0' THEN '未婚' 
                WHEN '1' THEN '已婚'
                WHEN '2' THEN '离婚'
                WHEN '3' THEN '鳏寡'
                WHEN '4' THEN '其他'
                END as MaritalStatusVM,CountryName,ChineseCode,PlaceBirth 
                from s_Person LEFT   JOIN m_Country on m_Country.Countryid=s_Person.Nationality");
            sql.Append(string.Format(" where Entityid in ({0})", id));
            return db.Fetch<PersonModelVM>(sql);
        }

        public List<CompanyModelVM> ExportCompany(string id)
        {
            Sql sql = new Sql();
            sql.Append(@"SELECT Entityid,CRNo,CIBRNO,Language,FullName_En,FullName_Cn,CountryID,CompanyType,
                        IncorporationDate,AuthorizedCapital,Areas,
                        CASE Areas WHEN '0' THEN '本地'
                        WHEN '1' THEN '国外'
                        WHEN '2' THEN '其他'
                        END as AreasVM,
                        PlaceRegistration,Listed,  
                        CASE Listed WHEN '0' THEN '未上市'
                        WHEN '1' THEN '已上市'
                        END as ListedVM,
                        IssuedShares,
                        CASE IssuedShares WHEN '0' THEN '未发行'
                        WHEN '1' THEN '已发行'
                        END as IssuedSharesVM,
                        ActiveStatus,WindingUpMode,DissolutionDate,RegisterOfCharges,ImportantNote,Remarks,Show
                        from s_Company");
            sql.Append(string.Format(" where Entityid in ({0})", id));
            return db.Fetch<CompanyModelVM>(sql);
        }

        public List<CompanyModelVM> ExprortNewC(CompanySearchVM company)
        {
            Sql sql = new Sql();
            sql.Append(@"SELECT Entityid,CRNo,CIBRNO,Language,FullName_En,FullName_Cn,CountryID,CompanyType,
                        IncorporationDate,AuthorizedCapital,Areas,
                        CASE Areas WHEN '0' THEN '本地'
                        WHEN '1' THEN '国外'
                        WHEN '2' THEN '其他'
                        END as AreasVM,
                        PlaceRegistration,Listed,  
                        CASE Listed WHEN '0' THEN '未上市'
                        WHEN '1' THEN '已上市'
                        END as ListedVM,
                        IssuedShares,
                        CASE IssuedShares WHEN '0' THEN '未发行'
                        WHEN '1' THEN '已发行'
                        END as IssuedSharesVM,
                        ActiveStatus,WindingUpMode,DissolutionDate,RegisterOfCharges,ImportantNote,Remarks,Show
                        from s_Company");
            if (!string.IsNullOrEmpty(company.CRNo))
            {
                sql.Where(" CRNo  like  @0  ", company.CRNo + "%");
            }
            if (!string.IsNullOrEmpty(company.FullName_En))
            {
                sql.Where(" FullName_En  like  @0  ", company.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(company.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", company.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(company.CIBRNO))
            {
                sql.Where(" CIBRNO  like  @0  ", company.CIBRNO + "%");
            }
            if (!string.IsNullOrEmpty(company.IncorporationDateStart))
            {
                sql.Where("IncorporationDate>= @0  ", company.IncorporationDateStart);
            }

            if (!string.IsNullOrEmpty(company.IncorporationDateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(company.IncorporationDateEnd);
                    sql.Where(" IncorporationDate<= @0  ", tmpdate.AddHours(23));
                }
                catch (Exception)
                {

                    sql.Where(" IncorporationDate<= @0  ", company.IncorporationDateEnd);
                }

            }
            if (!string.IsNullOrEmpty(company.DissolutionDateStart))
            {
                sql.Where("DissolutionDate>= @0  ", company.DissolutionDateStart);
            }

            if (!string.IsNullOrEmpty(company.DissolutionDateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(company.DissolutionDateEnd);
                    sql.Where("DissolutionDate<= @0  ", tmpdate.AddHours(23));
                }
                catch (Exception)
                {

                    sql.Where("DissolutionDate<= @0  ", company.DissolutionDateEnd);
                }

            }
            if (company.Listed != -1)
            {
                sql.Where(" Listed  =  @0  ", company.Listed);
            }
            sql.OrderBy("Entityid  DESC ");
            return db.Fetch<CompanyModelVM>(sql);
        }

        public List<PersonModelVM> ExprortNewP(PersonSearchVM person)
        {
            Sql sql = new Sql();
            sql.Append(@"SELECT Entityid,FullName_En,FullName_Cn,Alias_En,
                Alias_Cn,Nickname,Gender,
                CASE Gender WHEN '0' THEN '男'
                WHEN '1' THEN '女'
                WHEN '2' THEN '其他'
                END as GenderVM,
                Birthdate,MaritalStatus, 
                CASE MaritalStatus 
                WHEN '0' THEN '未婚' 
                WHEN '1' THEN '已婚'
                WHEN '2' THEN '离婚'
                WHEN '3' THEN '鳏寡'
                WHEN '4' THEN '其他'
                END as MaritalStatusVM,CountryName,ChineseCode,PlaceBirth 
                from s_Person LEFT   JOIN m_Country on m_Country.Countryid=s_Person.Nationality");
            if (!string.IsNullOrEmpty(person.FullName_En))
            {
                sql.Where(" FullName_En  like  @0  ", person.FullName_En + "%");
            }
            if (!string.IsNullOrEmpty(person.FullName_Cn))
            {
                sql.Where(" FullName_Cn  like  @0  ", person.FullName_Cn + "%");
            }
            if (!string.IsNullOrEmpty(person.Surname))
            {
                sql.Where(" Surname  like  @0  ", person.Surname + "%");
            }
            if (!string.IsNullOrEmpty(person.GivenNames))
            {
                sql.Where(" GivenNames  like  @0  ", person.GivenNames + "%");
            }
            if (!string.IsNullOrEmpty(person.Alias_En))
            {
                sql.Where(" Alias_En  like  @0  ", person.Alias_En + "%");
            }
            if (!string.IsNullOrEmpty(person.Alias_Cn))
            {
                sql.Where(" Alias_Cn  like  @0  ", person.Alias_Cn + "%");
            }
            if (!string.IsNullOrEmpty(person.Nickname))
            {
                sql.Where(" Nickname  like  @0  ", person.Nickname + "%");
            }
            if (person.Gender != -1)
            {
                sql.Where(" Gender  = @0  ", person.Gender);
            }
            if (!string.IsNullOrEmpty(person.BirthdateStart))
            {
                sql.Where("Birthdate>=@0  ", person.BirthdateStart);
            }

            if (!string.IsNullOrEmpty(person.BirthdateEnd))
            {
                try
                {
                    var tmpdate = DateTime.Parse(person.BirthdateEnd);
                    sql.Where(" Birthdate<= @0  ", tmpdate.AddHours(23));
                }
                catch (Exception)
                {
                    sql.Where(" Birthdate<= @0  ", person.BirthdateEnd);
                }

            }
            if (person.MaritalStatus != -1)
            {
                sql.Where(" MaritalStatus  = @0  ", person.MaritalStatus);
            }
            if (person.Nationality != -1)
            {
                sql.Where(" Nationality  = @0  ", person.Nationality);
            }
            if (!string.IsNullOrEmpty(person.PlaceBirth))
            {
                sql.Where(" PlaceBirth  like  @0  ", person.PlaceBirth + "%");
            }
            if (!string.IsNullOrEmpty(person.ChineseCode))
            {
                sql.Where(" ChineseCode  like  @0  ", person.ChineseCode + "%");
            }
            sql.OrderBy("Entityid  DESC ");
            return db.Fetch<PersonModelVM>(sql);
        }
        #endregion
    }
}
