using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class AbnormalmoduleController : Controller
    {
        // GET: Abnormalmodule
        public ActionResult Abnormallog()
        {
            ViewData["crumb"] = "异常管理-->异常登记";
            return View();
        }

        public ActionResult AbnormallogEdit()
        {
            ViewData["crumb"] = "异常管理-->异常登记详细";
            return View();
        }

    }
}