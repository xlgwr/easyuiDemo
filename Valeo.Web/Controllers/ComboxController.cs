using Valeo.Common;
using Valeo.Domain;
using Valeo.Domain.Parameter;
using Valeo.Service.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valeo.Service;
using Valeo.Domain.Valeo;

namespace Valeo.Controllers
{
    public class ComboxController : BaseController
    {
        ParameterService pservice = new ParameterService();
        // GET: Combox
        [HttpGet]
        public JsonResult Get(string id, int id2 = 0)
        {
            try
            {
                EnumCombox tmpenum = (EnumCombox)Enum.Parse(typeof(EnumCombox), id, true);


                var model = getCombox(tmpenum);

                if (id2 > 0)
                {
                    if (model != null)
                    {
                        model.Add(new Domain.Combox.ComboxListModel()
                        {
                            ComboxListKey = -1,
                            ComboxListName = "全部"
                        });
                    }
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "获取下拉数据出错。ID:" + id), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 获取系统参数币种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllCurrencys(int id = 0)
        {
            try
            {
                var model = pservice.GetAllCurrencys();
                if (id > 0)
                {
                    var ParameterModel = new ParameterModel() { Paramkey = "-1", Paramvalue = "全部", DspNo = -1, Paramtype = 0 };
                    model.Add(ParameterModel);

                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "得到所有的币种出错。"), JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 获取卡板类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult cardType(int id = 0)
        {
            try
            {
                v_cardService vserver = new v_cardService();

                var model = vserver.GetAllv_card();
                if (id > 0)
                {
                    var ParameterModel = new v_card() { cardNO = "全部"};
                    model.Add(ParameterModel);

                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "得到所有的卡板类型出错。"), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 获取纸箱型号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult cartonType(int id = 0)
        {
            try
            {
                v_cartonService vserver = new v_cartonService();

                var model = vserver.GetAllv_carton();
                if (id > 0)
                {
                    var ParameterModel = new v_carton() {  cartonNO = "全部" };
                    model.Add(ParameterModel);

                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonHandler.CreateMessage(0, "得到所有的纸箱型号出错。"), JsonRequestBehavior.AllowGet);
            }

        }
    }
}