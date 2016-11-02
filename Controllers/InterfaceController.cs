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


            bd.SyncDataFromSapSoapClient xc = new bd.SyncDataFromSapSoapClient();
            bd.OrderEn lcorder = new bd.OrderEn();
            List<bd.OrderEn> list = new List<bd.OrderEn>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lcorder = new bd.OrderEn();
                lcorder.BUSITYPE = dt.Rows[i]["BUSITYPE"] + "";
                lcorder.CODE = dt.Rows[i]["FWONO"] + "";
                lcorder.FOONO = dt.Rows[i]["FOONO"] + "";
                lcorder.ORDERCODE = dt.Rows[i]["CODE"] + "";
                lcorder.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
                lcorder.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
                lcorder.GOODSNUM = dt.Rows[i]["GOODSNUM"] + "";
                lcorder.GOODSWEIGHT = dt.Rows[i]["GOODSWEIGHT"] + "";
                lcorder.PACKKIND = dt.Rows[i]["PACKKIND"] + "";
                lcorder.REPWAYID = dt.Rows[i]["REPWAYID"] + "";
                lcorder.DECLWAY = dt.Rows[i]["DECLWAY"] + "";
                lcorder.TRADEWAYCODES = dt.Rows[i]["TRADEWAYCODES"] + "";
                lcorder.CUSNO = dt.Rows[i]["CUSNO"] + "";
                lcorder.CUSTOMDISTRICTCODE = dt.Rows[i]["CUSTOMDISTRICTCODE"] + "";
                lcorder.PORTCODE = dt.Rows[i]["PORTCODE"] + "";
                lcorder.PRICEIMPACT = dt.Rows[i]["PRICEIMPACT"] + "";
                lcorder.PAYPOYALTIES = dt.Rows[i]["PAYPOYALTIES"] + "";
                //lcorder.SFGOODSUNIT = dt.Rows[i]["SFGOODSUNIT"] + "";

                lcorder.FGOODSUNIT = dt.Rows[i]["FGOODSUNIT"] + "";
                lcorder.SGOODSUNIT = dt.Rows[i]["SGOODSUNIT"] + "";
                lcorder.ALLOWDECLARE = dt.Rows[i]["ALLOWDECLARE"] + "";

                lcorder.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
                lcorder.CREATEUSERNAME = dt.Rows[i]["CREATEUSERNAME"] + "";
                lcorder.CREATETIME = dt.Rows[i]["CREATETIME"] + "";
                lcorder.ARRIVEDNO = dt.Rows[i]["ARRIVEDNO"] + "";
                lcorder.CHECKEDGOODSNUM = dt.Rows[i]["CHECKEDGOODSNUM"] + "";
                lcorder.CHECKEDWEIGHT = dt.Rows[i]["CHECKEDWEIGHT"] + "";
                lcorder.ENTRUSTTYPEID = dt.Rows[i]["ENTRUSTTYPEID"] + "";
                lcorder.GOODSXT = dt.Rows[i]["GOODSXT"] + "";
                lcorder.BUSIUNITNAME = dt.Rows[i]["BUSIUNITNAME"] + "";
                lcorder.GOODSTYPEID = dt.Rows[i]["GOODSTYPEID"] + "";
                lcorder.LADINGBILLNO = dt.Rows[i]["LADINGBILLNO"] + "";
                lcorder.ISPREDECLARE = dt.Rows[i]["ISPREDECLARE"] + "";
                lcorder.ENTRUSTREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
                lcorder.CONTRACTNO = dt.Rows[i]["CONTRACTNO"] + "";
                lcorder.FIRSTLADINGBILLNO = dt.Rows[i]["FIRSTLADINGBILLNO"] + "";
                lcorder.SECONDLADINGBILLNO = dt.Rows[i]["SECONDLADINGBILLNO"] + "";
                lcorder.MANIFEST = dt.Rows[i]["MANIFEST"] + "";
                lcorder.WOODPACKINGID = dt.Rows[i]["WOODPACKINGID"] + "";
                lcorder.WEIGHTCHECK = dt.Rows[i]["WEIGHTCHECK"] + "";
                lcorder.ISWEIGHTCHECK = dt.Rows[i]["ISCHECKEDWEIGHT"] + "";
                lcorder.SHIPNAME = dt.Rows[i]["SHIPNAME"] + "";
                lcorder.FILGHTNO = dt.Rows[i]["FILGHTNO"] + "";
                lcorder.INSPUNITNAME = dt.Rows[i]["INSPUNITCODE"] + "";
                lcorder.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
                lcorder.INVOICENO = dt.Rows[i]["INVOICENO"] + "";
                lcorder.SPECIALRELATIONSHIP = dt.Rows[i]["SPECIALRELATIONSHIP"] + "";

                List<bd.Declcontainertruck> dlist = new List<bd.Declcontainertruck>();
                bd.Declcontainertruck d = new bd.Declcontainertruck();
                d.CDCARNAME = "1";
                d.CONTAINERNO = "2";
                d.CONTAINERTYPE = "3";

                dlist.Add(d);
                bd.Declcontainertruck d1 = new bd.Declcontainertruck();
                d1.CDCARNAME = "1";
                d1.CONTAINERNO = "2";
                d1.CONTAINERTYPE = "3";

                dlist.Add(d1);

                lcorder.Declcontainertruck = dlist.ToArray();
                list.Add(lcorder);
            }

            bd.Msgobj[] mo = xc.SyncData(list.ToArray());

            if (list.Count > 0)
            {

                //MSList = IFS.CheckData(ld);



                //    if (MSList.Count <= 0)
                //    {
                //        //int Order_Res = IFS.XCOrderData(ld);




                //        //ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
                //        //ServiceReference1.OrderEn DZOrder;
                //        //List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();


                //        //if (Order_Res == 1)
                //        //{
                //        //    MSList.Add(IFS.set_MObj("S", "保存成功"));
                //        //    IFS.SaveDZOrder(ld[0].CODE);

                //        //}
                //        //else
                //        //{
                //        //    MSList.Add(IFS.set_MObj("E", "保存失败"));
                //        //}
                //    }

            }
            //else
            //{
            //    MSList.Add(IFS.set_MObj("E", "没有指令"));
            //}

            //IFS.save_log(MSList, list[0].CODE, "1");

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