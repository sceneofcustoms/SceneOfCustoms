using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public ActionResult SeaInList()
        {
            return View();
        }

        //海运出口列表
        public ActionResult SeaOut_List()
        {
            return View();
        }


        //陆运进口列表
        public ActionResult LandIn_List() {

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


        public string Init_Base_Data()
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

            string REPUNITCODE = Request.Form["REPUNITCODE"];
            if (!string.IsNullOrEmpty(REPUNITCODE))
            {
                sql += "  REPUNITCODE =  '" + REPUNITCODE + "',";
            }

            string CUSTOMDISTRICTCODE = Request.Form["CUSTOMDISTRICTCODE"];
            if (!string.IsNullOrEmpty(CUSTOMDISTRICTCODE))
            {
                sql += "  CUSTOMDISTRICTCODE =  '" + CUSTOMDISTRICTCODE + "',";
            }


            string PASSMODE = Request.Form["PASSMODE"];
            if (!string.IsNullOrEmpty(PASSMODE))
            {
                sql += "  PASSMODE =  '" + PASSMODE + "',";
            }

            string IFCHAYAN = Request.Form["IFCHAYAN"];
            if (!string.IsNullOrEmpty(IFCHAYAN))
            {
                sql += "  IFCHAYAN =  '" + IFCHAYAN + "',";
            }


            string CHAYANTIMES = Request.Form["CHAYANTIMES"];
            if (!string.IsNullOrEmpty(CHAYANTIMES))
            {
                sql += "  CHAYANTIMES =  '" + CHAYANTIMES + "',";
            }

            string CHAYANREMARK = Request.Form["CHAYANREMARK"];
            if (!string.IsNullOrEmpty(CHAYANREMARK))
            {
                sql += "  CHAYANREMARK =  '" + CHAYANREMARK + "',";
            }

            string LIHUOSIGN = Request.Form["LIHUOSIGN"];
            if (!string.IsNullOrEmpty(LIHUOSIGN))
            {
                sql += "  LIHUOSIGN =  '" + LIHUOSIGN + "',";
            }

            string LIHUOTIMES = Request.Form["LIHUOTIMES"];
            if (!string.IsNullOrEmpty(LIHUOTIMES))
            {
                sql += "  LIHUOTIMES =  '" + LIHUOTIMES + "',";
            }

            string KOUHUOSIGN = Request.Form["KOUHUOSIGN"];
            if (!string.IsNullOrEmpty(KOUHUOSIGN))
            {
                sql += "  KOUHUOSIGN =  '" + KOUHUOSIGN + "',";

                sql += "  KOUHUOTIME = sysdate ,";
            }

            //string KOUHUOTIME = Request.Form["KOUHUOTIME"];
            //if (!string.IsNullOrEmpty(KOUHUOTIME))
            //{
            //    sql += "  KOUHUOTIME =  '" + KOUHUOTIME + "',";
            //}

            string IFTIAODANG = Request.Form["IFTIAODANG"];
            if (!string.IsNullOrEmpty(IFTIAODANG))
            {
                sql += "  IFTIAODANG =  '" + IFTIAODANG + "',";
            }
            string TIAODANGTIMES = Request.Form["TIAODANGTIMES"];
            if (!string.IsNullOrEmpty(TIAODANGTIMES))
            {
                sql += "  TIAODANGTIMES =  '" + TIAODANGTIMES + "',";
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID =" + ID;

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                return Json(new { Success = true}, JsonRequestBehavior.AllowGet);
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


            string sql = "update list_order set ";
            if (type != "")
            {
                string time = type + "TIME";
                string userid = type + "USERID";
                string username = type + "USERNAME";
                sql += time + "  = sysdate ," + userid + " =1, " + username + " ='lakers' ";
            }
            sql += " where ID =" + ID;

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                var datetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                return Json(new { Success = true, datetime = datetime }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}