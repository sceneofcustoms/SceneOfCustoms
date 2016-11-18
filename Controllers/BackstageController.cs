using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class BackstageController : Controller
    {
        int totalProperty = 0;
        string sql = string.Empty;
        DataTable dt = null;
        public ActionResult UserList()
        {
            ViewData["crumb"] = "后台管理-->用户管理";
            return View();
        }
        public ActionResult ModuleList()
        {
            ViewData["crumb"] = "后台管理-->模块管理";
            return View();
        }
        public ActionResult AuthorityList()
        {
            ViewData["crumb"] = "后台管理-->权限管理";
            return View();
        }
        public string loaduser()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string groupid = Request["groupid"];
            string where = "";
            sql = string.Empty;
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
            sql = "SELECT * FROM SYS_USER WHERE id='" + userid + "'";
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
        public string deleteuser()
        {
            string userid = Request["userid"];
            string sql = "delete FROM SYS_USER WHERE id='" + userid + "'";
            int result = DBMgr.ExecuteNonQuery(sql);
            return result > 0 ? "{success:true}" : "{success:false}";
        }
        public string inipassword()
        {
            string userid = Request["userid"];
            string sql = "SELECT * FROM  SYS_USER  WHERE id='" + userid + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string psd = Extension.ToSHA1(dt.Rows[0]["NAME"] + "");
            sql = "update SYS_USER set PASSWORD='" + psd + "' WHERE id='" + userid + "'";
            int result = DBMgr.ExecuteNonQuery(sql);
            return result > 0 ? "{success:true}" : "{success:false}";
        }
        public string loadmodule()
        {
            string sql = string.Empty;
            string moduleid = Request["ID"];
            if (string.IsNullOrEmpty(moduleid))
            {
                sql = @"select * from sys_module where  ParentId is null order by SortIndex";
            }
            else
            {
                sql = @"select * from sys_module where  ParentId ='" + moduleid + "' order by SortIndex";
            }
            DataTable dt = DBMgr.GetDataTable(sql);
            string result = "[";
            int i = 0;
            foreach (DataRow smEnt in dt.Rows)
            {
                if (i != dt.Rows.Count - 1)
                {
                    result += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "'},";
                }
                else
                {
                    result += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "'}";
                }
                i++;
            }
            result += "]";
            return result;
        }
        public string insertmodule()
        {
            string json = Request["json"];
            int result = 0;
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string newid = Guid.NewGuid().ToString();
            sql = @"insert into sys_module (ID,NAME,ISLEAF,URL,PARENTID,SORTINDEX) 
                          values ('" + newid + "','" + jo.Value<string>("NAME") + "','1','" + jo.Value<string>("URL") + "','" + jo.Value<string>("PARENTID") + "','" + jo.Value<string>("SORTINDEX") + "')";
            result = DBMgr.ExecuteNonQuery(sql);
            jo.Remove("ID");
            jo.Add("ID", newid);
            jo.Add("leaf", 1);
            sql = "select * from sys_module where ID='" + jo.Value<string>("PARENTID") + "'";
            dt = DBMgr.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ISLEAF"] + "" == "1")//如果父节点是叶子,需要改写父节点
                {
                    sql = "update sys_module set ISLEAF=NULL where ID='" + jo.Value<string>("PARENTID") + "'";
                    result = DBMgr.ExecuteNonQuery(sql);
                }
            }
            return result > 0 ? "{success:true,data:" + jo + "}" : "{success:false}";
        }
        public string modifymodule()
        {
            string json = Request["json"];
            int result = 0;
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            sql = @"update sys_module set NAME = '" + jo.Value<string>("NAME") + "' ,url = '" + jo.Value<string>("URL") + "',SORTINDEX = '" + jo.Value<string>("SORTINDEX") + "' where ID = '" + jo.Value<string>("ID") + "'";
            result = DBMgr.ExecuteNonQuery(sql);
            return result > 0 ? "{success:true,data:" + jo + "}" : "{success:false}";
        }
        public string deletemodule()
        {
            int result = 0;
            JObject jo = (JObject)JsonConvert.DeserializeObject(Request["json"]);
            sql = "delete from sys_module where ID='" + jo.Value<string>("ID") + "'";
            result = DBMgr.ExecuteNonQuery(sql);
            sql = "select * from sys_module where PARENTID='" + jo.Value<string>("PARENTID") + "'";
            dt = DBMgr.GetDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "update sys_module set isleaf=1 where ID='" + jo.Value<string>("PARENTID") + "'";
                result = DBMgr.ExecuteNonQuery(sql);
            }
            return result > 0 ? "{success:true}" : "{success:false}";
        }
        public string loadmodulebyuser()
        {
            string sql = string.Empty;
            string moduleid = Request["ID"];
            string userid = Request["userid"];
            //            if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(moduleid))
            //            {
            //                sql = @"select t.*,u.MODULEID from sys_module t left join (select * from sys_moduleuser where userid='{0}') u on t.ID=u.MODULEID
            //                where  t.ParentId ='{1}' order by t.SortIndex";
            //                sql = string.Format(sql, userid, moduleid);
            //            }
            //获取一级模块 && string.IsNullOrEmpty(moduleid)
            if (!string.IsNullOrEmpty(userid))
            {
                sql = @"select t.*,u.MODULEID from sys_module t  left join (select * from sys_moduleuser where userid='{0}') u on t.ID=u.MODULEID
                where  t.ParentId is null order by t.SortIndex";
                sql = string.Format(sql, userid);
            }
            string result = "[";
            if (!string.IsNullOrEmpty(sql))
            {
                DataTable dt = DBMgr.GetDataTable(sql);
                int i = 0;
                string children = string.Empty;
                foreach (DataRow smEnt in dt.Rows)
                {
                    children = getchildren(smEnt["ID"].ToString(), userid);
                    if (i != dt.Rows.Count - 1)
                    {
                        result += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "',checked:" + (string.IsNullOrEmpty(smEnt["MODULEID"] + "") ? "false" : "true") + ",children:" + children + "},";
                    }
                    else
                    {
                        result += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "',checked:" + (string.IsNullOrEmpty(smEnt["MODULEID"] + "") ? "false" : "true") + ",children:" + children + "}";
                    }
                    i++;
                }
            }
            result += "]";
            return result;
        }
        public string getchildren(string moduleid, string userid)
        {
            string children = "[";
            sql = @"select t.*,u.MODULEID from sys_module t left join (select * from sys_moduleuser where userid='{0}') u on t.ID=u.MODULEID
                where  t.ParentId ='{1}' order by t.SortIndex";
            sql = string.Format(sql, userid, moduleid);
            DataTable dt = DBMgr.GetDataTable(sql);
            int i = 0;
            foreach (DataRow smEnt in dt.Rows)
            {
                if (i != dt.Rows.Count - 1)
                {
                    children += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "',checked:" + (string.IsNullOrEmpty(smEnt["MODULEID"] + "") ? "false" : "true") + ",children:[]},";
                }
                else
                {
                    children += "{ID:'" + smEnt["ID"] + "',NAME:'" + smEnt["NAME"] + "',SORTINDEX:'" + smEnt["SORTINDEX"] + "',PARENTID:'" + smEnt["PARENTID"] + "',leaf:'" + smEnt["ISLEAF"] + "',URL:'" + smEnt["URL"] + "',checked:" + (string.IsNullOrEmpty(smEnt["MODULEID"] + "") ? "false" : "true") + ",children:[]}";
                }
                i++;
            }
            children += "]";
            return children;
        }
        public string saveauthority()
        {
            string userid = Request["userid"];
            string moduleids = Request["moduleids"];
            try
            {
                sql = @"DELETE FROM SYS_MODULEUSER WHERE USERID = '{0}'";
                sql = string.Format(sql, userid);
                DBMgr.ExecuteNonQuery(sql);
                string[] ids = moduleids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string moduleid in ids)
                {
                    sql = @"insert into sys_moduleuser (USERID,MODULEID) values ('{0}','{1}')";
                    sql = string.Format(sql, userid, moduleid);
                    DBMgr.ExecuteNonQuery(sql);
                }
                return "{success:true}";
            }
            catch (Exception ex)
            {
                return "{success:false}";
            }
        }
    }
}