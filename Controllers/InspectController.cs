using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    //退单管理
    [Authorize]
    public class InspectController : Controller
    {
        //空进列表
        public ActionResult AirIn_List()
        {
            ViewData["crumb"] = "报检操作-->空运进口";
            return View();
        }

        //空进编辑
        public ActionResult AirIn_Edit()
        {
            ViewData["crumb"] = "报检操作-->空运进口编辑";
            return View();
        }

        //空出列表
        public ActionResult AirOut_List()
        {
            ViewData["crumb"] = "报检操作-->空运出口";
            return View();
        }

        //空出编辑
        public ActionResult AirOut_Edit()
        {
            ViewData["crumb"] = "报检操作-->空运出口编辑";
            return View();
        }

        //海运进口列表
        public ActionResult SeaIn_List()
        {
            ViewData["crumb"] = "报检操作-->海运进口";
            return View();
        }

        //海运进口编辑
        public ActionResult SeaIn_Edit()
        {
            ViewData["crumb"] = "报检操作-->海运进口编辑";
            return View();
        }

        //海运出口列表
        public ActionResult SeaOut_List()
        {
            ViewData["crumb"] = "报检操作-->海运出口";
            return View();
        }

        //海运出口编辑
        public ActionResult SeaOut_Edit()
        {
            ViewData["crumb"] = "报检操作-->海运出口编辑";
            return View();
        }


        //陆运进口列表
        public ActionResult LandIn_List()
        {
            ViewData["crumb"] = "报检操作-->陆运进口";
            return View();
        }


        //陆运进口编辑
        public ActionResult LandIn_Edit()
        {
            ViewData["crumb"] = "报检操作-->陆运进口编辑";
            return View();
        }


        //陆运出口列表
        public ActionResult LandOut_List()
        {
            ViewData["crumb"] = "报检操作-->陆运出口";
            return View();
        }

        //陆运出口编辑
        public ActionResult LandOut_Edit()
        {
            ViewData["crumb"] = "报检操作-->陆运出口编辑";
            return View();
        }

    }
}