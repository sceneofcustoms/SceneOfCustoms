using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class RetreatorderController : Controller
    {
        // 退单管理列表
        public ActionResult BackOrder_List()
        {
            ViewData["crumb"] = "退单管理-->退单管理";
            return View();
        }
        // 签收操作列表
        public ActionResult SignOrder_List()
        {
            ViewData["crumb"] = "退单管理-->签收操作";
            return View();
        }
    }
}