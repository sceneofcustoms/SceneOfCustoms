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
                MO.MSG_ID = 1;
                MO.MSG_TYPE = "S";
                MO.MSG_TXT = "保存指令失败";
                MSList.Add(MO);
            }


            IDatabase db = SeRedis.redis.GetDatabase();//先将报文数据保存至缓存数据库
            db.ListRightPush("SyncDataFromSap", ld.ToString());
            MSList = CheckData(ld);
            if (MSList.Count <= 0)
            {
                if (ld.Count > 0)
                {
                    int Order_Res = InsertOrder(ld);
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
                      DIVIDENO,GOODSNUM,GOODSWEIGHT,SFGOODSUNIT,
                      PACKKIND, REPWAYID,DECLWAY,TRADEWAYCODES,
                      CUSNO,CUSTOMDISTRICTCODE,PORTCODE,SPECIALRELATIONSHIP,
                      PRICEIMPACT,PAYPOYALTIES,REPUNITCODE, CREATEUSERNAME,
                      CREATETIME,ARRIVEDNO,CHECKEDGOODSNUM,CHECKEDWEIGHT,
                      ENTRUSTTYPEID,GOODSXT,BUSIUNITNAME,GOODSTYPEID,
                      LADINGBILLNO,ISPREDECLARE,ENTRUSTREQUEST,CONTRACTNO,
                      FIRSTLADINGBILLNO,SECONDLADINGBILLNO,MANIFEST,WOODPACKINGID,
                      WEIGHTCHECK,ISCHECKEDWEIGHT,SHIPNAME,FILGHTNO,
                      INSPUNITCODE,TURNPRENO,INVOICENO
                       ) VALUES(LIST_SAPFOO_ID.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                    '{17}','{18}','{19}',to_date ('{20}', 'YYYY-MM-DD HH24:MI:SS' ),'{21}','{22}','{23}','{24}','{25}','{26}',
                    '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}',
                    '{37}','{38}','{39}','{40}','{41}','{42}'
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




        //  保存订单
        private int InsertOrder(List<OrderEn> ld)
        {
            int Order_Res = 1;
            string sql = "";
            DateTime dt = DateTime.Now;
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
                    o.CUSNO, o.CUSTOMDISTRICTCODE,o.PORTCODE,o.SPECIALRELATIONSHIP,                       
                    o.PRICEIMPACT, o.PAYPOYALTIES,o.REPUNITCODE,  o.CREATEUSERNAME,
                    dt.ToString(),  o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT,
                    o.ENTRUSTTYPEID , o.GOODSXT,o.BUSIUNITNAME,  o.GOODSTYPEID,
                    o.LADINGBILLNO, o.ISPREDECLARE, o.ENTRUSTREQUEST,  o.CONTRACTNO,
                    o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO,o.MANIFEST, 
                    o.WOODPACKINGID,o.WEIGHTCHECK, o.ISWEIGHTCHECK, o.SHIPNAME, o.FILGHTNO, 
                    o.INSPUNITNAME, o.TURNPRENO, o.INVOICENO                     
                    );
                Order_Res = DBMgr.ExecuteNonQuery(sql);
            }

            return Order_Res;
        }



        //验证数据
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


        //凭证类型转换
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
        private string GetENTRUSTTYPEID(List<OrderEn> oes, int busitype)
        {
            string ENTRUSTTYPEID = "";
            if (busitype == 11 || busitype == 10 || busitype == 21 || busitype == 20 || busitype == 31 || busitype == 30 || busitype == 51 || busitype == 50)
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


        private ActionResult Json(object p, JsonRequestBehavior jsonRequestBehavior)
        {
            throw new NotImplementedException();
        }
    }
}
