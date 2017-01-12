using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using Valeo.Domain.Product;
using Valeo.Domain.SearchDialog;
using System.Web.Mvc;
using Valeo.Domain.ModelDb;
using Valeo.Lang;
using App.Lang;

namespace Valeo.Service.SearchDialog
{
    public class SearchDialogService : BaseService
    {
        public Page<Domain.Models.AddressModel> GetPageAddress(long page, long rows, Domain.Models.AddressModel condition)
        {
            var sql = new Sql().Append(@"SELECT * from t_Address");

            if (condition.AddressID > 0)
            {
                sql.Where(" AddressID  =  @0  ", condition.AddressID);
            }

            if (!string.IsNullOrEmpty(condition.Address))
            {
                sql.Where(" Address  like  @0  ",  condition.Address + "%");
            }

            if (!string.IsNullOrEmpty(condition.BuildName))
            {
                sql.Where(" BuildName  like  @0  ",  condition.BuildName + "%");
            }
            sql.OrderBy("AddressID");

            return db.Page<Domain.Models.AddressModel>(page, rows, sql);
        }
        public Page<ProductListModel> GetPageProduct(long page, long rows, ProductSearchModel condition)
        {
            var sql = new Sql().Append(@"SELECT 
                                                ProductId
                                                ,BigType
                                                ,SmallType
                                                ,ProductName
                                                ,Currency
                                                ,Price
                                                ,Validity
                                                ,AgeLimit
                                                ,PeopleCount
                                                ,SeachType
                                                ,RecordType
                                                ,ReportType
                                                FROM m_Product");

            if (condition.BigType != -1)
            {
                sql.Where(" BigType  =  @0  ", condition.BigType);
            }

            if (condition.Currency != "-1")
            {
                sql.Where(" Currency = @0  ", condition.Currency);
            }

            if (!string.IsNullOrEmpty(condition.ProductNo))
            {
                sql.Where(" ProductId  like  @0  ",  condition.ProductNo + "%");
            }

            if (condition.MemberGradeId != -1)
            {
                sql.Where("ProductId in( SELECT PackProductID from m_MemberSetMealRelation WHERE MemberGradeId=@0 AND Type=0)", condition.MemberGradeId);
            }
            if (condition.SmallType != -1)
            {
                sql.Where(" SmallType = @0  ", condition.SmallType);
            }
            sql.Where(" Show = @0  ", 1); //只显示前端显示的产品
            return db.Page<ProductListModel>(page, rows, sql);
        }


      

        public Page<Valeo.Domain.Models.CompanyModel> GetCompanyPage(long page, long rows, CompanySearchVm condition)
        {
            var sql = new Sql().Append(@"SELECT  
                                                t1.Entityid
                                                ,CRNo
                                                ,Language
                                                ,FullName_En
                                                ,FullName_Cn
                                                ,CountryID
                                                ,CompanyType
                                                ,IncorporationDate
                                                ,AuthorizedCapital
                                                ,Areas
                                                ,PlaceRegistration
                                                ,Listed
                                                ,IssuedShares
                                                ,ActiveStatus
                                                ,WindingUpMode
                                                ,DissolutionDate
                                                ,RegisterOfCharges
                                                ,ImportantNote
                                                ,Remarks
                                                ,HtmlID
                                                ,Show
                                                ,DataGradeID
                                                ,Enable
                                                ,AddUser
                                                ,AddDatetime
                                                ,UpdUser
                                                ,UpdDatetime 
                                                FROM s_Company  t1 INNER JOIN s_Entity t2 on t1.Entityid=t2.Entityid");
            if (condition.Flag!=0)
            {
                sql.Where(" t2.Flag=@0  ", condition.Flag);
            }
            if (condition.Flag==1)
            {
                sql.Where(" t2.Param1=@0", condition.Param1);
            }
           
            if (!string.IsNullOrEmpty(condition.FullNameEn))
            {
                sql.Where(" t1.FullName_En  like  @0  ",  condition.FullNameEn + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullNameCn))
            {
                sql.Where(" t1.FullName_Cn  like  @0  ",  condition.FullNameCn + "%");
            }
            return db.Page<Valeo.Domain.Models.CompanyModel>(page, rows, sql);
        }

        public Page<Valeo.Domain.Models.PersonModel> GetPersonPage(long page, long rows, PersonSearchVm condition)
        {
            var sql = new Sql().Append(@"SELECT  
t1.Entityid
,FullName_En
,FullName_Cn 
,Alias_En
,Alias_Cn
,Nickname
,Gender
,Birthdate
,MaritalStatus
,ChineseCode
,Nationality
,PlaceBirth
,Show
,DataGradeID 
FROM s_Person  t1 INNER JOIN s_Entity t2 on t1.Entityid=t2.Entityid");
            if (condition.Flag!=0)
            {
                sql.Where(" t2.Flag=@0  ", condition.Flag);
            }
            
            if (condition.Flag == 1)
            {
                sql.Where(" t2.Param1=@0", condition.Param1);
            }

            if (!string.IsNullOrEmpty(condition.FullNameEn))
            {
                sql.Where(" t1.FullName_En  like  @0  ",  condition.FullNameEn + "%");
            }
            if (!string.IsNullOrEmpty(condition.FullNameCn))
            {
                sql.Where(" t1.FullName_Cn  like  @0  ",  condition.FullNameCn + "%");
            }
            return db.Page<Valeo.Domain.Models.PersonModel>(page, rows, sql);
        }

        public void AddEntity(EntityFormVm model)
        {
            using (var scope = db.GetTransaction())
            {
                try
                {
                    db.Insert(model.EntityInfo);
                    if (model.EntityInfo.Type == 2)
                    {
                        db.Insert(model.CompanyInfo);
                    }
                    else
                    {
                        db.Insert(model.PersonInfo);
                    }
                    //添加人际关系
                    foreach (var item in model.TelephoneList)
                    {
                        db.Insert(item);
                    }
                    //添加地址
                    foreach (var item in model.AddressList)
                    {
                        db.Insert(item);
                    }
                    //添加Emali
                    foreach (var item in model.EmailsList)
                    {
                        db.Insert(item);
                    }
                    //添加银行帐户
                    foreach (var item in model.BankAccountList)
                    {
                        db.Insert(item);
                    }
                    //添加证件表
                    foreach (var item in model.IdDocumentsList)
                    {
                        db.Insert(item);
                    }
                    //添加社交帐号
                    foreach (var item in model.SocialNetworkList)
                    {
                        db.Insert(item);
                    }
                    //添加媒体报导
                    foreach (var item in model.MediaReportList)
                    {
                        db.Insert(item);
                    }
                    //添加人际关系
                    foreach (var item in model.RelationshipsList)
                    {
                        db.Insert(item);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void EditEntity(EntityFormVm model)
        {
            var entityFormVmOld = GetEntityFormVm(model.EntityInfo.Entityid);
            using (var scope = db.GetTransaction())
            {
                try
                {
                    if (model.EntityInfo.Type == 2)
                    {
                        if (entityFormVmOld.EntityInfo.Type == 2)
                        {
                            db.Update(model.CompanyInfo);
                        }
                        else
                        {
                            db.Insert(model.CompanyInfo);
                            db.Delete(new Domain.Models.PersonModel() { Entityid = model.EntityInfo.Entityid });
                        }
                    }
                    else
                    {
                        if (entityFormVmOld.EntityInfo.Type != 2)
                        {
                            db.Update(model.PersonInfo);
                        }
                        else
                        {
                            db.Insert(model.PersonInfo);
                            db.Delete(new Domain.Models.CompanyModel() { Entityid = model.EntityInfo.Entityid });
                        }
                    }
                    entityFormVmOld.EntityInfo.Type = model.EntityInfo.Type;
                    entityFormVmOld.EntityInfo.Param1 = 5;
                    db.Insert(entityFormVmOld.EntityInfo);

                    //修改人际关系

                    foreach (var item in model.TelephoneList)
                    {
                        var isItem = entityFormVmOld.TelephoneList.FirstOrDefault(o => o.PhoneID == item.PhoneID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.TelephoneList let isItem = model.TelephoneList.FirstOrDefault(o => o.PhoneID == item.PhoneID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改地址 
                    foreach (var item in model.AddressList)
                    {
                        var isItem = entityFormVmOld.AddressList.FirstOrDefault(o => o.AddressID == item.AddressID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.AddressList let isItem = model.AddressList.FirstOrDefault(o => o.AddressID == item.AddressID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改Emali 
                    foreach (var item in model.EmailsList)
                    {
                        var isItem = entityFormVmOld.EmailsList.FirstOrDefault(o => o.Entityid == item.Entityid);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.EmailsList let isItem = model.EmailsList.FirstOrDefault(o => o.Entityid == item.Entityid) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改银行帐户 
                    foreach (var item in model.BankAccountList)
                    {
                        var isItem = entityFormVmOld.BankAccountList.FirstOrDefault(o => o.BankAccountID == item.BankAccountID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.BankAccountList let isItem = model.BankAccountList.FirstOrDefault(o => o.BankAccountID == item.BankAccountID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改证件表 
                    foreach (var item in model.IdDocumentsList)
                    {
                        var isItem = entityFormVmOld.IdDocumentsList.FirstOrDefault(o => o.IDDocumentID == item.IDDocumentID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.IdDocumentsList let isItem = model.IdDocumentsList.FirstOrDefault(o => o.IDDocumentID == item.IDDocumentID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改社交帐号 
                    foreach (var item in model.SocialNetworkList)
                    {
                        var isItem = entityFormVmOld.SocialNetworkList.FirstOrDefault(o => o.SocialNetworkID == item.SocialNetworkID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.SocialNetworkList let isItem = model.SocialNetworkList.FirstOrDefault(o => o.SocialNetworkID == item.SocialNetworkID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改媒体报导

                    foreach (var item in model.MediaReportList)
                    {
                        var isItem = entityFormVmOld.MediaReportList.FirstOrDefault(o => o.MediaReportID == item.MediaReportID);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.MediaReportList let isItem = model.MediaReportList.FirstOrDefault(o => o.MediaReportID == item.MediaReportID) where isItem == null select item)
                    {
                        db.Delete(item);
                    }
                    //修改人际关系 
                    foreach (var item in model.RelationshipsList)
                    {
                        var isItem = entityFormVmOld.RelationshipsList.FirstOrDefault(o => o.Relationid == item.Relationid);
                        if (isItem == null)
                        {

                            db.Insert(item);
                        }
                        else
                        {
                            db.Update(item);
                        }
                    }
                    foreach (var item in from item in entityFormVmOld.RelationshipsList let isItem = model.RelationshipsList.FirstOrDefault(o => o.Relationid == item.Relationid) where isItem == null select item)
                    {
                        db.Delete(item);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public EntityFormVm GetEntityFormVm(long entityid)
        {
            EntityFormVm entityFormVm = new EntityFormVm();
            var entity = db.FirstOrDefault<Valeo.Domain.Models.EntityModel>("Where Entityid=@0", entityid);
            if (entity.Type == 2)
            {
                var company = db.FirstOrDefault<Domain.Models.CompanyModel>("Where Entityid=@0", entityid);
                entityFormVm.CompanyInfo = company;
            }
            else
            {
                var person = db.FirstOrDefault<Domain.Models.PersonModel>("Where Entityid=@0", entityid);
                entityFormVm.PersonInfo = person;
            }

            entityFormVm.TelephoneList = db.Query<Domain.Models.TelephonesModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.AddressList = db.Query<Domain.Models.AddressModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.EmailsList = db.Query<Domain.Models.EmailsModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.BankAccountList = db.Query<Domain.Models.BankAccountModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.IdDocumentsList = db.Query<Domain.Models.IDDocumentsModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.SocialNetworkList = db.Query<Domain.Models.SocialNetworkModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.MediaReportList = db.Query<Domain.Models.MediaReportModel>("Where Entityid=@0", entityid).ToList();
            entityFormVm.RelationshipsList = db.Query<Domain.Models.RelationshipsModel>("Where Entityid=@0", entityid).ToList();
            return entityFormVm;
        }

        /// <summary>
        /// 获取所有的关系分类
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> getRelationList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                Sql sql = new Sql().Append(@" SELECT * FROM m_Relation ORDER BY Sort ASC");
                List<RelationModel> ListCountry = db.Fetch<RelationModel>(sql);
                foreach (RelationModel item in ListCountry)
                {
                    list.Add(new SelectListItem { Text = COMKey.COM(item.LanguageCode), Value = item.Relationid.ToString() });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }





    }
}
