using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SceneOfCustoms
{
    /// <summary>
    /// SyncDataFromSap 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SyncDataFromSap : System.Web.Services.WebService
    {

        [WebMethod(Description = @"param参数说明:为json格式字符串<br>1 凭证类型(LICENSETYPE)<br>2 FWO订单号(FWONO)<br>3 FO报关服务指令号
       (FOCUSTOMSNO)<br>4 总单号(TOTALNO)<br>5 分单号(DIVIDENO)<br>6 件数(GOODSNUM)<br>7毛重(GOODSGW)<br>8 净重(GOODSNW)<br>9 经营单位代码(BUSIUNITCODE)
        <br>10 经营单位名称(BUSIUNITNAME)<br>11 包装种类(PACKKIND)<br>12 申报方式(REPWAYID)<br>13报关方式(DECLWAY)<br>14贸易方式(TRADEWAYCODES)
        <br>15客户自编号(CUSNO)<br>16进/出口岸(PORTCODE)<br>17特殊关系确认(SPECIALRELATIONSHIP)<br>18价格影响确认(PRICEIMPACT)
        <br>19支付特许权使用费确认(PAYPOYALTIES)<br>20报关申报单位代码(REPUNITCODE)<br>21报关申报单位名称(REPUNITNAME)<br>22委托人员(SUBMITUSERNAME)
        <br>23委托时间(SUBMITTIME)<br>24委托电话(SUBMITUSERPHONE)<br>25运抵编号(ARRIVEDNO)<br>26集装箱车号信息(CONTAINERTRUCK)<br>27实际件数(ACTUALGOODSNUM)
        <br>28实际毛重(ACTUALGOODSGW)<br>29货物类型(整箱或散箱用中文标记)(GOODSTYPE)<br>30报关提单号(SECONDLADINGBILLNO)<")]
        public string SyncData(string param)
        {
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
            JObject json = JsonConvert.DeserializeObject<JObject>(param);
            //foreach (JProperty jp in json.Properties())
            //{ 

            //}
            string sql = @"insert into List_Order(ID,FWONO,FOCUSTOMSNO,FOINSPECTNO,BUSITYPE) VALUES(LIST_ORDER_ID.Nextval,'{0}','{1}','{2}',
            '{3}')";
            sql = string.Format(sql, json.Value<string>("FWONO"), json.Value<string>("FOCUSTOMSNO"), json.Value<string>("FOINSPECTNO"),
            json.Value<string>("BUSITYPE"));
            DBMgr.ExecuteNonQuery(sql);
            IDatabase db = SeRedis.redis.GetDatabase();//先将报文数据保存至缓存数据库
            string type = string.IsNullOrEmpty(json.Value<string>("FOCUSTOMSNO")) ? "报检" : "报关";
            db.ListRightPush("SyncDataFromSap", "{data:" + param + ",createtime:'" + DateTime.Now + "',from:'SAP',type:'" + type + "'}");
            return "true";
        }
    }
}
