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
            return View();
        }

        //空进编辑
        public ActionResult AirIn_Edit()
        {
            return View();
        }

        //空出列表
        public ActionResult AirOut_List()
        {
            return View();
        }

        //空出编辑
        public ActionResult AirOut_Edit()
        {
            return View();
        }

        //海运进口列表
        public ActionResult SeaIn_List()
        {
            return View();
        }

        //海运进口编辑
        public ActionResult SeaIn_Edit()
        {
            return View();
        }

        //海运出口列表
        public ActionResult SeaOut_List()
        {
            return View();
        }

        //海运出口编辑
        public ActionResult SeaOut_Edit()
        {
            return View();
        }






        //陆运进口列表
        public ActionResult LandIn_List()
        {

            return View();
        }



        //陆运进口编辑
        public ActionResult LandIn_Edit()
        {

            return View();
        }


        //陆运出口列表
        public ActionResult LandOut_List()
        {
            return View();
        }

        //陆运出口编辑
        public ActionResult LandOut_Edit()
        {
            return View();
        }

        //特殊监管列表
        public ActionResult SpecialSupervision_List()
        {

            return View();
        }

        //特殊监管编辑
        public ActionResult SpecialSupervision_Edit()
        {

            return View();
        }

        //叠加保税列表
        public ActionResult OverlayBonded_List()
        {
            return View();
        }

        //叠加保税编辑
        public ActionResult OverlayBonded_Edit()
        {
            string ID = Request["ID"];


            ViewData["name"] = "lakers";
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
        public string GetSeaInList()
        {
            string sql = "select t.*, t.rowid from list_order t where 1 = 1 and t.busitype='11'";
            string cusno = Request["CUSNO"];
            string contractno = Request["CONTRACTNO"];
            string sort = Request["sort"];
            string order = Request["order"];
            if (string.IsNullOrEmpty(sort))
                sort = "CUSNO";
            if (string.IsNullOrEmpty(order))
                order = "ASC";
            if (!string.IsNullOrEmpty(cusno))
            {
                sql += " and CUSNO like '%" + cusno + "%'";
            }
            if (!string.IsNullOrEmpty(contractno))
            {
                sql += "and CONTRACTNO = '" + contractno + "'";
            }

            sql += " order by " + sort + " " + order + "";
            //string sql = "select t.*, t.rowid from list_order t where 1 = 1 and cnsno like '%" + cnsno + "%' and contractno = '" + contractno + "' t.busitype='11' order by "+sort+" "+order+"";// t.busitype='11' order by "+sort+" "+order+"";
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = JsonConvert.SerializeObject(dt);
            int totalRow = dt.Rows.Count;
            var str = "{\"total\":" + totalRow + ",\"rows\":" + result + "}";

            return str;
        }





        [HttpGet]
        public string GetData()
        {
            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = JsonConvert.SerializeObject(dt);
            result = "{\"total\":\"28\",\"rows\":" + result + "}";
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

            if (Request.Params.AllKeys.Contains("PASSMODE"))
            {
                sql += "  PASSMODE =  '" + Request.Form["PASSMODE"] + "',";
            }

            if (Request.Params.AllKeys.Contains("IFCHAYAN"))
            {
                sql += "  IFCHAYAN =  '" + Request.Form["IFCHAYAN"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANTIMES"))
            {
                sql += "  CHAYANTIMES =  '" + Request.Form["CHAYANTIMES"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANREMARK"))
            {
                sql += "  CHAYANREMARK =  '" + Request.Form["CHAYANREMARK"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIHUOSIGN"))
            {
                sql += "  LIHUOSIGN =  '" + Request.Form["LIHUOSIGN"] + "',";
            }

            if (Request.Params.AllKeys.Contains("LIHUOTIMES"))
            {
                sql += "  LIHUOTIMES =  '" + Request.Form["LIHUOTIMES"] + "',";
            }


            if (Request.Params.AllKeys.Contains("KOUHUOSIGN"))
            {
                sql += "  KOUHUOSIGN =  '" + Request.Form["KOUHUOSIGN"] + "',";
                sql += "  KOUHUOTIME = sysdate ,";
            }

            if (Request.Params.AllKeys.Contains("IFTIAODANG"))
            {
                sql += "  IFTIAODANG =  '" + Request.Form["IFTIAODANG"] + "',";
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