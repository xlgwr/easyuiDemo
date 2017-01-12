using Valeo.Domain;
using Valeo.Service;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Valeo.Controllers.Email
{
    public class UserSelController : Controller
    {
        //业务处理对象
        EmailService Service = new EmailService();

        //
        // GET: /UserSel/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UserListPage(string serch, long page = 1, long rows = 10)
        {

            Page<MemberModel> list = Service.GetMenberList(page, rows, serch);
            var emailRecod = list.Items;
            var emailRecodlist = from recod in emailRecod
                                 select new
                                 {
                                     recod.MemberID,
                                     recod.MemberName,
                                     recod.Email,

                                 };


            var result = new
            {
                total = list.TotalItems,
                rows = emailRecodlist
            };
            return Json(result);
        }
	}
}