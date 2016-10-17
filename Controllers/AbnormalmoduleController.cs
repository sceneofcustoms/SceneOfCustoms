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
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    public class AbnormalmoduleController : Controller
    {
        // GET: Abnormalmodule
        public ActionResult Abnormallog()
        {
            ViewData["crumb"] = "异常管理-->异常登记";
            return View();
        }
        public ActionResult AbnormallogEdit()
        {
            //异常登记 add syy 20160930
            ViewData["ID"] = Request["ID"];
            ViewData["ORDER_ID"] = Request["ORDER_ID"];
            return View();
        }
        public string AbnormallogEditDetail()
        {
            //点击行显示对应的异常登记 add syy 20160930
            string ID = Request["ID"];
            string sql = "SELECT * FROM LIST_EXCEPTION WHERE FWONO = '" + ID + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            return result;
        }
        //异常登记主信息 add syy 20160930
        public string Edit_Order_E()
        {
            string ORDER_ID = Request.QueryString["ORDER_ID"];
            string sql = "select ID AS ORDER_ID , t.CODE,t.DECLSTATUS,t.BUSITYPE from list_order t where  ID = " + ORDER_ID;
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            OrderEntity OrderEntity = JsonConvert.DeserializeObject<OrderEntity>(result);       //将json转换为实例化类的一个数组
            string BUSITYPE = OrderEntity.BUSITYPE;                                             //取数组的一个值
            switch (BUSITYPE)
            {
                case "10":
                    OrderEntity.BUSITYPE = "空运出口";                                          //更改数组的值
                    break;
                case "11":
                    OrderEntity.BUSITYPE = "空运进口";
                    break;
                case "20":
                    OrderEntity.BUSITYPE = "海运出口";
                    break;
                case "21":
                    OrderEntity.BUSITYPE = "海运进口";
                    break;
                case "30":
                    OrderEntity.BUSITYPE = "陆运出口";
                    break;
                case "31":
                    OrderEntity.BUSITYPE = "陆运进口";
                    break;
                case "40":
                    OrderEntity.BUSITYPE = "国内出口";
                    break;
                case "41":
                    OrderEntity.BUSITYPE = "国内进口";
                    break;
                case "50":
                    OrderEntity.BUSITYPE = "特殊区域出口";
                    break;
                case "51":
                    OrderEntity.BUSITYPE = "特殊区域进口";
                    break;
            }
            var info =JsonConvert.SerializeObject(OrderEntity);                                 //将数组再转换为json格式
            return info;
        }
        //异常登记编辑框详细信息 add DLC 20161009
        public string AbnormallogEdit_Info()
        {
            string ID = Request.QueryString["ID"];
            string sql = "SELECT * FROM LIST_EXCEPTION WHERE ID = '" + ID + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            return result;
        }
        public ActionResult SaveAbnormal(FormCollection form)
        {
            string ID = Request["ID"];
            string ORDER_ID = Request["ORDER_ID"];
            DataTable dt;
            string sql = "";
            if (ID == "")
            {//新建异常
                sql = "INSERT INTO LIST_EXCEPTION";
                //ID、异常提交人、提交时间、一级异常类型、二级异常类型、相关责任人、联系方式、异常备注、异常回复部门、异常处理人、异常处理时间、异常处理结果、异常处理备注、(FWO号、提交人ID、)
                sql += " ( ID,SUBMITUSERNAME , SUBMITTIME , FIRSTLEVELTYPE , SECONDLEVELTYPE , CHARGEUSERNAME , CONTACTMETHOD , EXCEPTIONINFO , RESOLVEDEPTNAME , RESOLVEUSERNAME , RESOLVETIME , RESOLVERESULT , RESOLVEREMARK , FWONO , SUBMITUSERID )";
                sql += " VALUES ( LIST_EXCEPTION_ID.Nextval, '" + Request.Form["SUBMITUSERNAME"]
                    + "', to_date('" + Request.Form["SUBMITTIME"] + "','yyyy-MM-dd HH24:mi:ss')"
                    + ",'" + Request.Form["FIRSTLEVELTYPE"]
                    + "','" + Request.Form["SECONDLEVELTYPE"]
                    + "','" + Request.Form["CHARGEUSERNAME"]
                    + "','" + Request.Form["CONTACTMETHOD"]
                    + "','" + Request.Form["EXCEPTIONINFO"]
                    + "','" + Request.Form["RESOLVEDEPTNAME"]
                    + "','" + Request.Form["RESOLVEUSERNAME"]
                    + "', to_date('" + Request.Form["RESOLVETIME"] + "','yyyy-MM-dd HH24:mi:ss')"
                    + ",'" + Request.Form["RESOLVERESULT"]
                    + "','" + Request.Form["RESOLVEREMARK"]
                    + "','" + ORDER_ID + "','1' )";
            }
            else
            {//更新异常
                sql = "update LIST_EXCEPTION set";
                //异常提交人、提交时间、一级异常类型、二级异常类型、相关责任人、联系方式、异常备注、异常回复部门、异常处理人、异常处理时间、异常处理结果、异常处理备注、(FWO号、提交人ID、)
                if (Request.Params.AllKeys.Contains("SUBMITUSERNAME"))
                {
                    sql += " SUBMITUSERNAME = '" + Request.Form["SUBMITUSERNAME"] + "',";
                }
                //提交时间
                if (Request.Params.AllKeys.Contains("SUBMITTIME"))
                {
                    sql += " SUBMITTIME = to_date('" + Request.Form["SUBMITTIME"] + "','yyyy-MM-dd HH24:mi:ss'),";
                }
                //一级异常类型
                if (Request.Params.AllKeys.Contains("FIRSTLEVELTYPE"))
                {
                    sql += " FIRSTLEVELTYPE = '" + Request.Form["FIRSTLEVELTYPE"] + "',";
                }
                //二级异常类型
                if (Request.Params.AllKeys.Contains("SECONDLEVELTYPE"))
                {
                    sql += " SECONDLEVELTYPE = '" + Request.Form["SECONDLEVELTYPE"] + "',";
                }
                //相关责任人
                if (Request.Params.AllKeys.Contains("CHARGEUSERNAME"))
                {
                    sql += " CHARGEUSERNAME = '" + Request.Form["CHARGEUSERNAME"] + "',";
                }
                //联系方式
                if (Request.Params.AllKeys.Contains("CONTACTMETHOD"))
                {
                    sql += " CONTACTMETHOD = '" + Request.Form["CONTACTMETHOD"] + "',";
                }
                //异常备注
                if (Request.Params.AllKeys.Contains("EXCEPTIONINFO"))
                {
                    sql += " EXCEPTIONINFO = '" + Request.Form["EXCEPTIONINFO"] + "',";
                }
                //异常回复部门
                if (Request.Params.AllKeys.Contains("RESOLVEDEPTNAME"))
                {
                    sql += " RESOLVEDEPTNAME = '" + Request.Form["RESOLVEDEPTNAME"] + "',";
                }
                //异常处理人
                if (Request.Params.AllKeys.Contains("RESOLVEUSERNAME"))
                {
                    sql += " RESOLVEUSERNAME = '" + Request.Form["RESOLVEUSERNAME"] + "',";
                }
                //异常处理时间
                if (Request.Params.AllKeys.Contains("RESOLVETIME"))
                {
                    sql += " RESOLVETIME = to_date('" + Request.Form["RESOLVETIME"] + "','yyyy-MM-dd HH24:mi:ss'),";
                }
                //异常处理结果
                if (Request.Params.AllKeys.Contains("RESOLVERESULT"))
                {
                    sql += " RESOLVERESULT = '" + Request.Form["RESOLVERESULT"] + "',";
                }
                //异常处理备注
                if (Request.Params.AllKeys.Contains("RESOLVEREMARK"))
                {
                    sql += " RESOLVEREMARK = '" + Request.Form["RESOLVEREMARK"] + "'";
                }
                sql += " where ID =" + ID;
            }
            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                if (ID == "")
                {
                    sql = "SELECT count(1) FROM LIST_EXCEPTION WHERE FWONO = '" + ORDER_ID + "'";
                    dt = DBMgr.GetDataTable(sql);
                    sql = "update list_order set YCNUM='" + dt.Rows[0]["count(1)"] + "' where ID='" + ORDER_ID + "'";
                    DBMgr.ExecuteNonQuery(sql);
                }
                else {
                    sql = "SELECT count(1) FROM LIST_EXCEPTION WHERE FWONO = '" + ORDER_ID + "' and RESOLVERESULT='完成'";
                    dt = DBMgr.GetDataTable(sql);
                    sql = "update list_order set CLNUM='" + dt.Rows[0]["count(1)"] + "' where ID='" + ORDER_ID + "'";
                    DBMgr.ExecuteNonQuery(sql);
                }
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }

        }
        public string sql { get; set; }
    }
}