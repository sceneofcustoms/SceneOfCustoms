using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult BackMsg_List()
        {
            ViewData["crumb"] = "后台管理-->返回状态";
            return View();
        }
        public string LoadAttachmentList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from LIST_ATTACHMENT where 1=1";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    sql += " AND " + jo.Value<string>("ordercode") + " ='" + jo.Value<string>("ordercode_value") + "'";
                }
                if (jo.Value<string>("businessin_createname") != null && jo.Value<string>("businessin_createname") != "")
                {
                    sql += " AND CREATENAME = '" + jo.Value<string>("businessin_createname") + "' ";
                }
                if (jo.Value<string>("starttime") != "" && jo.Value<string>("starttime") != null)
                {
                    sql += " AND CREATETIME >= to_date('" + jo.Value<string>("starttime") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stoptime") != "" && jo.Value<string>("stoptime") != null)
                {
                    sql += " AND CREATETIME <= to_date('" + jo.Value<string>("stoptime") + "','yyyy-MM-dd')";
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
        public string LoadFooList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from LIST_SAPFOO where 1=1";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Replace(" ", "");
                    if (jo.Value<string>("ordercode") != "FWONO" && jo.Value<string>("ordercode") != "FOONO" && jo.Value<string>("ordercode") != "FOONOBJ")
                    {
                        sql += " AND " + jo.Value<string>("ordercode") + " ='" + ordercode_value + "'";
                    }
                    else
                    {
                        sql += " AND " + jo.Value<string>("ordercode") + " like '%" + ordercode_value + "%'";
                    }
                }
                if (jo.Value<string>("customs_busitype") != null && jo.Value<string>("customs_busitype") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("customs_busitype") + "' ";
                }
                if (jo.Value<string>("startdate") != "" && jo.Value<string>("startdate") != null)
                {
                    sql += " AND TIME >= to_date('" + jo.Value<string>("startdate") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stopdate") != "" && jo.Value<string>("stopdate") != null)
                {
                    sql += " AND TIME <= to_date('" + jo.Value<string>("stopdate") + "','yyyy-MM-dd')";
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

        //日志消息
        public string LoadMsgList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from MSG where 1=1";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Replace(" ", "");
                    if (jo.Value<string>("ordercode") != "FWONO" && jo.Value<string>("ordercode") != "FOONO" && jo.Value<string>("ordercode") != "FOONOBJ")
                    {
                        sql += " AND " + jo.Value<string>("ordercode") + " ='" + ordercode_value + "'";
                    }
                    else
                    {
                        sql += " AND " + jo.Value<string>("ordercode") + " like '%" + ordercode_value + "%'";
                    }
                }
                if (jo.Value<string>("starttime") != "" && jo.Value<string>("starttime") != null)
                {
                    sql += " AND CREATETIME >= to_date('" + jo.Value<string>("starttime") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stoptime") != "" && jo.Value<string>("stoptime") != null)
                {
                    sql += " AND CREATETIME <= to_date('" + jo.Value<string>("stoptime") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("customs_busitype") != null && jo.Value<string>("customs_busitype") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("customs_busitype") + "' ";
                }
                if (jo.Value<string>("businessin_source") != null && jo.Value<string>("businessin_source") != "")
                {
                    sql += " AND SOURCE = '" + jo.Value<string>("businessin_source") + "' ";
                }
                if (jo.Value<string>("businessin_status") != null && jo.Value<string>("businessin_status") != "")
                {
                    sql += " AND STATUS = '" + jo.Value<string>("businessin_status") + "' ";
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
        //返回消息
        public string LoadBackMsgList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from list_statuslog where 1=1";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Replace(" ", "");
                    sql += " AND " + jo.Value<string>("ordercode") + " ='" + ordercode_value + "'";
                }
                if (jo.Value<string>("starttime") != "" && jo.Value<string>("starttime") != null)
                {
                    sql += " AND CREATETIME >= to_date('" + jo.Value<string>("starttime") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stoptime") != "" && jo.Value<string>("stoptime") != null)
                {
                    sql += " AND CREATETIME <= to_date('" + jo.Value<string>("stoptime") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("businessin_type") != null && jo.Value<string>("businessin_type") != "")
                {
                    sql += " AND TYPE = '" + jo.Value<string>("businessin_type") + "' ";
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
            string ORDERCODE = Request["ORDERCODE"];
            if (!string.IsNullOrEmpty(FOONO) && !string.IsNullOrEmpty(FWONO) || !string.IsNullOrEmpty(ORDERCODE))
            {
                ViewData["is_passed"] = "1";
                ViewData["FWONO"] = FWONO;
                ViewData["FOONO"] = FOONO;
                ViewData["ORDERCODE"] = ORDERCODE;
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
            string sql = "";
            string ORDERCODE = Request["ORDERCODE"];
            if (!string.IsNullOrEmpty(ORDERCODE))
            {
                sql = "select * from LIST_ATTACHMENT where ORDERCODE='" + ORDERCODE + "'";
            }
            else
            {
                sql = "select * from LIST_ATTACHMENT where fwono='" + FWONO + "' and foono ='" + FOONO + "'";
            }


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
            // var uploadPath = Server.MapPath("/Upload/");
            chunk = chunk ?? 0;
            string FWONO = Request.QueryString["FWONO"];
            string FOONO = Request.QueryString["FOONO"];
            string ORDERCODE = Request.QueryString["ORDERCODE"];
            string direc_upload = DateTime.Now.ToString("yyyy-MM-dd");
            using (var fs = new FileStream(Path.Combine(@"D:\ftpserver\", name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
                string username = CurrentUser();
                username = string.IsNullOrEmpty(username) ? "SAP" : username;//如果是从本系统进入直接取登录账号,如果是外部调用直接标记SAP
                string sql = @"insert into list_attachment(ID,FILEPATH,FILENAME,FILESIZE,FWONO,FOONO,ORDERCODE,CREATENAME,CREATETIME,STATUS) 
                VALUES(LIST_ATTACHMENT_ID.Nextval,'/" + direc_upload + "/" + name + "','" + name + "'," + fileUpload.ContentLength + ",'" + FWONO + "','" + FOONO + "','" + ORDERCODE + "','" + username + "',sysdate,1)";
                DBMgr.ExecuteNonQuery(sql);
                if (!string.IsNullOrEmpty(ORDERCODE))
                {
                    sql = "update list_order set filerelate='1' where code='" + ORDERCODE + "'";
                    DBMgr.ExecuteNonQuery(sql);
                }
            }
            return Content("chunk uploaded", "text/plain");

        }

        [HttpPost]
        //移除文件
        public string delete_file()
        {
            string ids = Request["ids"];
            string[] arr = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string sql = "";
            DataTable dt;
            string UserName = ConfigurationManager.AppSettings["FTPUserName"];
            string Password = ConfigurationManager.AppSettings["FTPPassword"];
            System.Uri Uri = new Uri("ftp://" + ConfigurationManager.AppSettings["FTPServer"] + ":" + ConfigurationManager.AppSettings["FTPPortNO"]);
            FtpHelper ftp = new FtpHelper(Uri, UserName, Password);
            try
            {
                foreach (string str in arr)
                {
                    sql = "select * from list_attachment where id='" + str + "'";
                    dt = DBMgr.GetDataTable(sql);
                    sql = "delete from list_attachment where id='" + str + "'";
                    DBMgr.ExecuteNonQuery(sql);
                    ftp.DeleteFile(dt.Rows[0]["FILEPATH"] + ""); 
                    if (dt.Rows.Count == 1)
                    {
                        sql = "update list_order set filerelate='0' where code='" + dt.Rows[0]["ORDERCODE"] + "'";
                        DBMgr.ExecuteNonQuery(sql);
                    }
                }
                return "{success:true}";
            }
            catch
            {
                return "{success:false}";
            }
        }
        public string CurrentUser()
        {
            JObject json_user = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            if (json_user == null)
            {
                return "";
            }
            else
            {
                return json_user.GetValue("REALNAME") + "";
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


        //根据登录用户获取对应权限 展示菜单
        public string Header()
        {
            JObject json_user = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string sql = @"select ID,NAME,PARENTID,URL,SORTINDEX,IsLeaf,ICON from sys_module t 
            where t.parentid is null and t.ID IN (select MODULEID FROM sys_moduleuser where userid='{0}') order by sortindex";
            sql = string.Format(sql, json_user.GetValue("ID"));
            DataTable dt1 = DBMgr.GetDataTable(sql);
            string result = "";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string icon = string.IsNullOrEmpty(dt1.Rows[i]["ICON"] + "") ? "" : "<i class=\"iconfont\">&#" + dt1.Rows[i]["ICON"] + "</i>";
                if (string.IsNullOrEmpty(dt1.Rows[i]["URL"] + ""))
                {
                    result += "<li><a  href=\"javascript:void(0)\">" + icon + dt1.Rows[i]["NAME"] + "</a>";
                }
                else
                {
                    result += "<li><a  href=\"" + dt1.Rows[i]["URL"] + "\">" + icon + dt1.Rows[i]["NAME"] + "</a>";
                }
                sql = @"select ID,NAME,PARENTID,URL,SORTINDEX,IsLeaf,ICON from sys_module t where t.parentid='{0}'
                and t.ID IN (select MODULEID FROM sys_moduleuser where userid='{1}') order by sortindex";
                sql = string.Format(sql, dt1.Rows[i]["ID"], json_user.GetValue("ID"));
                DataTable dt2 = DBMgr.GetDataTable(sql);
                if (dt2.Rows.Count > 0)
                {
                    result += "<ul>";
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        string icon2 = string.IsNullOrEmpty(dt2.Rows[j]["ICON"] + "") ? "" : "<i class=\"iconfont\">&#" + dt2.Rows[j]["ICON"] + "</i>";
                        if (string.IsNullOrEmpty(dt2.Rows[j]["URL"] + ""))
                        {
                            result += "<li><a href=\"javascript:void(0)\">" + icon2 + dt2.Rows[j]["NAME"] + "</a>";
                        }
                        else
                        {
                            result += "<li><a href=\"" + dt2.Rows[j]["URL"] + "\">" + icon2 + dt2.Rows[j]["NAME"] + "</a>";
                        }
                        result += "</li>";
                    }
                    result += "</ul></li>";
                }
                else
                {
                    result += "</li>";
                }
            }
            return result;
        }
    }
}