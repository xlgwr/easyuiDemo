
using Valeo.Domain.ModelDb;
using Valeo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valeo.Domain.OnlineEntity
{
    public class EntityFormVM
    {
        /// <summary>
        /// 初始化各个表的数据
        /// </summary>
        public EntityFormVM()
        {
            entityInfo = new Models.EntityModel();
            personInfo = new Models.PersonModel();
            companyInfo = new Models.CompanyModel();
            relationModel = new List<RelationshipsModel>();
            relationVM = new List<RelationshipsVM>();
            telephonesModel = new List<TelephonesModel>();
            telephonesVM = new List<TelephonesVM>();
            addressModel = new List<Domain.Models.AddressModel>();
            emailsModel = new List<EmailsModel>();
            bankAccountModel = new List<BankAccountVM>();
            IDdocumentsModel = new List<IDDocumentsVM>();
            IDdocumentsVM = new List<IDDocumentsVM>();
            mediaReportModel = new List<MediaReportVM>();
            educationModel = new List<EducationVM>();
            employmentHistoryModel = new List<EmploymentHistoryModel>();
        }
        public Models.EntityModel entityInfo { get; set; }
        /// <summary>
        /// 个人
        /// </summary>
        public Models.PersonModel personInfo { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public Models.CompanyModel companyInfo { get; set; }
        /// <summary>
        /// 关系
        /// </summary>
        public List<RelationshipsModel> relationModel { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        public List<RelationshipsVM> relationVM { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public List<TelephonesModel> telephonesModel { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public List<TelephonesVM> telephonesVM { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public List<Domain.Models.AddressModel> addressModel { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public List<EmailsModel> emailsModel { get; set; }
        /// <summary>
        /// 银行账户
        /// </summary>
        public List<BankAccountVM> bankAccountModel { get; set; }
        /// <summary>
        /// 证件
        /// </summary>
        public List<IDDocumentsVM> IDdocumentsModel { get; set; }
        /// <summary>
        /// 证件
        /// </summary>
        public List<IDDocumentsVM> IDdocumentsVM { get; set; }
        /// <summary>
        /// 社交账户
        /// </summary>
        public List<SocialNetworkModel> socialNetworkModel { get; set; }
        /// <summary>
        /// 媒体报导
        /// </summary>
        public List<MediaReportVM> mediaReportModel { get; set; }
        /// <summary>
        /// 教育
        /// </summary>
        public List<EducationVM> educationModel { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        public List<EmploymentHistoryModel> employmentHistoryModel { get; set; }

        /// <summary>
        /// 社交
        /// </summary>
        public string relationModelString { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string telephonesModelString { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string addressModelString { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string emailsModelString { get; set; }
        /// <summary>
        /// 银行账户
        /// </summary>
        public string bankAccountModelString { get; set; }
        /// <summary>
        /// 证件
        /// </summary>
        public string IDdocumentsModelString { get; set; }
        /// <summary>
        /// 社交账户
        /// </summary>
        public string socialNetworkModelString { get; set; }
        /// <summary>
        /// 媒体报导
        /// </summary>
        public string mediaReportModelString { get; set; }
        /// <summary>
        /// 教育
        /// </summary>
        public string educationModelString { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string employmentHistoryModelString { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Types { get; set; }
    }
}
