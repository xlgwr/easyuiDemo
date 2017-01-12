using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Valeo.Domain;
using Valeo.Service;
using Valeo.Lang;

namespace Valeo.Controllers.PackageBuy
{
    public class PersonController : Controller
    {
        PersonService service = new PersonService();

        #region 【查询处理】

        /// <summary>
        /// 人员信息列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string condition = "")
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            var obj = service.GetPersonByCondition(page, 3, string.Empty);

            //分页数据
            ViewBag.Data = jss.Serialize(obj.Items);
            //总页数
            ViewBag.TotalPage = obj.TotalPages;
            //当前页
            ViewBag.CurrentPage = obj.CurrentPage;

            return View(ViewBag);
        }

        #endregion

        #region 【新增处理】

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 新增处理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(FormCollection form)
        {
            Person person = new Person() {
                PersonID = Guid.NewGuid().ToString(),
                PersonName = form["PersonName"], 
                PersonAge = Convert.ToInt32(form["PersonAge"])
            };

            if (service.AddPerson(person))
            {
                return Json(new { result = BaseRes.COM_MSG_ADD_SUC });
            }
            else
            {
                return Json(new { result = BaseRes.COM_MSG_ADD_FAIL });
            }
        }

        #endregion

        #region 【修改处理】

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="PersonID">人员编号：注意和路由规则一致</param>
        /// <returns></returns>
        public ActionResult Edit(string PersonID)
        {
            var list = service.GetPersonList(string.Format(" PersonID = '{0}'", PersonID));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ViewBag.Data = jss.Serialize(list);

            return View("Edit");
        }

        /// <summary>
        /// 编辑处理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(FormCollection form)
        {
            Person person = new Person()
            {
                PersonID = form["PersonID"],
                PersonName = form["PersonName"],
                PersonAge = Convert.ToInt32(form["PersonAge"])
            };

            if (service.ModifyPerson(person))
            {
                return Json(new { result = BaseRes.COM_MSG_UPD_SUC });
            }
            else
            {
                return Json(new { result = BaseRes.COM_MSG_UPD_FAIL });
            }
        }

        #endregion

        #region 【删除处理】

        /// <summary>
        /// 删除处理
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(string PersonID)
        {
            if (service.DeletePerson(PersonID))
            {
                return Json(new { result = BaseRes.COM_MSG_DEL_SUC });
            }
            else
            {
                return Json(new { result = BaseRes.COM_MSG_DEL_FAIL });
            }
        }

        #endregion
    }
}