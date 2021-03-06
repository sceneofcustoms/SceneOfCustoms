﻿﻿using Aspose.Cells;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    //订单方法
    public class OrderController : Controller
    {
        IDatabase db = SeRedis.redis.GetDatabase();

        //报关一线进口导出明细
        public DataTable One_lineIn(string ids)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt.Columns.Add("收货人");
            dt.Columns.Add("一程提运单号");
            dt.Columns.Add("二程提运单号");
            dt.Columns.Add("总件数");
            dt.Columns.Add("总重量");
            dt.Columns.Add("总单号");
            dt.Columns.Add("分单号");
            dt.Columns.Add("是否法检");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("关务接受时间");
            dt.Columns.Add("报入海关时间");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("实物放行时间");
            dt.Columns.Add("贸易方式");
            //dt.Columns.Add("单证放行状态");
            //dt.Columns.Add("实物放行状态");
            dt.Columns.Add("载货清单号");
            //dt.Columns.Add("万达号");
            dt.Columns.Add("业务编号");
            //dt.Columns.Add("转关预录入号");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("实物放行人");
            dt.Columns.Add("集装箱信息");
            dt.Columns.Add("关区代码");
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("海关通关状态");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["收货人"] = data_dt.Rows[i]["BUSIUNITNAME"];
                dr["一程提运单号"] = data_dt.Rows[i]["FIRSTLADINGBILLNO"];
                dr["二程提运单号"] = data_dt.Rows[i]["SECONDLADINGBILLNO"];
                dr["总件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["总重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["总单号"] = data_dt.Rows[i]["TOTALNO"];
                dr["分单号"] = data_dt.Rows[i]["DIVIDENO"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["是否法检"] = data_dt.Rows[i]["LAWCONDITION"];
                dr["关务接受时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["贸易方式"] = data_dt.Rows[i]["TRADEWAYCODES"];
                dr["载货清单号"] = data_dt.Rows[i]["MANIFEST"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                string sql2 = "select CONTAINERNO from LIST_DECLCONTAINERTRUCK where ordercode='" + data_dt.Rows[i]["CODE"] + "'";
                dt1 = DBMgr.GetDataTable(sql2);
                string CONTAINERNO = "";
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["CONTAINERNO"] + ""))
                    {
                        CONTAINERNO += dt1.Rows[j]["CONTAINERNO"] + "/";
                    }
                }
                dr["集装箱信息"] = CONTAINERNO;
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报关一线出口导出明细
        public DataTable One_lineOut(string ids)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt.Columns.Add("发货人");
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("总件数");
            dt.Columns.Add("总重量");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("报入海关时间");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("实物加封时间");
            dt.Columns.Add("实物放行时间");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("实物放行人");
            dt.Columns.Add("集装箱信息");
            dt.Columns.Add("贸易方式");
            dt.Columns.Add("转关预录入号");
            dt.Columns.Add("关区代码");
            dt.Columns.Add("海关通关状态");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                //dr["发货人"] = data_dt.Rows[i]["SFGOODSUNIT"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["总件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["总重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["实物加封时间"] = data_dt.Rows[i]["SHIWUJIAFENGTIME"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["贸易方式"] = data_dt.Rows[i]["ASSOCIATETRADEWAY"];
                string sql2 = "select CONTAINERNO from LIST_DECLCONTAINERTRUCK where ordercode='" + data_dt.Rows[i]["CODE"] + "'";
                dt1 = DBMgr.GetDataTable(sql2);
                string CONTAINERNO = "";
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["CONTAINERNO"] + ""))
                    {
                        CONTAINERNO += dt1.Rows[j]["CONTAINERNO"] + "/";
                    }
                }
                dr["集装箱信息"] = CONTAINERNO;
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报关国内结转导出明细
        public DataTable DomesticBlc(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("实物放行时间");
            dt.Columns.Add("实物放行人");
            dt.Columns.Add("业务类型");
            dt.Columns.Add("企业");
            dt.Columns.Add("发票合同");
            dt.Columns.Add("贸易方式");
            dt.Columns.Add("件数");
            dt.Columns.Add("重量");
            dt.Columns.Add("关务接受时间");
            dt.Columns.Add("报入海关时间");
            dt.Columns.Add("报关人");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("报关状态");
            dt.Columns.Add("报检状态");
            dt.Columns.Add("报关方式");
            dt.Columns.Add("关区代码");
            dt.Columns.Add("海关通关状态");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["业务类型"] = data_dt.Rows[i]["BUSITYPE"];
                dr["企业"] = data_dt.Rows[i]["BUSIUNITNAME"];
                dr["发票合同"] = data_dt.Rows[i]["CONTRACTNO"];
                dr["贸易方式"] = data_dt.Rows[i]["ASSOCIATETRADEWAY"];
                dr["件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["关务接受时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["报关人"] = data_dt.Rows[i]["BAOGUANUSERNAME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["报关状态"] = data_dt.Rows[i]["DECLSTATUS"];
                //dr["报检状态"] = data_dt.Rows[i]["INSPSTATUS"];
                dr["报关方式"] = data_dt.Rows[i]["DECLWAY"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dr["海关通关状态"] = data_dt.Rows[i]["DECLARATIONSTATUS"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报关特殊监管导出明细
        public DataTable SpecialSupervision(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("业务编号");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("实物放行时间");
            dt.Columns.Add("收/发货人");
            dt.Columns.Add("件数");
            dt.Columns.Add("重量");
            dt.Columns.Add("报入海关时间");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("实物放行人");
            dt.Columns.Add("关务接单时间");
            dt.Columns.Add("进出口类别");
            dt.Columns.Add("申报方式");
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["收/发货人"] = data_dt.Rows[i]["SFGOODSUNIT"];
                dr["件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["关务接单时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                //dr["进出口类别"] = data_dt.Rows[i]["INOUTTYPE"];
                dr["申报方式"] = data_dt.Rows[i]["REPWAYID"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报检一线进口导出明细
        public DataTable One_lineInbj(string ids)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt.Columns.Add("收货人");
            dt.Columns.Add("一程提运单号");
            dt.Columns.Add("二程提运单号");
            dt.Columns.Add("总件数");
            dt.Columns.Add("总重量");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("报检单号");
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("总单号");
            dt.Columns.Add("分单号");
            dt.Columns.Add("万达号");
            dt.Columns.Add("载货清单号");
            dt.Columns.Add("木质包装");
            dt.Columns.Add("通关单标志");
            //dt.Columns.Add("通关单张数");
            //dt.Columns.Add("通关单号");
            dt.Columns.Add("集装箱信息");
            dt.Columns.Add("合同/发票号");
            dt.Columns.Add("报检时间");
            dt.Columns.Add("报检人");
            dt.Columns.Add("放行时间");
            dt.Columns.Add("放行人");
            dt.Columns.Add("集装箱信息");
            //dt.Columns.Add("报关状态");
            dt.Columns.Add("报检状态");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["收货人"] = data_dt.Rows[i]["SGOODSUNIT"];
                dr["一程提运单号"] = data_dt.Rows[i]["FIRSTLADINGBILLNO"];
                dr["二程提运单号"] = data_dt.Rows[i]["SECONDLADINGBILLNO"];
                dr["总件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["总重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["报检单号"] = data_dt.Rows[i]["INSPECTIONCODE"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["总单号"] = data_dt.Rows[i]["TOTALNO"];
                dr["分单号"] = data_dt.Rows[i]["DIVIDENO"];
                //dr["万达号"] = data_dt.Rows[i]["text"];
                dr["载货清单号"] = data_dt.Rows[i]["MANIFEST"];
                dr["木质包装"] = data_dt.Rows[i]["WOODPACKINGID"];
                //dr["通关单标志"] = data_dt.Rows[i]["text"];
                //dr["通关单张数"] = data_dt.Rows[i]["text"];
                //dr["通关单号"] = data_dt.Rows[i]["CLEARANCENO"];
                dr["合同/发票号"] = data_dt.Rows[i]["CONTRACTNO"];
                dr["报检时间"] = data_dt.Rows[i]["BAOJIANTIME"];
                //dr["报检人"] = data_dt.Rows[i]["text"];
                dr["放行时间"] = data_dt.Rows[i]["BAOJIANFANGXINGTIME"];
                dr["放行人"] = data_dt.Rows[i]["BAOJIANFANGXINGUSERNAME"];
                //dr["报关状态"] = data_dt.Rows[i]["DECLSTATUS"];

                string sql2 = "select CONTAINERNO from LIST_DECLCONTAINERTRUCK where ordercode='" + data_dt.Rows[i]["CODE"] + "'";
                dt1 = DBMgr.GetDataTable(sql2);
                string CONTAINERNO = "";
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["CONTAINERNO"] + ""))
                    {
                        CONTAINERNO += dt1.Rows[j]["CONTAINERNO"] + "/";
                    }
                }

                dr["报检状态"] = data_dt.Rows[i]["INSPSTATUS"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报检国内结转导出明细
        public DataTable DomesticKnotbj(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("报检单号");
            dt.Columns.Add("实物放行时间");
            dt.Columns.Add("实物放行人");
            dt.Columns.Add("业务类型");
            dt.Columns.Add("企业");
            dt.Columns.Add("发票合同");
            dt.Columns.Add("贸易方式");
            dt.Columns.Add("件数");
            dt.Columns.Add("重量");
            dt.Columns.Add("关务接受时间");
            dt.Columns.Add("报入海关时间");
            dt.Columns.Add("报关人");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("报检时间");
            dt.Columns.Add("报检人");
            dt.Columns.Add("报检放行时间");
            dt.Columns.Add("报检放行人");
            //dt.Columns.Add("报关状态");
            dt.Columns.Add("报检状态");
            //dt.Columns.Add("报关方式");
            dt.Columns.Add("关区代码");
            dt.Columns.Add("国检状态");

            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["报检单号"] = data_dt.Rows[i]["INSPECTIONCODE"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["业务类型"] = data_dt.Rows[i]["XCBUSINAME"];
                dr["企业"] = data_dt.Rows[i]["BUSIUNITNAME"];
                dr["发票合同"] = data_dt.Rows[i]["CONTRACTNO"];
                dr["贸易方式"] = data_dt.Rows[i]["TRADEWAYCODES"];
                dr["件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["关务接受时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["报关人"] = data_dt.Rows[i]["BAOGUANUSERNAME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["报检时间"] = data_dt.Rows[i]["BAOJIANTIME"];
                dr["报检人"] = data_dt.Rows[i]["BAOJIANUSERNAME"];
                dr["报检放行时间"] = data_dt.Rows[i]["BAOJIANFANGXINGTIME"];
                dr["报检放行人"] = data_dt.Rows[i]["BAOJIANFANGXINGUSERNAME"];
                //dr["报关状态"] = data_dt.Rows[i]["DECLSTATUS"];
                dr["报检状态"] = data_dt.Rows[i]["INSPSTATUS"];
                //dr["报关方式"] = data_dt.Rows[i]["DECLWAY"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dr["国检状态"] = data_dt.Rows[i]["INSPECTIONSTATUS"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //异常登记导出明细
        public DataTable Abnormal(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWO订单号");
            dt.Columns.Add("FO号");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("总单号");
            dt.Columns.Add("分单号");
            dt.Columns.Add("一程提运单号");
            dt.Columns.Add("二程提运单号");
            dt.Columns.Add("万达号");
            dt.Columns.Add("载货清单号");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("报检单号");
            string sql = "select * from list_order where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["总单号"] = data_dt.Rows[i]["TOTALNO"];
                dr["分单号"] = data_dt.Rows[i]["DIVIDENO"];
                dr["一程提运单号"] = data_dt.Rows[i]["FIRSTLADINGBILLNO"];
                dr["二程提运单号"] = data_dt.Rows[i]["SECONDLADINGBILLNO"];
                //dr["万达号"] = data_dt.Rows[i]["text"];
                dr["载货清单号"] = data_dt.Rows[i]["MANIFEST"];
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
                //dr["报检单号"] = data_dt.Rows[i]["text"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //后台同步日志导出明细
        public DataTable SyncFoo(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("唯一号");
            dt.Columns.Add("FWO号");
            dt.Columns.Add("FOO指令号");
            dt.Columns.Add("发送时间");
            dt.Columns.Add("订单号");
            dt.Columns.Add("业务类型");
            dt.Columns.Add("客户编号");
            dt.Columns.Add("合同发票号");
            dt.Columns.Add("总单号");
            dt.Columns.Add("分单号");
            dt.Columns.Add("木质包装");
            dt.Columns.Add("件数");
            dt.Columns.Add("重量");
            dt.Columns.Add("申报方式");
            dt.Columns.Add("申报关区");
            dt.Columns.Add("口岸");
            dt.Columns.Add("报关申报单位");
            dt.Columns.Add("报检申报单位");
            dt.Columns.Add("订单需求");
            dt.Columns.Add("委托类型");
            dt.Columns.Add("载货清单");
            dt.Columns.Add("船名");
            dt.Columns.Add("航次");
            dt.Columns.Add("提单号");
            dt.Columns.Add("贸易方式");
            dt.Columns.Add("报关方式");
            dt.Columns.Add("包装种类");
            dt.Columns.Add("运抵编号");
            dt.Columns.Add("一程提单号");
            dt.Columns.Add("二程提单号");
            dt.Columns.Add("货物类型");
            dt.Columns.Add("经营单位");
            dt.Columns.Add("创建人");
            dt.Columns.Add("特殊关系确认");
            dt.Columns.Add("价格影响确认");
            dt.Columns.Add("支付特许权使用费确认");
            dt.Columns.Add("是否需要重量确认");
            dt.Columns.Add("确认件数");
            dt.Columns.Add("确认重量");
            dt.Columns.Add("发货单位+编码");
            dt.Columns.Add("委托时间");
            dt.Columns.Add("收货单位+编码");
            dt.Columns.Add("报关可执行");
            dt.Columns.Add("集装箱及车辆信息");
            dt.Columns.Add("货物形态");
            dt.Columns.Add("是否提前报关");
            dt.Columns.Add("二线合同专用发票号");
            dt.Columns.Add("重量确认标志");
            string sql = "select * from LIST_SAPFOO where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["唯一号"] = data_dt.Rows[i]["ONLYCODE"];
                dr["FWO号"] = data_dt.Rows[i]["FWONO"];
                dr["FOO指令号"] = data_dt.Rows[i]["FOONO"];
                dr["发送时间"] = data_dt.Rows[i]["TIME"];
                dr["订单号"] = data_dt.Rows[i]["CODE"];
                dr["业务类型"] = data_dt.Rows[i]["BUSITYPE"];
                dr["客户编号"] = data_dt.Rows[i]["CUSNO"];
                dr["合同发票号"] = data_dt.Rows[i]["CONTRACTNO"];
                dr["总单号"] = data_dt.Rows[i]["TOTALNO"];
                dr["分单号"] = data_dt.Rows[i]["DIVIDENO"];
                dr["木质包装"] = data_dt.Rows[i]["WOODPACKINGID"];
                dr["件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["申报方式"] = data_dt.Rows[i]["REPWAYID"];
                dr["申报关区"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dr["口岸"] = data_dt.Rows[i]["PORTCODE"];
                dr["报关申报单位"] = data_dt.Rows[i]["REPUNITCODE"];
                dr["报检申报单位"] = data_dt.Rows[i]["INSPUNITCODE"];
                dr["订单需求"] = data_dt.Rows[i]["ENTRUSTREQUEST"];
                dr["委托类型"] = data_dt.Rows[i]["ENTRUSTTYPEID"];
                dr["载货清单"] = data_dt.Rows[i]["MANIFEST"];
                dr["船名"] = data_dt.Rows[i]["SHIPNAME"];
                dr["航次"] = data_dt.Rows[i]["FILGHTNO"];
                dr["提单号"] = data_dt.Rows[i]["LADINGBILLNO"];
                dr["贸易方式"] = data_dt.Rows[i]["TRADEWAYCODES"];
                dr["报关方式"] = data_dt.Rows[i]["DECLWAY"];
                dr["包装种类"] = data_dt.Rows[i]["PACKKIND"];
                dr["运抵编号"] = data_dt.Rows[i]["ARRIVEDNO"];
                dr["一程提单号"] = data_dt.Rows[i]["FIRSTLADINGBILLNO"];
                dr["二程提单号"] = data_dt.Rows[i]["SECONDLADINGBILLNO"];
                dr["货物类型"] = data_dt.Rows[i]["GOODSTYPEID"];
                dr["经营单位"] = data_dt.Rows[i]["BUSIUNITNAME"];
                dr["创建人"] = data_dt.Rows[i]["CREATEUSERNAME"];
                dr["特殊关系确认"] = data_dt.Rows[i]["SPECIALRELATIONSHIP"];
                dr["价格影响确认"] = data_dt.Rows[i]["PRICEIMPACT"];
                dr["支付特许权使用费确认"] = data_dt.Rows[i]["PAYPOYALTIES"];
                dr["是否需要重量确认"] = data_dt.Rows[i]["WEIGHTCHECK"];
                dr["确认件数"] = data_dt.Rows[i]["CHECKEDGOODSNUM"];
                dr["确认重量"] = data_dt.Rows[i]["CHECKEDWEIGHT"];
                dr["发货单位+编码"] = data_dt.Rows[i]["FGOODSUNIT"];
                dr["委托时间"] = data_dt.Rows[i]["CREATETIME"];
                dr["收货单位+编码"] = data_dt.Rows[i]["SGOODSUNIT"];
                dr["报关可执行"] = data_dt.Rows[i]["ALLOWDECLARE"];
                dr["集装箱及车辆信息"] = data_dt.Rows[i]["CONTAINERTRUCKS"];
                dr["货物形态"] = data_dt.Rows[i]["GOODSXT"];
                dr["是否提前报关"] = data_dt.Rows[i]["ISPREDECLARE"];
                dr["二线合同专用发票号"] = data_dt.Rows[i]["INVOICENO"];
                dr["重量确认标志"] = data_dt.Rows[i]["ISCHECKEDWEIGHT"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //后台同步状态导出明细
        public DataTable SyncMsg(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWO号");
            dt.Columns.Add("来源");
            dt.Columns.Add("描述");
            dt.Columns.Add("状态");
            dt.Columns.Add("时间");
            string sql = "select * from MSG where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["FWO号"] = data_dt.Rows[i]["FWONO"];
                dr["来源"] = data_dt.Rows[i]["SOURCE"];
                dr["描述"] = data_dt.Rows[i]["TEXT"];
                dr["状态"] = data_dt.Rows[i]["STATUS"];
                dr["时间"] = data_dt.Rows[i]["CREATETIME"];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //后台同步状态导出明细
        public DataTable OutDeclaration(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("业务单号");
            dt.Columns.Add("FWO号");
            dt.Columns.Add("业务类型");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("转关预录号");
            dt.Columns.Add("是否作废");
            dt.Columns.Add("下游返回日期");
            dt.Columns.Add("下游修改日期");
            dt.Columns.Add("海关状态");
            dt.Columns.Add("通关单张数");
            dt.Columns.Add("品名个数");
            dt.Columns.Add("是否删单");
            dt.Columns.Add("删单次数");
            dt.Columns.Add("删单备注");
            dt.Columns.Add("是否改单");
            dt.Columns.Add("改单次数");
            dt.Columns.Add("改单备注");
            dt.Columns.Add("是否移交");

            dt.Columns.Add("是否理货");
            dt.Columns.Add("理货次数");
            dt.Columns.Add("理货备注");
            dt.Columns.Add("是否查验");

            dt.Columns.Add("查验次数");
            dt.Columns.Add("查验备注");
            dt.Columns.Add("查验指令下发时间");
            dt.Columns.Add("是否调档");
            dt.Columns.Add("调档次数");
            dt.Columns.Add("是否扣货");
            dt.Columns.Add("扣货时间");
            string sql = "select o.fwono,o.foono,o.foonobj,o.code,o.XCBUSINAME,o.ONLYCODE,o.id,d.* from LIST_DECLARATION d left join list_order o on o.code=d.ordercode where 1=1  and d.id in(" + ids + ")";
            //string sql = "select * from MSG where ID in(" + ids + ") order by ID desc";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["业务单号"] = data_dt.Rows[i]["ORDERCODE"];
                dr["FWO号"] = data_dt.Rows[i]["FWONO"];
                dr["业务类型"] = data_dt.Rows[i]["XCBUSINAME"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["转关预录号"] = data_dt.Rows[i]["TRANSNAME"];
                dr["是否作废"] = data_dt.Rows[i]["ISDEL"];
                dr["下游返回日期"] = data_dt.Rows[i]["CREATETIME"];
                dr["下游修改日期"] = data_dt.Rows[i]["UPDATETIME"];
                dr["海关状态"] = data_dt.Rows[i]["CUSTOMSSTATUS"];
                dr["通关单张数"] = data_dt.Rows[i]["SHEETNUM"];
                dr["品名个数"] = data_dt.Rows[i]["COMMODITYNUM"];
                dr["是否删单"] = data_dt.Rows[i]["IFSHANDAN"];
                dr["删单次数"] = data_dt.Rows[i]["SHANDANTOTAL"];
                dr["删单备注"] = data_dt.Rows[i]["SHANDANDESC"];
                dr["是否改单"] = data_dt.Rows[i]["IFGAIDAN"];
                dr["改单次数"] = data_dt.Rows[i]["GAIDANTOTAL"];
                dr["改单备注"] = data_dt.Rows[i]["GAIDANDESC"];
                dr["是否移交"] = data_dt.Rows[i]["IFYIJIAO"];
                dr["是否理货"] = data_dt.Rows[i]["IFLIHUO"];
                dr["理货次数"] = data_dt.Rows[i]["LIHUOTOTAL"];
                dr["理货备注"] = data_dt.Rows[i]["LIHUODESC"];
                dr["是否查验"] = data_dt.Rows[i]["IFCHAYAN"];
                dr["查验次数"] = data_dt.Rows[i]["CHAYANTOTAL"];
                dr["查验备注"] = data_dt.Rows[i]["CHAYANDESC"];
                dr["查验指令下发时间"] = data_dt.Rows[i]["CHAYANZHILINGXIAFATIME"];
                dr["是否调档"] = data_dt.Rows[i]["IFTIAODANG"];
                dr["调档次数"] = data_dt.Rows[i]["TIAODANGTOTAL"];
                dr["是否扣货"] = data_dt.Rows[i]["IFKOUHUO"];
                dr["扣货时间"] = data_dt.Rows[i]["KOUHUOTIME"];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //后台订单列表导出明细
        public DataTable OutOrderList(string ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("业务单号");
            dt.Columns.Add("上游下发日期");
            dt.Columns.Add("发送下游日期");
            dt.Columns.Add("FWO号");
            dt.Columns.Add("报关FOO");
            dt.Columns.Add("报检FOO");
            dt.Columns.Add("业务类型");
            dt.Columns.Add("报关单号");
            dt.Columns.Add("海关状态");
            dt.Columns.Add("报检流水号");
            dt.Columns.Add("报检单号");
            dt.Columns.Add("是否法检");
            dt.Columns.Add("理货标记");
            dt.Columns.Add("理货次数");
            dt.Columns.Add("单证放行时间");
            dt.Columns.Add("实物放行时间");
            string sql = "select * from list_order where 1=1  and  id in(" + ids + ")";
            DataTable data_dt = DBMgr.GetDataTable(sql);
            DataRow dr;
            for (int i = 0; i < data_dt.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["业务单号"] = data_dt.Rows[i]["CODE"];
                dr["上游下发日期"] = data_dt.Rows[i]["CREATETIME"];
                dr["发送下游日期"] = data_dt.Rows[i]["UPDATETIME"];
                dr["FWO号"] = data_dt.Rows[i]["FWONO"];
                dr["报关FOO"] = data_dt.Rows[i]["FOONO"];
                dr["报检FOO"] = data_dt.Rows[i]["FOONOBJ"];
                dr["业务类型"] = data_dt.Rows[i]["XCBUSINAME"];
                dr["报关单号"] = data_dt.Rows[i]["DECLARATIONCODE"];
                dr["海关状态"] = data_dt.Rows[i]["DECLARATIONSTATUS"];
                dr["报检流水号"] = data_dt.Rows[i]["APPROVALCODE"];
                dr["报检单号"] = data_dt.Rows[i]["INSPECTIONCODE"];
                dr["是否法检"] = data_dt.Rows[i]["LAWCONDITION"];
                dr["理货标记"] = data_dt.Rows[i]["IFLIHUO"];
                dr["理货次数"] = data_dt.Rows[i]["LIHUOTOTAL"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //导出数据
        [HttpPost]
        public ActionResult DataExportOut()
        {
            string IDS = Request["ids"];
            string BUSITYPE = Request["BUSITYPE"];
            string Nowtime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            DataTable dt = new DataTable();
            string path = "";
            string OutPath = "";
            switch (BUSITYPE)
            {
                case "ONEIN":
                    path = "/Export/报关一线进口" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = One_lineIn(IDS);
                    break;
                case "ONEOUT":
                    path = "/Export/报关一线出口" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = One_lineOut(IDS);
                    break;
                case "BLC":
                    path = "/Export/报关国内结转" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = DomesticBlc(IDS);
                    break;
                case "SPECIAL":
                    path = "/Export/报关特殊监管" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = SpecialSupervision(IDS);
                    break;
                case "ONEINBJ":
                    path = "/Export/报检一线进口" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = One_lineInbj(IDS);
                    break;
                case "BLCBJ":
                    path = "/Export/报检国内结转" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = DomesticKnotbj(IDS);
                    break;
                case "SyncFoo":
                    path = "/Export/后台同步日志" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = SyncFoo(IDS);
                    break;
                case "SyncMsg":
                    path = "/Export/后台同步状态" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = SyncMsg(IDS);
                    break;
                case "Declaration":
                    path = "/Export/报关单列表" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = OutDeclaration(IDS);
                    break;
                case "OrderList":
                    path = "/Export/订单列表" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = OutOrderList(IDS);
                    break;
                default:
                    path = "/Export/异常登记" + Nowtime + ".xls";
                    OutPath = Server.MapPath("~" + path);
                    dt = Abnormal(IDS);
                    break;
            }
            OutFileToDisk(dt, OutPath);
            return Json(new { path = path }, JsonRequestBehavior.AllowGet);
        }

        /// <summary> 
        /// 导出数据到本地 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="path">保存路径</param> 
        public static void OutFileToDisk(DataTable dt, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格  
            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            //style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 9;//文字大小 
            style2.Font.IsBold = true;//粗体 
            //style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            //style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 9;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 
            cells.SetRowHeight(0, 38);
            //生成行2 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[0, i].PutValue(dt.Columns[i].ColumnName);
                cells[0, i].SetStyle(style2);
                cells.SetRowHeight(0, 25);
            }
            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                    cells[1 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(1 + i, 24);
            }
            workbook.Save(path);
        }
        //申报关区 进口口岸 

        public ActionResult OrderList()
        {
            ViewData["crumb"] = "订单管理-->订单列表";
            return View();
        }

        public ActionResult DeclarationList()
        {
            ViewData["crumb"] = "订单管理-->报关单列表";
            return View();
        }
        public ActionResult InspectionList()
        {
            ViewData["crumb"] = "订单管理-->报检单列表";
            return View();
        }

        public string LoadInspectionList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select o.fwono,o.foono,o.foonobj,o.code,o.XCBUSINAME,o.ONLYCODE,d.* from LIST_INSPECTION d left join list_order o on o.code=d.ordercode where 1=1 ";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Trim();
                    string name = "";
                    if (jo.Value<string>("ordercode") == "CODE" || jo.Value<string>("ordercode") == "DECLARATIONCODE" || jo.Value<string>("ordercode") == "APPROVALCODE")
                    {
                        name = "d." + jo.Value<string>("ordercode");
                    }
                    else
                    {
                        name = "o." + jo.Value<string>("ordercode");
                    }

                    sql += " AND " + name + " like '%" + ordercode_value + "%'";
                }
                if (jo.Value<string>("XCBUSINAME") != null && jo.Value<string>("XCBUSINAME") != "")
                {
                    sql += " AND o.XCBUSINAME = '" + jo.Value<string>("XCBUSINAME") + "' ";
                }

                if (jo.Value<string>("startdate") != "")
                {
                    string startdate = jo.Value<string>("startdate") + " 00:00:00";
                    sql += " AND d." + jo.Value<string>("orderdate") + " >= to_date('" + startdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
                if (jo.Value<string>("stopdate") != "")
                {
                    string stopdate = jo.Value<string>("stopdate") + " 23:59:59";
                    sql += " AND d." + jo.Value<string>("orderdate") + " <= to_date('" + stopdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            string sort = !string.IsNullOrEmpty(Request.Params["sort"]) && Request.Params["sort"] != "text" ? Request.Params["sort"] : "ID";
            sort = "d." + sort;
            string order = !string.IsNullOrEmpty(Request.Params["order"]) ? Request.Params["order"] : "DESC";
            sql = Extension.GetPageSql(sql, sort, order, ref total, (Page - 1) * PageSize, Page * PageSize);
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = "{\"total\":" + total + ",\"rows\":" + result + "}";
            return result;
        }


        public string LoadDeclarationList()
        {
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select o.fwono,o.foono,o.foonobj,o.code,o.XCBUSINAME,o.ONLYCODE,d.* from LIST_DECLARATION d left join list_order o on o.code=d.ordercode where 1=1 ";
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Trim();
                    string name = "";
                    if (jo.Value<string>("ordercode") == "ORDERCODE" || jo.Value<string>("ordercode") == "DECLARATIONCODE" || jo.Value<string>("ordercode") == "PREDECLCODE")
                    {
                        name = "d." + jo.Value<string>("ordercode");
                    }
                    else
                    {
                        name = "o." + jo.Value<string>("ordercode");
                    }

                    sql += " AND " + name + " like '%" + ordercode_value + "%'";
                }
                if (jo.Value<string>("XCBUSINAME") != null && jo.Value<string>("XCBUSINAME") != "")
                {
                    sql += " AND o.XCBUSINAME = '" + jo.Value<string>("XCBUSINAME") + "' ";
                }

                if (jo.Value<string>("startdate") != "")
                {
                    string startdate = jo.Value<string>("startdate") + " 00:00:00";
                    sql += " AND d." + jo.Value<string>("orderdate") + " >= to_date('" + startdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
                if (jo.Value<string>("stopdate") != "")
                {
                    string stopdate = jo.Value<string>("stopdate") + " 23:59:59";
                    sql += " AND d." + jo.Value<string>("orderdate") + " <= to_date('" + stopdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            string sort = !string.IsNullOrEmpty(Request.Params["sort"]) && Request.Params["sort"] != "text" ? Request.Params["sort"] : "ID";
            sort = "d." + sort;
            string order = !string.IsNullOrEmpty(Request.Params["order"]) ? Request.Params["order"] : "DESC";
            sql = Extension.GetPageSql(sql, sort, order, ref total, (Page - 1) * PageSize, Page * PageSize);
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = "{\"total\":" + total + ",\"rows\":" + result + "}";
            return result;
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
            string DECLARATIONCODE = Request.QueryString["DECLARATIONCODE"];//报关单号
            string INSPECTIONCODE = Request.QueryString["INSPECTIONCODE"];//报检单号

            string APPROVALCODE = Request.QueryString["APPROVALCODE"];//报检流水号

            string TYPE = Request.QueryString["TYPE"];// 判断是报关  还是报检
            string sql = "select *  from list_order  where  ID = " + ID;
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            JObject OrderArray = JsonConvert.DeserializeObject<JObject>(result);                //json转换为数组
            string CODE = OrderArray.Value<string>("CODE");                             //取数组的一个值


            if (TYPE == "01")
            {
                if (string.IsNullOrEmpty(DECLARATIONCODE))
                {
                    sql = "select * from LIST_DECLARATION where  ORDERCODE='" + CODE + "'  order by id asc";
                }
                else
                {
                    sql = "select * from LIST_DECLARATION where  DECLARATIONCODE='" + DECLARATIONCODE + "'";
                }
                dt = DBMgr.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DECLARATIONCODE = dt.Rows[0]["DECLARATIONCODE"] + "";
                    OrderArray["DECLARATIONCODE"] = DECLARATIONCODE;
                    OrderArray["ORDERCODE"] = OrderArray.Value<string>("CODE");
                    OrderArray["IFSHANDAN"] = dt.Rows[0]["IFSHANDAN"] + "";
                    OrderArray["SHANDANTOTAL"] = dt.Rows[0]["SHANDANTOTAL"] + "";
                    OrderArray["SHANDANDESC"] = dt.Rows[0]["SHANDANDESC"] + "";
                    OrderArray["IFGAIDAN"] = dt.Rows[0]["IFGAIDAN"] + "";
                    OrderArray["GAIDANTOTAL"] = dt.Rows[0]["GAIDANTOTAL"] + "";
                    OrderArray["GAIDANDESC"] = dt.Rows[0]["GAIDANDESC"] + "";
                    //OrderArray["IFLIHUO"] = dt.Rows[0]["IFLIHUO"] + "";
                    //OrderArray["LIHUOTOTAL"] = dt.Rows[0]["LIHUOTOTAL"] + "";
                    OrderArray["IFYIJIAO"] = dt.Rows[0]["IFYIJIAO"] + "";
                    OrderArray["IFCHAYAN"] = dt.Rows[0]["IFCHAYAN"] + "";
                    OrderArray["CHAYANTOTAL"] = dt.Rows[0]["CHAYANTOTAL"] + "";
                    OrderArray["CHAYANDESC"] = dt.Rows[0]["CHAYANDESC"] + "";
                    OrderArray["IFTIAODANG"] = dt.Rows[0]["IFTIAODANG"] + "";
                    OrderArray["TIAODANGTOTAL"] = dt.Rows[0]["TIAODANGTOTAL"] + "";
                    OrderArray["IFKOUHUO"] = dt.Rows[0]["IFKOUHUO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CHAYANZHILINGXIAFATIME"] + ""))
                    {
                        OrderArray["CHAYANZHILINGXIAFATIME"] = Convert.ToDateTime(dt.Rows[0]["CHAYANZHILINGXIAFATIME"]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["KOUHUOTIME"] + ""))
                    {
                        OrderArray["KOUHUOTIME"] = Convert.ToDateTime(dt.Rows[0]["KOUHUOTIME"]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {

                    OrderArray["DECLARATIONCODE"] = "";
                    OrderArray["ORDERCODE"] = OrderArray.Value<string>("CODE");

                    OrderArray["IFSHANDAN"] = "";
                    OrderArray["SHANDANTOTAL"] = "";
                    OrderArray["SHANDANDESC"] = "";

                    OrderArray["IFGAIDAN"] = "";
                    OrderArray["GAIDANTOTAL"] = "";
                    OrderArray["GAIDANDESC"] = "";

                    OrderArray["IFYIJIAO"] = "";
                    OrderArray["IFCHAYAN"] = "";
                    OrderArray["CHAYANTOTAL"] = "";
                    OrderArray["CHAYANDESC"] = "";

                    //OrderArray["IFLIHUO"] = "";
                    //OrderArray["LIHUOTOTAL"] = "";

                    OrderArray["IFTIAODANG"] = "";
                    OrderArray["TIAODANGTOTAL"] = "";
                    OrderArray["IFKOUHUO"] = "";

                    OrderArray["CHAYANZHILINGXIAFATIME"] = "";
                    OrderArray["KOUHUOTIME"] = "";
                }
            }
            else if (TYPE == "02")
            {
                if (string.IsNullOrEmpty(APPROVALCODE))
                {
                    sql = "select * from LIST_INSPECTION where  ORDERCODE='" + CODE + "' order by id asc";
                }
                else
                {
                    sql = "select * from LIST_INSPECTION where  APPROVALCODE='" + APPROVALCODE + "'";
                }
                dt = DBMgr.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    OrderArray["INSPECTIONCODE"] = dt.Rows[0]["INSPECTIONCODE"] + "";
                    OrderArray["APPROVALCODE"] = dt.Rows[0]["APPROVALCODE"] + "";
                    OrderArray["ORDERCODE"] = OrderArray.Value<string>("CODE");
                    OrderArray["IFSHANDAN"] = dt.Rows[0]["IFSHANDAN"] + "";
                    OrderArray["SHANDANTOTAL"] = dt.Rows[0]["SHANDANTOTAL"] + "";
                    OrderArray["SHANDANDESC"] = dt.Rows[0]["SHANDANDESC"] + "";

                    OrderArray["IFGAIDAN"] = dt.Rows[0]["IFGAIDAN"] + "";
                    OrderArray["GAIDANTOTAL"] = dt.Rows[0]["GAIDANTOTAL"] + "";
                    OrderArray["GAIDANDESC"] = dt.Rows[0]["GAIDANDESC"] + "";

                    OrderArray["IFXUNZHENG"] = dt.Rows[0]["IFXUNZHENG"] + "";
                    OrderArray["XUNZHENGDESC"] = dt.Rows[0]["XUNZHENGDESC"] + "";


                    OrderArray["IFCHAYAN"] = dt.Rows[0]["IFCHAYAN"] + "";
                    OrderArray["CHAYANTOTAL"] = dt.Rows[0]["CHAYANTOTAL"] + "";
                    OrderArray["CHAYANDESC"] = dt.Rows[0]["CHAYANDESC"] + "";

                }
                else
                {
                    OrderArray["INSPECTIONCODE"] = "";
                    OrderArray["ORDERCODE"] = OrderArray.Value<string>("CODE");

                    OrderArray["IFSHANDAN"] = "";
                    OrderArray["SHANDANTOTAL"] = "";
                    OrderArray["SHANDANDESC"] = "";

                    OrderArray["IFGAIDAN"] = "";
                    OrderArray["GAIDANTOTAL"] = "";
                    OrderArray["GAIDANDESC"] = "";

                    OrderArray["IFXUNZHENG"] = "";
                    OrderArray["XUNZHENGDESC"] = "";

                    OrderArray["IFCHAYAN"] = "";
                    OrderArray["CHAYANTOTAL"] = "";
                    OrderArray["CHAYANDESC"] = "";
                }
            }




            string DECLWAY = OrderArray.Value<string>("DECLWAY");                               //取数组的一个值
            switch (DECLWAY)
            {
                case "W":
                    OrderArray["DECLWAY"] = "无纸报关";                                         //更改数组的值
                    break;
                case "D":
                    OrderArray["DECLWAY"] = "无纸带清单报关";
                    break;
                case "L":
                    OrderArray["DECLWAY"] = "有纸带清单报关";
                    break;
                case "O":
                    OrderArray["DECLWAY"] = "有纸报关";
                    break;
                case "M":
                    OrderArray["DECLWAY"] = "通关无纸化";
                    break;
            }
            string BUSITYPE = OrderArray.Value<string>("BUSITYPE");
            switch (BUSITYPE)
            {
                case "10":
                    OrderArray["BUSITYPE"] = "空运出口";
                    break;
                case "11":
                    OrderArray["BUSITYPE"] = "空运进口";
                    break;
                case "20":
                    OrderArray["BUSITYPE"] = "海运出口";
                    break;
                case "21":
                    OrderArray["BUSITYPE"] = "海运进口";
                    break;
                case "30":
                    OrderArray["BUSITYPE"] = "陆运出口";
                    break;
                case "31":
                    OrderArray["BUSITYPE"] = "陆运进口";
                    break;
                case "40":
                    OrderArray["BUSITYPE"] = "国内出口";
                    break;
                case "41":
                    OrderArray["BUSITYPE"] = "国内进口";
                    break;
                case "50":
                    OrderArray["BUSITYPE"] = "特殊区域出口";
                    break;
                case "51":
                    OrderArray["BUSITYPE"] = "特殊区域进口";
                    break;
            }
            var info = JsonConvert.SerializeObject(OrderArray);                                 //将数组再转换为json格式
            return info;
        }

        [HttpGet]
        public string GetData()
        {
            string BUSITYPE = "";
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;//DECLARATIONCODE
            string sql = "select t.* from list_order  t where 1=1 ";
            //搜索查询 DLC 2016/10/14
            string data = Request["data"];
            if (data != null)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(data);      //json格式转换为数组
                //是否放行
                if (jo.Value<string>("IFFANGXING") == "1")
                {
                    sql += "AND　id in (select id from list_order where  DANZHENGFANGXINGUSERNAME is not  null and SHIWUFANGXINGUSERNAME is not null)";
                }
                else if (jo.Value<string>("IFFANGXING") == "0")
                {
                    sql += "AND　id in (select id from list_order where  DANZHENGFANGXINGUSERNAME is  null or SHIWUFANGXINGUSERNAME is  null)";
                }

                BUSITYPE = jo.Value<string>("BUSITYPE");
                if (jo.Value<string>("businessin_object") != null && jo.Value<string>("businessin_object") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("businessin_object") + "' ";
                }
                if (jo.Value<string>("businessout_object") != null && jo.Value<string>("businessout_object") != "")
                {
                    sql += " AND BUSITYPE = '" + jo.Value<string>("businessout_object") + "' ";
                }
                string YCBUSITYPE = jo.Value<string>("YCBUSITYPE");
                switch (YCBUSITYPE)
                {
                    case "10":
                        sql += " AND BUSITYPE = '10' ";
                        break;
                    case "11":
                        sql += " AND BUSITYPE = '11' ";
                        break;
                    case "20":
                        sql += " AND BUSITYPE = '20' ";
                        break;
                    case "21":
                        sql += " AND BUSITYPE = '21' ";
                        break;
                    case "30":
                        sql += " AND BUSITYPE = '30' ";
                        break;
                    case "31":
                        sql += " AND BUSITYPE = '31' ";
                        break;
                    case "GUONEIJIEZHUAN":
                        sql += " AND ASSOCIATENO is not null and CORRESPONDNO is null";
                        break;
                    case "DIEJIABAOSHUI":
                        sql += " AND ASSOCIATENO is not null and CORRESPONDNO is not null";
                        break;
                    case "TESHUJIANGUANQU":
                        sql += " AND BUSITYPE='50' or BUSITYPE='51'";
                        break;
                }


                if (jo.Value<string>("ENTRUSTTYPEID") != null && jo.Value<string>("ENTRUSTTYPEID") != "")
                {
                    sql += " AND ENTRUSTTYPEID = '" + jo.Value<string>("ENTRUSTTYPEID") + "' ";
                }

                if (jo.Value<string>("IFSEND") != null && jo.Value<string>("IFSEND") != "")
                {
                    sql += " AND IFSEND = '" + jo.Value<string>("IFSEND") + "' ";
                }

                if (jo.Value<string>("FILERELATE") != null && jo.Value<string>("FILERELATE") != "")
                {
                    sql += " AND FILERELATE = '" + jo.Value<string>("FILERELATE") + "' ";
                }

                if (jo.Value<string>("XCBUSINAME") != null && jo.Value<string>("XCBUSINAME") != "")
                {
                    sql += " AND XCBUSINAME = '" + jo.Value<string>("XCBUSINAME") + "' ";
                }

                if (jo.Value<string>("service_model") != null && jo.Value<string>("service_model") != "")
                {
                    sql += " AND WTFS = '" + jo.Value<string>("service_model") + "' ";
                }
                if (jo.Value<string>("out_in") != null && jo.Value<string>("out_in") != "")
                {
                    sql += " AND WTFS = '" + jo.Value<string>("out_in") + "' ";
                }
                if (jo.Value<string>("ordercode_value") != "" && jo.Value<string>("ordercode") != "text")
                {
                    string ordercode_value = jo.Value<string>("ordercode_value").Trim();
                    sql += " AND " + jo.Value<string>("ordercode") + " like '%" + ordercode_value + "%'";
                }
                if (jo.Value<string>("oprname_value") != null && jo.Value<string>("oprname_value") != "")
                {
                    string oprname_value = jo.Value<string>("oprname_value").Replace(" ", "");
                    sql += " AND " + jo.Value<string>("oprname") + " ='" + oprname_value + "'";
                }
                if (jo.Value<string>("startdate") != "")
                {
                    string startdate = jo.Value<string>("startdate") + " 00:00:00";
                    sql += " AND " + jo.Value<string>("orderdate") + " >= to_date('" + startdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
                if (jo.Value<string>("stopdate") != "")
                {
                    string stopdate = jo.Value<string>("stopdate") + " 23:59:59";
                    sql += " AND " + jo.Value<string>("orderdate") + " <= to_date('" + stopdate + "','yyyy-mm-dd hh24:mi:ss')";
                }
                if (jo.Value<string>("CUSTOMDISTRICTCODE") != null && jo.Value<string>("CUSTOMDISTRICTCODE") != "")
                {
                    sql += " AND CUSTOMDISTRICTCODE = '" + jo.Value<string>("CUSTOMDISTRICTCODE") + "' ";
                }
                if (jo.Value<string>("declaration_type") != null && jo.Value<string>("declaration_type") != "")
                {
                    sql += " AND DECLWAY = '" + jo.Value<string>("declaration_type") + "' ";
                }

                if (jo.Value<string>("DECLARATIONCODE") != null && jo.Value<string>("DECLARATIONCODE") != "")
                {
                    if (jo.Value<string>("DECLARATIONCODE") == "1")
                    {
                        sql += " AND DECLARATIONCODE  is not null ";
                    }
                    if (jo.Value<string>("DECLARATIONCODE") == "0")
                    {
                        sql += " AND DECLARATIONCODE  is  null ";
                    }
                }


                if (jo.Value<string>("INSPECTIONCODE") != null && jo.Value<string>("INSPECTIONCODE") != "")
                {
                    if (jo.Value<string>("INSPECTIONCODE") == "1")
                    {
                        sql += " AND INSPECTIONCODE  is not null ";
                    }
                    if (jo.Value<string>("INSPECTIONCODE") == "0")
                    {
                        sql += " AND INSPECTIONCODE  is  null ";
                    }
                }


                if (jo.Value<string>("APPROVALCODE") != null && jo.Value<string>("APPROVALCODE") != "")
                {
                    if (jo.Value<string>("APPROVALCODE") == "1")
                    {
                        sql += " AND APPROVALCODE  is not null ";
                    }
                    if (jo.Value<string>("APPROVALCODE") == "0")
                    {
                        sql += " AND APPROVALCODE  is  null ";
                    }
                }


                if (jo.Value<string>("LAWCONDITION") == "1")
                {
                    sql += " AND LAWCONDITION = '1' ";
                }
                else if (jo.Value<string>("LAWCONDITION") == "0")
                {
                    sql += " AND  LAWCONDITION='0' ";
                }


                if (jo.Value<string>("ISNEEDCLEARANCE") == "1")
                {
                    sql += " AND ISNEEDCLEARANCE = '1' ";
                }
                else if (jo.Value<string>("ISNEEDCLEARANCE") == "0")
                {
                    sql += " AND  ISNEEDCLEARANCE='0' ";
                }
                //if (jo.Value<string>("WOODPACKINGID") != null && jo.Value<string>("WOODPACKINGID") != "")
                //{
                //    sql += " AND WOODPACKINGID = '" + jo.Value<string>("WOODPACKINGID") + "' ";
                //}
                if (jo.Value<string>("IFLIHUO") == "1")
                {
                    sql += " AND IFLIHUO = '1' ";
                }
                else if (jo.Value<string>("IFLIHUO") == "0")
                {
                    sql += " AND IFLIHUO is null ";
                }

                //海关状态
                if (!string.IsNullOrEmpty(jo.Value<string>("customs_state")))
                {
                    if (jo.Value<string>("customs_state") == "1")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%提前转关生成%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "2")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%暂存未申报%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "3")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%已申报%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "4")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%申报退单%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "5")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%申报完成%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "6")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%现场申报%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "7")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%布控查验%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "8")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%已结关%' ";
                    }
                    else if (jo.Value<string>("customs_state") == "9")
                    {
                        sql += " AND  DECLARATIONSTATUS like '%已放行%' ";
                    }
                }
                //收发货单位
                if (!string.IsNullOrEmpty(jo.Value<string>("company_value")))
                {
                    sql += " AND  " + jo.Value<string>("company") + " like '%" + jo.Value<string>("company_value").Trim() + "%' ";
                }
            }
            else
            {
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

            //理货标识 要和业务走
            sql += "  IFLIHUO =  '" + Request.Form["IFLIHUO"] + "',";
            if (Request.Params.AllKeys.Contains("LIHUOTOTAL"))
            {
                sql += "  LIHUOTOTAL =  '" + Request.Form["LIHUOTOTAL"] + "',";
            }
            if (Request.Params.AllKeys.Contains("LIHUODESC"))
            {
                sql += "  LIHUODESC =  '" + Request.Form["LIHUODESC"] + "',";
            }


            sql += "  LAWCONDITION =  '" + Request.Form["LAWCONDITION"] + "',";

            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID =" + ID;

            if (DBMgr.ExecuteNonQuery(sql) == 1)
            {
                string FORMINFO = Request.Form.ToString();
                string type = Request.Form["type"];
                string CODE = Request.Form["CODE"];
                string DECLARATIONCODE = Request.Form["DECLARATIONCODE"];
                string APPROVALCODE = Request.Form["APPROVALCODE"];
                if (type == "02")
                {
                    UpdateXcInspection(APPROVALCODE, FORMINFO);
                    IFS.ZSBJ_ABNO(CODE);

                    string json = "{\"CODE\":'" + CODE + "',\"TYPE\":'BJ'}";
                    db.ListRightPush("XGW_BJYC", json);
                }
                else
                {
                    UpdateXcDeclaration(DECLARATIONCODE, FORMINFO);
                    IFS.ZSBG_ABNO(CODE);

                    string json = "{\"CODE\":'" + CODE + "',\"TYPE\":'BG'}";
                    db.ListRightPush("XGW_BGYC", json);
                }

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }

        }

        public static void UpdateXcInspection(string APPROVALCODE, string FORMINFO)
        {
            System.Collections.Specialized.NameValueCollection Request = System.Web.HttpUtility.ParseQueryString(FORMINFO);
            string sql = "update LIST_INSPECTION set";
            sql += "  IFSHANDAN =  '" + Request["IFSHANDAN"] + "',";
            if (Request.AllKeys.Contains("SHANDANTOTAL"))
            {
                sql += "  SHANDANTOTAL =  '" + Request["SHANDANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("SHANDANDESC"))
            {
                sql += "  SHANDANDESC =  '" + Request["SHANDANDESC"] + "',";
            }

            sql += "  IFGAIDAN =  '" + Request["IFGAIDAN"] + "',";
            if (Request.AllKeys.Contains("GAIDANTOTAL"))
            {
                sql += "  GAIDANTOTAL =  '" + Request["GAIDANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("GAIDANDESC"))
            {
                sql += "  GAIDANDESC =  '" + Request["GAIDANDESC"] + "',";
            }


            sql += "  IFCHAYAN =  '" + Request["IFCHAYAN"] + "',";
            if (Request.AllKeys.Contains("CHAYANTOTAL"))
            {
                sql += "  CHAYANTOTAL =  '" + Request["CHAYANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("CHAYANDESC"))
            {
                sql += "  CHAYANDESC =  '" + Request["CHAYANDESC"] + "',";
            }

            sql += "  IFXUNZHENG =  '" + Request["IFXUNZHENG"] + "',";
            if (Request.AllKeys.Contains("XUNZHENGDESC"))
            {
                sql += "  XUNZHENGDESC =  '" + Request["XUNZHENGDESC"] + "',";
            }


            sql = sql.Substring(0, sql.Length - 1);
            sql += " where APPROVALCODE ='" + APPROVALCODE + "'";
            DBMgr.ExecuteNonQuery(sql);
        }

        //同步报关单现场表里的数据 DLC 2016-11-3
        public static void UpdateXcDeclaration(string DECLARATIONCODE, string FORMINFO)
        {
            System.Collections.Specialized.NameValueCollection Request = System.Web.HttpUtility.ParseQueryString(FORMINFO);

            string sql = "update LIST_DECLARATION set";



            sql += "  IFSHANDAN =  '" + Request["IFSHANDAN"] + "',";
            if (Request.AllKeys.Contains("SHANDANTOTAL"))
            {
                sql += "  SHANDANTOTAL =  '" + Request["SHANDANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("SHANDANDESC"))
            {
                sql += "  SHANDANDESC =  '" + Request["SHANDANDESC"] + "',";
            }
            sql += "  IFGAIDAN =  '" + Request["IFGAIDAN"] + "',";
            if (Request.AllKeys.Contains("GAIDANTOTAL"))
            {
                sql += "  GAIDANTOTAL =  '" + Request["GAIDANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("GAIDANDESC"))
            {
                sql += "  GAIDANDESC =  '" + Request["GAIDANDESC"] + "',";
            }
            sql += "  IFYIJIAO =  '" + Request["IFYIJIAO"] + "',";


            //理货标识 要和业务走
            //sql += "  IFLIHUO =  '" + Request["IFLIHUO"] + "',";
            //if (Request.AllKeys.Contains("LIHUOTOTAL"))
            //{
            //    sql += "  LIHUOTOTAL =  '" + Request["LIHUOTOTAL"] + "',";
            //}
            //if (Request.AllKeys.Contains("LIHUODESC"))
            //{
            //    sql += "  LIHUODESC =  '" + Request["LIHUODESC"] + "',";
            //}

            sql += "  IFCHAYAN =  '" + Request["IFCHAYAN"] + "',";
            if (Request.AllKeys.Contains("CHAYANTOTAL"))
            {
                sql += "  CHAYANTOTAL =  '" + Request["CHAYANTOTAL"] + "',";
            }
            if (Request.AllKeys.Contains("CHAYANDESC"))
            {
                sql += "  CHAYANDESC =  '" + Request["CHAYANDESC"] + "',";
            }

            if (Request.AllKeys.Contains("CHAYANZHILINGXIAFATIME"))
            {
                sql += "  CHAYANZHILINGXIAFATIME =  to_date('" + Request["CHAYANZHILINGXIAFATIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            sql += "  IFTIAODANG =  '" + Request["IFTIAODANG"] + "',";

            if (Request.AllKeys.Contains("TIAODANGTOTAL"))
            {
                sql += "  TIAODANGTOTAL =  '" + Request["TIAODANGTOTAL"] + "',";
            }
            sql += "  IFKOUHUO =  '" + Request["IFKOUHUO"] + "',";
            if (Request.AllKeys.Contains("KOUHUOTIME"))
            {
                sql += "  KOUHUOTIME =  to_date('" + Request["KOUHUOTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " where DECLARATIONCODE ='" + DECLARATIONCODE + "'";
            DBMgr.ExecuteNonQuery(sql);
        }

        [HttpPost]
        public ActionResult Edit_Ajax_Scene(FormCollection form)
        {
            string ID = Request.Form["ID"];
            string type = Request.Form["type"];
            string datetime = Request.Form["date"];
            string ASSOCIATENO = Request.Form["ASSOCIATENO"];
            string INSPSTATUS = Request.Form["INSPSTATUS"];//报检
            string DECLSTATUS = Request.Form["DECLSTATUS"];//报关
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string name = jo.Value<string>("REALNAME");

            string sql = "update list_order set  ";

            if (!string.IsNullOrEmpty(INSPSTATUS))
            {
                sql += " INSPSTATUS='" + INSPSTATUS + "',";
            }

            if (!string.IsNullOrEmpty(DECLSTATUS))
            {
                sql += " DECLSTATUS='" + DECLSTATUS + "',";
            }

            if (!string.IsNullOrEmpty(type))
            {
                string time = type + "TIME";
                string userid = type + "USERID";
                string username = type + "USERNAME";
                if (string.IsNullOrEmpty(datetime))
                {
                    name = "";
                }
                sql += time + "  = to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss') ," + username + " ='" + name + "', " + userid + " =  '" + jo.Value<string>("ID") + "'";
            }
            if (!string.IsNullOrEmpty(ASSOCIATENO))
            {
                sql += " where ASSOCIATENO ='" + ASSOCIATENO + "'";
            }
            else
            {
                sql += " where ID ='" + ID + "'";
            }
            int res = DBMgr.ExecuteNonQuery(sql);
            if (res == 1)
            {
                //IFS.Callback_TM(type, ID, ASSOCIATENO);
                SendData(type, ID);
                datetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                return Json(new { Success = true, datetime = datetime, name = name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }
        }

        public void SendData(string type, string id)
        {
            if (type == "XIAOBAO")
            {
                //销保时间
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BG'}";
                db.ListRightPush("XGW_XBSJ", json);
            }
            else if (type == "CHAYANSTART")
            {
                //查验起始时间
                //string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BG'}";
                //db.ListRightPush("XGW_CYQSSJ", json);
            }
            else if (type == "CHAYANEND")
            {
                //查验完成时间
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BG'}";
                db.ListRightPush("XGW_CYWCSJ", json);
            }
            else if (type == "BAOJIANFANGXING")
            {
                //报检放行时间
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BJ'}";
                db.ListRightPush("XGW_BJFXSJ", json);
            }
            else if (type == "CHAYANFANGXING")
            {
                // 查验放行时间 完成
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BJ'}";
                db.ListRightPush("XGW_CYFXSJ", json);
            }
            else if (type == "BJCHAYAN")
            {
                //报检查验 
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BJ'}";
                db.ListRightPush("XGW_BJCYSJ", json);
            }
            else if (type == "BAORUHAIGUAN")
            {
                //报入海关时间
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BG'}";
                db.ListRightPush("XGW_BRHGSJ", json);
            }
            else if (type == "BAOJIAN")
            {
                //报检时间
                //string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BJ'}";
                //db.ListRightPush("XGW_BJSJ", json);
            }
            else if (type == "SHIWUFANGXING")
            {
                //实物放行时间
                string json = "{\"TIME\":'" + type + "',\"ID\":'" + id + "',\"TYPE\":'BG'}";
                db.ListRightPush("XGW_SWFXSJ", json);
            }
        }


        //报关批量更新
        public ActionResult BatchUpdate()
        {
            ViewData["ids"] = Request["ids"];
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);    //获取会员信息
            ViewData["USERNAME"] = jo.Value<string>("REALNAME");
            ViewData["USERID"] = jo.Value<string>("ID");
            return View();
        }
        //报检批量更新
        public ActionResult bjBatchUpdate()
        {
            ViewData["ids"] = Request["ids"];
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);    //获取会员信息
            ViewData["USERNAME"] = jo.Value<string>("REALNAME");
            ViewData["USERID"] = jo.Value<string>("ID");
            return View();
        }


        //报关批量更新保存
        public ActionResult SaveUpdateData(FormCollection form)
        {
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string ids = Request.Form["ids"];
            string sql = "update list_order set ";
            if (Request.Form["BAORUHAIGUANTIME"] != "")
            {
                sql += "BAORUHAIGUANTIME =  to_date('" + Request.Form["BAORUHAIGUANTIME"] + "','yyyy-MM-dd hh24:mi:ss'), BAORUHAIGUANUSERNAME ='" + jo.Value<string>("REALNAME") + "', BAORUHAIGUANUSERID='" + jo.Value<string>("ID") + "',";
            }
            if (Request.Form["DANZHENGFANGXINGTIME"] != "")
            {
                sql += "DANZHENGFANGXINGTIME =  to_date('" + Request.Form["DANZHENGFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),DANZHENGFANGXINGUSERNAME = '" + jo.Value<string>("REALNAME") + "',DANZHENGFANGXINGUSERID ='" + jo.Value<string>("ID") + "',";
            }
            if (Request.Form["SHIWUFANGXINGTIME"] != "")
            {
                sql += "SHIWUFANGXINGTIME =  to_date('" + Request.Form["SHIWUFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),SHIWUFANGXINGUSERNAME ='" + jo.Value<string>("REALNAME") + "',SHIWUFANGXINGUSERID ='" + jo.Value<string>("ID") + "',";
            }

            if (!string.IsNullOrEmpty(Request.Form["IFPASSMODE"]))
            {
                sql += "PASSMODE =  '" + Request.Form["PASSMODE"] + "',";
            }
            //IF_LIHUO
            if (!string.IsNullOrEmpty(Request.Form["IF_LIHUO"]))
            {
                sql += "IFLIHUO =  '" + Request["IFLIHUO"] + "',";
                sql += "LIHUOTOTAL =  '" + Request["LIHUOTOTAL"] + "',";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID in (" + ids + ")";

            try
            {
                if (DBMgr.ExecuteNonQuery(sql) != 0)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }



        }
        //报检批量更新保存
        public ActionResult SavebjUpdateData(FormCollection form)
        {
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string ids = Request.Form["ids"];
            string sql = "update list_order set ";
            if (Request.Form["BAOJIANTIME"] != "")
            {
                sql += "BAOJIANTIME =  to_date('" + Request.Form["BAOJIANTIME"] + "','yyyy-MM-dd hh24:mi:ss'),BAOJIANUSERNAME='" + jo.Value<string>("REALNAME") + "',BAOJIANUSERID='" + jo.Value<string>("ID") + "',";
            }
            if (Request.Form["BAOJIANFANGXINGTIME"] != "")
            {
                sql += "BAOJIANFANGXINGTIME =  to_date('" + Request.Form["BAOJIANFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'), BAOJIANFANGXINGUSERNAME='" + jo.Value<string>("REALNAME") + "',BAOJIANFANGXINGUSERID='" + jo.Value<string>("ID") + "',";

            }
            if (Request.Form["XUNZHENGTIME"] != "")
            {
                sql += "XUNZHENGTIME =  to_date('" + Request.Form["XUNZHENGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),XUNZHENGUSERNAME='" + jo.Value<string>("REALNAME") + "',XUNZHENGUSERID='" + jo.Value<string>("ID") + "',";
            }
            if (Request.Form["BJCHAYANTIME"] != "")
            {
                sql += "BJCHAYANTIME =  to_date('" + Request.Form["BJCHAYANTIME"] + "','yyyy-MM-dd hh24:mi:ss'),BJCHAYANUSERNAME='" + jo.Value<string>("REALNAME") + "',BJCHAYANUSERID='" + jo.Value<string>("ID") + "',";
            }
            if (Request.Form["CHAYANFANGXINGTIME"] != "")
            {
                sql += "CHAYANFANGXINGTIME =  to_date('" + Request.Form["CHAYANFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),CHAYANFANGXINGUSERNAME='" + jo.Value<string>("REALNAME") + "',CHAYANFANGXINGUSERID='" + jo.Value<string>("ID") + "',";
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID in (" + ids + ")";
            if (DBMgr.ExecuteNonQuery(sql) != 0)
            {
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }
        }

        //急装箱信息
        public ActionResult JizhuangxiangInfo()
        {
            ViewData["CODE"] = Request["CODE"];
            return View();
        }
        //急装箱列表
        public string LoadJzxList()
        {
            string CODE = Request["data"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from LIST_DECLCONTAINERTRUCK where ORDERCODE='" + CODE + "' ";
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
        //报关单信息
        public ActionResult BaoguandanInfo()
        {
            return View();
        }

        //报关单列表
        public string LoadBgdList()
        {
            string CODE = Request["data"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from list_declaration where ORDERCODE='" + CODE + "' and ISDEL='0'";
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

        public string LoadBjdList()
        {
            string CODE = Request["data"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select * from LIST_INSPECTION where ORDERCODE='" + CODE + "' and ISDEL='0' ";
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

        //报关单信息更新保存
        public ActionResult SaveBgdinfo()
        {
            string ID = Request.Form["ID"];
            string sql = "update list_declaration set ";

            if (Request.Params.AllKeys.Contains("ISPRINT"))
            {
                sql += "  ISPRINT =  '" + Request.Form["ISPRINT"] + "',";
            }
            if (Request.Params.AllKeys.Contains("PRINTNUM"))
            {
                sql += "  PRINTNUM =  '" + Request.Form["PRINTNUM"] + "',";
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID=" + ID;
            if (DBMgr.ExecuteNonQuery(sql) != 0)
            {
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }
        }

        //通关单信息
        public ActionResult TongguandanInfo()
        {
            return View();
        }
    }
}