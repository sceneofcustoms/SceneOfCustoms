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
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{

    //订单方法

    public class OrderController : Controller
    {



        //申报关区 进口口岸 
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

            OrderEntity OrderEntity = JsonConvert.DeserializeObject<OrderEntity>(result);       //将json转换为实例化类的一个数组
            string DECLWAY = OrderEntity.DECLWAY;                                             //取数组的一个值
            switch (DECLWAY)
            {
                case "W":
                    OrderEntity.DECLWAY = "无纸报关";                                          //更改数组的值
                    break;
                case "D":
                    OrderEntity.DECLWAY = "无纸带清单报关";
                    break;
                case "L":
                    OrderEntity.DECLWAY = "有纸带清单报关";
                    break;
                case "O":
                    OrderEntity.DECLWAY = "有纸报关";
                    break;
                case "M":
                    OrderEntity.DECLWAY = "通关无纸化";
                    break;
            }
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
            var info = JsonConvert.SerializeObject(OrderEntity);                                 //将数组再转换为json格式
            return info;
        }



        [HttpGet]
        public string GetData()
        {
            string BUSITYPE = "";
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select t.* from list_order  t where 1=1  ";
            //搜索查询 DLC 2016/10/14
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                BUSITYPE = jo.Value<string>("BUSITYPE");
                if (jo.Value<string>("businessin_object") != null && jo.Value<string>("businessin_object") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("businessin_object") + "' ";
                }
                if (jo.Value<string>("businessout_object") != null && jo.Value<string>("businessout_object") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("businessout_object") + "' ";
                }
                if (jo.Value<string>("service_model") != null && jo.Value<string>("service_model") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("service_model") + "' ";
                }
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    sql += " AND " + jo.Value<string>("ordercode") + " ='" + jo.Value<string>("ordercode_value") + "'";
                }
                if (jo.Value<string>("oprname_value") != null && jo.Value<string>("oprname_value") != "")
                {
                    sql += " AND " + jo.Value<string>("oprname") + " ='" + jo.Value<string>("oprname_value") + "'";
                }
                if (jo.Value<string>("startdate") != "")
                {
                    sql += " AND " + jo.Value<string>("orderdate") + " >= to_date('" + jo.Value<string>("startdate") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("stopdate") != "")
                {
                    sql += " AND " + jo.Value<string>("orderdate") + " <= to_date('" + jo.Value<string>("stopdate") + "','yyyy-MM-dd')";
                }
                if (jo.Value<string>("CUSTOMDISTRICTCODE") != null && jo.Value<string>("CUSTOMDISTRICTCODE") != "")
                {
                    sql += " AND CUSTOMDISTRICTCODE = '" + jo.Value<string>("CUSTOMDISTRICTCODE") + "' ";
                }
                if (jo.Value<string>("declaration_type") != null && jo.Value<string>("declaration_type") != "")
                {
                    sql += " AND DECLWAY = '" + jo.Value<string>("declaration_type") + "' ";
                }
                if (jo.Value<string>("LAWCONDITION") != null && jo.Value<string>("LAWCONDITION") != "")
                {
                    sql += " AND LAWCONDITION = '" + jo.Value<string>("LAWCONDITION") + "' ";
                }
                if (jo.Value<string>("WOODPACKINGID") != null && jo.Value<string>("WOODPACKINGID") != "")
                {
                    sql += " AND WOODPACKINGID = '" + jo.Value<string>("WOODPACKINGID") + "' ";
                }
            }
            else { 
                BUSITYPE = Request["BUSITYPE"];
            }
            //end
            switch (BUSITYPE)
            {
                case "ONEIN":
                    sql += " and FOONO is not null  AND (BUSITYPE='11' OR BUSITYPE='21' OR BUSITYPE='31')";
                    break;
                case "ONEINBJ":
                    sql += " and FOONOBJ is not null  AND (BUSITYPE='11' OR BUSITYPE='21' OR BUSITYPE='31')";
                    break;
                case "ONEOUT":
                    sql += " and FOONO is not null  AND (BUSITYPE='10' OR BUSITYPE='20' OR BUSITYPE='30')";
                    break;
                case "SPECIAL":
                    sql += " and FOONO is not null  and (BUSITYPE='50' OR BUSITYPE='51') "; //特殊监管
                    break;
                case "BLC":
                    sql += " and FOONO is not null  and (BUSITYPE='40' OR BUSITYPE='41') ";
                    break;
                case "BLCBJ":
                    sql += " and FOONOBJ is not null  and (BUSITYPE='40' OR BUSITYPE='41') ";
                    break;
            }
            //switch (BUSITYPE)
            //{
            //    case "ONEIN":
            //        sql += " AND (BUSITYPE='11' OR BUSITYPE='21' OR BUSITYPE='31')";
            //        break;
            //    case "ONEINBJ":
            //        sql += " AND (BUSITYPE='11' OR BUSITYPE='21' OR BUSITYPE='31')";
            //        break;
            //    case "ONEOUT":
            //        sql += "  AND (BUSITYPE='10' OR BUSITYPE='20' OR BUSITYPE='30')";
            //        break;
            //    case "SPECIAL":
            //        sql += "  and (BUSITYPE='50' OR BUSITYPE='51') "; //特殊监管
            //        break;
            //    case "BLC":
            //        sql += "  and (BUSITYPE='40' OR BUSITYPE='41') ";
            //        break;
            //    case "BLCBJ":
            //        sql += "  and (BUSITYPE='40' OR BUSITYPE='41') ";
            //        break;
            //}
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

            sql += "  PASSMODE =  '" + Request.Form["PASSMODE"] + "',";

            sql += "  IFCHAYAN =  '" + Request.Form["IFCHAYAN"] + "',";

            sql += "  KOUHUOSIGN =  '" + Request.Form["KOUHUOSIGN"] + "',";

            sql += "  IFTIAODANG =  '" + Request.Form["IFTIAODANG"] + "',";

            sql += "  LIHUOSIGN =  '" + Request.Form["LIHUOSIGN"] + "',";


            if (Request.Params.AllKeys.Contains("CHAYANTIMES"))
            {
                sql += "  CHAYANTIMES =  '" + Request.Form["CHAYANTIMES"] + "',";
            }

            if (Request.Params.AllKeys.Contains("CHAYANREMARK"))
            {
                sql += "  CHAYANREMARK =  '" + Request.Form["CHAYANREMARK"] + "',";
            }


            if (Request.Params.AllKeys.Contains("LIHUOTIMES"))
            {
                sql += "  LIHUOTIMES =  '" + Request.Form["LIHUOTIMES"] + "',";
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


            if (Request.Params.AllKeys.Contains("SHANDANTOTAL"))
            {
                sql += "  SHANDANTOTAL =  '" + Request.Form["SHANDANTOTAL"] + "',";
            }
            if (Request.Params.AllKeys.Contains("SHANDANDESC"))
            {
                sql += "  SHANDANDESC =  '" + Request.Form["SHANDANDESC"] + "',";
            }
            if (Request.Params.AllKeys.Contains("GAIDANTOTAL"))
            {
                sql += "  GAIDANTOTAL =  '" + Request.Form["GAIDANTOTAL"] + "',";
            }
            if (Request.Params.AllKeys.Contains("GAIDANDESC"))
            {
                sql += "  GAIDANDESC =  '" + Request.Form["GAIDANDESC"] + "',";
            }

            //SYY 9-27
            if (Request.Params.AllKeys.Contains("CHAYANZHILINGXIAFATIME"))
            {
                sql += "  CHAYANZHILINGXIAFATIME =  to_date('" + Request.Form["CHAYANZHILINGXIAFATIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Params.AllKeys.Contains("KOUHUOTIME"))
            {
                sql += "  KOUHUOTIME =  to_date('" + Request.Form["KOUHUOTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }

            //lakers
            if (Request.Params.AllKeys.Contains("IFXUNZHENG"))
            {
                sql += "  IFXUNZHENG =  '" + Request.Form["IFXUNZHENG"] + "',";
            }

            if (Request.Params.AllKeys.Contains("XUNZHENGDESC"))
            {
                sql += "  XUNZHENGDESC =  '" + Request.Form["XUNZHENGDESC"] + "',";
            }
            if (Request.Params.AllKeys.Contains("CHAYANTYPE"))
            {
                sql += "  CHAYANTYPE =  '" + Request.Form["CHAYANTYPE"] + "',";
            }

            if (Request.Params.AllKeys.Contains("INSPSTATUS"))
            {
                sql += "  INSPSTATUS =  '" + Request.Form["INSPSTATUS"] + "',";
            }

            if (Request.Params.AllKeys.Contains("DECLSTATUS"))
            {
                sql += "  DECLSTATUS =  '" + Request.Form["DECLSTATUS"] + "',";
            }

            if (Request.Params.AllKeys.Contains("JGCZCBH"))
            {
                sql += "  JGCZCBH =  '" + Request.Form["JGCZCBH"] + "',";
            }

            sql += "  IFSHANDAN =  '" + Request.Form["IFSHANDAN"] + "',";
            sql += "  IFGAIDAN =  '" + Request.Form["IFGAIDAN"] + "',";
            sql += "  IFYIJIAO =  '" + Request.Form["IFYIJIAO"] + "',";
            sql += "  LAWCONDITION =  '" + Request.Form["LAWCONDITION"] + "',";

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


        //急装箱信息
        public ActionResult JizhuangxiangInfo()
        {
            return View();
        }


        //报关单信息
        public ActionResult BaoguandanInfo()
        {
            return View();
        }


        //通关单信息
        public ActionResult TongguandanInfo()
        {
            return View();
        }




    }
}