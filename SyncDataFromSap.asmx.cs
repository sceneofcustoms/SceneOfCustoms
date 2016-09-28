﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
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
        public Msgobj[] SyncData(List<OrderEntity> ld)
        {
            Msgobj MO = new Msgobj();
            IDatabase db = SeRedis.redis.GetDatabase();//先将报文数据保存至缓存数据库
            db.ListRightPush("SyncDataFromSap", ld.ToString());

            if (ld.Count() == 0)
            {
                OrderEntity obj = new OrderEntity();

                obj.BUSITYPE = "1";
                obj.CODE = "2";
                obj.FOONO = "3";
                obj.TOTALNO = "4";
                obj.DIVIDENO = "5";
                obj.GOODSNUM = "6";
                obj.GOODSWEIGHT = "7";
                obj.PACKKIND = "8";
                obj.REPWAYID = "9";
                obj.DECLWAY = "10";
                obj.TRADEWAYCODES = "11";
                obj.CUSNO = "12";
                obj.CUSTOMDISTRICTCODE = "13";
                obj.PORTCODE = "14";
                obj.PRICEIMPACT = "15";
                obj.PAYPOYALTIES = "16";
                obj.SFGOODSUNIT = "17";
                obj.REPUNITCODE = "18";
                obj.CREATEUSERNAME = "19";
                obj.CREATETIME = DateTime.Now.ToLocalTime().ToString();
                obj.ARRIVEDNO = "20";
                obj.CHECKEDGOODSNUM = "21";
                obj.CHECKEDWEIGHT = "22";
                obj.ENTRUSTTYPEID = "23";
                obj.GOODSXT = "24";
                obj.BUSIUNITNAME = "25";
                obj.GOODSTYPEID = "26";
                obj.LADINGBILLNO = "27";
                obj.ISPREDECLARE = "28";
                obj.ENTRUSTREQUEST = "29";
                obj.CONTRACTNO = "30";
                obj.FIRSTLADINGBILLNO = "31";
                obj.SECONDLADINGBILLNO = "32";
                obj.MANIFEST = "33";
                obj.WOODPACKINGID = "34";
                obj.WEIGHTCHECK = "35";
                obj.ISWEIGHTCHECK = "36";
                obj.SHIPNAME = "37";
                obj.FILGHTNO = "38";
                obj.INSPUNITNAME = "39";
                obj.TURNPRENO = "40";
                obj.INVOICENO = "41";
                obj.SPECIALRELATIONSHIP = "42";


                OrderEntity obj1 = new OrderEntity();
                obj1.BUSITYPE = "123123123";
                obj1.CODE = "123123123";
                obj1.FOONO = "123123123";
                obj1.TOTALNO = "123123123111aaaa";
                obj1.DIVIDENO = "123123123111aaaa";
                obj1.GOODSNUM = "123";
                obj1.GOODSWEIGHT = "123123";
                obj1.SFGOODSUNIT = "bb";
                obj1.PACKKIND = "123123123";
                obj1.REPWAYID = "123123123";
                obj1.DECLWAY = "123123123";
                obj1.TRADEWAYCODES = "123123123";
                obj1.CUSNO = "123123123";
                obj1.CUSTOMDISTRICTCODE = "123123123";
                obj1.PORTCODE = "123123123";
                obj1.PRICEIMPACT = "1231231";
                obj1.PAYPOYALTIES = "123123";
                obj1.REPUNITCODE = "123123123";
                obj1.CREATEUSERNAME = "123123123";
                obj1.CREATETIME = DateTime.Now.ToLocalTime().ToString();
                obj1.ARRIVEDNO = "123";
                obj1.CHECKEDGOODSNUM = "123";
                obj1.CHECKEDWEIGHT = "123";
                obj1.ENTRUSTTYPEID = "123";
                obj1.GOODSXT = "123";
                obj1.BUSIUNITNAME = "123";
                obj1.GOODSTYPEID = "123";
                obj1.LADINGBILLNO = "123";
                obj1.ISPREDECLARE = "123";
                obj1.ENTRUSTREQUEST = "123";
                obj1.CONTRACTNO = "123";
                obj1.FIRSTLADINGBILLNO = "123";
                obj1.SECONDLADINGBILLNO = "123";
                obj1.MANIFEST = "123";
                obj1.WOODPACKINGID = "123";
                obj1.WEIGHTCHECK = "123";
                obj1.ISWEIGHTCHECK = "123";
                obj1.SHIPNAME = "123";
                obj1.FILGHTNO = "123";
                obj1.INSPUNITNAME = "123";
                obj1.TURNPRENO = "123";
                obj1.INVOICENO = "123";
                obj1.SPECIALRELATIONSHIP = "42";

                //List<OrderEntity> ld = new List<OrderEntity>();
                ld.Add(obj);
                ld.Add(obj1);
            }
            DateTime dt = DateTime.Now;
            foreach (OrderEntity o in ld)
            {
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
                      INSPUNITNAME,TURNPRENO,INVOICENO
                       ) VALUES(LIST_ORDER_ID.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
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

                DBMgr.ExecuteNonQuery(sql);
            }

            //param 参数为json格式的字符串{LICENSETYPE:'decl',FWONO:'',FOCUSTOMSNO:'',CONTAINERDETAIL:[{}]}
            //1 凭证类型    LICENSETYPE 对应本系统的业务类型
            //2 FWO订单号   FWONO
            //3 FO报关服务指令号  FOCUSTOMSNO
            //4 总单号      TOTALNO
            //5 分单号      DIVIDENO
            //6 件数   
            //7 毛重 
            //9 货物包装 
            //10申报方式 
            //11报关方式 
            //12贸易方式 
            //13客户自编号
            //14进/出口岸
            //15特殊关系确认
            //16价格影响确认
            //17支付特许权使用费确认
            //18报关申报单位
            //19委托人员
            //20委托时间
            //21委托电话
            //22运抵编号 
            //24实际件数
            //25实际毛重              
            //27货物类型(整箱|散箱) 

            //33报关提单号  FWO Header，海运通用信息 一程提单号/订舱提单号
            //34是否提前报关
            //35需求备注
            //36合同号
            //37二线合同专用发票号 不知道有什么用
            //38口岸计划操作时间   不知道有什么用
            //------报检这边不一样的字段--------
            //39木质包装
            //40报检申报单位
            //            JObject json = JsonConvert.DeserializeObject<JObject>(param);
            //            //foreach (JProperty jp in json.Properties())
            //            //{ 

            //            //}
            //            string sql = @"insert into List_Order(ID,FWONO,FOCUSTOMSNO,FOINSPECTNO,BUSITYPE) VALUES(LIST_ORDER_ID.Nextval,'{0}','{1}','{2}',
            //            '{3}')";
            //            sql = string.Format(sql, json.Value<string>("FWONO"), json.Value<string>("FOCUSTOMSNO"), json.Value<string>("FOINSPECTNO"),
            //            json.Value<string>("BUSITYPE"));
            //            DBMgr.ExecuteNonQuery(sql);

            //return "true";

            MO.MSG_TYPE = "S";
            MO.MSG_ID = 1;
            MO.MSG_TXT = "测试";
            MO.CODE = "12312312312";
            Msgobj[] _MO = new Msgobj[1];
            _MO[0] = MO;
            return _MO;

            //return Json(new { Success = true, Message = "成功" }, JsonRequestBehavior.AllowGet);




            //new { Success = true, Message = "成功" }
            //return Json(new { Success = true, Message = "成功" }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult Json(object p, JsonRequestBehavior jsonRequestBehavior)
        {
            throw new NotImplementedException();
        }
    }
}
