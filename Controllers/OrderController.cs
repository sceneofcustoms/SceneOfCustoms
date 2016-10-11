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
            ServiceReference2.CustomerServiceSoapClient danzheng = new ServiceReference2.CustomerServiceSoapClient();
            ServiceReference2.OrderEn dzOrder = new ServiceReference2.OrderEn();

            dzOrder.ARRIVEDNO = "1";
            dzOrder.REPNO = "1";
            //dzOrder.BUSIUNITCODE = "1";

            List<ServiceReference2.OrderEn> orderList = new List<ServiceReference2.OrderEn>();

            orderList.Add(dzOrder);
            string text = danzheng.SendOrderData(orderList.ToArray());
            ViewData["text"] = text;
            return View();
        }

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


        //测试tm
        public ActionResult testTm()
        {
            List<OrderEn> ld = new List<OrderEn>();


            //List<List<OrderEn>> test = GroupByFoo(ld);
            //foreach (List<OrderEn> o in test)
            //{
            //    //o[1].ARRIVEDNO;//2
            //    //o[0].ENTRUSTREQUEST //12312
            //}



            OrderEn obj = new OrderEn();
            obj.BUSITYPE = "飞力达FWO-海运出口-整箱";
            obj.CODE = "00000110000000001169";
            obj.FOONO = "00000000800000000541";
            obj.TOTALNO = "4";
            obj.DIVIDENO = "5";
            obj.GOODSNUM = "5000";
            obj.GOODSWEIGHT = "6000.0";
            obj.PACKKIND = "袋";
            obj.REPWAYID = "进口集报";
            obj.DECLWAY = "通关无纸化";
            obj.TRADEWAYCODES = "一般贸易/外资设备物品";
            obj.CUSNO = "12";
            obj.CUSTOMDISTRICTCODE = "昆山海关";
            obj.PORTCODE = "昆山海关";
            obj.PRICEIMPACT = "X";
            obj.PAYPOYALTIES = "X";
            obj.SFGOODSUNIT = "LY test Vendor1";
            obj.REPUNITCODE = "江苏飞力达国际物流股份有限公司营运中心3223980002";
            obj.CREATEUSERNAME = "洪家伟";
            obj.CREATETIME = DateTime.Now.ToLocalTime().ToString();
            obj.ARRIVEDNO = "FOO11113333333777";
            obj.CHECKEDGOODSNUM = "5000";
            obj.CHECKEDWEIGHT = "6000.0";
            obj.ENTRUSTTYPEID = "无";
            obj.GOODSXT = "普通货";
            obj.BUSIUNITNAME = "LY test Vendor1";
            obj.GOODSTYPEID = "FCL（整箱装载）";
            obj.LADINGBILLNO = "FL161000001";
            obj.ISPREDECLARE = "X";
            obj.ENTRUSTREQUEST = "FOO9994449999";
            obj.CONTRACTNO = "FOO444555666";
            obj.FIRSTLADINGBILLNO = "11111C";
            obj.SECONDLADINGBILLNO = "11111C";
            obj.MANIFEST = "66633322222";
            obj.WOODPACKINGID = "非木";
            obj.WEIGHTCHECK = "X";
            obj.ISWEIGHTCHECK = "X";
            obj.SHIPNAME = "COSCO KOREA";
            obj.FILGHTNO = "S334";
            obj.INSPUNITNAME = "江苏飞力达国际物流股份有限公司营运中心";
            obj.TURNPRENO = "40";
            obj.INVOICENO = "11111111111222222222";
            obj.SPECIALRELATIONSHIP = "X";
            ld.Add(obj);
            IList<Msgobj> ms = CheckData(ld);

            //if (ms.Count != 0) return ms;


            DateTime dt = DateTime.Now;


            //ViewData["sql"] = sql;
            return View();
        }


        private ServiceReference2.OrderEn ZDOrderData(List<OrderEn> ListOrder)
        {
            string sql = "";
            DataTable dt;
            ServiceReference2.OrderEn DZOrder = new ServiceReference2.OrderEn();

            DZOrder.CUSNO = ListOrder[1].CUSNO; //企业编号
            DZOrder.REPNO = ""; //申报单位编号   --
            DZOrder.ENTRUSTTYPE = GetENTRUSTTYPEID(ListOrder, ListOrder[1].BUSITYPE); //委托类型代码

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
            DZOrder.BUSITYPE = JudgeBusiType(ListOrder[1].BUSITYPE, ListOrder[1].ENTRUSTTYPEID);
            //业务类型名称 --
            DZOrder.BUSITYPENAME = "";

            //申报方式代码
            sql = "select CODE, NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + ListOrder[1].REPWAYID + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.REPWAYID = dt.Rows[0]["CODE"] + "";
            //申报方式名称 --
            DZOrder.REPWAYNAME = dt.Rows[0]["NAME"] + "";

            //申报关区代码
            sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + ListOrder[1].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.CUSTOMAREACODE = dt.Rows[0]["CODE"] + "";
            //申报关区代码 --
            DZOrder.CUSTOMAREANAME = dt.Rows[0]["NAME"] + "";

            //报关方式代码
            sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + ListOrder[1].DECLWAY + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.DECLWAY = dt.Rows[0]["CODE"] + "";
            //报关方式名称 --
            DZOrder.DECLWAYNAME = dt.Rows[0]["NAME"] + "";

            //经营单位代码
            sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + ListOrder[1].BUSIUNITNAME + "'";
            dt = DB_BaseData.GetDataTable(sql);
            DZOrder.BUSIUNITCODE = dt.Rows[0]["BUSIUNITCODE"] + "";
            //经营单位名称
            DZOrder.BUSIUNITNAME = dt.Rows[0]["BUSIUNITNAME"] + "";

            //经营单位社会号
            DZOrder.BUSIUNITNUM = "";

            //件数
            DZOrder.GOODSNUM = Int32.Parse(ListOrder[1].GOODSNUM);

            //毛重
            DZOrder.GOODSGW = decimal.Parse(ListOrder[1].GOODSWEIGHT);

            //净重
            DZOrder.GOODSNW = decimal.Parse(ListOrder[1].CHECKEDWEIGHT);

            //包装种类名称
            DZOrder.PACKKINDNAME = ListOrder[1].PACKKIND;

            //订单要求 --
            DZOrder.ORDERREQUEST = ListOrder[1].ENTRUSTREQUEST;

            //申报单位  报关 报检

            if (DZOrder.ENTRUSTTYPE == "01")
            {
                DZOrder.DECLREPCODE = ListOrder[1].REPUNITCODE.Substring(ListOrder[1].REPUNITCODE.Length, 10);
                DZOrder.DECLREPNAME = ListOrder[1].REPUNITCODE.Remove(ListOrder[1].REPUNITCODE.Length, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "02")
            {
                DZOrder.INSPREPCODE = ListOrder[1].REPUNITCODE.Substring(ListOrder[1].INSPUNITNAME.Length, 10);
                DZOrder.INSPREPNAME = ListOrder[1].REPUNITCODE.Remove(ListOrder[1].INSPUNITNAME.Length, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "03")
            {
                DZOrder.DECLREPCODE = ListOrder[1].REPUNITCODE.Substring(ListOrder[1].REPUNITCODE.Length, 10);
                DZOrder.DECLREPNAME = ListOrder[1].REPUNITCODE.Remove(ListOrder[1].REPUNITCODE.Length, 10);
                DZOrder.INSPREPCODE = ListOrder[1].REPUNITCODE.Substring(ListOrder[1].INSPUNITNAME.Length, 10);
                DZOrder.INSPREPNAME = ListOrder[1].REPUNITCODE.Remove(ListOrder[1].INSPUNITNAME.Length, 10);
            }

            //总单号
            DZOrder.TOTALNO = ListOrder[1].TOTALNO;

            //分单号
            DZOrder.TOTALNO = ListOrder[1].TOTALNO;

            //转关预录入号
            DZOrder.TURNPRENO = ListOrder[1].TURNPRENO;

            //进出口岸
            DZOrder.PORTCODE = ListOrder[1].PORTCODE;

            //委托时间
            DZOrder.SUBMITTIME = DateTime.Now;

            //委托人员
            DZOrder.SUBMITUSERNAME = ListOrder[1].CREATEUSERNAME;

            //运抵编号
            DZOrder.ARRIVEDNO = ListOrder[1].ARRIVEDNO;


            return DZOrder;
        }

        //  保存订单
        private int InsertOrder(List<OrderEn> ld)
        {
            int Order_Res = 1;
            DateTime dt = DateTime.Now;
            List<List<OrderEn>> GroupOrder = GroupByFoo(ld);


            ServiceReference2.CustomerServiceSoapClient danzheng = new ServiceReference2.CustomerServiceSoapClient();

            ServiceReference2.OrderEn DZOrder;

            List<ServiceReference2.OrderEn> DZOrderList = new List<ServiceReference2.OrderEn>();



            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                DZOrder = new ServiceReference2.OrderEn();

                //转成单证的数据
                DZOrder = ZDOrderData(ListOrder);



                foreach (OrderEn O in ListOrder)
                {

                }
                //o[1].ARRIVEDNO;//2
                //o[0].ENTRUSTREQUEST //12312


                DZOrderList.Add(DZOrder);
            }

            string text = danzheng.SendOrderData(DZOrderList.ToArray());


            foreach (OrderEn o in ld)
            {
                if (o.SPECIALRELATIONSHIP == "")
                {
                    o.SPECIALRELATIONSHIP = "0";
                }
                else
                {
                    o.SPECIALRELATIONSHIP = "1";
                }

                if (o.PRICEIMPACT == "")
                {
                    o.PRICEIMPACT = "0";
                }
                else
                {
                    o.PRICEIMPACT = "1";
                }

                if (o.PAYPOYALTIES == "")
                {
                    o.PAYPOYALTIES = "0";
                }
                else
                {
                    o.PAYPOYALTIES = "1";
                }

                if (o.ISPREDECLARE == "")
                {
                    o.ISPREDECLARE = "0";
                }
                else
                {
                    o.ISPREDECLARE = "1";
                }

                if (o.WEIGHTCHECK == "")
                {
                    o.WEIGHTCHECK = "0";
                }
                else
                {
                    o.WEIGHTCHECK = "1";
                }


                if (o.ISWEIGHTCHECK == "")
                {
                    o.ISWEIGHTCHECK = "0";
                }
                else
                {
                    o.ISWEIGHTCHECK = "1";
                }
                o.BUSITYPE = JudgeBusiType(o.BUSITYPE, o.ENTRUSTTYPEID);

                string sql = "";
                sql = @"insert into List_Order(
                      ID,
                      BUSITYPE,CODE,FOONO,TOTALNO,
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
                    o.BUSITYPE, o.CODE, o.FOONO, o.TOTALNO,
                    o.DIVIDENO, o.GOODSNUM, o.GOODSWEIGHT, o.SFGOODSUNIT,
                    o.PACKKIND, o.REPWAYID, o.DECLWAY, o.TRADEWAYCODES,
                    o.CUSNO, o.CUSTOMDISTRICTCODE, o.PORTCODE, o.SPECIALRELATIONSHIP,
                    o.PRICEIMPACT, o.PAYPOYALTIES, o.REPUNITCODE, o.CREATEUSERNAME,
                     dt.ToString(), o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT,
                    o.ENTRUSTTYPEID, o.GOODSXT, o.BUSIUNITNAME, o.GOODSTYPEID,
                    o.LADINGBILLNO, o.ISPREDECLARE, o.ENTRUSTREQUEST, o.CONTRACTNO,
                    o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO, o.MANIFEST, o.WOODPACKINGID,
                    o.WEIGHTCHECK, o.ISWEIGHTCHECK, o.SHIPNAME, o.FILGHTNO,
                    o.INSPUNITNAME, o.TURNPRENO, o.INVOICENO
                    );
                Order_Res = DBMgr.ExecuteNonQuery(sql);
            }

            return Order_Res;
        }


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
                else if (o.FOONO.Substring(0, 4) != "SOBG" || o.FOONO.Substring(0, 4) != "SOBJ")
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

                    sql = "select CODE,NAME||'('||CODE||')' NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + o.REPWAYID + "'";
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
                    sql = "select CODE,NAME||'('||CODE||')' NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + o.CUSTOMDISTRICTCODE + "' ORDER BY CODE";

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
                    sql = "select CODE,NAME||'('||CODE||')' NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + o.DECLWAY + "'";
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
                    sql = "select CODE,NAME||'('||CODE||')' NAME from BASE_CUSTOMDISTRICT  where ENABLED=1 and NAME ='" + o.PORTCODE + "'";
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
                    sql = "SELECT CODE,NAME||'('||CODE||')' NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + o.BUSIUNITNAME + "'";
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
                        sql = @"select ID,CODE,NAME||'('||CODE||')' NAME from BASE_DECLTRADEWAY WHERE enabled=1 and NAME ='" + arr[i] + "'";
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
            string BUSITYPE = Request.Params["BUSITYPE"];
            string TYPE = Request.Params["TYPE"];
            int PageSize = Convert.ToInt32(Request.Params["rows"]);
            //int PageSize = 20;
            int Page = Convert.ToInt32(Request.Params["page"]);
            //int Page = 1;
            int total = 0;

            string sql = "select t.* from list_order  t where 1=1  ";
            if (!string.IsNullOrEmpty(BUSITYPE))
            {
                sql += " and BUSITYPE ='" + BUSITYPE + "'";
            }

            if (TYPE == "SpecialSupervision")
            {
                sql += " and BUSITYPE in (50,51) "; //特殊监管
            }
            else if (TYPE == "OverlayBonded")
            {
                sql += " and BUSITYPE in (40,41) and CORRESPONDNO is not null";//叠加保税
            }
            else if (TYPE == "DomesticKnot")
            {
                //sql += " and BUSITYPE in (40,41) and CORRESPONDNO is  null";//国内结转
            }

            string sort = !string.IsNullOrEmpty(Request.Params["sort"]) && Request.Params["sort"] != "text" ? Request.Params["sort"] : "ID";
            string order = !string.IsNullOrEmpty(Request.Params["order"]) ? Request.Params["order"] : "DESC";
            //string sort ="ID";
            //string order ="DESC";
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