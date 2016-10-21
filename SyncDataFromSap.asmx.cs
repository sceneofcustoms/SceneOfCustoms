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
            //IDatabase db = SeRedis.redis.GetDatabase();//先将报文数据保存至缓存数据库
            //db.ListRightPush("SyncDataFromSap", ld.ToString());
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            int SaveSap_Res = SavaSapFoo(ld);
            if (SaveSap_Res == 0)
            {
                MSList.Add(IFS.set_MObj("E", "保存指令失败"));
                IFS.save_log(MSList, ld[0].CODE, "1");
                return MSList;
            }

            if (ld.Count > 0)
            {
                MSList = IFS.CheckData(ld);

                if (MSList.Count <= 0)
                {
                    int Order_Res = IFS.XCOrderData(ld);
                    if (Order_Res == 1)
                    {
                        MSList.Add(IFS.set_MObj("S", "保存成功"));
                        IFS.SaveDZOrder(ld[0].CODE);
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
            return MSList;
        }


        // 保存指令 方法
        private int SavaSapFoo(List<OrderEn> ld)
        {
            int Order_Res = 0;
            DateTime dt = DateTime.Now;
            string Nowtime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string sql = "";
            foreach (OrderEn o in ld)
            {

                sql = @"insert into LIST_SAPFOO(
                      ID,TIME,
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
                      INSPUNITCODE,TURNPRENO,INVOICENO,FGOODSUNIT,ALLOWDECLARE,CODE,ONLYCODE
                       ) VALUES(LIST_SAPFOO_ID.Nextval,sysdate,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                    '{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}',
                    '{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}',
                    '{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}'
                    )";
                try
                {
                    sql = string.Format(sql,
    o.BUSITYPE, o.CODE, o.FOONO, o.TOTALNO,
    o.DIVIDENO, o.GOODSNUM, o.GOODSWEIGHT, o.SGOODSUNIT,
    o.PACKKIND, o.REPWAYID, o.DECLWAY, o.TRADEWAYCODES,
    o.CUSNO, o.CUSTOMDISTRICTCODE, o.PORTCODE, o.SPECIALRELATIONSHIP,
    o.PRICEIMPACT, o.PAYPOYALTIES, o.REPUNITCODE, o.CREATEUSERNAME,
    o.CREATETIME, o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT,
    o.ENTRUSTTYPEID, o.GOODSXT, o.BUSIUNITNAME, o.GOODSTYPEID,
    o.LADINGBILLNO, o.ISPREDECLARE, o.ENTRUSTREQUEST, o.CONTRACTNO,
    o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO, o.MANIFEST, o.WOODPACKINGID,
    o.WEIGHTCHECK, o.ISWEIGHTCHECK, o.SHIPNAME, o.FILGHTNO,
    o.INSPUNITNAME, o.TURNPRENO, o.INVOICENO, o.FGOODSUNIT, o.ALLOWDECLARE, o.ORDERCODE, Nowtime
    );
                    Order_Res = DBMgr.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            return Order_Res;
        }
    }
}
