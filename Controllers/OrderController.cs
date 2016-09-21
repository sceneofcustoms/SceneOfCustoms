using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        //空进列表
        public ActionResult AirIn_List()
        {
            ViewData["crumb"] = "关务操作-->空运进口";
            return View();
        }

        //空进编辑
        public ActionResult AirIn_Edit()
        {
            ViewData["crumb"] = "关务操作-->空运进口编辑";
            return View();
        }

        //空出列表
        public ActionResult AirOut_List()
        {
            ViewData["crumb"] = "关务操作-->空运出口";
            return View();
        }

        //空出编辑
        public ActionResult AirOut_Edit()
        {
            ViewData["crumb"] = "关务操作-->空运出口编辑";
            return View();
        }

        //海运进口列表
        public ActionResult SeaIn_List()
        {
            ViewData["crumb"] = "关务操作-->海运进口";
            return View();
        }

        //海运进口编辑
        public ActionResult SeaIn_Edit()
        {
            ViewData["crumb"] = "关务操作-->海运进口编辑";
            return View();
        }

        //海运出口列表
        public ActionResult SeaOut_List()
        {
            ViewData["crumb"] = "关务操作-->海运出口";
            return View();
        }

        //海运出口编辑
        public ActionResult SeaOut_Edit()
        {
            ViewData["crumb"] = "关务操作-->海运出口编辑";
            return View();
        }


        //陆运进口列表
        public ActionResult LandIn_List()
        {
            ViewData["crumb"] = "关务操作-->陆运进口";
            return View();
        }



        //陆运进口编辑
        public ActionResult LandIn_Edit()
        {
            ViewData["crumb"] = "关务操作-->陆运进口编辑";
            return View();
        }


        //陆运出口列表
        public ActionResult LandOut_List()
        {
            ViewData["crumb"] = "关务操作-->陆运出口";
            return View();
        }

        //陆运出口编辑
        public ActionResult LandOut_Edit()
        {
            ViewData["crumb"] = "关务操作-->陆运出口编辑";
            return View();
        }

        //特殊监管列表
        public ActionResult SpecialSupervision_List()
        {
            ViewData["crumb"] = "关务操作-->特殊监管";
            return View();
        }

        //特殊监管编辑
        public ActionResult SpecialSupervision_Edit()
        {
            ViewData["crumb"] = "关务操作-->特殊监管编辑";
            return View();
        }

        //叠加保税列表
        public ActionResult OverlayBonded_List()
        {
            ViewData["crumb"] = "关务操作-->叠加保税";
            return View();
        }

        //叠加保税编辑
        public ActionResult OverlayBonded_Edit()
        {
            ViewData["crumb"] = "关务操作-->叠加保税编辑";

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
                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where ASSOCIATENO='" + ASSOCIATENO + "' and CODE !=" + CODE;
                dt = DBMgr.GetDataTable(sql);
                ViewData["id2"] = dt.Rows[0]["ID"] + "";// 第二个订单

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CORRESPONDNO='" + correspondno + "' and ASSOCIATENO !='" + ASSOCIATENO + "' and BUSITYPE = 41";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id3"] = dt.Rows[0]["ID"] + "";// 第三个订单

                sql = "select ID,CODE, ASSOCIATENO,CORRESPONDNO from list_order where CORRESPONDNO='" + correspondno + "' and ASSOCIATENO !='" + ASSOCIATENO + "' and BUSITYPE = 40";
                dt = DBMgr.GetDataTable(sql);
                ViewData["id4"] = dt.Rows[0]["ID"] + "";// 第四个订单
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
            ViewData["crumb"] = "关务操作-->国内结转";
            return View();
        }

        //国内结转编辑
        public ActionResult DomesticKnot_Edit()
        {
            ViewData["crumb"] = "关务操作-->国内结转编辑";
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

        public string Get_SBGQ()
        {
            IDatabase db = SeRedis.redis.GetDatabase();
            string json_sbgq = "[]";//申报关区 进口口岸 
            if (db.KeyExists("common_data:sbgq"))
            {
                json_sbgq = db.StringGet("common_data:sbgq");
            }
            else
            {
                string sql = "select CODE,NAME||'('||CODE||')' NAME from BASE_CUSTOMDISTRICT  where ENABLED=1 ORDER BY CODE";
                json_sbgq = JsonConvert.SerializeObject(DB_BaseData.GetDataTable(sql));
                db.StringSet("common_data:sbgq", json_sbgq);
            }
            return json_sbgq;
        }

        //报关车号 
        public string Get_BGCH()
        {
            IDatabase db = SeRedis.redis.GetDatabase();
            string json_truckno = "[]";
            if (db.KeyExists("common_data:truckno"))
            {
                json_truckno = db.StringGet("common_data:truckno");
            }
            else
            {
                string sql = @"select t.license, t.license||'('||t.whitecard||')' as MERGENAME,t.whitecard,t1.NAME||'('|| t1.CODE||')' as UNITNO from sys_declarationcar t
                left join base_motorcade t1 on t.motorcade=t1.code where t.enabled=1";
                json_truckno = JsonConvert.SerializeObject(DB_BaseData.GetDataTable(sql));
                db.StringSet("common_data:truckno", json_truckno);
            }
            return json_truckno;
        }


        public string Init_Base_Data()
        {
            return "";
        }


        public string Edit_Order()
        {
            string ID = Request.QueryString["ID"];
            string sql = "select t.*, t.rowid from list_order t where  ID = " + ID;
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            return result;
        }



        [HttpGet]
        public string GetData()
        {
            string BUSITYPE = Request.Params["BUSITYPE"];
            string TYPE = Request.Params["TYPE"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;

            string sql = "select t.* from (select *　from list_order ) t where 1=1  ";

            if (!string.IsNullOrEmpty(BUSITYPE))
            {
                sql += " and BUSITYPE =" + BUSITYPE;
            }

            if (TYPE == "SpecialSupervision")
            {
                sql += " and BUSITYPE in (50,51) "; //特殊监管
            }
            else if (TYPE == "OverlayBonded")
            {
                sql += " and BUSITYPE in (40,41) and CORRESPONDNO is not null";//叠加保税
            }
            else if (TYPE == "DomesticKnot")
            {
                sql += " and BUSITYPE in (40,41) and CORRESPONDNO is  null";//国内结转
            }


            sql = Extension.GetPageSql(sql, "ID", "desc", ref total, (Page - 1) * PageSize, Page * PageSize);
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = "{\"total\":" + total + ",\"rows\":" + result + "}";
            return result;

        }


        [HttpPost]
        public ActionResult SaveData(FormCollection form)
        {

            string ID = Request.Form["ID"];
            string sql = "update list_order set ";



            if (Request.Params.AllKeys.Contains("REPUNITCODE"))
            {
                sql += "  REPUNITCODE =  '" + Request.Form["REPUNITCODE"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CUSTOMDISTRICTCODE"))
            {
                sql += "  CUSTOMDISTRICTCODE =  '" + Request.Form["CUSTOMDISTRICTCODE"] + "',";
            }

            sql += "  PASSMODE =  '" + Request.Form["PASSMODE"] + "',";

            sql += "  IFCHAYAN =  '" + Request.Form["IFCHAYAN"] + "',";

            sql += "  KOUHUOSIGN =  '" + Request.Form["KOUHUOSIGN"] + "',";

            sql += "  IFTIAODANG =  '" + Request.Form["IFTIAODANG"] + "',";

            sql += "  LIHUOSIGN =  '" + Request.Form["LIHUOSIGN"] + "',";


            if (Request.Params.AllKeys.Contains("CHAYANTIMES"))
            {
                sql += "  CHAYANTIMES =  '" + Request.Form["CHAYANTIMES"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANREMARK"))
            {
                sql += "  CHAYANREMARK =  '" + Request.Form["CHAYANREMARK"] + "',";
            }


            if (Request.Params.AllKeys.Contains("LIHUOTIMES"))
            {
                sql += "  LIHUOTIMES =  '" + Request.Form["LIHUOTIMES"] + "',";
            }


            if (Request.Params.AllKeys.Contains("TIAODANGTIMES"))
            {
                sql += "  TIAODANGTIMES =  '" + Request.Form["TIAODANGTIMES"] + "',";
            }

            if (Request.Params.AllKeys.Contains("DECLCARNO"))
            {
                sql += "  DECLCARNO =  '" + Request.Form["DECLCARNO"] + "',";
            }

            if (Request.Params.AllKeys.Contains("IFKXCHAYAN"))
            {
                sql += "  IFKXCHAYAN =  '" + Request.Form["IFKXCHAYAN"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIDANDESC"))
            {
                sql += "  LIDANDESC =  '" + Request.Form["LIDANDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIHUOZILIAODESC"))
            {
                sql += "  LIHUOZILIAODESC =  '" + Request.Form["LIHUOZILIAODESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("BAOGUANDESC"))
            {
                sql += "  BAOGUANDESC =  '" + Request.Form["BAOGUANDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("DANZHENGFANGXINGDESC"))
            {
                sql += "  DANZHENGFANGXINGDESC =  '" + Request.Form["DANZHENGFANGXINGDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANSTARTDESC"))
            {
                sql += "  CHAYANSTARTDESC =  '" + Request.Form["CHAYANSTARTDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANENDDESC"))
            {
                sql += "  CHAYANENDDESC =  '" + Request.Form["CHAYANENDDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIHUOSTARTDESC"))
            {
                sql += "  LIHUOSTARTDESC =  '" + Request.Form["LIHUOSTARTDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIHUOENDDESC"))
            {
                sql += "  LIHUOENDDESC =  '" + Request.Form["LIHUOENDDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("SHIWUFANGXINGDESC"))
            {
                sql += "  SHIWUFANGXINGDESC =  '" + Request.Form["SHIWUFANGXINGDESC"] + "',";
            }

            if (Request.Params.AllKeys.Contains("SHIWUJIAFENGDESC"))
            {
                sql += "  SHIWUJIAFENGDESC =  '" + Request.Form["SHIWUJIAFENGDESC"] + "',";
            }



            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID =" + ID;

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Edit_Ajax_Scene(FormCollection form)
        {
            string ID = Request.Form["ID"];
            string type = Request.Form["type"];
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string sql = "update list_order set ";
            if (type != "")
            {
                string time = type + "TIME";
                string userid = type + "USERID";
                string username = type + "USERNAME";
                sql += time + "  = sysdate ," + username + " ='" + jo.Value<string>("REALNAME") + "', " + userid + " =  " + jo.Value<string>("ID");
            }
            sql += " where ID =" + ID;

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                var datetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                return Json(new { Success = true, datetime = datetime, name = jo.Value<string>("REALNAME") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}