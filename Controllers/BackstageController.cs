using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class BackstageController : Controller
    {
        int totalProperty = 0;
        public ActionResult UserList()
        {
            ViewData["crumb"] = "后台管理-->用户管理";
            return View();
        }
        public string loaduser()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string groupid = Request["groupid"];
            string where = "";
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(Request["NAME"]))
            {
                where += " and NAME like '%" + Request["NAME"] + "%'";
            }
            if (!string.IsNullOrEmpty(Request["REALNAME"]))
            {
                where += " and REALNAME like '%" + Request["REALNAME"] + "%'";
            }
            sql = "SELECT * FROM SYS_USER WHERE 1=1 " + where;
            sql = Extension.GetPageSql(sql, "CREATETIME", "desc", ref totalProperty, Convert.ToInt32(Request["start"]), Convert.ToInt32(Request["limit"]));
            string json = JsonConvert.SerializeObject(DBMgr.GetDataTable(sql), iso);
            return "{rows:" + json + ",total:" + totalProperty + "}";
        }
        public string getuser()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string userid = Request["userid"];
            string sql = "SELECT * FROM SYS_USER WHERE id='" + userid + "'";
            string json = JsonConvert.SerializeObject(DBMgr.GetDataTable(sql), iso).Replace("[", "").Replace("]", "");
            return json;
        }
        public string saveuser()
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(Request["json"]);
            string sql = string.Empty;
            int result = 0;
            if (!string.IsNullOrEmpty(jo.Value<string>("ID")))
            {
                sql = @"update Sys_User set REALNAME='{1}',STATUS='{2}',MOBILE='{3}',EMAIL='{4}',REMARK='{5}' where ID='{0}'";
                sql = string.Format(sql, jo.Value<string>("ID"), jo.Value<string>("REALNAME"), jo.Value<string>("STATUS"),
                      jo.Value<string>("MOBILE"), jo.Value<string>("EMAIL"), jo.Value<string>("REMARK"));
                result = DBMgr.ExecuteNonQuery(sql);
            }
            else
            {
                sql = @"insert into Sys_User (ID,NAME,REALNAME,PASSWORD,STATUS,CREATETIME,MOBILE,EMAIL,REMARK) values 
                      (SYS_USER_ID.Nextval,'{0}','{1}','{2}','{3}',sysdate,'{4}','{5}','{6}')";

                sql = string.Format(sql, jo.Value<string>("NAME"), jo.Value<string>("REALNAME"), Extension.ToSHA1(jo.Value<string>("NAME")),
                      jo.Value<string>("STATUS"), jo.Value<string>("MOBILE"), jo.Value<string>("EMAIL"), jo.Value<string>("REMARK"));
                result = DBMgr.ExecuteNonQuery(sql);
            }
            return result > 0 ? "{success:true}" : "{success:false}";
        }
    }
}