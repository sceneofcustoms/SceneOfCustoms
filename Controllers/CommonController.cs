using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public ActionResult Index()
        {
            return View();
        }
        public string CurrentUser()
        {
            JObject json_user = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            return json_user.GetValue("REALNAME") + "";          
        }
        public void ImportOrder_AirIn()
        {
            string sql = "select t.*, t.rowid from list_order t where t.busitype='11'";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            { 
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,CUSTOMDISTRICTNAME,REPUNITCODE,
                REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,SUBMITUSERID,
                SUBMITUSERNAME,SUBMITUSERPHONE,CSPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND,
                BUSIKIND,ORDERWAY,CLEARUNIT,CLEARUNITNAME,CREATETIME,BUSISHORTCODE,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,BUSISHORTNAME,
                SCENEDECLAREID,SCENEINSPECTID,SUBMITTIME
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',
                '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',
                '{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}',sysdate,'{45}','{46}','{47}','{48}','{49}','{50}',
                '{51}',{52})";
                sql = string.Format(sql, "11", dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"],
                   dr["GOODSNUM"], dr["GOODSWEIGHT"], dr["WOODPACKINGID"], dr["CLEARANCENO"],
                  dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],
                  dr["CUSTOMDISTRICTNAME"],dr["REPUNITCODE"], dr["REPUNITCODE"],
                   dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"], dr["INSPUNITCODE"],
                   dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["ID"], dr["CREATEUSERNAME"],
                   dr["STATUS"],dr["SUBMITUSERID"],dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"],
                   dr["CSPHONE"],dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"],
                   dr["PACKKIND"], "001", "1",dr["CLEARUNIT"], dr["CLEARUNITNAME"], dr["BUSISHORTCODE"],
                   dr["SPECIALRELATIONSHIP"],dr["PRICEIMPACT"],dr["PAYPOYALTIES"],
                   dr["BUSISHORTNAME"], dr["SCENEDECLAREID"], dr["SCENEINSPECTID"], "to_date('" + dr["SUBMITTIME"] + "','yyyy-MM-dd HH24:mi:ss')");
                DBMgr.ExecuteNonQuery(sql);
            } 
        }
        public void ImportOrder_GuoNei()
        { 
            string sql = "select t.*, t.rowid from list_order t where (t.busitype='41' or t.busitype='40') and createtime>to_date('2016/8/1 00:00:00','yyyy-mm-dd hh24:mi:ss')";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,
                CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,
                CUSTOMDISTRICTNAME,REPUNITCODE,REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,
                INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,
                SUBMITUSERID,SUBMITUSERNAME,SUBMITUSERPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,
                TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND, CLEARUNIT,
                CLEARUNITNAME,CREATETIME,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,SCENEDECLAREID,
              SCENEINSPECTID,CORRESPONDNO,ASSOCIATENO,IETYPE
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}',
              '{5}','{6}','{7}','{8}','{9}','{10}',
              '{11}','{12}','{13}','{14}','{15}','{16}',
              '{17}','{18}','{19}','{20}','{21}','{22}',
              '{23}','{24}','{25}','{26}','{27}','{28}',
              '{29}','{30}','{31}','{32}','{33}','{34}', 
              '{35}', '{36}','{37}','{38}','{39}','{40}',
'{41}',sysdate, '{42}','{43}','{44}','{45}',
              '{46}','{47}','{48}','{49}')";

               
                sql = string.Format(sql, dr["BUSITYPE"], dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"],dr["GOODSNUM"], dr["GOODSWEIGHT"],
                  dr["WOODPACKINGID"], dr["CLEARANCENO"], dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],                 
                  dr["CUSTOMDISTRICTNAME"], dr["REPUNITCODE"], dr["REPUNITCODE"],dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"],
                   dr["INSPUNITCODE"], dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["CREATEUSERID"], dr["CREATEUSERNAME"],  dr["STATUS"],  
                  dr["SUBMITUSERID"], dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"],  dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],                 
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"], dr["PACKKIND"],dr["CLEARUNIT"],
                   dr["CLEARUNITNAME"],  dr["SPECIALRELATIONSHIP"], dr["PRICEIMPACT"], dr["PAYPOYALTIES"],dr["SCENEDECLAREID"],

                   dr["SCENEINSPECTID"], dr["CORRESPONDNO"], dr["ASSOCIATENO"], dr["IETYPE"]);
                   
                 
                DBMgr.ExecuteNonQuery(sql);
            }
        }
        public void ImportOrder()
        {
            string sql = "select t.*, t.rowid from list_order t where t.busitype='10' ";
            DataTable dt = DB_BaseData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                sql = @"INSERT INTO LIST_ORDER (
                ID,BUSITYPE,CODE,CUSNO,BUSIUNITCODE,BUSIUNITNAME,
                CONTRACTNO,TOTALNO,DIVIDENO,TURNPRENO,GOODSNUM,GOODSWEIGHT,
                WOODPACKINGID,CLEARANCENO,LAWCONDITION,ENTRUSTTYPEID,REPWAYID,CUSTOMDISTRICTCODE,
                CUSTOMDISTRICTNAME,REPUNITCODE,REPUNITNAME,DECLWAY,PORTCODE,PORTNAME,
                INSPUNITCODE,INSPUNITNAME,ENTRUSTREQUEST,CREATEUSERID,CREATEUSERNAME,STATUS,
                SUBMITUSERID,SUBMITUSERNAME,SUBMITUSERPHONE,CUSTOMERCODE,CUSTOMERNAME,DECLCARNO,
                TRADEWAYCODES,TRADEWAYCODES1,GOODSGW,GOODSNW,PACKKIND, CLEARUNIT,
                CLEARUNITNAME,CREATETIME,SPECIALRELATIONSHIP,PRICEIMPACT,PAYPOYALTIES,SCENEDECLAREID,
              SCENEINSPECTID,CORRESPONDNO,ASSOCIATENO,IETYPE
                ) VALUES (LIST_ORDER_id.Nextval,'{0}','{1}','{2}','{3}','{4}',
              '{5}','{6}','{7}','{8}','{9}','{10}',
              '{11}','{12}','{13}','{14}','{15}','{16}',
              '{17}','{18}','{19}','{20}','{21}','{22}',
              '{23}','{24}','{25}','{26}','{27}','{28}',
              '{29}','{30}','{31}','{32}','{33}','{34}', 
              '{35}', '{36}','{37}','{38}','{39}','{40}',
'{41}',sysdate, '{42}','{43}','{44}','{45}',
              '{46}','{47}','{48}','{49}')";


                sql = string.Format(sql, dr["BUSITYPE"], dr["CODE"], dr["CUSNO"], dr["BUSIUNITCODE"], dr["BUSIUNITNAME"],
                   dr["CONTRACTNO"], dr["TOTALNO"], dr["DIVIDENO"], dr["TURNPRENO"], dr["GOODSNUM"], dr["GOODSWEIGHT"],
                  dr["WOODPACKINGID"], dr["CLEARANCENO"], dr["LAWCONDITION"], dr["ENTRUSTTYPEID"], dr["REPWAYID"], dr["CUSTOMDISTRICTCODE"],
                  dr["CUSTOMDISTRICTNAME"], dr["REPUNITCODE"], dr["REPUNITCODE"], dr["DECLWAY"], dr["PORTCODE"], dr["PORTNAME"],
                   dr["INSPUNITCODE"], dr["INSPUNITCODE"], dr["ENTRUSTREQUEST"], dr["CREATEUSERID"], dr["CREATEUSERNAME"], dr["STATUS"],
                  dr["SUBMITUSERID"], dr["SUBMITUSERNAME"], dr["SUBMITUSERPHONE"], dr["CUSTOMERCODE"], dr["CUSTOMERNAME"], dr["DECLCARNO"],
                   dr["TRADEWAYCODES"], dr["TRADEWAYCODES1"], dr["GOODSGW"], dr["GOODSNW"], dr["PACKKIND"], dr["CLEARUNIT"],
                   dr["CLEARUNITNAME"], dr["SPECIALRELATIONSHIP"], dr["PRICEIMPACT"], dr["PAYPOYALTIES"], dr["SCENEDECLAREID"],

                   dr["SCENEINSPECTID"], dr["CORRESPONDNO"], dr["ASSOCIATENO"], dr["IETYPE"]);


                DBMgr.ExecuteNonQuery(sql);
            }
        }
    }
}