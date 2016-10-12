using SceneOfCustoms.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    //报关操作
    [Authorize]
    public class DeclareController : Controller
    {

        //一线进口列表
        public ActionResult OnelineIn_List()
        {
            ViewData["crumb"] = "报关操作-->一线进口";
            return View();
        }

        //一线进口编辑
        public ActionResult OnelineIn_Edit()
        {
            ViewData["crumb"] = "报关操作-->一线进口编辑";
            return View();
        }

        //一线出口列表
        public ActionResult OnelineOut_List()
        {
            ViewData["crumb"] = "报关操作-->一线出口";
            return View();
        }

        //一线出口编辑
        public ActionResult OnelineOut_Edit()
        {
            ViewData["crumb"] = "报关操作-->一线出口编辑";
            return View();
        }

        //结转BLC列表
        public ActionResult DomesticBlc_List()
        {
            ViewData["crumb"] = "报关操作-->结转BLC";
            return View();
        }

        //结转BLC编辑
        public ActionResult DomesticBlc_Edit()
        {
            string ID = Request["ID"];
            string sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where id=" + ID;
            DataTable dt = DBMgr.GetDataTable(sql);

            if (!string.IsNullOrEmpty(dt.Rows[0]["ASSOCIATENO"] + ""))
            {
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";
                string CODE = ASSOCIATENO.Replace("GL", "");

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CODE=" + CODE + " and BUSITYPE ='41'";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id1"] = dt.Rows[0]["ID"] + "";//一单ID

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO,BUSIUNITNAME from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and BUSITYPE ='40'";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id2"] = dt.Rows[0]["ID"] + "";//二单ID
                ViewData["BUSIUNITNAME"] = dt.Rows[0]["BUSIUNITNAME"] + "";
            }

            ViewData["crumb"] = "报关操作-->结转BLC编辑";
            return View();
        }

        //空进列表
        public ActionResult AirIn_List()
        {
            ViewData["crumb"] = "报关操作-->空运进口";
            return View();
        }

        //空进编辑 
        public ActionResult AirIn_Edit()
        {
            ViewData["crumb"] = "报关操作-->空运进口编辑";
            return View();
        }

        //空出列表
        public ActionResult AirOut_List()
        {
            ViewData["crumb"] = "报关操作-->空运出口";
            return View();
        }

        //空出编辑
        public ActionResult AirOut_Edit()
        {
            ViewData["crumb"] = "报关操作-->空运出口编辑";
            return View();
        }

        //海运进口列表
        public ActionResult SeaIn_List()
        {
            ViewData["crumb"] = "报关操作-->海运进口";
            return View();
        }

        //海运进口编辑
        public ActionResult SeaIn_Edit()
        {
            ViewData["crumb"] = "报关操作-->海运进口编辑";
            return View();
        }

        //海运出口列表
        public ActionResult SeaOut_List()
        {
            ViewData["crumb"] = "报关操作-->海运出口";
            return View();
        }

        //海运出口编辑
        public ActionResult SeaOut_Edit()
        {
            ViewData["crumb"] = "报关操作-->海运出口编辑";
            return View();
        }


        //陆运进口列表
        public ActionResult LandIn_List()
        {
            ViewData["crumb"] = "报关操作-->陆运进口";
            return View();
        }



        //陆运进口编辑
        public ActionResult LandIn_Edit()
        {
            ViewData["crumb"] = "报关操作-->陆运进口编辑";
            return View();
        }


        //陆运出口列表
        public ActionResult LandOut_List()
        {
            ViewData["crumb"] = "报关操作-->陆运出口";
            return View();
        }

        //陆运出口编辑
        public ActionResult LandOut_Edit()
        {
            ViewData["crumb"] = "报关操作-->陆运出口编辑";
            return View();
        }

        //特殊监管列表
        public ActionResult SpecialSupervision_List()
        {
            ViewData["crumb"] = "报关操作-->特殊监管";
            return View();
        }

        //特殊监管编辑
        public ActionResult SpecialSupervision_Edit()
        {
            ViewData["crumb"] = "报关操作-->特殊监管编辑";
            return View();
        }

        //叠加保税列表
        public ActionResult OverlayBonded_List()
        {
            ViewData["crumb"] = "报关操作-->叠加保税";
            return View();
        }

        //叠加保税编辑
        public ActionResult OverlayBonded_Edit()
        {
            ViewData["crumb"] = "报关操作-->叠加保税编辑";

            string ID = Request["ID"];
            string sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where id=" + ID;
            DataTable dt = DBMgr.GetDataTable(sql);

            if (!string.IsNullOrEmpty(dt.Rows[0]["CORRESPONDNO"] + ""))
            {
                //有2个tab
                string correspondno = dt.Rows[0]["CORRESPONDNO"] + "";//四单关联号

                string CODE = correspondno.Replace("GF", ""); // 第一个订单
                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CODE=" + CODE;
                dt = DBMgr.GetDataTable(sql);
                ViewData["id1"] = dt.Rows[0]["ID"] + "";

                string ASSOCIATENO = correspondno.Replace("GF", "GL");
                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO,BUSIUNITNAME from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and CODE !='" + CODE + "'";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id2"] = dt.Rows[0]["ID"] + "";// 第二个订单
                ViewData["BUSIUNITNAME2"] = dt.Rows[0]["BUSIUNITNAME"] + "";


                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CORRESPONDNO='" + correspondno + "' and ASSOCIATENO !='" + ASSOCIATENO + "' and BUSITYPE = '41'";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id3"] = dt.Rows[0]["ID"] + "";// 第三个订单

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO,BUSIUNITNAME from list_order where CORRESPONDNO='" + correspondno + "' and ASSOCIATENO !='" + ASSOCIATENO + "' and BUSITYPE = '40'";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id4"] = dt.Rows[0]["ID"] + "";// 第四个订单
                ViewData["BUSIUNITNAME4"] = dt.Rows[0]["BUSIUNITNAME"] + "";
            }
            else
            {
                //1个tab
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";//二单关联号
                if (!string.IsNullOrEmpty(ASSOCIATENO))
                {
                    string CODE = ASSOCIATENO.Replace("GL", ""); // 第一个订单
                    sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CODE=" + CODE;
                    dt = DBMgr.GetDataTable(sql);
                    ViewData["id1"] = dt.Rows[0]["ID"] + "";

                    sql = "select ID,CODE,ASSOCIATENO,CORRESPONDNO from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and CODE !=" + CODE;
                    dt = DBMgr.GetDataTable(sql);
                    ViewData["id2"] = dt.Rows[0]["ID"] + "";// 第二个订单
                }

            }

            return View();
        }

        //国内结转列表
        public ActionResult DomesticKnot_List()
        {
            ViewData["crumb"] = "报关操作-->国内结转";
            return View();
        }

        //国内结转编辑
        public ActionResult DomesticKnot_Edit()
        {
            ViewData["crumb"] = "报关操作-->国内结转编辑";
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

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO,BUSIUNITNAME from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and BUSITYPE =40";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id2"] = dt.Rows[0]["ID"] + "";//二单ID
                ViewData["BUSIUNITNAME"] = dt.Rows[0]["BUSIUNITNAME"] + "";
            }

            return View();
        }
    }
}