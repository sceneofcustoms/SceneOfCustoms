using Newtonsoft.Json;
using SceneCustoms.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index()
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
            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = JsonConvert.SerializeObject(dt);
            return result;
        }


        [HttpGet]
        public ActionResult Edit()
        {
            //var str1 = "{\"name\":\"easyui\", \"email\":\"easyui@gmail.com\", \"subject\":\"Subject Title\", \"message\":\"Message Content\", \"language\":\"de\"}";
            //ViewData["str1"] = str1;
            return View();
        }



        [HttpGet]
        public string GetData()
        {

            var str1 = "{\"total\":\"28\",\"rows\":[{\"productid\":\"FI-SW-01\",\"productname\":\"Koi\",\"unitcost\":\"10.00\",\"status\":\"P\",\"listprice\":\"36.50\",\"attr1\":\"Large\",\"itemid\":\"EST-1\"}]}";
            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = JsonConvert.SerializeObject(dt);
            var str = "{\"total\":\"28\",\"rows\":" + result + "}";

            return str;

        }

        [HttpPost]
        public ActionResult SaveData(FormCollection form)
        {
            string str = form["name"];
            string str1 = Request.Form["name"];
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}