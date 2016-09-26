﻿using SceneOfCustoms.Common;
using System;
using System.Collections.Generic;
using System.Data;
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


        //国内结转列表
        public ActionResult DomesticKnot_List()
        {
            ViewData["crumb"] = "报检操作-->国内业务";
            return View();
        }

        //国内结转编辑
        public ActionResult DomesticKnot_Edit()
        {
            ViewData["crumb"] = "报检操作-->国内业务编辑";
            string ID = Request["ID"];
            string sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where id=" + ID;
            DataTable dt = DBMgr.GetDataTable(sql);

            if (!string.IsNullOrEmpty(dt.Rows[0]["ASSOCIATENO"] + ""))
            {
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";
                string CODE = ASSOCIATENO.Replace("GL", "");

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CODE=" + CODE + " and BUSITYPE =41";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id1"] = dt.Rows[0]["ID"] + "";//一单ID

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and BUSITYPE =40";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id2"] = dt.Rows[0]["ID"] + "";//二单ID
            }

            return View();
        }

        //一线进口

        public ActionResult OnelineIn_List()
        {


            ViewData["crumb"] = "报检操作-->一线进口";
            return View();
        }



    }
}