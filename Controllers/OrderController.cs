using Newtonsoft.Json;
using SceneCustoms.Common;
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

        [HttpGet]
        public string GetData()
        {

            var str1 = "{\"total\":\"28\",\"rows\":[{\"productid\":\"FI-SW-01\",\"productname\":\"Koi\",\"unitcost\":\"10.00\",\"status\":\"P\",\"listprice\":\"36.50\",\"attr1\":\"Large\",\"itemid\":\"EST-1\"}]}";


            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = JsonConvert.SerializeObject(dt);
            var str= "{\"total\":\"28\",\"rows\":"+result+"}";

            return str;


        }
	}
}