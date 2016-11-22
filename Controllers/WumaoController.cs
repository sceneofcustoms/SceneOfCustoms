﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class WumaoController : Controller
    {
        // GET: Wumao

        public ActionResult Wumao_List()
        {
            ViewData["crumb"] = "数据";
            return View();
        }

        public ActionResult DataMatching_List()
        {
            ViewData["crumb"] = "物贸通数据匹配";
            return View();
        }

        public ActionResult DataMatching_Edit()
        {
            ViewData["ID"] = Request["ID"];
            ViewData["crumb"] = "物贸通数据修改";
            return View();
        }





        public string LoadDataMatching_Edit()
        {
            string ID = Request.QueryString["ID"];
            string sql = "SELECT * FROM LIST_WUMAODADAMATCHING WHERE ID = '" + ID + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            return result;
        }


        public string syncdata()
        {
            MessageQueue mq = new MessageQueue("FormatName:DIRECT=TCP:221.224.206.253\\Private$\\etf");
            //MessageQueue mq = MessageQueue.Create(@"221.224.206.235\Private$\etf",true);
            Message msg = new Message();
            //System.Messaging.MessageQueue MessageQueue1 = new System.Messaging.MessageQueue();
            //MessageQueue1 = System.Messaging.MessageQueue.Create(".\\MyTransQueue", true);
            //msg.Body = xmlDoc.ToString();
            msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
            mq.Send(msg, MessageQueueTransactionType.Single);
            return "1";
        }




        public string LoadDataMatching_List()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from LIST_WUMAODADAMATCHING where 1=1";
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

        public ActionResult SaveDataMatching(FormCollection form)
        {

            string ID = Request["ID"];
            string sql = "";

            if (!string.IsNullOrEmpty(ID))
            {
                sql = "update LIST_WUMAODADAMATCHING set ";
                sql += " BUSINAME = '" + Request.Form["BUSINAME"] + "',";
                sql += " GOODS_NATURE_ID = '" + Request.Form["GOODS_NATURE_ID"] + "',";
                sql += " I_E_FALG_TYPE = '" + Request.Form["I_E_FALG_TYPE"] + "',";
                sql += " BIZ_TYPE_ID = '" + Request.Form["BIZ_TYPE_ID"] + "',";
                sql += " TRAFFICTYPE = '" + Request.Form["TRAFFICTYPE"] + "',";
                sql += " BILL_TYPE = '" + Request.Form["BILL_TYPE"] + "',";
                sql += " APPCIQTYPE = '" + Request.Form["APPCIQTYPE"] + "',";
                sql += " OUT_TRAF_MODE = '" + Request.Form["OUT_TRAF_MODE"] + "',";
                sql = sql.Substring(0, sql.Length - 1);
                sql += " where ID =" + ID;
            }
            else
            {
                sql = "INSERT INTO LIST_WUMAODADAMATCHING ";
                sql += " ( ID,BUSINAME,GOODS_NATURE_ID , I_E_FALG_TYPE , BIZ_TYPE_ID ,  TRAFFICTYPE , BILL_TYPE , APPCIQTYPE , OUT_TRAF_MODE )";
                sql += " VALUES ( LIST_WUMAODADAMATCHING_ID.Nextval, '" + Request.Form["BUSINAME"]
                    + "','" + Request.Form["GOODS_NATURE_ID"]
                    + "','" + Request.Form["I_E_FALG_TYPE"]
                    + "','" + Request.Form["BIZ_TYPE_ID"]
                    + "','" + Request.Form["TRAFFICTYPE"]
                    + "','" + Request.Form["BILL_TYPE"]
                    + "','" + Request.Form["APPCIQTYPE"]
                    + "','" + Request.Form["OUT_TRAF_MODE"]
                    +"')";
            }

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, }, JsonRequestBehavior.AllowGet);
            }
        }


        public string LoadWumao_List()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from list_wumao where 1=1";
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

    }
}