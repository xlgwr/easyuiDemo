using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarKey
{

    /// <summary>
    /// 系统常量定义
    /// </summary>
    public class Comon
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// 启用
        /// </summary>
        public const int Enabled = 1;

        /// <summary>
        /// 禁用
        /// </summary>
        public const int Disabled = 0;

    }
 
    /// <summary>
    /// 系统语言KEY
    /// </summary>
    public class Lang
    {
     
        /// <summary>
        /// {0}必须在{1}和{2}之间 
        /// </summary>
        public const string  COM_MSG_VER_001="COM_MSG_VER_001" ;

        /// <summary>
        ///  {0}最多{1}个汉字,{2}个字符	 
        /// </summary>
        public const string  COM_MSG_VER_002="COM_MSG_VER_002" ;
 
        /// <summary>
        ///  {0}最少{1}个字符 
        /// </summary>
        public const string  COM_MSG_VER_003="COM_MSG_VER_003" ;
	 
        /// <summary>
        ///  {0}必须填写 
        /// </summary>
        public const string  COM_MSG_VER_004="COM_MSG_VER_004" ;
        
        /// <summary>
        ///  {0}必须是日期格式：2012-10-10 
        /// </summary>
        public const string  COM_MSG_VER_005="COM_MSG_VER_005" ;
        
        /// <summary>
        ///  {0}由字母,数字,下划线，中划线组成 
        /// </summary>
        public const string  COM_MSG_VER_006="COM_MSG_VER_006" ;
                
        /// <summary>
        ///  {0}只能填写汉字 
        /// </summary>
        public const string  COM_MSG_VER_007="COM_MSG_VER_007" ;
                
        /// <summary>
        ///  {0}不能等同于{1} 
        /// </summary>
        public const string  COM_MSG_VER_008="COM_MSG_VER_008" ;

        /// <summary>
        /// 输入不能为空
        /// </summary>
        public const string COM_MSG_VER_100 = "COM_MSG_VER_100";

        /// <summary>
        /// 输入超过{1}个字符
        /// </summary>
        public const string COM_MSG_VER_101 = "COM_MSG_VER_101";
        
        /// <summary>
        /// 输入数据格式错误
        /// </summary>
        public const string COM_MSG_VER_102 = "COM_MSG_VER_102"; 
        
        /// <summary>
        /// 只能由字母,数字,下划线组成
        /// </summary>
        public const string COM_MSG_VER_103 = "COM_MSG_VER_103"; 
        
        /// <summary>
        /// 必须是{1}~{2}的数字
        /// </summary>
        public const string COM_MSG_VER_104 = "COM_MSG_VER_104"; 
         
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public class PaymentWay
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// paypal=0
        /// </summary>
        public const int paypal = 0;

        /// <summary>
        /// 中国银联=1
        /// </summary>
        public const int CHyl = 1;

        /// <summary>
        /// 支付宝=2
        /// </summary>
        public const int Zfb = 2;


    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public class PayStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// 未付款=0
        /// </summary>
        public const int fk = 0;

        /// <summary>
        /// 已付款=1
        /// </summary>
        public const int mfk = 1;

    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// 未启用=0
        /// </summary>
        public const int wqy = 0;

        /// <summary>
        /// 启用=1
        /// </summary>
        public const int qy = 1;

        /// <summary>
        /// 取消=2
        /// </summary>
        public const int qx = 2;


    }


    /// <summary>
    /// 查询状态
    /// </summary>
    public class SchStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// 未处理=0
        /// </summary>
        public const int wcl = 0;

        /// <summary>
        /// 处理中=1
        /// </summary>
        public const int clz = 1;

        /// <summary>
        /// 取消=2
        /// </summary>
        public const int qx = 2;

        /// <summary>
        /// 完成=3
        /// </summary>
        public const int wc = 2;


    }

    /// <summary>
    /// 称呼
    /// </summary>
    public class Salutation
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const int ItemAll = -1;

        /// <summary>
        /// Mr
        /// </summary>
        public const int Mr = 0;

        /// <summary>
        /// Mrs
        /// </summary>
        public const int Mrs = 1;

        /// <summary>
        /// Ms
        /// </summary>
        public const int Ms = 2;


    }

    /// <summary>
    /// 会员级别,三个级别，（值为对应数据库值，需要完全按照数据库，固定 1，2，3）
    /// </summary>
    public class MemberGradeID
    {
        /// <summary>
        /// 第一级别
        /// </summary>
        public const int one = 1;

        /// <summary>
        /// 第二级别
        /// </summary>
        public const int two = 2;

        /// <summary>
        /// 第三级别
        /// </summary>
        public const int three = 3;

    }

    /// <summary>
    /// 头部标题KEY
    /// </summary>
    public class PageTitle{

        /// <summary>
        /// 网站首页
        /// </summary>
         public const int Home = 0;

        /// <summary>
        /// 关于我们
        /// </summary>
         public const int About = 1;

         /// <summary>
         /// 服务种类
         /// </summary>
         public const int Srevice = 2;

         /// <summary>
         /// 成为会员
         /// </summary>
         public const int Member = 3;

         /// <summary>
         /// 支付方式
         /// </summary>
         public const int Payment = 4;

         /// <summary>
         /// 使用限制
         /// </summary>
         public const int UserLimit = 5;

         /// <summary>
         /// 隐身条例
         /// </summary>
         public const int PrivatyPolicy = 6;

         /// <summary>
         /// 免责声明
         /// </summary>
         public const int Disclaimer = 7;

         /// <summary>
         /// 注册协议
         /// </summary>
         public const int RegistrationAgreement = 8;

         /// <summary>
         /// 在线法庭查册指引
         /// </summary>
         public const int OnlineCourt = 9;

         /// <summary>
         /// 在线土地查册指引
         /// </summary>
         public const int OnlineLand = 10;

         /// <summary>
         /// 在线公司查册指引
         /// </summary>
         public const int OnlineCompany = 11;

         /// <summary>
         /// 在线信贷查询指引
         /// </summary>
         public const int OnlineLoan = 12;

         /// <summary>
         /// 在线公共数据指引
         /// </summary>
         public const int OnlinePublic = 13;

         /// <summary>
         /// 自动监察
         /// </summary>
         public const int OnlineAuto = 14;

         /// <summary>
         /// 离线法庭查册指引
         /// </summary>
         public const int OfflineCourt = 15;

         /// <summary>
         /// 离线土地查册指引
         /// </summary>
         public const int OfflineLand = 16;

         /// <summary>
         /// 离线公司查册指引
         /// </summary>
         public const int OfflineCompany = 17;

         /// <summary>
         /// 其他服务查册指引
         /// </summary>
         public const int OfflineOther = 18;

         /// <summary>
         /// 联系我们
         /// </summary>
         public const int ContactAs = 19;
    }

    public class PageLang
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        public const string zhCN = "zh-CN";

       /// <summary>
       /// 中文繁体
       /// </summary>
        public const string zhTW = "zh-TW";

        /// <summary>
        /// 英语
        /// </summary>
        public const string enUS = "en-US";
    }

    /// <summary>
    /// 界面下拉Key
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 全部
        /// </summary>
        public const int All = 0;

        /// <summary>
        /// 登录界面
        /// </summary>
        public const int Login = 1;

        /// <summary>
        /// 注册界面
        /// </summary>
        public const int Register = 2;

        /// <summary>
        /// 套餐购买界面
        /// </summary>
        public const int Buy = 3;

        /// <summary>
        /// 在线法庭界面
        /// </summary>
        public const int OnlineCourt = 4;

        /// <summary>
        /// 在线土地界面
        /// </summary>
        public const int OnlineLand = 5;

        /// <summary>
        /// 在线公司界面
        /// </summary>
        public const int OnlineCompany = 6;

        /// <summary>
        /// 在线信贷界面
        /// </summary>
        public const int OnlineCredit = 7;

        /// <summary>
        /// 在线公共
        /// </summary>
        public const int OnlineCommont = 8;

        /// <summary>
        /// 自动监察
        /// </summary>
        public const int AutoMinitor = 9;

        /// <summary>
        /// 离线法庭
        /// </summary>
        public const int OfflineCourt = 10;

        /// <summary>
        /// 离线土地
        /// </summary>
        public const int OfflineLand = 11;

        /// <summary>
        /// 离线公司
        /// </summary>
        public const int OfflineCompany = 12;

        /// <summary>
        /// 离线其他
        /// </summary>
        public const int OfflineOther = 13;

        /// <summary>
        /// 基本信息
        /// </summary>
        public const int MainInfo =14;

        /// <summary>
        /// 密码管理
        /// </summary>
        public const int ManagerPWD = 15;

        /// <summary>
        /// 我的订单
        /// </summary>
        public const int MyOrder = 16;

        /// <summary>
        /// 产品使用明细
        /// </summary>
        public const int ProductDetails  = 17;

        /// <summary>
        /// 搜索记录
        /// </summary>
        public const int SearchRecord  = 18;

        /// <summary>
        /// 消息管理
        /// </summary>
        public const int MessageManager = 19;

        /// <summary>
        /// 退出
        /// </summary>
        public const int Exit = 20;
    }

    public class ServicePage
    {
        /// <summary>
        /// 全部
        /// </summary>
        public const int All = 0;

        /// <summary>
        /// 登录界面
        /// </summary>
        public const int Login = 1;

        /// <summary>
        /// 注册界面
        /// </summary>
        public const int Register = 2;

        /// <summary>
        /// 会员管理
        /// </summary>
        public const int MemberManager = 3;

        /// <summary>
        /// 离线搜索
        /// </summary>
        public const int OffineSearch = 4;

        /// <summary>
        /// 订单管理
        /// </summary>
        public const int OrderManager = 5;

        /// <summary>
        /// 任务管理
        /// </summary>
        public const int TaskManager = 6;

        /// <summary>
        /// 产品管理
        /// </summary>
        public const int ProductManager = 7;

        /// <summary>
        /// 报告管理
        /// </summary>
        public const int ReportManager = 8;

        /// <summary>
        /// 财务管理
        /// </summary>
        public const int FinanceManager = 9;

        /// <summary>
        /// 财务报告
        /// </summary>
        public const int FinanceReport = 10;

        /// <summary>
        /// 用户信息管理
        /// </summary>
        public const int UserInfoManager = 11;

        /// <summary>
        /// 采集数据
        /// </summary>
        public const int CollectData = 12;

        /// <summary>
        /// 线上数据
        /// </summary>
        public const int OnlineData = 13;

        /// <summary>
        /// 邮件管理
        /// </summary>
        public const int EmailManager = 14;

        /// <summary>
        /// 基本资料
        /// </summary>
        public const int ParamManager = 15;

        /// <summary>
        /// 网页编辑
        /// </summary>
        public const int EditWeb = 16;

        /// <summary>
        /// 消息管理
        /// </summary>
        public const int MessageManager =17;

        /// <summary>
        /// 注销
        /// </summary>
        public const int Exit = 18;
        #region valeo
        /// <summary>
        /// 发货管理
        /// </summary>
        public const int DeliveryManage = 20;
        /// <summary>
        /// 送货状态
        /// </summary>
        public const int DeliveryStatusManage = 30;
        /// <summary>
        /// 数据追溯
        /// </summary>
        public const int MaterialTraceability = 50;
        #endregion


    }
}
