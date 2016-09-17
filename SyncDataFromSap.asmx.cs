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

        [WebMethod]
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
            //8 收货方/发货方
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
            //23报关车号
            //24实际件数
            //25实际毛重
            //26委托方式  这个貌似没有什么用
            //27货物类型(整箱|散箱)
            //28车号 取明细第一个
            //29集装箱号 取明细第一个
            //30集装箱|报关车号详细 CONTAINERDETAIL  以json数组形式保存
            //31货物形态   不知道是有什么用
            //32经营单位
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
