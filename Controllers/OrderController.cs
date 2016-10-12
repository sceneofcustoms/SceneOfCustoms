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

    //订单方法

    public class OrderController : Controller
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


        //测试tm
        public ActionResult testTm()
        {
            List<OrderEn> ld = new List<OrderEn>();
            Msgobj MO = new Msgobj();
            OrderEn obj = new OrderEn();

            string sql = "select * from list_sapfoo  order by id desc";
            DataTable dt = DBMgr.GetDataTable(sql);


            obj.BUSITYPE = dt.Rows[0]["BUSITYPE"] + "";
            obj.CODE = dt.Rows[0]["FWONO"] + "";
            obj.FOONO = dt.Rows[0]["FOONO"] + "";
            obj.TOTALNO = dt.Rows[0]["TOTALNO"] + "";
            obj.DIVIDENO = dt.Rows[0]["DIVIDENO"] + "";
            obj.GOODSNUM = dt.Rows[0]["GOODSNUM"] + "";
            obj.GOODSWEIGHT = dt.Rows[0]["GOODSWEIGHT"] + "";
            obj.PACKKIND = dt.Rows[0]["PACKKIND"] + "";
            obj.REPWAYID = dt.Rows[0]["REPWAYID"] + "";
            obj.DECLWAY = dt.Rows[0]["DECLWAY"] + "";
            obj.TRADEWAYCODES = dt.Rows[0]["TRADEWAYCODES"] + "";
            obj.CUSNO = dt.Rows[0]["CUSNO"] + "";
            obj.CUSTOMDISTRICTCODE = dt.Rows[0]["CUSTOMDISTRICTCODE"] + "";
            obj.PORTCODE = dt.Rows[0]["PORTCODE"] + "";
            obj.PRICEIMPACT = dt.Rows[0]["PRICEIMPACT"] + "";
            obj.PAYPOYALTIES = dt.Rows[0]["PAYPOYALTIES"] + "";
            obj.SFGOODSUNIT = dt.Rows[0]["SFGOODSUNIT"] + "";
            obj.REPUNITCODE = dt.Rows[0]["REPUNITCODE"] + "";
            obj.CREATEUSERNAME = dt.Rows[0]["CREATEUSERNAME"] + "";
            obj.CREATETIME = DateTime.Now.ToLocalTime().ToString();
            obj.ARRIVEDNO = dt.Rows[0]["ARRIVEDNO"] + "";
            obj.CHECKEDGOODSNUM = dt.Rows[0]["CHECKEDGOODSNUM"] + "";
            obj.CHECKEDWEIGHT = dt.Rows[0]["CHECKEDWEIGHT"] + "";
            obj.ENTRUSTTYPEID = dt.Rows[0]["ENTRUSTTYPEID"] + "";
            obj.GOODSXT = dt.Rows[0]["GOODSXT"] + "";
            obj.BUSIUNITNAME = dt.Rows[0]["BUSIUNITNAME"] + "";
            obj.GOODSTYPEID = dt.Rows[0]["GOODSTYPEID"] + "";
            obj.LADINGBILLNO = dt.Rows[0]["LADINGBILLNO"] + "";
            obj.ISPREDECLARE = dt.Rows[0]["ISPREDECLARE"] + "";
            obj.ENTRUSTREQUEST = dt.Rows[0]["ENTRUSTREQUEST"] + "";
            obj.CONTRACTNO = dt.Rows[0]["CONTRACTNO"] + "";
            obj.FIRSTLADINGBILLNO = dt.Rows[0]["FIRSTLADINGBILLNO"] + "";
            obj.SECONDLADINGBILLNO = dt.Rows[0]["SECONDLADINGBILLNO"] + "";
            obj.MANIFEST = dt.Rows[0]["MANIFEST"] + "";
            obj.WOODPACKINGID = dt.Rows[0]["WOODPACKINGID"] + "";
            obj.WEIGHTCHECK = dt.Rows[0]["WEIGHTCHECK"] + "";
            obj.ISWEIGHTCHECK = dt.Rows[0]["ISCHECKEDWEIGHT"] + "";
            obj.SHIPNAME = dt.Rows[0]["SHIPNAME"] + "";
            obj.FILGHTNO = dt.Rows[0]["FILGHTNO"] + "";
            obj.INSPUNITNAME = dt.Rows[0]["INSPUNITCODE"] + "";
            obj.TURNPRENO = dt.Rows[0]["TURNPRENO"] + "";
            obj.INVOICENO = dt.Rows[0]["INVOICENO"] + "";
            obj.SPECIALRELATIONSHIP = dt.Rows[0]["SPECIALRELATIONSHIP"] + "";
            
            //obj.BUSITYPE = "飞力达FWO-海运出口-整箱";
            //obj.CODE = "00000110000000001169";
            //obj.FOONO = "SOBG00000000800000000541";
            //obj.TOTALNO = "4";
            //obj.DIVIDENO = "5";
            //obj.GOODSNUM = "5000";
            //obj.GOODSWEIGHT = "6000.0";
            //obj.PACKKIND = "袋";
            //obj.REPWAYID = "进口集报";
            //obj.DECLWAY = "通关无纸化";
            //obj.TRADEWAYCODES = "一般贸易/外资设备物品";
            //obj.CUSNO = "12";
            //obj.CUSTOMDISTRICTCODE = "昆山海关";
            //obj.PORTCODE = "昆山海关";
            //obj.PRICEIMPACT = "X";
            //obj.PAYPOYALTIES = "X";
            //obj.SFGOODSUNIT = "纬新资通(昆山)有限公司3223640063";
            //obj.REPUNITCODE = "江苏飞力达国际物流股份有限公司营运中心3223980002";
            //obj.CREATEUSERNAME = "洪家伟";
            //obj.CREATETIME = DateTime.Now.ToLocalTime().ToString();
            //obj.ARRIVEDNO = "FOO11113333333777";
            //obj.CHECKEDGOODSNUM = "5000";
            //obj.CHECKEDWEIGHT = "6000.0";
            //obj.ENTRUSTTYPEID = "";
            //obj.GOODSXT = "普通货";
            //obj.BUSIUNITNAME = "纬新资通(昆山)有限公司3223640063";
            //obj.GOODSTYPEID = "FCL（整箱装载）";
            //obj.LADINGBILLNO = "FL161000001";
            //obj.ISPREDECLARE = "X";
            //obj.ENTRUSTREQUEST = "FOO9994449999";
            //obj.CONTRACTNO = "FOO444555666";
            //obj.FIRSTLADINGBILLNO = "11111C";
            //obj.SECONDLADINGBILLNO = "11111C";
            //obj.MANIFEST = "66633322222";
            //obj.WOODPACKINGID = "非木";
            //obj.WEIGHTCHECK = "X";
            //obj.ISWEIGHTCHECK = "X";
            //obj.SHIPNAME = "COSCO KOREA";
            //obj.FILGHTNO = "S334";
            //obj.INSPUNITNAME = "江苏飞力达国际物流股份有限公司营运中心3223980002";
            //obj.TURNPRENO = "40";
            //obj.INVOICENO = "11111111111222222222";
            //obj.SPECIALRELATIONSHIP = "X";
            ld.Add(obj);
            IList<Msgobj> MSList = CheckData(ld);
            int Order_Res = InsertOrder(ld);

            if (MSList.Count <= 0)
            {
                if (ld.Count > 0)
                {
                    if (Order_Res == 1)
                    {
                        MO.MSG_ID = 1;
                        MO.MSG_TYPE = "S";
                        MO.MSG_TXT = "保存成功";
                        MSList.Add(MO);
                    }
                    else
                    {
                        MO.MSG_ID = 1;
                        MO.MSG_TYPE = "E";
                        MO.MSG_TXT = "保存失败";
                        MSList.Add(MO);
                    }

                }
                else
                {
                    MO.MSG_ID = 1;
                    MO.MSG_TYPE = "E";
                    MO.MSG_TXT = "数据不可为空";
                    MSList.Add(MO);
                }
            }

            return View();
        }

        //转换单证数据
        private ServiceReference1.OrderEn ZDOrderData(List<OrderEn> ListOrder)
        {
            string sql = "";
            string name = "";
            DataTable dt;
            ServiceReference1.OrderEn DZOrder = new ServiceReference1.OrderEn();

            DZOrder.CUSNO = ListOrder[0].CUSNO; //企业编号
            DZOrder.REPNO = ""; //申报单位编号   --
            DZOrder.ENTRUSTTYPE = GetENTRUSTTYPEID(ListOrder, ListOrder[0].BUSITYPE); //委托类型代码

            //委托类型名称
            if (DZOrder.ENTRUSTTYPE == "01")
            {
                DZOrder.ENTRUSTTYPENAME = "报关";
            }
            else if (DZOrder.ENTRUSTTYPE == "02")
            {
                DZOrder.ENTRUSTTYPENAME = "报检";
            }
            else if (DZOrder.ENTRUSTTYPE == "03")
            {
                DZOrder.ENTRUSTTYPENAME = "报关报检";
            }

            //业务类型代码
            DZOrder.BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);
            //业务类型名称 --
            DZOrder.BUSITYPENAME = "";

            //申报方式代码
            sql = "select CODE, NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + ListOrder[0].REPWAYID + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.REPWAYID = dt.Rows[0]["CODE"] + "";
            //申报方式名称 --
            DZOrder.REPWAYNAME = dt.Rows[0]["NAME"] + "";

            //申报关区代码
            sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + ListOrder[0].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.CUSTOMAREACODE = dt.Rows[0]["CODE"] + "";
            //申报关区代码 --
            DZOrder.CUSTOMAREANAME = dt.Rows[0]["NAME"] + "";

            //报关方式代码
            sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + ListOrder[0].DECLWAY + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.DECLWAY = dt.Rows[0]["CODE"] + "";
            //报关方式名称 --
            DZOrder.DECLWAYNAME = dt.Rows[0]["NAME"] + "";

            //经营单位代码
            name = ListOrder[0].BUSIUNITNAME.Remove(ListOrder[0].BUSIUNITNAME.Length - 10, 10);
            sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.BUSIUNITCODE = dt.Rows[0]["CODE"] + "";
            //经营单位名称
            DZOrder.BUSIUNITNAME = dt.Rows[0]["NAME"] + "";

            //经营单位社会号
            DZOrder.BUSIUNITNUM = "";

            //件数
            DZOrder.GOODSNUM = Int32.Parse(ListOrder[0].GOODSNUM);

            //毛重
            DZOrder.GOODSGW = decimal.Parse(ListOrder[0].GOODSWEIGHT);

            //净重
            DZOrder.GOODSNW = decimal.Parse(ListOrder[0].CHECKEDWEIGHT);

            //包装种类名称
            DZOrder.PACKKINDNAME = ListOrder[0].PACKKIND;

            //订单要求 --
            DZOrder.ORDERREQUEST = ListOrder[0].ENTRUSTREQUEST;

            //申报单位  报关 报检

            if (DZOrder.ENTRUSTTYPE == "01")
            {
                DZOrder.DECLREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
                DZOrder.DECLREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "02")
            {
                DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
                DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "03")
            {
                DZOrder.DECLREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
                DZOrder.DECLREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
                DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
                DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
            }

            //总单号
            DZOrder.TOTALNO = ListOrder[0].TOTALNO;

            //分单号
            DZOrder.TOTALNO = ListOrder[0].TOTALNO;

            //转关预录入号
            DZOrder.TURNPRENO = ListOrder[0].TURNPRENO;

            //进出口岸
            DZOrder.PORTCODE = ListOrder[0].PORTCODE;

            //委托时间
            DZOrder.SUBMITTIME = DateTime.Now;

            //委托人员
            DZOrder.SUBMITUSERNAME = ListOrder[0].CREATEUSERNAME;

            //运抵编号
            DZOrder.ARRIVEDNO = ListOrder[0].ARRIVEDNO;

            //货物类型
            DZOrder.GOODSTYPEID = ListOrder[0].GOODSTYPEID;

            //海关提单号 二程提单号
            DZOrder.SECONDLADINGBILLNO = ListOrder[0].SECONDLADINGBILLNO;

            //国检提单号 一程提单号
            DZOrder.FIRSTLADINGBILLNO = ListOrder[0].FIRSTLADINGBILLNO;

            //载货清单号
            DZOrder.MANIFEST = ListOrder[0].MANIFEST;

            //载货清单号
            DZOrder.MANIFEST = ListOrder[0].MANIFEST;

            //木质包装
            DZOrder.WOODPACKINGID = ListOrder[0].WOODPACKINGID;



            if (ListOrder[0].WEIGHTCHECK == "")
            {
                ListOrder[0].WEIGHTCHECK = "0";
            }
            else
            {
                ListOrder[0].WEIGHTCHECK = "1";
            }


            if (ListOrder[0].ISWEIGHTCHECK == "")
            {
                ListOrder[0].ISWEIGHTCHECK = "0";
            }
            else
            {
                ListOrder[0].ISWEIGHTCHECK = "1";
            }

            //是否需要重量确认
            DZOrder.WEIGHTCHECK = Int32.Parse(ListOrder[0].WEIGHTCHECK);

            //重量确认
            DZOrder.ISWEIGHTCHECK = Int32.Parse(ListOrder[0].ISWEIGHTCHECK);

            //船名
            DZOrder.SHIPNAME = ListOrder[0].SHIPNAME;

            //航次
            DZOrder.FILGHTNO = ListOrder[0].FILGHTNO;

            //通关单号
            //DZOrder.CLEARANCENO = ListOrder[0].CLEARANCENO;

            return DZOrder;
        }

        //保存现场数据
        private int XCOrderData(List<OrderEn> o)
        {
            string sql = "";
            DateTime dt = DateTime.Now;
            if (o[0].SPECIALRELATIONSHIP == "")
            {
                o[0].SPECIALRELATIONSHIP = "0";
            }
            else
            {
                o[0].SPECIALRELATIONSHIP = "1";
            }

            if (o[0].PRICEIMPACT == "")
            {
                o[0].PRICEIMPACT = "0";
            }
            else
            {
                o[0].PRICEIMPACT = "1";
            }

            if (o[0].PAYPOYALTIES == "")
            {
                o[0].PAYPOYALTIES = "0";
            }
            else
            {
                o[0].PAYPOYALTIES = "1";
            }

            if (o[0].ISPREDECLARE == "")
            {
                o[0].ISPREDECLARE = "0";
            }
            else
            {
                o[0].ISPREDECLARE = "1";
            }

            if (o[0].WEIGHTCHECK == "")
            {
                o[0].WEIGHTCHECK = "0";
            }
            else
            {
                o[0].WEIGHTCHECK = "1";
            }


            if (o[0].ISWEIGHTCHECK == "")
            {
                o[0].ISWEIGHTCHECK = "0";
            }
            else
            {
                o[0].ISWEIGHTCHECK = "1";
            }

            o[0].ENTRUSTTYPEID = GetENTRUSTTYPEID(o, o[0].BUSITYPE); //委托类型代码
            o[0].BUSITYPE = JudgeBusiType(o[0].BUSITYPE, o[0].ENTRUSTTYPEID);
            sql = @"insert into List_Order(
                      ID,
                      BUSITYPE,FWONO,FOONO,TOTALNO,
                      DIVIDENO,GOODSNUM,GOODSWEIGHT,SFGOODSUNIT,
                      PACKKIND, REPWAYID,DECLWAY,TRADEWAYCODES,
                      CUSNO,CUSTOMDISTRICTCODE,PORTCODE,SPECIALRELATIONSHIP,
                      PRICEIMPACT,PAYPOYALTIES,REPUNITCODE, CREATEUSERNAME,
                      CREATETIME,ARRIVEDNO,CHECKEDGOODSNUM,CHECKEDWEIGHT,
                      ENTRUSTTYPEID,GOODSXT,BUSIUNITNAME,GOODSTYPEID,
                      LADINGBILLNO,ISPREDECLARE,ENTRUSTREQUEST,CONTRACTNO,
                      FIRSTLADINGBILLNO,SECONDLADINGBILLNO,MANIFEST,WOODPACKINGID,
                      WEIGHTCHECK,ISWEIGHTCHECK,SHIPNAME,FILGHTNO,
                      INSPUNITNAME,TURNPRENO,INVOICENO,URL
                       ) VALUES(LIST_ORDER_ID.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                    '{17}','{18}','{19}',to_date ('{20}', 'YYYY-MM-DD HH24:MI:SS' ),'{21}','{22}','{23}','{24}','{25}','{26}',
                    '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}',
                    '{37}','{38}','{39}','{40}','{41}','{42}','SAP'
                    )";

            sql = string.Format(sql,
                o[0].BUSITYPE, o[0].CODE, o[0].FOONO, o[0].TOTALNO,
                o[0].DIVIDENO, o[0].GOODSNUM, o[0].GOODSWEIGHT, o[0].SFGOODSUNIT,
                o[0].PACKKIND, o[0].REPWAYID, o[0].DECLWAY, o[0].TRADEWAYCODES,
                o[0].CUSNO, o[0].CUSTOMDISTRICTCODE, o[0].PORTCODE, o[0].SPECIALRELATIONSHIP,
                o[0].PRICEIMPACT, o[0].PAYPOYALTIES, o[0].REPUNITCODE, o[0].CREATEUSERNAME,
                dt.ToString(), o[0].ARRIVEDNO, o[0].CHECKEDGOODSNUM, o[0].CHECKEDWEIGHT,
                o[0].ENTRUSTTYPEID, o[0].GOODSXT, o[0].BUSIUNITNAME, o[0].GOODSTYPEID,
                o[0].LADINGBILLNO, o[0].ISPREDECLARE, o[0].ENTRUSTREQUEST, o[0].CONTRACTNO,
                o[0].FIRSTLADINGBILLNO, o[0].SECONDLADINGBILLNO, o[0].MANIFEST,
                o[0].WOODPACKINGID, o[0].WEIGHTCHECK, o[0].ISWEIGHTCHECK, o[0].SHIPNAME, o[0].FILGHTNO,
                o[0].INSPUNITNAME, o[0].TURNPRENO, o[0].INVOICENO
                );
            DBMgr.ExecuteNonQuery(sql);
            return 1;
        }

        // 保存订单
        private int InsertOrder(List<OrderEn> ld)
        {
            int Order_Res = 1;
            List<List<OrderEn>> GroupOrder = GroupByFoo(ld);
            ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
            ServiceReference1.OrderEn DZOrder;
            List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                DZOrder = new ServiceReference1.OrderEn();
                //转成单证的数据
                //DZOrder = ZDOrderData(ListOrder);

                //生成现场订单
                XCOrderData(ListOrder);
                //DZOrderList.Add(DZOrder);
            }

            //string text = danzheng.SendOrderData(DZOrderList.ToArray());
            return Order_Res;
        }

        //检查数据
        private List<Msgobj> CheckData(List<OrderEn> ld)
        {
            IDatabase db = SeRedis.redis.GetDatabase();
            DataTable dt;
            string sql = "";
            Msgobj m;
            List<Msgobj> MsgobjList = new List<Msgobj>();

            foreach (OrderEn o in ld)
            {
                if (string.IsNullOrEmpty(o.CODE))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "FWO不可为空";
                    MsgobjList.Add(m);
                }

                if (string.IsNullOrEmpty(o.FOONO))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "FOONO不可为空";
                    MsgobjList.Add(m);
                }
                else if (o.FOONO.Substring(0, 4) != "SOBG" && o.FOONO.Substring(0, 4) != "SOBJ")
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "FOONO(" + o.FOONO + ")不符合";
                    MsgobjList.Add(m);
                }

                string BUSITYPE;
                BUSITYPE = JudgeBusiType(o.BUSITYPE, o.ENTRUSTTYPEID);
                if (string.IsNullOrEmpty(o.BUSITYPE))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "凭证类型不可为空";
                    MsgobjList.Add(m);
                }
                else if (BUSITYPE == "0")
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "凭证类型无法配";
                    MsgobjList.Add(m);

                }

                //委托类型
                string ENTRUSTTYPEID = GetENTRUSTTYPEID(ld, BUSITYPE);

                if (!string.IsNullOrEmpty(ENTRUSTTYPEID))
                {
                    if (ENTRUSTTYPEID == "01")
                    {
                        if (string.IsNullOrEmpty(o.REPUNITCODE))
                        {
                            m = new Msgobj();
                            m.MSG_ID = 1;
                            m.MSG_TYPE = "E";
                            m.MSG_TXT = "报关申报单位不可为空";
                            MsgobjList.Add(m);
                        }

                    }
                    else if (ENTRUSTTYPEID == "02")
                    {
                        if (string.IsNullOrEmpty(o.INSPUNITNAME))
                        {
                            m = new Msgobj();
                            m.MSG_ID = 1;
                            m.MSG_TYPE = "E";
                            m.MSG_TXT = "报检申报单位不可为空";
                            MsgobjList.Add(m);
                        }
                    }
                    else if (ENTRUSTTYPEID == "03")
                    {

                        if (string.IsNullOrEmpty(o.REPUNITCODE) || string.IsNullOrEmpty(o.INSPUNITNAME))
                        {
                            m = new Msgobj();
                            m.MSG_ID = 1;
                            m.MSG_TYPE = "E";
                            m.MSG_TXT = "报关申报单位/报检申报单位不可为空";
                            MsgobjList.Add(m);
                        }
                    }

                }


                if (string.IsNullOrEmpty(o.REPWAYID))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "申报方式不可为空";
                    MsgobjList.Add(m);
                }
                else
                {

                    sql = "select CODE,NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + o.REPWAYID + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "申报方式(" + o.REPWAYID + ")无法匹配";
                        MsgobjList.Add(m);
                    }
                }

                if (string.IsNullOrEmpty(o.CUSTOMDISTRICTCODE))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "申报关区不可为空";
                    MsgobjList.Add(m);
                }
                else
                {
                    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + o.CUSTOMDISTRICTCODE + "' ORDER BY CODE";

                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "申报关区(" + o.CUSTOMDISTRICTCODE + ")无法匹配";
                        MsgobjList.Add(m);
                    }

                }

                //  REPUNITCODE  INSPUNITCODE

                if (string.IsNullOrEmpty(o.DECLWAY))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "报关方式不可为空";
                    MsgobjList.Add(m);
                }
                else
                {
                    sql = "select CODE,NAME from SYS_DECLWAY where enabled=1 and NAME ='" + o.DECLWAY + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "申报关区(" + o.DECLWAY + ")无法匹配";
                        MsgobjList.Add(m);
                    }
                }

                if (string.IsNullOrEmpty(o.PORTCODE))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "口岸关区不可为空";
                    MsgobjList.Add(m);
                }
                else
                {
                    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1 and NAME ='" + o.PORTCODE + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "口岸关区(" + o.PORTCODE + ")无法匹配";
                        MsgobjList.Add(m);
                    }
                }

                if (string.IsNullOrEmpty(o.BUSIUNITNAME))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "经营单位不可为空";
                    MsgobjList.Add(m);
                }
                else
                {
                    string name = o.BUSIUNITNAME.Remove(o.BUSIUNITNAME.Length - 10, 10);
                    sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "经营单位(" + o.BUSIUNITNAME + ")无法匹配";
                        MsgobjList.Add(m);
                    }
                }


                if (string.IsNullOrEmpty(o.GOODSWEIGHT))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "毛重不可为空";
                    MsgobjList.Add(m);
                }


                if (string.IsNullOrEmpty(o.GOODSNUM))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "件数不可为空";
                    MsgobjList.Add(m);
                }


                if (string.IsNullOrEmpty(o.TRADEWAYCODES))
                {
                    m = new Msgobj();
                    m.MSG_ID = 1;
                    m.MSG_TYPE = "E";
                    m.MSG_TXT = "贸易方式不可为空";
                    MsgobjList.Add(m);
                }
                else
                {
                    //o.TRADEWAYCODES = o.TRADEWAYCODES.Substring(0, o.TRADEWAYCODES.Length - 1);
                    string[] arr = o.TRADEWAYCODES.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        //贸易方式 
                        sql = @"select ID,CODE,NAME from BASE_DECLTRADEWAY WHERE enabled=1 and NAME ='" + arr[i] + "'";
                        dt = DB_BaseData.GetDataTable(sql);
                        if (dt.Rows.Count <= 0)
                        {
                            m = new Msgobj();
                            m.MSG_ID = 1;
                            m.MSG_TYPE = "E";
                            m.MSG_TXT = "贸易方式(" + arr[i] + ")无法匹配";
                            MsgobjList.Add(m);
                        }
                    }

                }


                //空运进口
                if (BUSITYPE == "11")
                {
                    if (string.IsNullOrEmpty(o.DIVIDENO))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "分单号不可为空";
                        MsgobjList.Add(m);
                    }

                    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "木质包装不可为空";
                        MsgobjList.Add(m);
                    }

                }


                //海
                if (BUSITYPE == "21" || BUSITYPE == "22")
                {
                    if (string.IsNullOrEmpty(o.SHIPNAME))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "船名不可为空";
                        MsgobjList.Add(m);
                    }

                    if (string.IsNullOrEmpty(o.FILGHTNO))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "航次不可为空";
                        MsgobjList.Add(m);
                    }

                    if (string.IsNullOrEmpty(o.GOODSTYPEID))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "货物类型不可为空";
                        MsgobjList.Add(m);
                    }
                }


                //陆
                if (BUSITYPE == "30" || BUSITYPE == "31")
                {
                    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                    {
                        m = new Msgobj();
                        m.MSG_ID = 1;
                        m.MSG_TYPE = "E";
                        m.MSG_TXT = "木质包装不可为空";
                        MsgobjList.Add(m);
                    }
                }



            }
            return MsgobjList;

        }

        //业务类型转换
        private string JudgeBusiType(string busitype, string ENTRUSTTYPEID)
        {
            string busitypeid = "0";
            if (busitype.IndexOf("空运进口") >= 0)
            {
                busitypeid = "11";
            }
            if (busitype.IndexOf("空运出口") >= 0)
            {
                busitypeid = "10";
            }
            if (busitype.IndexOf("海运进口") >= 0)
            {
                busitypeid = "21";
            }
            if (busitype.IndexOf("海运出口") >= 0)
            {
                busitypeid = "20";
            }
            if (busitype.IndexOf("陆运进口") >= 0)
            {
                busitypeid = "31";
            }
            if (busitype.IndexOf("陆运出口") >= 0)
            {
                busitypeid = "30";
            }

            if (busitype.IndexOf("特殊监管") >= 0)
            {
                if (ENTRUSTTYPEID == "进口企业")
                {
                    busitypeid = "51";
                }
                if (ENTRUSTTYPEID == "出口企业")
                {
                    busitypeid = "50";
                }
            }

            //委托方式   进口企业/出口企业/HUB仓进/HUB仓出
            return busitypeid;
        }

        //整合订单
        private List<List<OrderEn>> GroupByFoo(List<OrderEn> oes)
        {
            List<List<OrderEn>> lloes = new List<List<OrderEn>>();
            List<OrderEn> oes_split1 = new List<OrderEn>();
            List<OrderEn> oes_split2 = new List<OrderEn>();
            List<OrderEn> oes_split3 = new List<OrderEn>();
            List<OrderEn> oes_split4 = new List<OrderEn>();
            // 进口企业/出口企业/HUB仓进/HUB仓出
            if (string.IsNullOrEmpty(oes[0].ENTRUSTTYPEID))
            {
                lloes.Add(oes);
            }
            else
            {
                foreach (OrderEn oe in oes)
                {
                    if (oe.ENTRUSTTYPEID == "进口企业")
                    {
                        oes_split1.Add(oe);
                    }
                    if (oe.ENTRUSTTYPEID == "出口企业")
                    {
                        oes_split2.Add(oe);
                    }
                    if (oe.ENTRUSTTYPEID == "HUB仓进")
                    {
                        oes_split3.Add(oe);
                    }
                    if (oe.ENTRUSTTYPEID == "HUB仓出")
                    {
                        oes_split4.Add(oe);
                    }
                }
            }
            return lloes;
        }

        //获取委托类型
        private string GetENTRUSTTYPEID(List<OrderEn> oes, string busitype)
        {
            string ENTRUSTTYPEID = "";
            if (busitype == "11" || busitype == "10" || busitype == "21" || busitype == "20" || busitype == "31" || busitype == "30" || busitype == "51" || busitype == "50")
            {
                if (oes.Count == 2)
                {
                    ENTRUSTTYPEID = "03";
                }
                if (oes.Count == 1)
                {
                    if (oes[0].FOONO.Substring(0, 4) == "SOBG")
                    {
                        ENTRUSTTYPEID = "01";
                    }
                    else
                    {
                        ENTRUSTTYPEID = "02";
                    }
                }
            }
            return ENTRUSTTYPEID;
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
            string sql = "select t.*, t.rowid from list_order t where  ID = " + ID;
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = JsonConvert.SerializeObject(dt, iso);
            result = result.Substring(1, result.Length - 1);
            result = result.Substring(0, result.Length - 1);
            return result;
        }



        [HttpGet]
        public string GetData()
        {
            string BUSITYPE = Request["BUSITYPE"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            int Page = Convert.ToInt32(Request.Params["page"]);
            int total = 0;
            string sql = "select t.* from list_order  t where 1=1  ";

            switch (BUSITYPE)
            {
                case "ONEIN":
                    sql += " AND (BUSITYPE='11' OR BUSITYPE='21' OR BUSITYPE='31')";
                    break;
                case "ONEOUT":
                    sql += " AND (BUSITYPE='10' OR BUSITYPE='20' OR BUSITYPE='30')";
                    break;
                case "SPECIAL":
                    sql += " and (BUSITYPE='50' OR BUSITYPE='51') "; //特殊监管
                    break;
                case "BLC":
                    sql += " and (BUSITYPE='40' OR BUSITYPE='41') ";
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