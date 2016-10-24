using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SceneOfCustoms.Controllers
{
    public class CommonController : Controller
    {
        int total = 0;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Attachment_List()
        {
            ViewData["crumb"] = "后台管理-->随附文件";
            return View();
        }
        public ActionResult SyncFoo_List()
        {
            ViewData["crumb"] = "后台管理-->同步日志";
            return View();
        }

        public ActionResult SyncMsg_List()
        {
            ViewData["crumb"] = "后台管理-->同步状态";
            return View();
        }

        public string LoadAttachmentList()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string sql = @"select * from LIST_ATTACHMENT";
            DataTable dt = DBMgr.GetDataTable(Extension.GetPageSql(sql, "createtime", "desc", ref total, Convert.ToInt32(Request["start"]), Convert.ToInt32(Request["limit"])));
            string filedata = JsonConvert.SerializeObject(dt, iso);
            return "{rows:" + filedata + ",total:" + total + "}";
        }
        [HttpGet]
        public string GetData()
        {
            string BUSITYPE = "";
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "";

            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                BUSITYPE = jo.Value<string>("BUSITYPE");
            }
            else
            {
                BUSITYPE = Request["BUSITYPE"];
            }


            switch (BUSITYPE)
            {
                case "SyncFoo":
                    sql = " select * from LIST_SAPFOO where 1=1";
                    break;
                case "SyncMsg":
                    sql = " select * from MSG where 1=1";
                    break;
            }

            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    sql += " AND " + jo.Value<string>("ordercode") + " ='" + jo.Value<string>("ordercode_value") + "'";
                }
                if (jo.Value<string>("startdate") != "")
                {
                    sql += " AND TIME >= to_date('" + jo.Value<string>("startdate") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stopdate") != "")
                {
                    sql += " AND TIME <= to_date('" + jo.Value<string>("stopdate") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("customs_busitype") != null && jo.Value<string>("customs_busitype") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("customs_busitype") + "' ";
                }
            }

            string sort = !string.IsNullOrEmpty(Request.Params["sort"]) && Request.Params["sort"] != "text" ? Request.Params["sort"] : "ID";
            string order = !string.IsNullOrEmpty(Request.Params["order"]) ? Request.Params["order"] : "DESC";
            sql = Extension.GetPageSql(sql, sort, order, ref total, (Page - 1) * PageSize, Page * PageSize);
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = "{\"total\":" + total + ",\"rows\":" + result + "}";
            return result;
        }

        public ActionResult Attachment_Edit()
        {
            string FWONO = Request["FWONO"];
            string FOONO = Request["FOONO"];
            var Path = Server.MapPath("../");
            ViewData["Path"] = Path;
            if (!string.IsNullOrEmpty(FOONO) && !string.IsNullOrEmpty(FWONO))
            {
                ViewData["is_passed"] = "1";
                ViewData["FWONO"] = FWONO;
                ViewData["FOONO"] = FOONO;
            }
            else
            {
                ViewData["is_passed"] = "0";
            }
            return View();
        }

        public string load_file()
        {
            string FWONO = Request["FWONO"];
            string FOONO = Request["FOONO"];
            string sql = "select * from LIST_ATTACHMENT where fwono='" + FWONO + "' and foono ='" + FOONO + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            return "{rows:" + result + "}";
        }

        //文件上传
        public ActionResult UploadFile(int? chunk, string name)
        {
            var fileUpload = Request.Files[0];
            var uploadPath = Server.MapPath("/Upload/");
            chunk = chunk ?? 0;
            string FWONO = Request.QueryString["FWONO"];
            string FOONO = Request.QueryString["FOONO"];
            //回传TM 接口

                using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
                {
                    var buffer = new byte[fileUpload.InputStream.Length];
                    fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, buffer.Length);
                    string username = CurrentUser();
                    string sql = @"insert into list_attachment(ID,FILEPATH,FILENAME,FILESIZE,FWONO,FOONO,CREATENAME,CREATETIME,STATUS) 
                VALUES(LIST_ATTACHMENT_ID.Nextval,'/Upload/" + name + "','" + fileUpload.FileName + "'," + fileUpload.ContentLength + ",'" + FWONO + "','" + FOONO + "','" + username + "',sysdate,1)";
                    DBMgr.ExecuteNonQuery(sql);
                    string status = IFS.ZSZLSJ_TM(FWONO, FOONO);

                }
            return Content("chunk uploaded", "text/plain");

        }

        [HttpPost]
        //移除文件
        public string delete_file()
        {
            string ids = Request["ids"];
            string[] arr = ids.Split(',');
            string id = "";
            string sql = "";
            DataTable dt;
            FileInfo file;
            for (int i = 0; i < arr.Length; i++)
            {
                id = arr[i];
                sql = "select * from list_attachment where id=" + id;
                dt = DBMgr.GetDataTable(sql);
                file = new FileInfo(Server.MapPath(dt.Rows[0]["FILEPATH"] + ""));//指定文件路径
                if (file.Exists)//判断文件是否存在
                {
                    file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
                    file.Delete();//删除文件
                    sql = "delete from list_attachment where id=" + id;
                    DBMgr.ExecuteNonQuery(sql);
                }
            }
            return "1";
        }
        public string CurrentUser()
        {
            JObject json_user = Extension.Get_UserInfo(HttpContext.User.Identity.Name);

            if (json_user == null)
            {
                return "游客";
            }
            else
            {
                return json_user.GetValue("REALNAME") + "";
            }
        }
        public void ImportOrder_AirIn()
        {
            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,CUSTOMDISTRICTNAME,REPUNITCODE,
                REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,SUBMITUSERID,
                SUBMITUSERNAME,SUBMITUSERPHONE,CSPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND,
                BUSIKIND,ORDERWAY,CLEARUNIT,CLEARUNITNAME,CREATETIME,BUSISHORTCODE,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,BUSISHORTNAME,
                SCENEDECLAREID,SCENEINSPECTID,SUBMITTIME
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',
                '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',
                '{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}',sysdate,'{45}','{46}','{47}','{48}','{49}','{50}',
                '{51}',{52})";
                sql = string.Format(sql, "11", dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"],
                   dr["GOODSNUM"], dr["GOODSWEIGHT"], dr["WOODPACKINGID"], dr["CLEARANCENO"],
                  dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],
                  dr["CUSTOMDISTRICTNAME"], dr["REPUNITCODE"], dr["REPUNITCODE"],
                   dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"], dr["INSPUNITCODE"],
                   dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["ID"], dr["CREATEUSERNAME"],
                   dr["STATUS"], dr["SUBMITUSERID"], dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"],
                   dr["CSPHONE"], dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"],
                   dr["PACKKIND"], "001", "1", dr["CLEARUNIT"], dr["CLEARUNITNAME"], dr["BUSISHORTCODE"],
                   dr["SPECIALRELATIONSHIP"], dr["PRICEIMPACT"], dr["PAYPOYALTIES"],
                   dr["BUSISHORTNAME"], dr["SCENEDECLAREID"], dr["SCENEINSPECTID"], "to_date('" + dr["SUBMITTIME"] + "','yyyy-MM-dd HH24:mi:ss')");
                DBMgr.ExecuteNonQuery(sql);
            }
        }
        public void ImportOrder_GuoNei()
        {
            string sql = "select t.*, t.rowid from list_order t where (t.busitype='41' or t.busitype='40') and createtime>to_date('2016/8/1 00:00:00','yyyy-mm-dd hh24:mi:ss')";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,
                CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,
                CUSTOMDISTRICTNAME,REPUNITCODE,REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,
                INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,
                SUBMITUSERID,SUBMITUSERNAME,SUBMITUSERPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,
                TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND, CLEARUNIT,
                CLEARUNITNAME,CREATETIME,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,SCENEDECLAREID,
              SCENEINSPECTID,CORRESPONDNO,ASSOCIATENO,IETYPE
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}',
              '{5}','{6}','{7}','{8}','{9}','{10}',
              '{11}','{12}','{13}','{14}','{15}','{16}',
              '{17}','{18}','{19}','{20}','{21}','{22}',
              '{23}','{24}','{25}','{26}','{27}','{28}',
              '{29}','{30}','{31}','{32}','{33}','{34}', 
              '{35}', '{36}','{37}','{38}','{39}','{40}',
'{41}',sysdate, '{42}','{43}','{44}','{45}',
              '{46}','{47}','{48}','{49}')";


                sql = string.Format(sql, dr["BUSITYPE"], dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"], dr["GOODSNUM"], dr["GOODSWEIGHT"],
                  dr["WOODPACKINGID"], dr["CLEARANCENO"], dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],
                  dr["CUSTOMDISTRICTNAME"], dr["REPUNITCODE"], dr["REPUNITCODE"], dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"],
                   dr["INSPUNITCODE"], dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["CREATEUSERID"], dr["CREATEUSERNAME"], dr["STATUS"],
                  dr["SUBMITUSERID"], dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"], dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"], dr["PACKKIND"], dr["CLEARUNIT"],
                   dr["CLEARUNITNAME"], dr["SPECIALRELATIONSHIP"], dr["PRICEIMPACT"], dr["PAYPOYALTIES"], dr["SCENEDECLAREID"],

                   dr["SCENEINSPECTID"], dr["CORRESPONDNO"], dr["ASSOCIATENO"], dr["IETYPE"]);


                DBMgr.ExecuteNonQuery(sql);
            }
        }
        public void ImportOrder()
        {
            string sql = "select t.*, t.rowid from list_order t where t.busitype='10' ";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,
                CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,
                CUSTOMDISTRICTNAME,REPUNITCODE,REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,
                INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,
                SUBMITUSERID,SUBMITUSERNAME,SUBMITUSERPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,
                TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND, CLEARUNIT,
                CLEARUNITNAME,CREATETIME,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,SCENEDECLAREID,
              SCENEINSPECTID,CORRESPONDNO,ASSOCIATENO,IETYPE
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}',
              '{5}','{6}','{7}','{8}','{9}','{10}',
              '{11}','{12}','{13}','{14}','{15}','{16}',
              '{17}','{18}','{19}','{20}','{21}','{22}',
              '{23}','{24}','{25}','{26}','{27}','{28}',
              '{29}','{30}','{31}','{32}','{33}','{34}', 
              '{35}', '{36}','{37}','{38}','{39}','{40}',
'{41}',sysdate, '{42}','{43}','{44}','{45}',
              '{46}','{47}','{48}','{49}')";


                sql = string.Format(sql, dr["BUSITYPE"], dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"], dr["GOODSNUM"], dr["GOODSWEIGHT"],
                  dr["WOODPACKINGID"], dr["CLEARANCENO"], dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],
                  dr["CUSTOMDISTRICTNAME"], dr["REPUNITCODE"], dr["REPUNITCODE"], dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"],
                   dr["INSPUNITCODE"], dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["CREATEUSERID"], dr["CREATEUSERNAME"], dr["STATUS"],
                  dr["SUBMITUSERID"], dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"], dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"], dr["PACKKIND"], dr["CLEARUNIT"],
                   dr["CLEARUNITNAME"], dr["SPECIALRELATIONSHIP"], dr["PRICEIMPACT"], dr["PAYPOYALTIES"], dr["SCENEDECLAREID"],

                   dr["SCENEINSPECTID"], dr["CORRESPONDNO"], dr["ASSOCIATENO"], dr["IETYPE"]);


                DBMgr.ExecuteNonQuery(sql);
            }
        }
    }
}