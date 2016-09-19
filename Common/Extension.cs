
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SceneOfCustoms.Common
{
    public static class Extension
    {
        public static string ToSHA1(this string value)
        {
            string result = string.Empty;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] array = sha1.ComputeHash(Encoding.Unicode.GetBytes(value));
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString("x2");
            }
            return result;
        }
        public static JObject Get_UserInfo(string account)
        {
            IDatabase db = SeRedis.redis.GetDatabase();
            string result = "";
            if (db.KeyExists(account))
            {
                result = db.StringGet(account);
            }
            else
            {
                string sql = @"select u.* from SYS_USER u where u.name ='" + account + "'";
                DataTable dt = DBMgr.GetDataTable(sql);
                IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
                iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                string jsonstr = JsonConvert.SerializeObject(dt, iso);
                jsonstr = jsonstr.Replace("[", "").Replace("]", "");
                db.StringSet(account, jsonstr);
                result = jsonstr;
            }
            return (JObject)JsonConvert.DeserializeObject(result);
        }

        //获取订单CODE
        public static string getOrderCode()
        {
            string sql = "", sql1 = "";

            sql = "select sys_code_id.nextval from dual";
            string code = string.Empty;
            try
            {
                DataTable dt = DBMgr.GetDataTable(sql);
                int CodeId = int.Parse(dt.Rows[0][0].ToString());
                sql1 = "select YEARMONTH, code from sys_code where id=" + CodeId;
                DataTable dt1 = DBMgr.GetDataTable(sql1);
                while (dt1.Rows.Count <= 0)
                {
                    dt = DBMgr.GetDataTable(sql);
                    CodeId = int.Parse(dt.Rows[0][0].ToString());
                    sql1 = "select YEARMONTH, code from sys_code where id=" + CodeId;
                    dt1 = DBMgr.GetDataTable(sql1);
                }
                code = dt1.Rows[0][0].ToString() + dt1.Rows[0][1].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            return code;
        }

        //集装箱及报关车号列表更新
        public static void predeclcontainer_update(string ordercode, string containertruck)
        {

            DBMgr.ExecuteNonQuery("delete from list_predeclcontainer where ORDERCODE = '" + ordercode + "'");
            if (!string.IsNullOrEmpty(containertruck))
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(containertruck);
                for (int i = 0; i < ja.Count; i++)
                {
                    string sql = @"insert into list_predeclcontainer(ID,ORDERCODE,CONTAINERORDER,CONTAINERNO,CONTAINERSIZE,CONTAINERSIZEE,CONTAINERWEIGHT,
                    CONTAINERTYPE,HSCODE,FORMATNAME,CDCARNO,CDCARNAME,UNITNO,ELESHUT) values(LIST_PREDECLCONTAINER_id.Nextval,'{0}','{1}','{2}','{3}',
                    '{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
                    sql = string.Format(sql, ordercode, i, ja[i].Value<string>("CONTAINERNO"), ja[i].Value<string>("CONTAINERSIZE"), ja[i].Value<string>("CONTAINERSIZEE"),
                    ja[i].Value<string>("CONTAINERWEIGHT"), ja[i].Value<string>("CONTAINERTYPE"), ja[i].Value<string>("HSCODE"), ja[i].Value<string>("FORMATNAME"),
                    ja[i].Value<string>("CDCARNO"), ja[i].Value<string>("CDCARNAME"), ja[i].Value<string>("UNITNO"), ja[i].Value<string>("ELESHUT"));
                    DBMgr.ExecuteNonQuery(sql);
                }
            }
        }

        //订单新增或者更新时对随附文件表的操作  非国内业务都会用到  封装by panhuaguo 2016-08-03
        //originalfileids 这个字符串存储的是订单修改时原始随附文件id用逗号分隔

        //提交后订单修改时记录字段信息变更情况
        public static void Insert_FieldUpdate_History(string ordercode, JObject json_new, JObject json_user, string busitype)
        {
            //国内订单已受理后前端为空时能修改的字段
            string JsonFieldComments = "";
            switch (busitype)
            {
                case "40"://国内出口
                case "41"://国内进口
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',GOODSNUM:'件数',PACKKIND:'包装',GOODSGW:'毛重',GOODSNW:'净重',CONTRACTNO:'合同号',FILINGNUMBER:'账册备案号',
                        LAWCONDITION:'法检状况',CLEARANCENO:'通关单号',ASSOCIATEPEDECLNO:'出口报关单',REPUNITCODE:'报关申报单位',INSPUNITCODE:'报检申报单位',ENTRUSTREQUEST:'需求备注'}";
                    break;
                case "10"://空运出口
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',TOTALNO:'总单号',DIVIDENO:'分单号',GOODSNUM:'件数',PACKKIND:'包装',GOODSGW:'毛重'',GOODSNW:'净重',CONTRACTNO:'合同号',ARRIVEDNO:'运抵编号',TURNPRENO:'转关预录号',
                        CLEARANCENO:'通关单号',DECLCARNO:'报关车号',ENTRUSTREQUEST:'需求备注',LAWCONDITION:'法检状况',CHECKEDGOODSNUM:'确认件数',CHECKEDWEIGHT:'确认毛重',
                        WEIGHTCHECK:'需重量确认',ISWEIGHTCHECK:'重量确认',SELFCHECK:'需自审',ISSELFCHECK:'自审确认'}";
                    break;
                case "11"://空运进口                    
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',TOTALNO:'总单号',GOODSNUM:'件数',PACKKIND:'包装',GOODSNW:'净重',CONTRACTNO:'合同号',TURNPRENO:'转关预录号',
                        CLEARANCENO:'通关单号',DECLCARNO:'报关车号',ENTRUSTREQUEST:'需求备注',LAWCONDITION:'法检状况'}";
                    break;
                case "20"://海运出口       
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',PACKKIND:'包装',GOODSNW:'净重',CONTRACTNO:'合同号',SECONDLADINGBILLNO:'提单号',ARRIVEDNO:'运抵编号',
                        LAWCONDITION:'法检状况',CLEARANCENO:'通关单号',CONTAINERNO:'集装箱号',DECLCARNO:'报关车号',TURNPRENO:'转关预录号',ENTRUSTREQUEST:'需求备注'}";
                    break;
                case "21"://海运进口     
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',PACKKIND:'包装',GOODSNW:'净重',CONTRACTNO:'合同号',SECONDLADINGBILLNO:'国检提单号',SECONDLADINGBILLNO:'海关提单号',TRADEWAYCODES_ZS:'贸易方式',
                        TURNPRENO:'转关预录号',WOODPACKINGID:'木质包装',CLEARANCENO:'通关单号',LAWCONDITION:'法检状况',CONTAINERNO:'集装箱号',DECLCARNO:'报关车号',ENTRUSTREQUEST:'需求备注'}";
                    break;
                case "30"://陆运出口       
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',FILGHTNO:'航次号',CONTRACTNO:'合同号',PACKKIND:'包装',GOODSNW:'净重',ARRIVEDNO:'运抵编号',LAWCONDITION:'法检状况',
                        CLEARANCENO:'通关单号',CONTAINERNO:'集装箱号',DECLCARNO:'报关车号',TURNPRENO:'转关预录号',TOTALNO:'总单号',DIVIDENO:'分单号',ENTRUSTREQUEST:'需求备注'}";
                    break;
                case "31"://陆运进口     
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',FILGHTNO:'航次号',DIVIDENO:'分单号',GOODSNUM:'件数',PACKKIND:'包装',GOODSNW:'净重',CONTRACTNO:'合同号',MANIFEST:'载货清单号',
                        CLEARANCENO:'通关单号',LAWCONDITION:'法检状况',CONTAINERNO:'集装箱号',DECLCARNO:'报关车号',ENTRUSTREQUEST:'需求备注'}";
                    break;
                case "50"://特殊区域出口                         
                case "51"://特殊区域进口      
                    JsonFieldComments = @"{SPECIALRELATIONSHIP:'特殊关系确认',PRICEIMPACT:'价格影响确认',PAYPOYALTIES:'支付特许权使用费确认',
                        CUSNO:'客户编号',PACKKIND:'包装',GOODSNW:'净重',CONTRACTNO:'合同号',TURNPRENO:'对方转关号',LAWCONDITION:'法检状况',CLEARANCENO:'通关单号',
                        GOODSTYPEID:'货物类型',CONTAINERNO:'集装箱号',DECLCARNO:'报关车号',ENTRUSTREQUEST:'需求备注',BUSITYPE:'业务类型'}";
                    break;
            }

            string sql = "select * from list_order where CODE = '" + ordercode + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string ori_json = JsonConvert.SerializeObject(dt, iso).TrimStart('[').TrimEnd(']');
            JObject json_key = JsonConvert.DeserializeObject<JObject>(JsonFieldComments);
            JObject json_ori = JsonConvert.DeserializeObject<JObject>(ori_json);
            foreach (JProperty jp in json_key.Properties())
            {
                if (jp.Name == "SPECIALRELATIONSHIP" || jp.Name == "PRICEIMPACT" || jp.Name == "PAYPOYALTIES" || jp.Name == "SPECIALRELATIONSHIP" || jp.Name == "LAWCONDITION"
                    || jp.Name == "WEIGHTCHECK" || jp.Name == "ISWEIGHTCHECK" || jp.Name == "SELFCHECK" || jp.Name == "ISSELFCHECK")
                {
                    if (!json_ori.Value<bool>(jp.Name) && json_new.Value<string>(jp.Name) == "on")
                    {
                        sql = @"insert into list_updatehistory(id,ORDERCODE,USERID,UPDATETIME,NEWFIELD,NAME,CODE,FIELD,FIELDNAME,TYPE) values
                        (LIST_UPDATEHISTORY_ID.nextval,'{0}','{1}',sysdate,'{2}','{3}','{4}','{5}','{6}','1')";
                        sql = string.Format(sql, ordercode, json_user.Value<string>("ID"), json_new.Value<string>(jp.Name), json_user.Value<string>("NAME"), ordercode, jp.Name, jp.Value);
                        DBMgr.ExecuteNonQuery(sql);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(json_ori.Value<string>(jp.Name)) && !string.IsNullOrEmpty(json_new.Value<string>(jp.Name)))
                    {
                        sql = @"insert into list_updatehistory(id,ORDERCODE,USERID,UPDATETIME,NEWFIELD,NAME,CODE,FIELD,FIELDNAME,TYPE) values
                        (LIST_UPDATEHISTORY_ID.nextval,'{0}','{1}',sysdate,'{2}','{3}','{4}','{5}','{6}','1')";
                        sql = string.Format(sql, ordercode, json_user.Value<string>("ID"), json_new.Value<string>(jp.Name), json_user.Value<string>("NAME"), ordercode, jp.Name, jp.Value);
                        DBMgr.ExecuteNonQuery(sql);
                    }
                }
            }
        }

        public static string GetPageSql(string tempsql, string order, string asc, ref int totalProperty, int start, int limit)
        {
            string sql = "select count(1) from ( " + tempsql + " )";
            totalProperty = Convert.ToInt32(DBMgr.GetDataTable(sql).Rows[0][0]);
            string pageSql = @"SELECT * FROM ( SELECT tt.*, ROWNUM AS rowno FROM ({0} ORDER BY {1} {2}) tt WHERE ROWNUM <= {4}) table_alias WHERE table_alias.rowno >= {3}";
            pageSql = string.Format(pageSql, tempsql, order, asc, start + 1, limit);
            return pageSql;
        }


    }
}