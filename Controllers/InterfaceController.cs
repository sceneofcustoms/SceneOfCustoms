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
    public class InterfaceController : Controller
    {

        //测试接口  单证
        public ActionResult test()
        {
            ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
            ServiceReference1.OrderEn dzOrder = new ServiceReference1.OrderEn();

            dzOrder.ARRIVEDNO = "1";
            dzOrder.REPNO = "1";
            //dzOrder.BUSIUNITCODE = "1";

            List<ServiceReference1.OrderEn> orderList = new List<ServiceReference1.OrderEn>();

            orderList.Add(dzOrder);
            string text = danzheng.SendOrderData(orderList.ToArray());
            ViewData["text"] = text;
            return View();
        }

        [HttpPost]
        //测试tm过来数据
        public string testTm()
        {
            List<OrderEn> ld = new List<OrderEn>();
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            OrderEn obj;
            string ids = Request.Form["ids"];
            string sql = "select * from list_sapfoo where id in(" + ids + ") order by id desc";
            DataTable dt = DBMgr.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                obj = new OrderEn();
                obj.BUSITYPE = dt.Rows[i]["BUSITYPE"] + "";
                obj.CODE = dt.Rows[i]["FWONO"] + "";
                obj.FOONO = dt.Rows[i]["FOONO"] + "";
                obj.ORDERCODE = dt.Rows[i]["CODE"] + "";
                obj.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
                obj.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
                obj.GOODSNUM = dt.Rows[i]["GOODSNUM"] + "";
                obj.GOODSWEIGHT = dt.Rows[i]["GOODSWEIGHT"] + "";
                obj.PACKKIND = dt.Rows[i]["PACKKIND"] + "";
                obj.REPWAYID = dt.Rows[i]["REPWAYID"] + "";
                obj.DECLWAY = dt.Rows[i]["DECLWAY"] + "";
                obj.TRADEWAYCODES = dt.Rows[i]["TRADEWAYCODES"] + "";
                obj.CUSNO = dt.Rows[i]["CUSNO"] + "";
                obj.CUSTOMDISTRICTCODE = dt.Rows[i]["CUSTOMDISTRICTCODE"] + "";
                obj.PORTCODE = dt.Rows[i]["PORTCODE"] + "";
                obj.PRICEIMPACT = dt.Rows[i]["PRICEIMPACT"] + "";
                obj.PAYPOYALTIES = dt.Rows[i]["PAYPOYALTIES"] + "";
                //obj.SFGOODSUNIT = dt.Rows[i]["SFGOODSUNIT"] + "";

                obj.FGOODSUNIT = dt.Rows[i]["FGOODSUNIT"] + "";
                obj.SGOODSUNIT = dt.Rows[i]["SGOODSUNIT"] + "";
                obj.ALLOWDECLARE = dt.Rows[i]["ALLOWDECLARE"] + "";

                obj.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
                obj.CREATEUSERNAME = dt.Rows[i]["CREATEUSERNAME"] + "";
                obj.CREATETIME = DateTime.Now.ToLocalTime().ToString();
                obj.ARRIVEDNO = dt.Rows[i]["ARRIVEDNO"] + "";
                obj.CHECKEDGOODSNUM = dt.Rows[i]["CHECKEDGOODSNUM"] + "";
                obj.CHECKEDWEIGHT = dt.Rows[i]["CHECKEDWEIGHT"] + "";
                obj.ENTRUSTTYPEID = dt.Rows[i]["ENTRUSTTYPEID"] + "";
                obj.GOODSXT = dt.Rows[i]["GOODSXT"] + "";
                obj.BUSIUNITNAME = dt.Rows[i]["BUSIUNITNAME"] + "";
                obj.GOODSTYPEID = dt.Rows[i]["GOODSTYPEID"] + "";
                obj.LADINGBILLNO = dt.Rows[i]["LADINGBILLNO"] + "";
                obj.ISPREDECLARE = dt.Rows[i]["ISPREDECLARE"] + "";
                obj.ENTRUSTREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
                obj.CONTRACTNO = dt.Rows[i]["CONTRACTNO"] + "";
                obj.FIRSTLADINGBILLNO = dt.Rows[i]["FIRSTLADINGBILLNO"] + "";
                obj.SECONDLADINGBILLNO = dt.Rows[i]["SECONDLADINGBILLNO"] + "";
                obj.MANIFEST = dt.Rows[i]["MANIFEST"] + "";
                obj.WOODPACKINGID = dt.Rows[i]["WOODPACKINGID"] + "";
                obj.WEIGHTCHECK = dt.Rows[i]["WEIGHTCHECK"] + "";
                obj.ISWEIGHTCHECK = dt.Rows[i]["ISCHECKEDWEIGHT"] + "";
                obj.SHIPNAME = dt.Rows[i]["SHIPNAME"] + "";
                obj.FILGHTNO = dt.Rows[i]["FILGHTNO"] + "";
                obj.INSPUNITNAME = dt.Rows[i]["INSPUNITCODE"] + "";
                obj.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
                obj.INVOICENO = dt.Rows[i]["INVOICENO"] + "";
                obj.SPECIALRELATIONSHIP = dt.Rows[i]["SPECIALRELATIONSHIP"] + "";
                ld.Add(obj);
            }
            if (ld.Count > 0)
            {

                MSList = IFS.CheckData(ld);
                int Order_Res = IFS.InsertOrder(ld);

                if (MSList.Count <= 0)
                {
                    if (Order_Res == 1)
                    {
                        MSList.Add(IFS.set_MObj("S", "保存成功"));
                    }
                    else
                    {
                        MSList.Add(IFS.set_MObj("E", "保存失败"));
                    }
                }

            }
            else
            {
                MSList.Add(IFS.set_MObj("E", "没有指令"));
            }

            IFS.save_log(MSList, ld[0].CODE, "1");

            return "1";
        }


        //转换单证数据
        //private ServiceReference1.OrderEn ZDOrderData(List<OrderEn> ListOrder)
        //{
        //    string sql = "";
        //    string name = "";
        //    DataTable dt;
        //    ServiceReference1.OrderEn DZOrder = new ServiceReference1.OrderEn();

        //    DZOrder.CUSNO = ListOrder[0].CODE; //企业编号
        //    DZOrder.REPNO = ""; //申报单位编号   --
        //    DZOrder.ENTRUSTTYPE =  GetENTRUSTTYPEID(ListOrder, ListOrder[0].BUSITYPE); //委托类型代码

        //    //委托类型名称
        //    if (DZOrder.ENTRUSTTYPE == "01")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报关";
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "02")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报检";
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "03")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报关报检";
        //    }

        //    //业务类型代码
        //    DZOrder.BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);
        //    //业务类型名称 --
        //    DZOrder.BUSITYPENAME = "";

        //    //申报方式代码
        //    sql = "select CODE, NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + ListOrder[0].REPWAYID + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.REPWAYID = dt.Rows[0]["CODE"] + "";
        //        //申报方式名称 --
        //        DZOrder.REPWAYNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //申报关区代码
        //    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + ListOrder[0].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.CUSTOMAREACODE = dt.Rows[0]["CODE"] + "";
        //        //申报关区代码 --
        //        DZOrder.CUSTOMAREANAME = dt.Rows[0]["NAME"] + "";
        //    }
        //    //报关方式代码
        //    sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + ListOrder[0].DECLWAY + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.DECLWAY = dt.Rows[0]["CODE"] + "";
        //        //报关方式名称 --
        //        DZOrder.DECLWAYNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //经营单位代码
        //    name = ListOrder[0].BUSIUNITNAME.Remove(ListOrder[0].BUSIUNITNAME.Length - 10, 10);
        //    sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.BUSIUNITCODE = dt.Rows[0]["CODE"] + "";
        //        //经营单位名称
        //        DZOrder.BUSIUNITNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //经营单位社会号
        //    DZOrder.BUSIUNITNUM = "";

        //    //件数
        //    DZOrder.GOODSNUM = Int32.Parse(ListOrder[0].GOODSNUM);

        //    //毛重
        //    DZOrder.GOODSGW = decimal.Parse(ListOrder[0].GOODSWEIGHT);


        //    //净重
        //    //if (string.IsNullOrEmpty(ListOrder[0].CHECKEDWEIGHT))
        //    //{
        //    //    DZOrder.GOODSNW = decimal.Parse(ListOrder[0].CHECKEDWEIGHT);

        //    //}

        //    //包装种类名称
        //    DZOrder.PACKKINDNAME = ListOrder[0].PACKKIND;

        //    //订单要求 --
        //    DZOrder.ORDERREQUEST = ListOrder[0].ENTRUSTREQUEST;

        //    //委托单位代码
        //    DZOrder.CUSTOMERCODE = "3223980001";
        //    //委托单位代码
        //    DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";


        //    //申报单位  报关 报检

        //    if (DZOrder.ENTRUSTTYPE == "01")
        //    {

        //        DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "02")
        //    {
        //        DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //        DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "03")
        //    {
        //        DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //        DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //    }




        //    //总单号
        //    DZOrder.TOTALNO = ListOrder[0].TOTALNO;

        //    //分单号
        //    DZOrder.TOTALNO = ListOrder[0].TOTALNO;

        //    //转关预录入号
        //    DZOrder.TURNPRENO = ListOrder[0].TURNPRENO;

        //    //进出口岸
        //    DZOrder.PORTCODE = ListOrder[0].PORTCODE;

        //    //委托时间
        //    DZOrder.SUBMITTIME = DateTime.Now;

        //    //委托人员
        //    DZOrder.SUBMITUSERNAME = ListOrder[0].CREATEUSERNAME;

        //    //运抵编号
        //    DZOrder.ARRIVEDNO = ListOrder[0].ARRIVEDNO;

        //    //货物类型
        //    DZOrder.GOODSTYPEID = ListOrder[0].GOODSTYPEID;

        //    //海关提单号 二程提单号
        //    DZOrder.SECONDLADINGBILLNO = ListOrder[0].SECONDLADINGBILLNO;

        //    //国检提单号 一程提单号
        //    DZOrder.FIRSTLADINGBILLNO = ListOrder[0].FIRSTLADINGBILLNO;

        //    //载货清单号
        //    DZOrder.MANIFEST = ListOrder[0].MANIFEST;

        //    //载货清单号
        //    DZOrder.MANIFEST = ListOrder[0].MANIFEST;

        //    //木质包装
        //    DZOrder.WOODPACKINGID = ListOrder[0].WOODPACKINGID;



        //    if (ListOrder[0].WEIGHTCHECK == "")
        //    {
        //        ListOrder[0].WEIGHTCHECK = "0";
        //    }
        //    else
        //    {
        //        ListOrder[0].WEIGHTCHECK = "1";
        //    }


        //    if (ListOrder[0].ISWEIGHTCHECK == "")
        //    {
        //        ListOrder[0].ISWEIGHTCHECK = "0";
        //    }
        //    else
        //    {
        //        ListOrder[0].ISWEIGHTCHECK = "1";
        //    }

        //    //是否需要重量确认
        //    DZOrder.WEIGHTCHECK = Int32.Parse(ListOrder[0].WEIGHTCHECK);

        //    //重量确认
        //    DZOrder.ISWEIGHTCHECK = Int32.Parse(ListOrder[0].ISWEIGHTCHECK);

        //    //船名
        //    DZOrder.SHIPNAME = ListOrder[0].SHIPNAME;

        //    //航次
        //    DZOrder.FILGHTNO = ListOrder[0].FILGHTNO;

        //    //通关单号
        //    //DZOrder.CLEARANCENO = ListOrder[0].CLEARANCENO;

        //    return DZOrder;
        //}






    }
}