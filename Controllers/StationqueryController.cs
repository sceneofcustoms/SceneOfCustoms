using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class StationqueryController : Controller
    {
        // GET: Stationquery
        public ActionResult Carcardquery()
        {
            ViewData["crumb"] = "站场查询-->车卡查询";
            return View();
        }
    }
}