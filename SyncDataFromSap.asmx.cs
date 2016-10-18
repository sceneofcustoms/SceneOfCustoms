﻿using Newtonsoft.Json;
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
using System.Web.Services;

namespace SceneOfCustoms
{
    /// <summary>
    /// SyncDataFromSap 的摘要说明
    /// </summary>
    //[System.Xml.Serialization.XmlInclude(typeof(OrderEntity))]
    //[System.Xml.Serialization.XmlInclude(typeof(OrderEntity))]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SyncDataFromSap : System.Web.Services.WebService
    {
        //param参数说明:为json格式字符串

        [WebMethod(Description = @"param参数说明:订单集合<br>返回值:一个对象, Success true/false, Message 消息<br>1 凭证类型(LICENSETYPE)<br>2 FWO订单号(FWONO)<br>3 FO报关服务指令号
       (FOCUSTOMSNO)<br>4 总单号(TOTALNO)<br>5 分单号(DIVIDENO)<br>6 件数(GOODSNUM)<br>7毛重(GOODSGW)<br>8 净重(GOODSNW)<br>9 经营单位代码(BUSIUNITCODE)
        <br>10 经营单位名称(BUSIUNITNAME)<br>11 包装种类(PACKKIND)<br>12 申报方式(REPWAYID)<br>13报关方式(DECLWAY)<br>14贸易方式(TRADEWAYCODES)
        <br>15客户自编号(CUSNO)<br>16进/出口岸(PORTCODE)<br>17特殊关系确认(SPECIALRELATIONSHIP)<br>18价格影响确认(PRICEIMPACT)
        <br>19支付特许权使用费确认(PAYPOYALTIES)<br>20报关申报单位代码(REPUNITCODE)<br>21报关申报单位名称(REPUNITNAME)<br>22委托人员(SUBMITUSERNAME)
        <br>23委托时间(SUBMITTIME)<br>24委托电话(SUBMITUSERPHONE)<br>25运抵编号(ARRIVEDNO)<br>26集装箱车号信息(CONTAINERTRUCK)<br>27实际件数(ACTUALGOODSNUM)
        <br>28实际毛重(ACTUALGOODSGW)<br>29货物类型(整箱或散箱用中文标记)(GOODSTYPE)<br>30报关提单号(SECONDLADINGBILLNO)")]
        public List<Msgobj> SyncData(List<OrderEn> ld)
        {

            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            int SaveSap_Res = SavaSapFoo(ld);
            if (SaveSap_Res == 0)
            {
                MSList.Add(set_MObj("E", "保存指令失败"));
                save_log(MSList, ld, "1");
                return MSList;
            }

            IDatabase db = SeRedis.redis.GetDatabase();//先将报文数据保存至缓存数据库
            db.ListRightPush("SyncDataFromSap", ld.ToString());


            if (ld.Count > 0)
            {
                MSList = CheckData(ld);

                if (MSList.Count <= 0)
                {
                    int Order_Res = InsertOrder(ld);
                    if (Order_Res == 1)
                    {
                        MSList.Add(set_MObj("S", "保存成功"));
                    }
                    else
                    {
                        MSList.Add(set_MObj("E", "保存失败"));
                    }
                }

            }
            else
            {
                MSList.Add(set_MObj("E", "没有指令"));
            }


            save_log(MSList, ld, "1");

            return MSList;
        }


        // 保存指令 方法
        private int SavaSapFoo(List<OrderEn> ld)
        {
            int Order_Res = 1;
            DateTime dt = DateTime.Now;
            string sql = "";
            foreach (OrderEn o in ld)
            {

                sql = @"insert into LIST_SAPFOO(
                      ID,
                      BUSITYPE,FWONO,FOONO,TOTALNO,
                      DIVIDENO,GOODSNUM,GOODSWEIGHT,SGOODSUNIT,
                      PACKKIND, REPWAYID,DECLWAY,TRADEWAYCODES,
                      CUSNO,CUSTOMDISTRICTCODE,PORTCODE,SPECIALRELATIONSHIP,
                      PRICEIMPACT,PAYPOYALTIES,REPUNITCODE, CREATEUSERNAME,
                      CREATETIME,ARRIVEDNO,CHECKEDGOODSNUM,CHECKEDWEIGHT,
                      ENTRUSTTYPEID,GOODSXT,BUSIUNITNAME,GOODSTYPEID,
                      LADINGBILLNO,ISPREDECLARE,ENTRUSTREQUEST,CONTRACTNO,
                      FIRSTLADINGBILLNO,SECONDLADINGBILLNO,MANIFEST,WOODPACKINGID,
                      WEIGHTCHECK,ISCHECKEDWEIGHT,SHIPNAME,FILGHTNO,
                      INSPUNITCODE,TURNPRENO,INVOICENO,FGOODSUNIT,ALLOWDECLARE
                       ) VALUES(LIST_SAPFOO_ID.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                    '{17}','{18}','{19}',to_date ('{20}', 'YYYY-MM-DD HH24:MI:SS' ),'{21}','{22}','{23}','{24}','{25}','{26}',
                    '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}',
                    '{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}'
                    )";

                sql = string.Format(sql,
                    o.BUSITYPE, o.CODE, o.FOONO, o.TOTALNO,
                    o.DIVIDENO, o.GOODSNUM, o.GOODSWEIGHT, o.SGOODSUNIT,
                    o.PACKKIND, o.REPWAYID, o.DECLWAY, o.TRADEWAYCODES,
                    o.CUSNO, o.CUSTOMDISTRICTCODE, o.PORTCODE, o.SPECIALRELATIONSHIP,
                    o.PRICEIMPACT, o.PAYPOYALTIES, o.REPUNITCODE, o.CREATEUSERNAME,
                     dt.ToString(), o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT,
                    o.ENTRUSTTYPEID, o.GOODSXT, o.BUSIUNITNAME, o.GOODSTYPEID,
                    o.LADINGBILLNO, o.ISPREDECLARE, o.ENTRUSTREQUEST, o.CONTRACTNO,
                    o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO, o.MANIFEST, o.WOODPACKINGID,
                    o.WEIGHTCHECK, o.ISWEIGHTCHECK, o.SHIPNAME, o.FILGHTNO,
                    o.INSPUNITNAME, o.TURNPRENO, o.INVOICENO, o.FGOODSUNIT, o.ALLOWDECLARE
                    );
                Order_Res = DBMgr.ExecuteNonQuery(sql);

            }

            return Order_Res;
        }


        public Msgobj set_MObj(string MSG_TYPE, string MSG_TXT)
        {
            Msgobj MO = new Msgobj();
            MO.MSG_ID = 1;
            MO.MSG_TYPE = MSG_TYPE;
            MO.MSG_TXT = MSG_TXT;
            return MO;
        }

        //存日志  1 sap->现场  2 现场->单证云
        private void save_log(List<Msgobj> MSList, List<OrderEn> ld, string source)
        {
            if (source == "1")
            {
                source = "SAP->新关务";
            }
            else if (source == "2")
            {
                source = "新关务->单证云";
            }

            string STATUS;
            if (MSList[0].MSG_TYPE == "E")
            {
                STATUS = "失败";
            }
            else
            {
                STATUS = "成功";
            }
            string TEXT = "";
            string FWONO = ld[0].CODE;
            foreach (Msgobj m in MSList)
            {
                if (!string.IsNullOrEmpty(m.MSG_TXT))
                {
                    TEXT += "[" + m.MSG_TXT + "]";
                }
            }

            string sql = @"INSERT INTO MSG (ID,FWONO,SOURCE,TEXT,STATUS,CREATETIME) VALUES (MSG_ID.Nextval,'" + FWONO + "','" + source + "','" + TEXT + "','" + STATUS + "',sysdate)";
            DBMgr.ExecuteNonQuery(sql);
        }

        //转换单证数据
        private ServiceReference1.OrderEn ZDOrderData(List<OrderEn> ListOrder)
        {
            string sql = "";
            string name = "";
            DataTable dt;
            ServiceReference1.OrderEn DZOrder = new ServiceReference1.OrderEn();

            DZOrder.CUSNO = ListOrder[0].CODE; //企业编号
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
            if (dt.Rows.Count > 0)
            {
                DZOrder.REPWAYID = dt.Rows[0]["CODE"] + "";
                //申报方式名称 --
                DZOrder.REPWAYNAME = dt.Rows[0]["NAME"] + "";
            }

            //申报关区代码
            sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + ListOrder[0].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
            dt = DB_BaseData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DZOrder.CUSTOMAREACODE = dt.Rows[0]["CODE"] + "";
                //申报关区代码 --
                DZOrder.CUSTOMAREANAME = dt.Rows[0]["NAME"] + "";
            }
            //报关方式代码
            sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + ListOrder[0].DECLWAY + "'";
            dt = DB_BaseData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DZOrder.DECLWAY = dt.Rows[0]["CODE"] + "";
                //报关方式名称 --
                DZOrder.DECLWAYNAME = dt.Rows[0]["NAME"] + "";
            }

            //经营单位代码
            name = ListOrder[0].BUSIUNITNAME.Remove(ListOrder[0].BUSIUNITNAME.Length - 10, 10);
            sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
            dt = DB_BaseData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DZOrder.BUSIUNITCODE = dt.Rows[0]["CODE"] + "";
                //经营单位名称
                DZOrder.BUSIUNITNAME = dt.Rows[0]["NAME"] + "";
            }

            //经营单位社会号
            DZOrder.BUSIUNITNUM = "";

            //件数
            DZOrder.GOODSNUM = Int32.Parse(ListOrder[0].GOODSNUM);

            //毛重
            DZOrder.GOODSGW = decimal.Parse(ListOrder[0].GOODSWEIGHT);


            //净重
            //if (string.IsNullOrEmpty(ListOrder[0].CHECKEDWEIGHT))
            //{
            //    DZOrder.GOODSNW = decimal.Parse(ListOrder[0].CHECKEDWEIGHT);

            //}

            //包装种类名称
            DZOrder.PACKKINDNAME = ListOrder[0].PACKKIND;

            //订单要求 --
            DZOrder.ORDERREQUEST = ListOrder[0].ENTRUSTREQUEST;

            //委托单位代码
            DZOrder.CUSTOMERCODE = "3223980001";
            //委托单位代码
            DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";


            //申报单位  报关 报检

            if (DZOrder.ENTRUSTTYPE == "01")
            {

                DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
                DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "02")
            {
                DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
                DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
            }
            else if (DZOrder.ENTRUSTTYPE == "03")
            {
                DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
                DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
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
                      DIVIDENO,GOODSNUM,GOODSWEIGHT,SGOODSUNIT,
                      PACKKIND, REPWAYID,DECLWAY,TRADEWAYCODES,
                      CUSNO,CUSTOMDISTRICTCODE,PORTCODE,SPECIALRELATIONSHIP,
                      PRICEIMPACT,PAYPOYALTIES,REPUNITCODE, CREATEUSERNAME,
                      CREATETIME,ARRIVEDNO,CHECKEDGOODSNUM,CHECKEDWEIGHT,
                      ENTRUSTTYPEID,GOODSXT,BUSIUNITNAME,GOODSTYPEID,
                      LADINGBILLNO,ISPREDECLARE,ENTRUSTREQUEST,CONTRACTNO,
                      FIRSTLADINGBILLNO,SECONDLADINGBILLNO,MANIFEST,WOODPACKINGID,
                      WEIGHTCHECK,ISWEIGHTCHECK,SHIPNAME,FILGHTNO,
                      INSPUNITNAME,TURNPRENO,INVOICENO,URL,FGOODSUNIT,ALLOWDECLARE
                       ) VALUES(LIST_ORDER_ID.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                    '{17}','{18}','{19}',to_date ('{20}', 'YYYY-MM-DD HH24:MI:SS' ),'{21}','{22}','{23}','{24}','{25}','{26}',
                    '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}',
                    '{37}','{38}','{39}','{40}','{41}','{42}','SAP','{43}','{44}'
                    )";

            sql = string.Format(sql,
                o[0].BUSITYPE, o[0].CODE, o[0].FOONO, o[0].TOTALNO,
                o[0].DIVIDENO, o[0].GOODSNUM, o[0].GOODSWEIGHT, o[0].SGOODSUNIT,
                o[0].PACKKIND, o[0].REPWAYID, o[0].DECLWAY, o[0].TRADEWAYCODES,
                o[0].CUSNO, o[0].CUSTOMDISTRICTCODE, o[0].PORTCODE, o[0].SPECIALRELATIONSHIP,
                o[0].PRICEIMPACT, o[0].PAYPOYALTIES, o[0].REPUNITCODE, o[0].CREATEUSERNAME,
                dt.ToString(), o[0].ARRIVEDNO, o[0].CHECKEDGOODSNUM, o[0].CHECKEDWEIGHT,
                o[0].ENTRUSTTYPEID, o[0].GOODSXT, o[0].BUSIUNITNAME, o[0].GOODSTYPEID,
                o[0].LADINGBILLNO, o[0].ISPREDECLARE, o[0].ENTRUSTREQUEST, o[0].CONTRACTNO,
                o[0].FIRSTLADINGBILLNO, o[0].SECONDLADINGBILLNO, o[0].MANIFEST,
                o[0].WOODPACKINGID, o[0].WEIGHTCHECK, o[0].ISWEIGHTCHECK, o[0].SHIPNAME, o[0].FILGHTNO,
                o[0].INSPUNITNAME, o[0].TURNPRENO, o[0].INVOICENO, o[0].FGOODSUNIT, o[0].ALLOWDECLARE
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
                DZOrder = ZDOrderData(ListOrder);
                DZOrderList.Add(DZOrder);

                //生成现场订单
                XCOrderData(ListOrder);


            }

            string DZ_res = danzheng.SendOrderData(DZOrderList.ToArray());
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            if (DZ_res == "success")
            {
                MO.MSG_TYPE = "S";
            }
            else
            {
                MO.MSG_TYPE = "E";
                MO.MSG_TXT = DZ_res;
            }
            MSList.Add(MO);
            save_log(MSList, ld, "2");

            return Order_Res;
        }

        //检查数据
        private List<Msgobj> CheckData(List<OrderEn> ld)
        {
            DataTable dt;
            string sql = "";
            List<Msgobj> MsgobjList = new List<Msgobj>();
            //判断委托类型
            bool is_empty;
            string ENTRUSTTYPEID = ld[0].ENTRUSTTYPEID;
            if (ENTRUSTTYPEID == "")
            {
                is_empty = true;
            }
            else if (ENTRUSTTYPEID == "进口企业" || ENTRUSTTYPEID == "出口企业" || ENTRUSTTYPEID == "HUB仓进" || ENTRUSTTYPEID == "HUB仓出")
            {
                is_empty = false;
            }
            else
            {
                is_empty = true;
            }
            foreach (OrderEn o in ld)
            {
                if (is_empty)
                {
                    if (!string.IsNullOrEmpty(o.ENTRUSTTYPEID))
                    {
                        MsgobjList.Add(set_MObj("E", "委托方式有异常"));
                        return MsgobjList;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(o.ENTRUSTTYPEID))
                    {
                        MsgobjList.Add(set_MObj("E", "委托方式有异常"));
                        return MsgobjList;
                    }

                }

            }


            //验证一组单子是否正常
            List<List<OrderEn>> GroupOrder = GroupByFoo(ld);
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                if (ListOrder.Count > 2)
                {
                    MsgobjList.Add(set_MObj("E", "指令数量异常"));
                    return MsgobjList;
                }
                else if (ListOrder.Count == 2)
                {
                    if (ListOrder[0].FOONO.Length >= 4 && ListOrder[1].FOONO.Length >= 4)
                    {
                        string FOONO = ListOrder[0].FOONO.Substring(0, 4) + ListOrder[1].FOONO.Substring(0, 4);
                        if (FOONO != "SOBGSOBJ" && FOONO != "SOBJSOBG")
                        {
                            MsgobjList.Add(set_MObj("E", "FOONO不符合"));
                            return MsgobjList;
                        }
                    }
                    else
                    {
                        MsgobjList.Add(set_MObj("E", "FOONO不符合"));
                        return MsgobjList;
                    }


                }
                else if (ListOrder.Count == 1)
                {
                    if (ListOrder[0].FOONO.Length >= 4)
                    {
                        string FOONO = ListOrder[0].FOONO.Substring(0, 4);
                        if (FOONO != "SOBG" && FOONO != "SOBJ")
                        {
                            MsgobjList.Add(set_MObj("E", "FOONO(" + ListOrder[0].FOONO + ")不符合"));
                            return MsgobjList;
                        }
                    }
                    else
                    {
                        MsgobjList.Add(set_MObj("E", "FOONO(" + ListOrder[0].FOONO + ")不符合"));
                        return MsgobjList;
                    }

                }
            }


            //报关单  报检单 

            foreach (OrderEn o in ld)
            {
                if (string.IsNullOrEmpty(o.CODE))
                {
                    MsgobjList.Add(set_MObj("E", "FWO不可为空"));
                }

                if (string.IsNullOrEmpty(o.FOONO))
                {
                    MsgobjList.Add(set_MObj("E", "FOONO不可为空"));
                }

                string BUSITYPE;
                BUSITYPE = JudgeBusiType(o.BUSITYPE, o.ENTRUSTTYPEID);
                if (string.IsNullOrEmpty(o.BUSITYPE))
                {
                    MsgobjList.Add(set_MObj("E", "凭证类型不可为空"));
                }
                else if (BUSITYPE == "0")
                {
                    MsgobjList.Add(set_MObj("E", "凭证类型无法配"));

                }


                //报关申报单位
                if (o.FOONO.Substring(0, 4) == "SOBG" && string.IsNullOrEmpty(o.REPUNITCODE))
                {
                    MsgobjList.Add(set_MObj("E", "报关申报单位不可为空"));
                }

                //报检申报单位
                if (o.FOONO.Substring(0, 4) == "SOBJ" && string.IsNullOrEmpty(o.INSPUNITNAME))
                {
                    MsgobjList.Add(set_MObj("E", "报检申报单位不可为空"));
                }


                if (string.IsNullOrEmpty(o.REPWAYID))
                {
                    MsgobjList.Add(set_MObj("E", "申报方式不可为空"));
                }
                else
                {

                    sql = "select CODE,NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + o.REPWAYID + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgobjList.Add(set_MObj("E", "申报方式(" + o.REPWAYID + ")无法匹配"));
                    }
                }

                if (string.IsNullOrEmpty(o.CUSTOMDISTRICTCODE))
                {
                    MsgobjList.Add(set_MObj("E", "申报关区不可为空"));
                }
                else
                {
                    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + o.CUSTOMDISTRICTCODE + "' ORDER BY CODE";

                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgobjList.Add(set_MObj("E", "申报关区(" + o.CUSTOMDISTRICTCODE + ")无法匹配"));
                    }

                }

                //  REPUNITCODE  INSPUNITCODE

                if (string.IsNullOrEmpty(o.DECLWAY))
                {
                    MsgobjList.Add(set_MObj("E", "报关方式不可为空"));
                }
                else
                {
                    sql = "select CODE,NAME from SYS_DECLWAY where enabled=1 and NAME ='" + o.DECLWAY + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgobjList.Add(set_MObj("E", "申报关区(" + o.DECLWAY + ")无法匹配"));
                    }
                }

                if (string.IsNullOrEmpty(o.PORTCODE))
                {
                    MsgobjList.Add(set_MObj("E", "口岸关区不可为空"));
                }
                else
                {
                    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1 and NAME ='" + o.PORTCODE + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgobjList.Add(set_MObj("E", "口岸关区(" + o.PORTCODE + ")无法匹配"));
                    }
                }

                if (string.IsNullOrEmpty(o.BUSIUNITNAME))
                {
                    MsgobjList.Add(set_MObj("E", "经营单位不可为空"));
                }
                else
                {
                    string name = o.BUSIUNITNAME.Remove(o.BUSIUNITNAME.Length - 10, 10);
                    sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgobjList.Add(set_MObj("E", "经营单位(" + o.BUSIUNITNAME + ")无法匹配"));
                    }
                }


                if (string.IsNullOrEmpty(o.GOODSWEIGHT))
                {
                    MsgobjList.Add(set_MObj("E", "毛重不可为空"));
                }


                if (string.IsNullOrEmpty(o.GOODSNUM))
                {
                    MsgobjList.Add(set_MObj("E", "件数不可为空"));
                }


                if (string.IsNullOrEmpty(o.TRADEWAYCODES))
                {
                    MsgobjList.Add(set_MObj("E", "贸易方式不可为空"));
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
                            MsgobjList.Add(set_MObj("E", "贸易方式(" + arr[i] + ")无法匹配"));
                        }
                    }

                }


                //空运进口
                if (BUSITYPE == "11")
                {
                    if (string.IsNullOrEmpty(o.DIVIDENO))
                    {
                        MsgobjList.Add(set_MObj("E", "分单号不可为空"));
                    }

                    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                    {
                        MsgobjList.Add(set_MObj("E", "木质包装不可为空"));
                    }

                }


                //海
                if (BUSITYPE == "21" || BUSITYPE == "22")
                {
                    if (string.IsNullOrEmpty(o.SHIPNAME))
                    {
                        MsgobjList.Add(set_MObj("E", "船名不可为空"));
                    }

                    if (string.IsNullOrEmpty(o.FILGHTNO))
                    {
                        MsgobjList.Add(set_MObj("E", "航次不可为空"));
                    }

                    if (string.IsNullOrEmpty(o.GOODSTYPEID))
                    {
                        MsgobjList.Add(set_MObj("E", "货物类型不可为空"));
                    }
                }


                //陆
                if (BUSITYPE == "30" || BUSITYPE == "31")
                {
                    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                    {
                        MsgobjList.Add(set_MObj("E", "木质包装不可为空"));
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



    }
}
