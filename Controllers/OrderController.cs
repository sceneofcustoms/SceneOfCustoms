﻿using Aspose.Cells;
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
        //报关一线进口导出明细
        public DataTable One_lineIn(string ids)
        {
            DataTable dt = new DataTable();
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
            dt.Columns.Add("单证放行状态");
            dt.Columns.Add("实物放行状态");
            dt.Columns.Add("载货清单号");
            dt.Columns.Add("万达号");
            dt.Columns.Add("业务编号");
            dt.Columns.Add("转关预录入号");
            dt.Columns.Add("单证放行人");
            dt.Columns.Add("实物放行人");
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
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
                dr["是否法检"] = data_dt.Rows[i]["LAWCONDITION"];
                dr["关务接受时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["贸易方式"] = data_dt.Rows[i]["ASSOCIATETRADEWAY"];
                //dr["单证放行状态"] = data_dt.Rows[i]["text"];
                //dr["实物放行状态"] = data_dt.Rows[i]["text"];
                //dr["万达号"] = data_dt.Rows[i]["text"];
                dr["载货清单号"] = data_dt.Rows[i]["MANIFEST"];
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                dr["转关预录入号"] = data_dt.Rows[i]["TURNPRENO"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                //dr["海关通关状态"] = data_dt.Rows[i]["text"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //报关一线出口导出明细
        public DataTable One_lineOut(string ids)
        {
            DataTable dt = new DataTable();
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
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["实物加封时间"] = data_dt.Rows[i]["SHIWUJIAFENGTIME"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["贸易方式"] = data_dt.Rows[i]["ASSOCIATETRADEWAY"];
                dr["转关预录入号"] = data_dt.Rows[i]["TURNPRENO"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                //dr["海关通关状态"] = data_dt.Rows[i]["text"];
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
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
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
                dr["报检状态"] = data_dt.Rows[i]["INSPSTATUS"];
                dr["报关方式"] = data_dt.Rows[i]["DECLWAY"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                //dr["海关通关状态"] = data_dt.Rows[i]["text"];
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
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
                dr["实物放行时间"] = data_dt.Rows[i]["SHIWUFANGXINGTIME"];
                dr["收/发货人"] = data_dt.Rows[i]["SFGOODSUNIT"];
                dr["件数"] = data_dt.Rows[i]["GOODSNUM"];
                dr["重量"] = data_dt.Rows[i]["GOODSWEIGHT"];
                dr["报入海关时间"] = data_dt.Rows[i]["BAORUHAIGUANTIME"];
                dr["单证放行时间"] = data_dt.Rows[i]["DANZHENGFANGXINGTIME"];
                dr["单证放行人"] = data_dt.Rows[i]["DANZHENGFANGXINGUSERNAME"];
                dr["实物放行人"] = data_dt.Rows[i]["SHIWUFANGXINGUSERNAME"];
                dr["关务接单时间"] = data_dt.Rows[i]["GUANWUJIESHOUTIME"];
                dr["进出口类别"] = data_dt.Rows[i]["INOUTTYPE"];
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
            dt.Columns.Add("通关单张数");
            dt.Columns.Add("通关单号");
            dt.Columns.Add("合同/发票号");
            dt.Columns.Add("报检时间");
            dt.Columns.Add("报检人");
            dt.Columns.Add("放行时间");
            dt.Columns.Add("放行人");
            dt.Columns.Add("报关状态");
            dt.Columns.Add("报检状态");
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
                dr["业务编号"] = data_dt.Rows[i]["CODE"];
                //dr["报检单号"] = data_dt.Rows[i]["text"];
                dr["FWO订单号"] = data_dt.Rows[i]["FWONO"];
                dr["FO号"] = data_dt.Rows[i]["FOONO"];
                dr["总单号"] = data_dt.Rows[i]["TOTALNO"];
                dr["分单号"] = data_dt.Rows[i]["DIVIDENO"];
                //dr["万达号"] = data_dt.Rows[i]["text"];
                dr["载货清单号"] = data_dt.Rows[i]["MANIFEST"];
                dr["木质包装"] = data_dt.Rows[i]["WOODPACKINGID"];
                //dr["通关单标志"] = data_dt.Rows[i]["text"];
                //dr["通关单张数"] = data_dt.Rows[i]["text"];
                dr["通关单号"] = data_dt.Rows[i]["CLEARANCENO"];
                dr["合同/发票号"] = data_dt.Rows[i]["CONTRACTNO"];
                dr["报检时间"] = data_dt.Rows[i]["BAOJIANTIME"];
                //dr["报检人"] = data_dt.Rows[i]["text"];
                dr["放行时间"] = data_dt.Rows[i]["BAOJIANFANGXINGTIME"];
                dr["放行人"] = data_dt.Rows[i]["BAOJIANFANGXINGUSERNAME"];
                dr["报关状态"] = data_dt.Rows[i]["DECLSTATUS"];
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
                //dr["报关单号"] = data_dt.Rows[i]["ASSOCIATEPEDECLNO"];
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
                dr["报检状态"] = data_dt.Rows[i]["INSPSTATUS"];
                dr["报关方式"] = data_dt.Rows[i]["DECLWAY"];
                dr["关区代码"] = data_dt.Rows[i]["CUSTOMDISTRICTCODE"];
                //dr["海关通关状态"] = data_dt.Rows[i]["text"];
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
            JObject OrderArray = JsonConvert.DeserializeObject<JObject>(result);                //json转换为数组
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
                string type = Request.Form["type"];
                if (type == "02")
                {
                    IFS.ZSBJ_ABNO(ID);
                }
                else
                {
                    IFS.ZSBG_ABNO(ID);
                }

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
            string datetime = Request.Form["date"];
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            string sql = "update list_order set ";
            if (type != "")
            {
                string time = type + "TIME";
                string userid = type + "USERID";
                string username = type + "USERNAME";
                sql += time + "  = to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss') ," + username + " ='" + jo.Value<string>("REALNAME") + "', " + userid + " =  " + jo.Value<string>("ID");
            }
            sql += " where ID =" + ID;
            int res = DBMgr.ExecuteNonQuery(sql);

            if (res == 1)
            {
                IFS.Callback_TM(type, ID);
                datetime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                return Json(new { Success = true, datetime = datetime, name = jo.Value<string>("REALNAME") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, sql = sql }, JsonRequestBehavior.AllowGet);
            }

        }
        //报关批量更新
        public ActionResult BatchUpdate()
        {
            ViewData["ids"] = Request["ids"];
            JObject jo = Extension.Get_UserInfo(HttpContext.User.Identity.Name);    //获取会员信息
            ViewData["USERNAME"] =jo.Value<string>("REALNAME");
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
            string ids = Request.Form["ids"];
            string bname = Request["bname"];
            string dname = Request["dname"];
            string sname = Request["sname"];
            string sql = "update list_order set ";
            if (Request.Form["BAORUHAIGUANTIME"] != "")
            {
                sql += "  BAORUHAIGUANTIME =  to_date('" + Request.Form["BAORUHAIGUANTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["DANZHENGFANGXINGTIME"] != "")
            {
                sql += "  DANZHENGFANGXINGTIME =  to_date('" + Request.Form["DANZHENGFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["SHIWUFANGXINGTIME"] != "")
            {
                sql += "  SHIWUFANGXINGTIME =  to_date('" + Request.Form["SHIWUFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (bname != "" && bname != null)
            {
                sql += "  BAORUHAIGUANUSERNAME =  '" + bname + "',";
            }
            if (dname != "" && dname != null)
            {
                sql += "  DANZHENGFANGXINGUSERNAME =  '" + dname + "',";
            }
            if (sname != "" && sname != null)
            {
                sql += "  SHIWUFANGXINGUSERNAME =  '" + sname + "',";
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
        //报检批量更新保存
        public ActionResult SavebjUpdateData(FormCollection form)
        {
            string ids = Request.Form["ids"];
            string bjname = Request["bjname"];
            string bjfxname = Request["bjfxname"];
            string xzname = Request["xzname"];
            string cyname = Request["cyname"];
            string cyfxname = Request["cyfxname"];
            string sql = "update list_order set ";
            if (Request.Form["BAOJIANTIME"] != "")
            {
                sql += "  BAOJIANTIME =  to_date('" + Request.Form["BAOJIANTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["BAOJIANFANGXINGTIME"] != "")
            {
                sql += "  BAOJIANFANGXINGTIME =  to_date('" + Request.Form["BAOJIANFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["XUNZHENGTIME"] != "")
            {
                sql += "  XUNZHENGTIME =  to_date('" + Request.Form["XUNZHENGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["CHAYANZHILINGXIAFATIME"] != "")
            {
                sql += "  CHAYANZHILINGXIAFATIME =  to_date('" + Request.Form["CHAYANZHILINGXIAFATIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (Request.Form["CHAYANFANGXINGTIME"] != "")
            {
                sql += "  CHAYANFANGXINGTIME =  to_date('" + Request.Form["CHAYANFANGXINGTIME"] + "','yyyy-MM-dd hh24:mi:ss'),";
            }
            if (bjname != "" && bjname != null)
            {
                sql += "  BAOJIANUSERNAME =  '" + bjname + "',";
            }
            if (bjfxname != "" && bjfxname != null)
            {
                sql += "  BAOJIANFANGXINGUSERNAME =  '" + bjfxname + "',";
            }
            if (xzname != "" && xzname != null)
            {
                sql += "  XUNZHENGUSERNAME =  '" + xzname + "',";
            }
            if (cyname != "" && cyname != null)
            {
                sql += "  CHAYANZHILINGXIAFAUSERNAME =  '" + cyname + "',";
            }
            if (cyfxname != "" && cyfxname != null)
            {
                sql += "  CHAYANFANGXINGUSERNAME =  '" + cyfxname + "',";
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