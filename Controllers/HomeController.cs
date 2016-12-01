using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["crumb"] = "关务系统>>首页";

            return View();
        }
    }
}