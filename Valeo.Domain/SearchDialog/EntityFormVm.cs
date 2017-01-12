using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valeo.Domain;

namespace Valeo.Domain.SearchDialog
{
    public class EntityFormVm
    {
        public Models.EntityModel EntityInfo { get; set; }
        /// <summary>
        /// 公司信息
        /// </summary>
        public Models.CompanyModel CompanyInfo { get; set; }

        /// <summary>
        /// 个人信息
        /// </summary>
        public Models.PersonModel PersonInfo { get; set; }

        /// <summary>
        /// 电话表
        /// </summary>
        public List<Models.TelephonesModel> TelephoneList { get; set; }

        /// <summary>
        /// 地址表
        /// </summary>
        public List<Models.AddressModel> AddressList { get; set; }

        /// <summary>
        /// 邮件表
        /// </summary>
        public List<Models.EmailsModel> EmailsList { get; set; }
        /// <summary>
        /// 银行帐户
        /// </summary>
        public List<Models.BankAccountModel> BankAccountList { get; set; }
        /// <summary>
        /// 证件表
        /// </summary>
        public List<Models.IDDocumentsModel> IdDocumentsList { get; set; }
        /// <summary>
        /// 社交帐号
        /// </summary>
        public List<Models.SocialNetworkModel> SocialNetworkList { get; set; }
        /// <summary>
        /// 媒体报导
        /// </summary>
        public List<Models.MediaReportModel> MediaReportList { get; set; }
        /// <summary>
        /// 人际关系
        /// </summary>
        public List<Models.RelationshipsModel> RelationshipsList { get; set; }
    }
}
