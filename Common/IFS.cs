using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace SceneOfCustoms.Common
{
    public class IFS
    {

        public static string XCBUSINAME;

        public static string IFINSERT = "0";


        public static void SendWuMao(string ORDERCODE, string ONLYCODE)
        {
            string sql = "SELECT * FROM LIST_WUMAO WHERE ORDERCODE = '" + ORDERCODE + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            XmlDocument xmlDoc = new XmlDocument();
            //string path = Server.MapPath("/tem/tem.xml");
            string path = @"D:/AppFile/FeiliCustoms/Webroot/tem/tem.xml";
            xmlDoc.Load(path);
            XmlElement node;
            if (dt.Rows.Count > 0)
            {
                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/GATEPASS_NO");
                node.InnerText = dt.Rows[0]["GATEPASS_NO"] + "";
                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/CORP_NO");
                node.InnerText = dt.Rows[0]["ORDERCODE"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/GOODS_NATURE_ID");
                node.InnerText = dt.Rows[0]["GOODS_NATURE_ID"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/PROVIDER_NAME");
                node.InnerText = dt.Rows[0]["PROVIDER_NAME"] + "";

                //if(){

                //DateTime.ParseExact(o[0].CREATETIME, "yyyyMMddHHmmss.fffffff", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss")
                //}
                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/D_DATE");
                //node.InnerText = dt.Rows[0]["D_DATE"] + "";
                node.InnerText = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/I_E_FALG_TYPE");
                node.InnerText = dt.Rows[0]["I_E_FALG_TYPE"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRANSPORT_CODE");
                node.InnerText = dt.Rows[0]["TRANSPORT_CODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRANSPORT_NAME");
                node.InnerText = dt.Rows[0]["TRANSPORT_NAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/APPCOMPANY");
                node.InnerText = dt.Rows[0]["APPCOMPANY"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/APPCOMPANY_NAME");
                node.InnerText = dt.Rows[0]["APPCOMPANY_NAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/APPCIQID");
                node.InnerText = dt.Rows[0]["APPCIQID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/BIZ_TYPE_ID");
                node.InnerText = dt.Rows[0]["BIZ_TYPE_ID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/BIZ_TYPE_NAME");
                node.InnerText = dt.Rows[0]["BIZ_TYPE_NAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRADE_CODE");
                node.InnerText = dt.Rows[0]["TRADE_CODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRADE_NAME");
                node.InnerText = dt.Rows[0]["TRADE_NAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/CONSIGNEE_CODE");
                node.InnerText = dt.Rows[0]["CONSIGNEE_CODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/CONSIGNEE_NAME");
                node.InnerText = dt.Rows[0]["CONSIGNEE_NAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRADE_CODE_IN");
                node.InnerText = dt.Rows[0]["TRADE_CODE_IN"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRADE_NAME_IN");
                node.InnerText = dt.Rows[0]["TRADE_NAME_IN"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/PACK_NO");
                node.InnerText = dt.Rows[0]["PACK_NO"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/GROSS_WT");
                node.InnerText = dt.Rows[0]["GROSS_WT"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/NET_WT");
                node.InnerText = dt.Rows[0]["NET_WT"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRAFFICTYPE");
                node.InnerText = dt.Rows[0]["TRAFFICTYPE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/BILL_TYPE");
                node.InnerText = dt.Rows[0]["BILL_TYPE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/ENTRY_ID_OUT");
                node.InnerText = dt.Rows[0]["ENTRY_ID_OUT"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/APPCIQTYPE");
                node.InnerText = dt.Rows[0]["APPCIQTYPE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/WRAP_TYPE_ID");
                node.InnerText = dt.Rows[0]["WRAP_TYPE_ID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/MAINCODE");
                node.InnerText = dt.Rows[0]["MAINCODE"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/SUBCODE");
                node.InnerText = dt.Rows[0]["SUBCODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/MANUAL_NO");
                node.InnerText = dt.Rows[0]["MANUAL_NO"] + "";



                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/OUT_CODE");
                node.InnerText = dt.Rows[0]["OUT_CODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRANSFER_NO");
                node.InnerText = dt.Rows[0]["TRANSFER_NO"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/GOODS_TYPE_LY");
                node.InnerText = dt.Rows[0]["GOODS_TYPE_LY"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/ISHDZ");
                node.InnerText = dt.Rows[0]["ISHDZ"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/TRADETYPE");
                node.InnerText = dt.Rows[0]["TRADETYPE"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/OUT_GOODS_TYPE_LY");
                node.InnerText = dt.Rows[0]["OUT_GOODS_TYPE_LY"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/LY_BIZ_TYPE_ID");
                node.InnerText = dt.Rows[0]["LY_BIZ_TYPE_ID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/GOODS_TYPE_ID");
                node.InnerText = dt.Rows[0]["GOODS_TYPE_ID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/LYTYPE_ID");
                node.InnerText = dt.Rows[0]["LYTYPE_ID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/AREA_CODE");
                node.InnerText = dt.Rows[0]["AREA_CODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/MEANSOFTRANSPORTNAME");
                node.InnerText = dt.Rows[0]["MEANSOFTRANSPORTNAME"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/MEANSOFTRANSPORTID");
                node.InnerText = dt.Rows[0]["MEANSOFTRANSPORTID"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/REMARK");
                node.InnerText = dt.Rows[0]["REMARK"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/FULL_NO_ZD");
                node.InnerText = dt.Rows[0]["FULL_NO_ZD"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/OUT_TRAF_MODE");
                node.InnerText = dt.Rows[0]["OUT_TRAF_MODE"] + "";

                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/IS_BLR");
                node.InnerText = dt.Rows[0]["IS_BLR"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/SEND_USER");
                node.InnerText = dt.Rows[0]["SEND_USER"] + "";


                node = (XmlElement)xmlDoc.SelectSingleNode("PASS_HEAD/SEND_TIME");
                node.InnerText = dt.Rows[0]["SEND_TIME"] + "";
            }

            xmlDoc.Save(path);

            //MessageQueue mq = new MessageQueue("FormatName:DIRECT=TCP:221.224.206.253\\Private$\\etf");
            //Message msg = new Message();
            //msg.Body = xmlDoc.ToString();
            //msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });

            //MessageQueue mq = new MessageQueue("FormatName:DIRECT=TCP:221.224.206.245\\Private$\\DataCenter_SZ");

            MessageQueue mq = new MessageQueue("FormatName:DIRECT=TCP:58.210.121.35\\Private$\\DataCenter_KS");

            Message msg = new Message();
            ////ZYDFL_S_系统名称_十个0_十个0_企业内部编号_GUID.xml old

            //作业单 ZYDFL_S_FL_申报单位十位编码_十个0_企业内部编号_GUID.xml new

            string guid = Guid.NewGuid().ToString();
            string Label = "ZYDFL_S_FL_" + dt.Rows[0]["APPCOMPANY"] + "_0000000000_" + dt.Rows[0]["ORDERCODE"] + "_" + guid + ".xml";

            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                msg.BodyStream = fstream;
                msg.Label = Label;
                mq.Send(msg, MessageQueueTransactionType.Single);
                sql = "update LIST_WUMAO set STATUS='1' where ordercode='" + ORDERCODE + "'";
                DBMgr.ExecuteNonQuery(sql);
            }

        }



        //推送到单证的数据
        public static void SaveDZOrder(string FWO, string ONLYCODE)
        {
            ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
            ServiceReference1.OrderEn DZOrder;
            List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();
            ServiceReference1.ContainerEn ContainerEn;
            ServiceReference1.FileEn FileEn;
            List<ServiceReference1.FileEn> FileEnList = new List<ServiceReference1.FileEn>();
            List<ServiceReference1.ContainerEn> ContainerEnList = new List<ServiceReference1.ContainerEn>();
            DataTable dt;
            DataTable dtCon;
            DataTable dtFile;
            string sql = "select * from list_order where ONLYCODE ='" + ONLYCODE + "'";
            dt = DBMgr.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DZOrder = new ServiceReference1.OrderEn();
                DZOrder.DOCSERVICECODE = "GWYKS";
                DZOrder.DOCSERVICENAME = "关务云昆山";
                DZOrder.CUSTOMERCODE = "KSJSBGYXGS";
                DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";
                DZOrder.CUSNO = dt.Rows[i]["CODE"] + "";
                DZOrder.FIRSTLADINGBILLNO = dt.Rows[i]["FIRSTLADINGBILLNO"] + ""; //海关提单号
                DZOrder.SECONDLADINGBILLNO = dt.Rows[i]["SECONDLADINGBILLNO"] + ""; //国检提单号
                DZOrder.ENTRUSTTYPE = dt.Rows[i]["ENTRUSTTYPEID"] + "";
                DZOrder.BUSITYPE = dt.Rows[i]["BUSITYPE"] + "";
                DZOrder.REPWAYID = dt.Rows[i]["REPWAYID"] + "";
                DZOrder.CUSTOMAREACODE = dt.Rows[i]["CUSTOMDISTRICTCODE"] + "";
                DZOrder.DECLWAY = dt.Rows[i]["DECLWAY"] + "";
                DZOrder.BUSIUNITCODE = dt.Rows[i]["BUSIUNITCODE"] + "";
                DZOrder.BUSIUNITNAME = dt.Rows[i]["BUSIUNITNAME"] + "";
                if (!string.IsNullOrEmpty(dt.Rows[i]["GOODSNUM"] + ""))
                {
                    DZOrder.GOODSNUM = Decimal.Parse(dt.Rows[i]["GOODSNUM"] + "");
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["GOODSWEIGHT"] + ""))
                {
                    DZOrder.GOODSGW = Decimal.Parse(dt.Rows[i]["GOODSWEIGHT"] + "");
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["CHECKEDWEIGHT"] + ""))
                {
                    DZOrder.GOODSNW = Decimal.Parse(dt.Rows[i]["CHECKEDWEIGHT"] + "");
                }
                DZOrder.PACKKINDNAME = dt.Rows[i]["PACKKIND"] + "";
                DZOrder.GOODSTYPEID = dt.Rows[i]["GOODSTYPEID"] + "";
                //贸易方式
                //DZOrder.TRADEWAYCODE = dt.Rows[i]["TRADEWAYCODES"] + "";
                DZOrder.ORDERREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
                DZOrder.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
                DZOrder.REPUNITNAME = dt.Rows[i]["REPUNITNAME"] + "";
                DZOrder.INSPREPCODE = dt.Rows[i]["INSPUNITCODE"] + "";
                DZOrder.INSPREPNAME = dt.Rows[i]["INSPUNITNAME"] + "";
                DZOrder.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
                DZOrder.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
                DZOrder.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
                DZOrder.PORTNAME = dt.Rows[i]["PORTCODE"] + "";
                DZOrder.CONTRACTNO = dt.Rows[i]["CONTRACTNO"] + "";

                //DZOrder.PORTNAME = dt.Rows[i]["PORTCODE"] + "";
                DZOrder.TRADEWAYNAME = dt.Rows[i]["TRADEWAYCODES"] + "";

                if (!string.IsNullOrEmpty(dt.Rows[i]["PAYPOYALTIES"] + ""))
                {
                    DZOrder.PAYPOYALTIES = Int32.Parse(dt.Rows[i]["PAYPOYALTIES"] + "");
                }

                if (!string.IsNullOrEmpty(dt.Rows[i]["PRICEIMPACT"] + ""))
                {
                    DZOrder.PRICEIMPACT = Int32.Parse(dt.Rows[i]["PRICEIMPACT"] + "");
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["SPECIALRELATIONSHIP"] + ""))
                {
                    DZOrder.SPECIALRELATIONSHIP = Int32.Parse(dt.Rows[i]["SPECIALRELATIONSHIP"] + "");
                }
                DZOrder.CORRESPONDNO = dt.Rows[i]["CORRESPONDNO"] + "";
                DZOrder.ASSOCIATENO = dt.Rows[i]["ASSOCIATENO"] + "";
                if (!string.IsNullOrEmpty(dt.Rows[i]["SUBMITTIME"] + ""))
                {
                    DZOrder.SUBMITTIME = Convert.ToDateTime(dt.Rows[i]["SUBMITTIME"]);
                }
                DZOrder.SUBMITUSERNAME = dt.Rows[i]["SUBMITUSERNAME"] + "";

                DZOrder.ARRIVEDNO = dt.Rows[i]["ARRIVEDNO"] + "";
                DZOrder.MANIFEST = dt.Rows[i]["MANIFEST"] + "";

                DZOrder.WOODPACKINGID = dt.Rows[i]["WOODPACKINGID"] + "";
                if (!string.IsNullOrEmpty(dt.Rows[i]["WEIGHTCHECK"] + ""))
                {
                    DZOrder.WEIGHTCHECK = Int32.Parse(dt.Rows[i]["WEIGHTCHECK"] + "");
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["ISWEIGHTCHECK"] + ""))
                {
                    DZOrder.ISWEIGHTCHECK = Int32.Parse(dt.Rows[i]["ISWEIGHTCHECK"] + "");
                }
                DZOrder.SHIPNAME = dt.Rows[i]["SHIPNAME"] + "";
                DZOrder.FILGHTNO = dt.Rows[i]["FILGHTNO"] + "";
                DZOrder.Number = Int32.Parse(dt.Rows[i]["SENDNUMBER"] + "");
                DZOrder.PLATFORMCODE = "xinguanwu";

                //集装箱
                sql = "select * from list_Declcontainertruck where ordercode='" + dt.Rows[i]["CODE"] + "'";
                dtCon = DBMgr.GetDataTable(sql);
                for (int j = 0; j < dtCon.Rows.Count; j++)
                {
                    ContainerEn = new ServiceReference1.ContainerEn();
                    ContainerEn.CDCARNAME = dtCon.Rows[j]["CDCARNAME"] + "";//沪BL1353
                    ContainerEn.CDCARNO = dtCon.Rows[j]["CDCARNO"] + "";//2200172079
                    ContainerEn.CONTAINERNO = dtCon.Rows[j]["CONTAINERNO"] + "";//TCLU5430888
                    if (dtCon.Rows[i]["CONTAINERTYPE"].ToString().Length == 4)
                    {
                        ContainerEn.CONTAINERTYPE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Substring(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//hou GP
                        ContainerEn.CONTAINERSIZE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Remove(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//qian   20
                    }
                    ContainerEnList.Add(ContainerEn);
                }
                DZOrder.ContainerList = ContainerEnList.ToArray();

                //文件
                sql = "select * from list_attachment where ordercode='" + dt.Rows[i]["CODE"] + "'";
                dtFile = DBMgr.GetDataTable(sql);
                string activeDir = @"C:\fileserver\";
                for (int j = 0; j < dtFile.Rows.Count; j++)
                {
                    FileEn = new ServiceReference1.FileEn();
                    FileEn.FileContent = GetFileData(activeDir + dtFile.Rows[j]["FILEPATH"] + "");
                    FileEn.FileFormat = ServiceReference1.FileFormatEnum.PDF;
                    FileEnList.Add(FileEn);
                }
                DZOrder.Files = FileEnList.ToArray();
                DZOrderList.Add(DZOrder);
            }

            string DZ_res = danzheng.SendOrderData(DZOrderList.ToArray());
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();

            if (DZ_res == "success")
            {
                string NewTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                MO.MSG_TYPE = "S";
                MO.MSG_TXT = "下发成功";
                sql = @"update list_order set IFSEND='1',SENDTIME =to_date('" + NewTime + "','yyyy-mm-dd hh24:mi:ss') where  ONLYCODE ='" + ONLYCODE + "'";
                DBMgr.ExecuteNonQuery(sql);
            }
            else
            {
                MO.MSG_TYPE = "E";
                MO.MSG_TXT = DZ_res;
            }
            MSList.Add(MO);
            save_log(MSList, FWO + "", "2");
        }

        public static byte[] GetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                //MessageBoxHelper.ShowPrompt(ex.Message);
                return new byte[0];
            }
            finally
            {
                if (fs != null)
                {

                    //关闭资源
                    fs.Close();
                }
            }
        }

        //保存发送单证数据
        public static int XCOrderData(List<OrderEn> ld, string ONLYCODE)
        {
            int res = 0;
            string ASSOCIATENO = "";
            string CORRESPONDNO = "";
            string ASS1 = "";
            string ASS2 = "";
            List<List<OrderEn>> GroupOrder = GroupByConFoo(ld);

            //关联号 如果4单 2单和4单关联号一样，下面在去判断 2单关联号
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {

                if (ListOrder[0].BUSITYPE.IndexOf("叠加保税") >= 0)
                {
                    if (GroupOrder.Count >= 2)
                    {
                        if (ListOrder[0].ENTRUSTTYPEID == "HUB 仓进")
                        {
                            CORRESPONDNO = "GF" + ListOrder[0].ORDERCODE;
                            ASS2 = "GL" + ListOrder[0].ORDERCODE;//2单关联号
                        }
                        if (ListOrder[0].ENTRUSTTYPEID == "HUB 仓出")
                        {
                            ASS1 = "GL" + ListOrder[0].ORDERCODE;//2单关联号
                        }
                    }
                    else
                    {
                        if (ListOrder[0].ENTRUSTTYPEID == "HUB 仓出" || ListOrder[0].ENTRUSTTYPEID == "HUB 仓进")
                        {
                            ASSOCIATENO = "GL" + ListOrder[0].ORDERCODE;
                        }
                    }
                }

                if (ListOrder[0].BUSITYPE.IndexOf("国内") >= 0)
                {
                    if (ListOrder[0].ENTRUSTTYPEID == "进口企业")
                    {
                        ASS1 = "GL" + ListOrder[0].ORDERCODE;//2单关联号
                    }
                }






            }

            DataTable dt;
            string sql = "";

            string NewTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            foreach (List<OrderEn> o in GroupOrder)
            {
                o[0].ONLYCODE = ONLYCODE;
                o[0].CREATETIME = NewTime;
                o[0].UPDATETIME = NewTime;
                //关联号

                if (GroupOrder.Count >= 2)
                {

                    if (o[0].BUSITYPE.IndexOf("国内") >= 0)
                    {
                        ASSOCIATENO = ASS1;
                    }


                    if (o[0].BUSITYPE.IndexOf("叠加保税") >= 0)
                    {
                        if (o[0].ENTRUSTTYPEID == "HUB 仓进" || o[0].ENTRUSTTYPEID == "出口企业")
                        {
                            ASSOCIATENO = ASS2;
                        }
                        else
                        {
                            ASSOCIATENO = ASS1;
                        }
                    }

                }
                //两单关联号，四单关联号
                o[0].ASSOCIATENO = ASSOCIATENO;
                o[0].CORRESPONDNO = CORRESPONDNO;

                //委托类型中文
                o[0].WTFS = o[0].ENTRUSTTYPEID;

                o[0].BUSINAME = o[0].BUSITYPE;

                //业务类型代码
                o[0].BUSITYPE = JudgeBusiType(o[0].BUSITYPE, o[0].ENTRUSTTYPEID);

                //发送下游状态
                o[0].IFSEND = "0";
                if (o[0].BUSITYPE == "20")
                {
                    sql = "select id,IFSEND from list_order where  code='" + o[0].ORDERCODE + "'";
                    dt = DBMgr.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["IFSEND"] + "" == "1" || dt.Rows[0]["IFSEND"] + "" == "2")
                        {
                            o[0].IFSEND = "2";
                        }
                    }
                }

                o[0].XCBUSINAME = IFS.XCBUSINAME;

                //委托类型代码 01,02,03。分别表示报关、报检、报关报检
                o[0].ENTRUSTTYPEID = GetENTRUSTTYPEID(o, o[0].BUSITYPE);


                //FWO订单号

                //总单号
                //分单号
                //件数
                //毛重
                //发货单位 
                if (!string.IsNullOrEmpty(o[0].FGOODSUNIT) && o[0].FGOODSUNIT.Length > 10)
                {
                    o[0].FGOODSUNITCODE = o[0].FGOODSUNIT.Substring(o[0].FGOODSUNIT.Length - 10, 10);
                    o[0].FGOODSUNIT = o[0].FGOODSUNIT.Remove(o[0].FGOODSUNIT.Length - 10, 10);
                }
                //收货单位 
                if (!string.IsNullOrEmpty(o[0].SGOODSUNIT) && o[0].SGOODSUNIT.Length > 10)
                {
                    o[0].SGOODSUNITCODE = o[0].SGOODSUNIT.Substring(o[0].SGOODSUNIT.Length - 10, 10);
                    o[0].SGOODSUNIT = o[0].SGOODSUNIT.Remove(o[0].SGOODSUNIT.Length - 10, 10);
                }
                //货物包装
                //申报方式代码
                if (!string.IsNullOrEmpty(o[0].REPWAYID))
                {
                    sql = "select CODE, NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + o[0].REPWAYID + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        o[0].REPWAYID = dt.Rows[0]["CODE"] + "";
                    }
                    else
                    {
                        o[0].REPWAYID = "";
                    }
                }

                //报关方式代码
                if (!string.IsNullOrEmpty(o[0].DECLWAY))
                {
                    sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + o[0].DECLWAY + "'";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        o[0].DECLWAY = dt.Rows[0]["CODE"] + "";
                    }
                    else
                    {
                        o[0].DECLWAY = "";
                    }
                }

                string TRADEWAYCODES = o[0].TRADEWAYCODES;
                //贸易方式

                if (!string.IsNullOrEmpty(o[0].TRADEWAYCODES))
                {
                    string[] arr = o[0].TRADEWAYCODES.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 0)
                    {
                        o[0].TRADEWAYCODES = arr[0];
                    }
                    if (arr.Length > 1)
                    {
                        o[0].ENTRUSTREQUEST += TRADEWAYCODES;
                    }
                }


                //客户编号
                if (!string.IsNullOrEmpty(o[0].CUSTOMDISTRICTCODE))
                {
                    //申报关区代码
                    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + o[0].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
                    dt = DB_BaseData.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        o[0].CUSTOMDISTRICTCODE = dt.Rows[0]["CODE"] + "";
                    }
                    else
                    {
                        o[0].CUSTOMDISTRICTCODE = "";
                    }
                }

                //口岸
                //特殊关系确认

                if (string.IsNullOrEmpty(o[0].SPECIALRELATIONSHIP + ""))
                {
                    o[0].SPECIALRELATIONSHIP = "0";
                }
                else
                {
                    o[0].SPECIALRELATIONSHIP = "1";
                }
                //价格影响确认

                if (string.IsNullOrEmpty(o[0].PRICEIMPACT + ""))
                {
                    o[0].PRICEIMPACT = "0";
                }
                else
                {
                    o[0].PRICEIMPACT = "1";
                }
                //支付特许权使用费确认

                if (string.IsNullOrEmpty(o[0].PAYPOYALTIES + ""))
                {
                    o[0].PAYPOYALTIES = "0";
                }
                else
                {
                    o[0].PAYPOYALTIES = "1";
                }

                //木质包装  取报检的指令
                if (o.Count == 2 && o[0].FOONO.Substring(0, 4) == "SOBG")
                {
                    o[0].WOODPACKINGID = o[1].WOODPACKINGID;
                }


                //报关/报检指令 
                //申报单位  报关 报检

                if (o[0].ENTRUSTTYPEID == "01")
                {
                    if (!string.IsNullOrEmpty(o[0].REPUNITCODE) && o[0].REPUNITCODE.Length > 10)
                    {
                        o[0].REPUNITNAME = o[0].REPUNITCODE.Remove(o[0].REPUNITCODE.Length - 10, 10);
                        o[0].REPUNITCODE = o[0].REPUNITCODE.Substring(o[0].REPUNITCODE.Length - 10, 10);
                        o[0].INSPUNITNAME = "";
                    }
                }
                else if (o[0].ENTRUSTTYPEID == "02")
                {
                    if (!string.IsNullOrEmpty(o[0].INSPUNITNAME) && o[0].INSPUNITNAME.Length > 10)
                    {
                        o[0].REPUNITCODE = "";
                        o[0].INSPUNITCODE = o[0].INSPUNITNAME.Substring(o[0].INSPUNITNAME.Length - 10, 10);
                        o[0].INSPUNITNAME = o[0].INSPUNITNAME.Remove(o[0].INSPUNITNAME.Length - 10, 10);
                    }
                    o[0].FOONOBJ = o[0].FOONO;//报检
                    o[0].FOONO = "";//报关
                }
                else if (o[0].ENTRUSTTYPEID == "03")
                {
                    if (o[0].FOONO.Substring(0, 4) == "SOBG")
                    {
                        if (!string.IsNullOrEmpty(o[0].REPUNITCODE) && o[0].REPUNITCODE.Length > 10)
                        {
                            o[0].REPUNITNAME = o[0].REPUNITCODE.Remove(o[0].REPUNITCODE.Length - 10, 10);
                            o[0].REPUNITCODE = o[0].REPUNITCODE.Substring(o[0].REPUNITCODE.Length - 10, 10);
                        }
                        else
                        {
                            o[0].REPUNITNAME = "";
                            o[0].REPUNITCODE = "";
                        }

                        if (!string.IsNullOrEmpty(o[1].INSPUNITNAME) && o[1].INSPUNITNAME.Length > 10)
                        {
                            o[0].INSPUNITCODE = o[1].INSPUNITNAME.Substring(o[1].INSPUNITNAME.Length - 10, 10);
                            o[0].INSPUNITNAME = o[1].INSPUNITNAME.Remove(o[1].INSPUNITNAME.Length - 10, 10);
                        }
                        else
                        {
                            o[0].INSPUNITCODE = "";
                            o[0].INSPUNITNAME = "";
                        }

                        //第一条指令为报关的话， 第二条指令一定为报检
                        o[0].FOONOBJ = o[1].FOONO;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(o[1].REPUNITCODE) && o[1].REPUNITCODE.Length > 10)
                        {
                            o[0].REPUNITCODE = o[1].REPUNITCODE.Substring(o[1].REPUNITCODE.Length - 10, 10);
                            o[0].REPUNITNAME = o[1].REPUNITCODE.Remove(o[1].REPUNITCODE.Length - 10, 10);
                        }
                        else
                        {
                            o[0].REPUNITCODE = "";
                            o[0].REPUNITNAME = "";
                        }


                        if (!string.IsNullOrEmpty(o[0].INSPUNITNAME) && o[0].INSPUNITNAME.Length > 10)
                        {
                            o[0].INSPUNITCODE = o[0].INSPUNITNAME.Substring(o[0].INSPUNITNAME.Length - 10, 10);
                            o[0].INSPUNITNAME = o[0].INSPUNITNAME.Remove(o[0].INSPUNITNAME.Length - 10, 10);
                        }
                        else
                        {
                            o[0].INSPUNITCODE = "";
                            o[0].INSPUNITNAME = "";
                        }

                        //第一条指令为报检的话，第二条指令一定为报关
                        o[0].FOONOBJ = o[0].FOONO;
                        o[0].FOONO = o[1].FOONO;
                    }
                }
                //委托人
                o[0].SUBMITUSERNAME = o[0].CREATEUSERNAME;
                //委托时间
                if (!string.IsNullOrEmpty(o[0].CREATETIME) && o[0].CREATETIME.Length == 22)
                {
                    o[0].SUBMITTIME = DateTime.ParseExact(o[0].CREATETIME, "yyyyMMddHHmmss.fffffff", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
                }
                //运抵编号
                //实际件数
                //实际毛重
                //经营单位
                if (!string.IsNullOrEmpty(o[0].BUSIUNITNAME) && o[0].BUSIUNITNAME.Length > 10)
                {
                    o[0].BUSIUNITCODE = o[0].BUSIUNITNAME.Substring(o[0].BUSIUNITNAME.Length - 10, 10);
                    o[0].BUSIUNITNAME = o[0].BUSIUNITNAME.Remove(o[0].BUSIUNITNAME.Length - 10, 10);
                }
                //货物类型
                //报关提单号
                //是否提前报关
                if (string.IsNullOrEmpty(o[0].ISPREDECLARE + ""))
                {
                    o[0].ISPREDECLARE = "0";
                }
                else
                {
                    o[0].ISPREDECLARE = "1";
                }
                //需求备注
                //合同号
                //一程提单号
                //二程提单号
                //载货清单号
                //木质包装
                //需重量确认标识
                if (string.IsNullOrEmpty(o[0].WEIGHTCHECK + ""))
                {
                    o[0].WEIGHTCHECK = "0";
                }
                else
                {
                    o[0].WEIGHTCHECK = "1";
                }
                //重量确认标识
                if (string.IsNullOrEmpty(o[0].ISWEIGHTCHECK + ""))
                {
                    o[0].ISWEIGHTCHECK = "0";
                }
                else
                {
                    o[0].ISWEIGHTCHECK = "1";
                }
                //船名
                //航次
                //转关预录入号
                //二线合同专用发票号

                //报关可执行 单证没有此字段 
                if (string.IsNullOrEmpty(o[0].ALLOWDECLARE + ""))
                {
                    o[0].ALLOWDECLARE = "0";
                    o[0].SENDNUMBER = "1";
                }
                else
                {
                    o[0].ALLOWDECLARE = "1";
                    o[0].SENDNUMBER = "2";
                }

                //二程提单号 报关提单号  都为海关提单号  sap 设计不合理


                if (o[0].BUSITYPE == "21" || o[0].BUSITYPE == "20")
                {
                    if (!string.IsNullOrEmpty(o[0].LADINGBILLNO + ""))
                    {
                        o[0].SECONDLADINGBILLNO = o[0].LADINGBILLNO + "";
                    }
                }


                //sap 设计不合理
                if (!string.IsNullOrEmpty(o[0].GOODSTYPEID + ""))
                {
                    if (o[0].GOODSTYPEID + "" == "FCL（整箱装载）")
                    {
                        o[0].GOODSTYPEID = "1";//整箱
                    }
                    else
                    {
                        o[0].GOODSTYPEID = "2"; //散货
                    }
                }
                res = EditOrder(o[0]);
            }


            if (IFS.IFINSERT == "1")
            {
                IDatabase db = SeRedis.redis.GetDatabase();
                string json = "{\"ONLYCODE\":" + GroupOrder[0][0].ONLYCODE + "}";
                db.ListRightPush("XGW_CheckFile", json);
            }
            //保存到单证
            return res;
        }

        //转成物贸通的数据
        public static int XCWumaoData(List<OrderEn> ld, string ONLYCODE)
        {
            Wumao wm = new Wumao();
            int res = 0;
            string sql = "";
            DataTable dt;
            string BUSITYPE = JudgeBusiType(ld[0].BUSITYPE, ld[0].ENTRUSTTYPEID);
            sql = "select * from LIST_WUMAODADAMATCHING where BUSINAME='" + ld[0].BUSITYPE + "'";
            dt = DBMgr.GetDataTable(sql);

            //报关申报单位  申报code
            string REPUNITCODE = "";
            string REPUNITNAME = "";
            if (!string.IsNullOrEmpty(ld[0].REPUNITCODE) && ld[0].REPUNITCODE.Length > 10)
            {
                REPUNITCODE = ld[0].REPUNITCODE.Substring(ld[0].REPUNITCODE.Length - 10, 10);
                REPUNITNAME = ld[0].REPUNITCODE.Remove(ld[0].REPUNITCODE.Length - 10, 10);
            }


            //收货方name  code
            string SGOODSUNIT = "";
            string SGOODSUNITCODE = "";
            if (!string.IsNullOrEmpty(ld[0].SGOODSUNIT) && ld[0].SGOODSUNIT.Length > 10)
            {
                SGOODSUNITCODE = ld[0].SGOODSUNIT.Substring(ld[0].SGOODSUNIT.Length - 10, 10);
                SGOODSUNIT = ld[0].SGOODSUNIT.Remove(ld[0].SGOODSUNIT.Length - 10, 10);
            }


            //发货方name  code
            string FGOODSUNIT = "";
            string FGOODSUNITCODE = "";
            if (!string.IsNullOrEmpty(ld[0].FGOODSUNIT) && ld[0].FGOODSUNIT.Length > 10)
            {
                FGOODSUNITCODE = ld[0].FGOODSUNIT.Substring(ld[0].FGOODSUNIT.Length - 10, 10);
                FGOODSUNIT = ld[0].FGOODSUNIT.Remove(ld[0].FGOODSUNIT.Length - 10, 10);
            }

            //经营单位
            string BUSIUNITNAME = "";
            string BUSIUNITCODE = "";
            if (!string.IsNullOrEmpty(ld[0].BUSIUNITNAME) && ld[0].BUSIUNITNAME.Length > 10)
            {
                BUSIUNITCODE = ld[0].BUSIUNITNAME.Substring(ld[0].BUSIUNITNAME.Length - 10, 10);
                BUSIUNITNAME = ld[0].BUSIUNITNAME.Remove(ld[0].BUSIUNITNAME.Length - 10, 10);
            }

            if (dt.Rows.Count > 0)
            {
                wm.GOODS_NATURE_ID = dt.Rows[0]["GOODS_NATURE_ID"] + "";

                if (BUSITYPE == "51" || BUSITYPE == "50")
                {
                    if (ld[0].ENTRUSTTYPEID == "进口企业")
                    {
                        wm.I_E_FALG_TYPE = "I";
                    }
                    else if (ld[0].ENTRUSTTYPEID == "出口企业")
                    {
                        wm.I_E_FALG_TYPE = "E";
                    }
                }
                else
                {
                    wm.I_E_FALG_TYPE = dt.Rows[0]["I_E_FALG_TYPE"] + "";
                }
                wm.BIZ_TYPE_ID = dt.Rows[0]["BIZ_TYPE_ID"] + "";
                wm.TRAFFICTYPE = dt.Rows[0]["TRAFFICTYPE"] + "";
                wm.BILL_TYPE = dt.Rows[0]["BILL_TYPE"] + "";
                wm.APPCIQTYPE = dt.Rows[0]["APPCIQTYPE"] + "";
                wm.OUT_TRAF_MODE = dt.Rows[0]["OUT_TRAF_MODE"] + "";
            }

            wm.TRADETYPE = "0110"; //监管方式


            wm.BUSINAME = ld[0].BUSITYPE;
            wm.ORDERCODE = ld[0].ORDERCODE;
            wm.FWONO = ld[0].CODE;
            wm.FOONO = ld[0].FOONO;

            wm.ONLYCODE = ONLYCODE;
            wm.WTFS = ld[0].ENTRUSTTYPEID;

            wm.PROVIDER_NAME = "电子零件";
            wm.D_DATE = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");


            wm.TRANSPORT_CODE = REPUNITCODE;
            wm.TRANSPORT_NAME = REPUNITNAME;
            wm.APPCOMPANY = REPUNITCODE;
            wm.APPCOMPANY_NAME = REPUNITNAME;
            //进出口标志代码
            //作业单类型名称
            //进出口标志代码

            if (BUSITYPE == "11" || BUSITYPE == "21")
            {
                wm.TRADE_CODE = SGOODSUNITCODE;
                wm.TRADE_NAME = SGOODSUNIT;
                wm.CONSIGNEE_CODE = SGOODSUNITCODE;
                wm.CONSIGNEE_NAME = SGOODSUNIT;
                wm.TRADE_CODE_IN = SGOODSUNITCODE;
                wm.TRADE_NAME_IN = SGOODSUNIT;
            }
            else if (BUSITYPE == "10")
            {
                wm.TRADE_CODE = FGOODSUNITCODE;
                wm.TRADE_NAME = FGOODSUNIT;
                wm.CONSIGNEE_CODE = FGOODSUNITCODE;
                wm.CONSIGNEE_NAME = FGOODSUNIT;
                wm.TRADE_CODE_IN = FGOODSUNITCODE;
                wm.TRADE_NAME_IN = FGOODSUNIT;
            }
            else if (BUSITYPE == "20")
            {
                wm.TRADE_CODE = BUSIUNITCODE;
                wm.TRADE_NAME = BUSIUNITNAME;
                wm.CONSIGNEE_CODE = BUSIUNITCODE;
                wm.CONSIGNEE_NAME = BUSIUNITNAME;
                wm.TRADE_CODE_IN = BUSIUNITCODE;
                wm.TRADE_NAME_IN = BUSIUNITNAME;
            }
            wm.PACK_NO = ld[0].GOODSNUM;
            wm.GROSS_WT = ld[0].GOODSWEIGHT;
            wm.NET_WT = ld[0].CHECKEDWEIGHT;
            wm.TRANSFER_NO = ld[0].TURNPRENO;

            wm.GOODS_TYPE_LY = "11";//提前报关

            //wm.GOODS_TYPE_LY = "22";//直转

            //wm.BILL_TYPE = "1";//0空1报关单2转关单

            wm.WRAP_TYPE_ID = "1"; //包装种类

            wm.LYTYPE_ID = "41"; //陆运ID
            wm.GOODS_TYPE_ID = "45"; //货物类型
            wm.LY_BIZ_TYPE_ID = "04";//业务类型

            //海关编号
            sql = "select DECLARATIONCODE　from LIST_DECLARATION where  ORDERCODE='" + ld[0].ORDERCODE + "' and isdel !='1'";
            dt = DBMgr.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                wm.APPCIQID = dt.Rows[0]["DECLARATIONCODE"] + "";
            }


            wm.MAINCODE = ld[0].TOTALNO;
            wm.SUBCODE = ld[0].DIVIDENO;
            wm.TONGGUANFSCODE = ld[0].TONGGUANFSCODE;
            wm.TONGGUANFSNAME = ld[0].TONGGUANFSNAME;
            res = EditWumao(wm);
            return res;
        }
        public static int EditWumao(Wumao wm)
        {
            string sql = "select id from LIST_WUMAO where ORDERCODE='" + wm.ORDERCODE + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            int res = 1;
            if (dt.Rows.Count > 0)
            {
                sql = @"update LIST_WUMAO set 
                                      I_E_FALG_TYPE='{0}',BIZ_TYPE_ID='{1}',TRAFFICTYPE='{2}',BILL_TYPE='{3}',  
                                      APPCIQTYPE='{4}',OUT_TRAF_MODE='{5}',BUSINAME='{6}',PROVIDER_NAME='{7}', 
                                      D_DATE=to_date('{8}','yyyy-mm-dd'),TRANSPORT_CODE='{9}',TRANSPORT_NAME='{10}',APPCOMPANY='{11}',  
                                      APPCOMPANY_NAME='{12}',TRADE_CODE ='{13}',TRADE_NAME='{14}',CONSIGNEE_CODE='{15}',  
                                      CONSIGNEE_NAME='{16}',TRADE_CODE_IN='{17}',TRADE_NAME_IN='{18}',PACK_NO='{19}', 
                                      GROSS_WT='{20}',NET_WT='{21}',GOODS_TYPE_LY='{22}',WRAP_TYPE_ID='{23}',
                                      MAINCODE='{24}',SUBCODE='{25}',TRANSFER_NO='{26}',ONLYCODE='{27}'
                                      ,WTFS='{28}',GOODS_NATURE_ID='{29}',TONGGUANFSCODE='{30}',TONGGUANFSNAME='{31}',
                                      LYTYPE_ID='{32}',GOODS_TYPE_ID='{33}',LY_BIZ_TYPE_ID='{34}',APPCIQID='{35}'
                                      where ORDERCODE='" + wm.ORDERCODE + "'";
                sql = string.Format(sql,
    wm.I_E_FALG_TYPE, wm.BIZ_TYPE_ID, wm.TRAFFICTYPE, wm.BILL_TYPE,
    wm.APPCIQTYPE, wm.OUT_TRAF_MODE, wm.BUSINAME, wm.PROVIDER_NAME,
    wm.D_DATE, wm.TRANSPORT_CODE, wm.TRANSPORT_NAME, wm.APPCOMPANY,
    wm.APPCOMPANY_NAME, wm.TRADE_CODE, wm.TRADE_NAME, wm.CONSIGNEE_CODE,
    wm.CONSIGNEE_NAME, wm.TRADE_CODE_IN, wm.TRADE_NAME_IN, wm.PACK_NO,
    wm.GROSS_WT, wm.NET_WT, wm.GOODS_TYPE_LY, wm.WRAP_TYPE_ID,
    wm.MAINCODE, wm.SUBCODE, wm.TRANSFER_NO, wm.ONLYCODE,
    wm.WTFS, wm.GOODS_NATURE_ID, wm.TONGGUANFSCODE, wm.TONGGUANFSNAME,
    wm.LYTYPE_ID, wm.GOODS_TYPE_ID, wm.LY_BIZ_TYPE_ID, wm.APPCIQID
    );
            }
            else
            {
                sql = @"insert into LIST_WUMAO(ID,
                                      I_E_FALG_TYPE,BIZ_TYPE_ID,TRAFFICTYPE,BILL_TYPE,
                                      APPCIQTYPE,OUT_TRAF_MODE,BUSINAME,PROVIDER_NAME,
                                      D_DATE,TRANSPORT_CODE,TRANSPORT_NAME,APPCOMPANY,
                                      APPCOMPANY_NAME,TRADE_CODE,TRADE_NAME,CONSIGNEE_CODE,
                                      CONSIGNEE_NAME,TRADE_CODE_IN,TRADE_NAME_IN,PACK_NO,
                                      GROSS_WT,NET_WT,GOODS_TYPE_LY,WRAP_TYPE_ID,
                                      MAINCODE,SUBCODE,TRANSFER_NO,ONLYCODE,
                                      WTFS,ORDERCODE,FWONO,FOONO,
                                      GOODS_NATURE_ID,TONGGUANFSCODE,TONGGUANFSNAME,
                                      LYTYPE_ID, GOODS_TYPE_ID, LY_BIZ_TYPE_ID, APPCIQID
                                       )VALUES(LIST_ORDER_ID.Nextval,
                   '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','yyyy-mm-dd'),'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                   '{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',
                   '{35}','{36}','{37}','{38}'
                  )";
                sql = string.Format(sql,
    wm.I_E_FALG_TYPE, wm.BIZ_TYPE_ID, wm.TRAFFICTYPE, wm.BILL_TYPE,
    wm.APPCIQTYPE, wm.OUT_TRAF_MODE, wm.BUSINAME, wm.PROVIDER_NAME,
    wm.D_DATE, wm.TRANSPORT_CODE, wm.TRANSPORT_NAME, wm.APPCOMPANY,
    wm.APPCOMPANY_NAME, wm.TRADE_CODE, wm.TRADE_NAME, wm.CONSIGNEE_CODE,
    wm.CONSIGNEE_NAME, wm.TRADE_CODE_IN, wm.TRADE_NAME_IN, wm.PACK_NO,
    wm.GROSS_WT, wm.NET_WT, wm.GOODS_TYPE_LY, wm.WRAP_TYPE_ID,
    wm.MAINCODE, wm.SUBCODE, wm.TRANSFER_NO, wm.ONLYCODE,
    wm.WTFS, wm.ORDERCODE, wm.FWONO, wm.FOONO,
    wm.GOODS_NATURE_ID, wm.TONGGUANFSCODE, wm.TONGGUANFSNAME,
    wm.LYTYPE_ID, wm.GOODS_TYPE_ID, wm.LY_BIZ_TYPE_ID, wm.APPCIQID
    );
            }
            res = DBMgr.ExecuteNonQuery(sql);
            return res;
        }

        public static int EditOrder(OrderEn o)
        {
            int res = 0;
            string sql = "select id from list_order where  code='" + o.ORDERCODE + "'";
            DataTable dt = DBMgr.GetDataTable(sql);

            if (dt.Rows.Count > 0)
            {
                sql = "delete from List_Declcontainertruck where ORDERCODE='" + o.ORDERCODE + "'";
                DBMgr.ExecuteNonQuery(sql);
                sql = @"update List_Order set 
                                  TOTALNO='{0}',DIVIDENO='{1}',GOODSNUM='{2}',GOODSWEIGHT='{3}',  
                                  FGOODSUNIT='{4}',FGOODSUNITCODE='{5}',SGOODSUNIT='{6}',SGOODSUNITCODE='{7}',  
                                  PACKKIND='{8}',REPWAYID='{9}',DECLWAY='{10}',TRADEWAYCODES='{11}',  
                                  CUSNO='{12}',CUSTOMDISTRICTCODE='{13}',PORTCODE='{14}',SPECIALRELATIONSHIP='{15}',  
                                  PRICEIMPACT='{16}',PAYPOYALTIES='{17}',REPUNITNAME='{18}',REPUNITCODE='{19}',  
                                  INSPUNITNAME='{20}',INSPUNITCODE='{21}',SUBMITUSERNAME='{22}',SUBMITTIME=to_date('{23}','yyyy-mm-dd hh24:mi:ss'),  
                                  ARRIVEDNO='{24}',CHECKEDGOODSNUM='{25}',CHECKEDWEIGHT='{26}',BUSIUNITNAME='{27}',  
                                  BUSIUNITCODE='{28}',GOODSTYPEID='{29}',ISPREDECLARE='{30}',ENTRUSTREQUEST='{31}',  
                                  CONTRACTNO='{32}',FIRSTLADINGBILLNO='{33}',SECONDLADINGBILLNO='{34}',MANIFEST='{35}', 
                                  WOODPACKINGID='{36}',WEIGHTCHECK='{37}',ISWEIGHTCHECK='{38}',SHIPNAME='{39}',  
                                  FILGHTNO='{40}',TURNPRENO='{41}',INVOICENO='{42}',ALLOWDECLARE='{43}', 
                                  SENDNUMBER='{44}',XCBUSINAME='{45}',UPDATETIME=to_date('{46}','yyyy-mm-dd hh24:mi:ss'),
                                  IFSEND='{47}',SENDURL='{48}',TONGGUANFSNAME='{49}',TONGGUANFSCODE='{50}',
                                  CGGROUPCODE='{51}',CGGROUPNAME='{52}'
                                  where code='" + o.ORDERCODE + "'";
                sql = string.Format(sql,
    o.TOTALNO, o.DIVIDENO, o.GOODSNUM, o.GOODSWEIGHT,
    o.FGOODSUNIT, o.FGOODSUNITCODE, o.SGOODSUNIT, o.SGOODSUNITCODE,
    o.PACKKIND, o.REPWAYID, o.DECLWAY, o.TRADEWAYCODES,
    o.CUSNO, o.CUSTOMDISTRICTCODE, o.PORTCODE, o.SPECIALRELATIONSHIP,
    o.PRICEIMPACT, o.PAYPOYALTIES, o.REPUNITNAME, o.REPUNITCODE,
    o.INSPUNITNAME, o.INSPUNITCODE, o.SUBMITUSERNAME, o.SUBMITTIME,
    o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT, o.BUSIUNITNAME,
    o.BUSIUNITCODE, o.GOODSTYPEID, o.ISPREDECLARE, o.ENTRUSTREQUEST,
    o.CONTRACTNO, o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO, o.MANIFEST,
    o.WOODPACKINGID, o.WEIGHTCHECK, o.ISWEIGHTCHECK, o.SHIPNAME,
    o.FILGHTNO, o.TURNPRENO, o.INVOICENO, o.ALLOWDECLARE,
    o.SENDNUMBER, o.XCBUSINAME, o.UPDATETIME, o.IFSEND,
    o.SENDURL, o.TONGGUANFSNAME, o.TONGGUANFSCODE, o.CGGROUPCODE,
    o.CGGROUPNAME
    );
            }
            else
            {
                sql = @"insert into List_Order(
                   ID,URL,
                   BUSITYPE,FWONO,FOONO,FOONOBJ,
                   ENTRUSTTYPEID,
                   TOTALNO,DIVIDENO,GOODSNUM,GOODSWEIGHT,
                   FGOODSUNIT,FGOODSUNITCODE,SGOODSUNIT,SGOODSUNITCODE,
                   PACKKIND,REPWAYID,DECLWAY,TRADEWAYCODES,
                   CUSNO,CUSTOMDISTRICTCODE,PORTCODE,SPECIALRELATIONSHIP,
                   PRICEIMPACT,PAYPOYALTIES,REPUNITNAME,REPUNITCODE,
                   INSPUNITNAME,INSPUNITCODE,SUBMITUSERNAME,SUBMITTIME,
                   ARRIVEDNO,CHECKEDGOODSNUM,CHECKEDWEIGHT,BUSIUNITNAME,
                   BUSIUNITCODE,GOODSTYPEID,LADINGBILLNO,ISPREDECLARE,
                   ENTRUSTREQUEST,CONTRACTNO,FIRSTLADINGBILLNO,SECONDLADINGBILLNO,
                   MANIFEST,WOODPACKINGID,WEIGHTCHECK,ISWEIGHTCHECK,
                   SHIPNAME,FILGHTNO,TURNPRENO,INVOICENO,
                   ALLOWDECLARE,CODE,ASSOCIATENO,CORRESPONDNO,
                   WTFS,ONLYCODE,SENDNUMBER,BUSINAME,
                   XCBUSINAME,UPDATETIME,CREATETIME,SENDURL,
                   TONGGUANFSNAME,TONGGUANFSCODE
                  ) VALUES(
                   LIST_ORDER_ID.Nextval,'SAP',
                   '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                   '{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}',to_date('{28}','yyyy-mm-dd hh24:mi:ss'),'{29}','{30}','{31}','{32}',
                   '{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}',
                   '{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}',to_date('{58}','yyyy-mm-dd hh24:mi:ss'),
                   to_date('{59}','yyyy-mm-dd hh24:mi:ss'),'{60}','{61}','{62}'
                  )";
                sql = string.Format(sql,
                    o.BUSITYPE, o.CODE, o.FOONO, o.FOONOBJ,
                    o.ENTRUSTTYPEID,
                    o.TOTALNO, o.DIVIDENO, o.GOODSNUM, o.GOODSWEIGHT,
                    o.FGOODSUNIT, o.FGOODSUNITCODE, o.SGOODSUNIT, o.SGOODSUNITCODE,
                    o.PACKKIND, o.REPWAYID, o.DECLWAY, o.TRADEWAYCODES,
                    o.CUSNO, o.CUSTOMDISTRICTCODE, o.PORTCODE, o.SPECIALRELATIONSHIP,
                    o.PRICEIMPACT, o.PAYPOYALTIES, o.REPUNITNAME, o.REPUNITCODE,
                    o.INSPUNITNAME, o.INSPUNITCODE, o.SUBMITUSERNAME, o.SUBMITTIME,
                    o.ARRIVEDNO, o.CHECKEDGOODSNUM, o.CHECKEDWEIGHT, o.BUSIUNITNAME,
                    o.BUSIUNITCODE, o.GOODSTYPEID, "", o.ISPREDECLARE,
                    o.ENTRUSTREQUEST, o.CONTRACTNO, o.FIRSTLADINGBILLNO, o.SECONDLADINGBILLNO,
                    o.MANIFEST, o.WOODPACKINGID, o.WEIGHTCHECK, o.ISWEIGHTCHECK,
                    o.SHIPNAME, o.FILGHTNO, o.TURNPRENO, o.INVOICENO,
                    o.ALLOWDECLARE, o.ORDERCODE, o.ASSOCIATENO, o.CORRESPONDNO,
                    o.WTFS, o.ONLYCODE, o.SENDNUMBER, o.BUSINAME,
                    o.XCBUSINAME, o.UPDATETIME, o.CREATETIME, o.SENDURL,
                    o.TONGGUANFSNAME, o.TONGGUANFSCODE
                    );
                IFS.IFINSERT = "1";

            }
            res = DBMgr.ExecuteNonQuery(sql);
            //卡号 车号
            foreach (Declcontainertruck d in o.Declcontainertruck)
            {
                if (!string.IsNullOrEmpty(d.CDCARNAME) || !string.IsNullOrEmpty(d.CDCARNO) || !string.IsNullOrEmpty(d.CONTAINERNO) || !string.IsNullOrEmpty(d.CONTAINERTYPE))
                {
                    sql = @"insert into LIST_DECLCONTAINERTRUCK(ID,ORDERCODE,CDCARNAME,CDCARNO,CONTAINERNO,CONTAINERTYPE) 
                    values(LIST_DECLCONTAINERTRUCK_ID.Nextval,'" + o.ORDERCODE + "','" + d.CDCARNAME + "','" + d.CDCARNO + "','" + d.CONTAINERNO + "','" + d.CONTAINERTYPE + "')";
                    DBMgr.ExecuteNonQuery(sql);
                }
            }
            return res;
        }

        //验证物贸通的数据
        public static List<Msgobj> CheckWumaoData(List<OrderEn> ld)
        {
            DataTable dt;
            string sql = "";
            List<Msgobj> MsgobjList = new List<Msgobj>();
            bool is_empty;


            string ENTRUSTTYPEID = ld[0].ENTRUSTTYPEID;
            if (ENTRUSTTYPEID == "")
            {
                is_empty = true;
            }
            else if (ENTRUSTTYPEID == "进口企业" || ENTRUSTTYPEID == "出口企业" || ENTRUSTTYPEID == "HUB 仓进" || ENTRUSTTYPEID == "HUB 仓出")
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
            List<List<OrderEn>> GroupOrder = GroupByConFoo(ld);
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                string BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);
                if (!string.IsNullOrEmpty(BUSITYPE) && BUSITYPE != "0")
                {
                    string ENTRUSTTYPE = GetENTRUSTTYPEID(ListOrder, BUSITYPE);
                    if (BUSITYPE == "40" || BUSITYPE == "41")
                    {
                        MsgobjList.Add(set_MObj("E", "二线不需要从新关务发物贸通"));
                        return MsgobjList;
                    }

                    if (BUSITYPE == "31" || BUSITYPE == "30")
                    {
                        MsgobjList.Add(set_MObj("E", "陆运需求未定，不可发物贸通！"));
                        return MsgobjList;
                    }
                }

                if (ListOrder.Count > 1)
                {
                    MsgobjList.Add(set_MObj("E", "指令条数超出！"));
                    return MsgobjList;
                }


                if (!string.IsNullOrEmpty(ListOrder[0].FOONO) && ListOrder[0].FOONO.Length >= 4)
                {
                    string FOONO = ListOrder[0].FOONO.Substring(0, 4);
                    if (FOONO != "SOBG")
                    {
                        MsgobjList.Add(set_MObj("E", "报关才可发物贸通！"));
                        return MsgobjList;
                    }
                }
                else
                {
                    MsgobjList.Add(set_MObj("E", "FOONO不符合"));
                    return MsgobjList;
                }
            }

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
                    MsgobjList.Add(set_MObj("E", "凭证类型不可为空" + o.FOONO));
                }
                else if (BUSITYPE == "0")
                {
                    MsgobjList.Add(set_MObj("E", "凭证类型无法匹配" + o.FOONO));
                }



                if (string.IsNullOrEmpty(o.BUSIUNITNAME) || o.BUSIUNITNAME.Length < 11)
                {
                    MsgobjList.Add(set_MObj("E", "经营单位不合格" + o.FOONO));
                }
                else
                {
                    string BUSIUNITCODE = o.BUSIUNITNAME.Substring(o.BUSIUNITNAME.Length - 10, 10);
                    if (!Regex.IsMatch(BUSIUNITCODE, @"^[A-Za-z0-9]+$"))
                    {
                        MsgobjList.Add(set_MObj("E", "经营单位不合格" + o.FOONO));
                    }
                }

                if (string.IsNullOrEmpty(o.SGOODSUNIT) || o.SGOODSUNIT.Length < 11)
                {
                    MsgobjList.Add(set_MObj("E", "收货单位不合格" + o.FOONO));
                }
                else
                {
                    string SGOODSUNITCODE = o.SGOODSUNIT.Substring(o.SGOODSUNIT.Length - 10, 10);
                    if (!Regex.IsMatch(SGOODSUNITCODE, @"^[A-Za-z0-9]+$"))
                    {
                        MsgobjList.Add(set_MObj("E", "收货单位不合格" + o.FOONO));
                    }
                }


                if (string.IsNullOrEmpty(o.FGOODSUNIT) || o.FGOODSUNIT.Length < 11)
                {
                    MsgobjList.Add(set_MObj("E", "发货单位不合格" + o.FOONO));
                }
                else
                {
                    string FGOODSUNITCODE = o.FGOODSUNIT.Substring(o.FGOODSUNIT.Length - 10, 10);
                    if (!Regex.IsMatch(FGOODSUNITCODE, @"^[A-Za-z0-9]+$"))
                    {
                        MsgobjList.Add(set_MObj("E", "发货单位不合格" + o.FOONO));
                    }
                }


                if (string.IsNullOrEmpty(o.REPUNITCODE) || o.REPUNITCODE.Length < 11)
                {
                    MsgobjList.Add(set_MObj("E", "报关申报单位不合格" + o.FOONO));
                }
                else
                {
                    string REPUNITCODE = o.REPUNITCODE.Substring(o.REPUNITCODE.Length - 10, 10);
                    if (!Regex.IsMatch(REPUNITCODE, @"^[A-Za-z0-9]+$"))
                    {
                        MsgobjList.Add(set_MObj("E", "报关申报单位不合格" + o.FOONO));
                    }
                }

                if (string.IsNullOrEmpty(o.ORDERCODE))
                {
                    MsgobjList.Add(set_MObj("E", "业务单号不可为空" + o.FOONO));
                }


                if (string.IsNullOrEmpty(o.CHECKEDGOODSNUM))
                {
                    MsgobjList.Add(set_MObj("E", "实际件数不可为空" + o.FOONO));
                }


                if (string.IsNullOrEmpty(o.CHECKEDWEIGHT))
                {
                    MsgobjList.Add(set_MObj("E", "实际毛重不可为空" + o.FOONO));
                }


                if (string.IsNullOrEmpty(o.GOODSWEIGHT))
                {
                    MsgobjList.Add(set_MObj("E", "毛重不可为空" + o.FOONO));
                }

                if (string.IsNullOrEmpty(o.TONGGUANFSCODE) || string.IsNullOrEmpty(o.TONGGUANFSNAME))
                {
                    MsgobjList.Add(set_MObj("E", "通关方式不可为空" + o.FOONO));
                }
                else
                {
                    if (o.TONGGUANFSNAME != "转关" && o.TONGGUANFSNAME != "一体化")
                    {

                    }

                }

            }


            return MsgobjList;
        }

        //检查数据 单证
        public static List<Msgobj> CheckData(List<OrderEn> ld)
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
            else if (ENTRUSTTYPEID == "进口企业" || ENTRUSTTYPEID == "出口企业" || ENTRUSTTYPEID == "HUB 仓进" || ENTRUSTTYPEID == "HUB 仓出")
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
            List<List<OrderEn>> GroupOrder = GroupByConFoo(ld);
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                if (ListOrder.Count > 2)
                {
                    MsgobjList.Add(set_MObj("E", "指令数量异常"));
                    return MsgobjList;
                }
                else if (ListOrder.Count == 2)
                {
                    if (!string.IsNullOrEmpty(ListOrder[0].FOONO) && ListOrder[0].FOONO.Length >= 4 && !string.IsNullOrEmpty(ListOrder[1].FOONO) && ListOrder[1].FOONO.Length >= 4)
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

                    if (!string.IsNullOrEmpty(ListOrder[0].ORDERCODE) && !string.IsNullOrEmpty(ListOrder[1].ORDERCODE))
                    {
                        if (ListOrder[0].ORDERCODE != ListOrder[1].ORDERCODE)
                        {
                            MsgobjList.Add(set_MObj("E", "业务单号生成有异常"));
                            return MsgobjList;
                        }
                    }
                    else
                    {
                        MsgobjList.Add(set_MObj("E", "业务单号不可为空"));
                        return MsgobjList;
                    }
                }
                else if (ListOrder.Count == 1)
                {
                    if (!string.IsNullOrEmpty(ListOrder[0].FOONO) && ListOrder[0].FOONO.Length >= 4)
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

                if (string.IsNullOrEmpty(ListOrder[0].ORDERCODE))
                {
                    MsgobjList.Add(set_MObj("E", "业务单号不可为空" + ListOrder[0].FOONO));
                    return MsgobjList;
                }

                string BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);

                //if (!string.IsNullOrEmpty(BUSITYPE) && BUSITYPE != "0")
                //{
                //    if (BUSITYPE == "20")
                //    {

                //        sql = "select id,ALLOWDECLARE from list_order where  code='" + ListOrder[0].ORDERCODE + "'";
                //        dt = DBMgr.GetDataTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["ALLOWDECLARE"] + "" == "1")
                //            {
                //                MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")报关可执行，不可发送多次"));
                //                return MsgobjList;
                //            }
                //            else
                //            {
                //                if (string.IsNullOrEmpty(ListOrder[0].ALLOWDECLARE + ""))
                //                {
                //                    MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")报关不可执行，不可发送多次"));
                //                    return MsgobjList;
                //                }
                //            }
                //        }

                //        sql = "select * from list_statuslog where ordercode ='" + ListOrder[0].ORDERCODE + "' and statuscode >'99'";
                //        dt = DBMgr.GetDataTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")单证已经提交报关单，不可发送"));
                //            return MsgobjList;
                //        }
                //    }
                //    else
                //    {
                //        sql = "select id from list_order where  code='" + ListOrder[0].ORDERCODE + "'";
                //        dt = DBMgr.GetDataTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")数据不可重复发送"));
                //            return MsgobjList;
                //        }
                //    }
                //}



                if (!string.IsNullOrEmpty(BUSITYPE) && BUSITYPE != "0")
                {

                    if (BUSITYPE == "40" || BUSITYPE == "41")
                    {
                        sql = @"select ONLYCODE from  list_order where ONLYCODE =(select ONLYCODE from list_order where  code='" + ListOrder[0].ORDERCODE + "')";
                        dt = DBMgr.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count != GroupOrder.Count)
                            {
                                MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")跟原始的不匹配！"));
                                return MsgobjList;
                            }
                            else
                            {
                                sql = @"select id from  list_order where ONLYCODE = '" + dt.Rows[0]["ONLYCODE"] + "' and code = '" + ListOrder[0].ORDERCODE + "' and WTFS = '" + ListOrder[0].ENTRUSTTYPEID + "'";
                                dt = DBMgr.GetDataTable(sql);
                                if (dt.Rows.Count < 1)
                                {
                                    MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")跟原始的主数据不匹配！"));
                                    return MsgobjList;
                                }
                            }
                        }
                    }



                    if (BUSITYPE != "20")
                    {
                        sql = "select id,ONLYCODE from list_order where  code='" + ListOrder[0].ORDERCODE + "' and IFSEND='1'";
                        dt = DBMgr.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")已经发送下游，不可在发！"));
                            return MsgobjList;
                        }

                    }
                    else if (BUSITYPE == "20")
                    {
                        sql = "select id,ONLYCODE from list_order where  code='" + ListOrder[0].ORDERCODE + "' and IFSEND='1' and SENDNUMBER='1'";
                        dt = DBMgr.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(ListOrder[0].ALLOWDECLARE))
                            {
                                MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")以发送一次给下游，在发必须报关可执行！"));
                                return MsgobjList;
                            }
                        }

                        sql = "select id,ONLYCODE from list_order where  code='" + ListOrder[0].ORDERCODE + "' and IFSEND='1' and SENDNUMBER='2'";
                        dt = DBMgr.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")已经下游2次了，不可在发！"));
                            return MsgobjList;
                        }

                    }





                }




                //string BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);
                //if (!string.IsNullOrEmpty(BUSITYPE) && BUSITYPE != "0")
                //{

                //    string ENTRUSTTYPE = GetENTRUSTTYPEID(ListOrder, BUSITYPE);
                //    if (BUSITYPE == "40" || BUSITYPE == "41")
                //    {
                //        sql = "select id from list_order where  code='" + ListOrder[0].ORDERCODE + "'";
                //        dt = DBMgr.GetDataTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")数据不可重复发送"));
                //            return MsgobjList;
                //        }
                //    }
                //    else
                //    {
                //        sql = "select id,ENTRUSTTYPEID from list_order where  code='" + ListOrder[0].ORDERCODE + "'";
                //        dt = DBMgr.GetDataTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["ENTRUSTTYPEID"] + "" != ENTRUSTTYPE)
                //            {
                //                MsgobjList.Add(set_MObj("E", "(" + ListOrder[0].FOONO + ")跟之前数据不匹配"));
                //                return MsgobjList;
                //            }
                //        }
                //    }
                //}

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
                    MsgobjList.Add(set_MObj("E", "凭证类型不可为空" + o.FOONO));
                }
                else if (BUSITYPE == "0")
                {
                    MsgobjList.Add(set_MObj("E", "凭证类型无法匹配" + o.FOONO));
                }

                //二次调整
                if (string.IsNullOrEmpty(o.BUSIUNITNAME) || o.BUSIUNITNAME.Length < 11)
                {
                    MsgobjList.Add(set_MObj("E", "经营单位不合格" + o.FOONO));
                }
                else
                {
                    string BUSIUNITCODE = o.BUSIUNITNAME.Substring(o.BUSIUNITNAME.Length - 10, 10);
                    if (!Regex.IsMatch(BUSIUNITCODE, @"^[A-Za-z0-9]+$"))
                    {
                        MsgobjList.Add(set_MObj("E", "经营单位不合格" + o.FOONO));
                    }
                }



                //二次调整
                if (string.IsNullOrEmpty(o.ORDERCODE))
                {
                    MsgobjList.Add(set_MObj("E", "业务单号不可为空" + o.FOONO));
                }


                if (string.IsNullOrEmpty(o.CUSTOMDISTRICTCODE))
                {
                    MsgobjList.Add(set_MObj("E", "申报关区不可为空" + o.FOONO));
                }
            }
            return MsgobjList;

        }

        //业务类型转换
        public static string JudgeBusiType(string busitype, string ENTRUSTTYPEID)
        {
            string busitypeid = "0";
            if (busitype.IndexOf("空运进口") >= 0)
            {
                busitypeid = "11";
                IFS.XCBUSINAME = "空进";
            }
            if (busitype.IndexOf("空运出口") >= 0)
            {
                busitypeid = "10";
                IFS.XCBUSINAME = "空出";
            }
            if (busitype.IndexOf("海运进口") >= 0)
            {
                busitypeid = "21";
                IFS.XCBUSINAME = "海进";
            }
            if (busitype.IndexOf("海运出口") >= 0)
            {
                busitypeid = "20";
                IFS.XCBUSINAME = "海出";
            }
            if (busitype.IndexOf("陆运进口") >= 0)
            {
                busitypeid = "31";
                IFS.XCBUSINAME = "陆进";
            }
            if (busitype.IndexOf("陆运出口") >= 0)
            {
                busitypeid = "30";
                IFS.XCBUSINAME = "陆出";
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
                IFS.XCBUSINAME = "特殊监管";
            }


            if (busitype.IndexOf("叠加保税") >= 0 || busitype.IndexOf("国内") >= 0)
            {
                //委托方式   进口企业/出口企业/HUB仓进/HUB仓出

                if (ENTRUSTTYPEID == "进口企业" || ENTRUSTTYPEID == "HUB 仓进")
                {
                    busitypeid = "41";
                }
                if (ENTRUSTTYPEID == "出口企业" || ENTRUSTTYPEID == "HUB 仓出")
                {
                    busitypeid = "40";
                }


                if (busitype.IndexOf("叠加保税") >= 0)
                {
                    IFS.XCBUSINAME = "叠加保税";
                }
                else
                {
                    IFS.XCBUSINAME = "国内";
                }

            }

            return busitypeid;
        }

        //整合订单
        public static List<List<OrderEn>> GroupByFoo(List<OrderEn> oes)
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
                    if (oe.ENTRUSTTYPEID == "HUB 仓进")
                    {
                        oes_split3.Add(oe);
                    }
                    if (oe.ENTRUSTTYPEID == "HUB 仓出")
                    {
                        oes_split4.Add(oe);
                    }
                }
                if (oes_split1.Count > 0)
                {
                    lloes.Add(oes_split1);
                }
                if (oes_split2.Count > 0)
                {
                    lloes.Add(oes_split2);
                }
                if (oes_split3.Count > 0)
                {
                    lloes.Add(oes_split3);
                }
                if (oes_split4.Count > 0)
                {
                    lloes.Add(oes_split4);
                }
            }
            return lloes;
        }


        //整合订单 并且把报关的FOO放在第一个，报关单FOO为主数据
        public static List<List<OrderEn>> GroupByConFoo(List<OrderEn> oes)
        {
            List<List<OrderEn>> lloes = new List<List<OrderEn>>();
            List<OrderEn> oes_split1 = new List<OrderEn>();
            List<OrderEn> oes_split2 = new List<OrderEn>();
            List<OrderEn> oes_split3 = new List<OrderEn>();
            List<OrderEn> oes_split4 = new List<OrderEn>();
            List<OrderEn> new_oes;
            string FOONO = "";

            // 进口企业/出口企业/HUB仓进/HUB仓出
            if (string.IsNullOrEmpty(oes[0].ENTRUSTTYPEID))
            {
                if (oes.Count == 2)
                {
                    FOONO = oes[0].FOONO.Substring(0, 4);
                    if (FOONO == "SOBJ")
                    {
                        new_oes = new List<OrderEn>();
                        new_oes.Add(oes[1]);
                        new_oes.Add(oes[0]);

                        lloes.Add(new_oes);
                    }
                    else
                    {
                        lloes.Add(oes);
                    }
                }
                else
                {
                    lloes.Add(oes);
                }
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
                    if (oe.ENTRUSTTYPEID == "HUB 仓进")
                    {
                        oes_split3.Add(oe);
                    }
                    if (oe.ENTRUSTTYPEID == "HUB 仓出")
                    {
                        oes_split4.Add(oe);
                    }
                }

                if (oes_split1.Count == 2)
                {
                    FOONO = oes_split1[0].FOONO.Substring(0, 4);
                    if (FOONO == "SOBJ")
                    {
                        new_oes = new List<OrderEn>();
                        new_oes.Add(oes_split1[1]);
                        new_oes.Add(oes_split1[0]);
                        lloes.Add(new_oes);
                    }
                    else
                    {
                        lloes.Add(oes_split1);
                    }
                }
                else if (oes_split1.Count == 1)
                {
                    lloes.Add(oes_split1);
                }


                if (oes_split2.Count == 2)
                {
                    FOONO = oes_split2[0].FOONO.Substring(0, 4);
                    if (FOONO == "SOBJ")
                    {
                        new_oes = new List<OrderEn>();
                        new_oes.Add(oes_split2[1]);
                        new_oes.Add(oes_split2[0]);
                        lloes.Add(new_oes);
                    }
                    else
                    {
                        lloes.Add(oes_split2);
                    }
                }
                else if (oes_split2.Count == 1)
                {
                    lloes.Add(oes_split2);
                }


                if (oes_split3.Count == 2)
                {
                    FOONO = oes_split3[0].FOONO.Substring(0, 4);
                    if (FOONO == "SOBJ")
                    {
                        new_oes = new List<OrderEn>();
                        new_oes.Add(oes_split3[1]);
                        new_oes.Add(oes_split3[0]);
                        lloes.Add(new_oes);
                    }
                    else
                    {
                        lloes.Add(oes_split3);
                    }
                }
                else if (oes_split3.Count == 1)
                {
                    lloes.Add(oes_split3);
                }


                if (oes_split4.Count == 2)
                {
                    FOONO = oes_split4[0].FOONO.Substring(0, 4);
                    if (FOONO == "SOBJ")
                    {
                        new_oes = new List<OrderEn>();
                        new_oes.Add(oes_split4[1]);
                        new_oes.Add(oes_split4[0]);
                        lloes.Add(new_oes);
                    }
                    else
                    {
                        lloes.Add(oes_split4);
                    }
                }
                else if (oes_split4.Count == 1)
                {
                    lloes.Add(oes_split4);
                }

            }
            return lloes;
        }


        //获取委托类型
        public static string GetENTRUSTTYPEID(List<OrderEn> oes, string busitype)
        {
            string ENTRUSTTYPEID = "";
            if (busitype == "11" || busitype == "10" || busitype == "21" || busitype == "20" || busitype == "31" || busitype == "30" || busitype == "40" || busitype == "41" || busitype == "51" || busitype == "50")
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

        public static Msgobj set_MObj(string MSG_TYPE, string MSG_TXT)
        {
            Msgobj MO = new Msgobj();
            MO.MSG_ID = 1;
            MO.MSG_TYPE = MSG_TYPE;
            MO.MSG_TXT = MSG_TXT;
            return MO;
        }

        //存日志  1 sap->现场  2 现场->单证云
        public static void save_log(List<Msgobj> MSList, string FWONO, string source)
        {
            if (source == "1")
            {
                source = "SAP->新关务";
            }
            else if (source == "2")
            {
                source = "新关务->单证云";
            }
            else if (source == "3")
            {
                source = "新关务->SAP";
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

        public static void Callback_TM(string type, string id, string ASSOCIATENO)
        {
            if (type == "XIAOBAO")
            {
                //销保时间
                ZSXBSJ(id);
                //testZSXBSJ(id);

            }
            else if (type == "CHAYANSTART")
            {
                //查验起始时间
                ZSHGXY(id);
            }
            else if (type == "CHAYANZHILINGXIAFA")
            {
                //查验时间
                ZSBJCY(id);
            }
            else if (type == "BAOJIANFANGXING")
            {
                //报检放行时间
                ZSSJFX(id);
            }
            else if (type == "CHAYANFANGXING")
            {
                //查验放行时间
                ZSSJCYWC(id);
            }
            else if (type == "BAORUHAIGUAN")
            {
                //报入海关时间
                ZSXCBG(id);
            }
            else if (type == "SHIWUFANGXING")
            {
                //实物放行时间
                ZSSWFX(id);
            }
        }


        // 报检异常
        public static void ZSBJ_ABNO(string code)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");

            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            sap.DT_CUS_CUS1002_REQITEMORDER Declaration;
            List<sap.DT_CUS_CUS1002_REQITEMORDER> Declaration_List = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();
            string sql = "select *　from list_order where code ='" + code + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONOBJ = "";
            if (dt.Rows.Count > 0)
            {
                FOONOBJ = dt.Rows[0]["FOONOBJ"] + "";
                FWONO = dt.Rows[0]["FWONO"] + "";
                sql = "select *　from LIST_INSPECTION where ordercode ='" + code + "' and ISDEL='0'";

                int SHANDANTOTAL = 0;
                int GAIDANTOTAL = 0;
                int CHAYANTOTAL = 0;
                dt = DBMgr.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Declaration = new sap.DT_CUS_CUS1002_REQITEMORDER();
                    Declaration.ZBGDH = dt.Rows[i]["INSPECTIONCODE"] + "";
                    Declaration.ZZGYLH = dt.Rows[i]["CLEARANCECODE"] + "";
                    Declaration.ZBGDZS = dt.Rows[i]["SHEETNUM"] + "";

                    if (!string.IsNullOrEmpty(dt.Rows[i]["SHANDANTOTAL"] + ""))
                    {
                        SHANDANTOTAL += int.Parse(dt.Rows[i]["SHANDANTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["GAIDANTOTAL"] + ""))
                    {
                        GAIDANTOTAL += int.Parse(dt.Rows[i]["GAIDANTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CHAYANTOTAL"] + ""))
                    {
                        CHAYANTOTAL += int.Parse(dt.Rows[i]["CHAYANTOTAL"] + "");
                    }
                    Declaration_List.Add(Declaration);
                }
                if (Declaration_List.Count > 0)
                {
                    m.ORDER = Declaration_List.ToArray();
                }
                m.ZCYCS = CHAYANTOTAL + "";
                m.ZBGSDCS = SHANDANTOTAL + "";//删单次数
                m.ZBGGDCS = GAIDANTOTAL + "";//改单次数

                ////调档标记
                ////删单
                //if (dt.Rows[0]["IFSHANDAN"] + "" == "1")
                //{
                //    m.ZSFBGSD = "X";
                //}
                ////改单
                //if (dt.Rows[0]["IFGAIDAN"] + "" == "1")
                //{
                //    m.ZSFBGGD = "X";
                //}

                ////查验
                //if (dt.Rows[0]["IFCHAYAN"] + "" == "1")
                //{
                //    m.ZBJCYBJ = "X";
                //}

                ////熏蒸
                //if (dt.Rows[0]["IFXUNZHENG"] + "" == "1")
                //{
                //    m.ZXZBJ = "X";
                //}
                if (!string.IsNullOrEmpty(FOONOBJ))
                {
                    FOONOBJ = FOONOBJ.Remove(0, 4);

                    string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
                    m.EVENT_CODE = "ZSBJ_ABNO";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONOBJ;
                    m.EVENT_DAT = datetime;


                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSBJ_ABNO(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSBJ_ABNO(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }
                }
            }


        }


        // 报关异常
        public static void ZSBG_ABNO(string code)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");

            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            sap.DT_CUS_CUS1002_REQITEMORDER Declaration;
            List<sap.DT_CUS_CUS1002_REQITEMORDER> Declaration_List = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();

            string sql = "select *　from list_order where code ='" + code + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            if (dt.Rows.Count > 0)
            {
                FWONO = dt.Rows[0]["FWONO"] + "";
                FOONO = dt.Rows[0]["FOONO"] + "";

                sql = "select *　from LIST_DECLARATION where ordercode ='" + code + "' and ISDEL='0' ";

                int TIAODANGTOTAL = 0;
                int LIHUOTOTAL = 0;
                int SHANDANTOTAL = 0;
                int GAIDANTOTAL = 0;
                int CHAYANTOTAL = 0;
                dt = DBMgr.GetDataTable(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Declaration = new sap.DT_CUS_CUS1002_REQITEMORDER();
                    Declaration.ZBGDH = dt.Rows[i]["DECLARATIONCODE"] + "";
                    Declaration.ZZGYLH = dt.Rows[i]["TRANSNAME"] + "";
                    Declaration.ZBGDZS = dt.Rows[i]["SHEETNUM"] + "";

                    if (!string.IsNullOrEmpty(dt.Rows[i]["TIAODANGTOTAL"] + ""))
                    {
                        TIAODANGTOTAL += int.Parse(dt.Rows[i]["TIAODANGTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["LIHUOTOTAL"] + ""))
                    {
                        LIHUOTOTAL += int.Parse(dt.Rows[i]["LIHUOTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["SHANDANTOTAL"] + ""))
                    {
                        SHANDANTOTAL += int.Parse(dt.Rows[i]["SHANDANTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["GAIDANTOTAL"] + ""))
                    {
                        GAIDANTOTAL += int.Parse(dt.Rows[i]["GAIDANTOTAL"] + "");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CHAYANTOTAL"] + ""))
                    {
                        CHAYANTOTAL += int.Parse(dt.Rows[i]["CHAYANTOTAL"] + "");
                    }
                    Declaration_List.Add(Declaration);
                }
                if (Declaration_List.Count > 0)
                {
                    m.ORDER = Declaration_List.ToArray();
                }

                m.ZDDCS = TIAODANGTOTAL + "";//调档次数
                m.ZLHCS = LIHUOTOTAL + "";//理货次数
                m.ZBGSDCS = SHANDANTOTAL + "";//删单次数
                m.ZBGGDCS = GAIDANTOTAL + "";//改单次数
                m.ZCYCS = CHAYANTOTAL + "";

                //m.ZHGXYBZ = dt.Rows[0]["CHAYANTOTAL"] + "";//查验备注

                //查验标记
                //if (dt.Rows[0]["IFCHAYAN"] + "" == "1")
                //{
                //    m.ZBGCYBJ = "X";
                //}

                //调档标记
                //if (dt.Rows[0]["IFTIAODANG"] + "" == "1")
                //{
                //    m.ZDDBJ = "X";
                //}

                ////理货标记
                //if (dt.Rows[0]["LIHUOSIGN"] + "" == "1")
                //{
                //    m.ZLHBJ = "X";
                //}

                ////扣货标记
                //if (dt.Rows[0]["KOUHUOSIGN"] + "" == "1")
                //{
                //    m.ZKHBJ = "X";
                //}

                ////删单
                //if (dt.Rows[0]["IFSHANDAN"] + "" == "1")
                //{
                //    m.ZSFBGSD = "X";
                //}
                ////改单
                //if (dt.Rows[0]["IFGAIDAN"] + "" == "1")
                //{
                //    m.ZSFBGGD = "X";
                //}
            }
            if (!string.IsNullOrEmpty(FOONO))
            {
                FOONO = FOONO.Remove(0, 4);
            }
            string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            m.EVENT_CODE = "ZSBG_ABNO";
            m.FWO_ID = FWONO;
            m.FOO_ID = FOONO;
            m.EVENT_DAT = datetime;


            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;

            List<Msgobj> MSList = new List<Msgobj>();
            sap.DT_CUS_CUS1002_RES res;
            try
            {
                res = api.SI_CUS_CUS1002(mlist);
                MSList.Add(set_MObj(res.EV_ERROR, "ZSBG_ABNO(" + res.EV_MSG + ")"));
                save_log(MSList, FWONO, "3");
            }
            catch (Exception e)
            {
                MSList.Add(set_MObj("E", "ZSBG_ABNO(接口回调报错)"));
                save_log(MSList, FWONO, "3");
            }

        }


        // 销保时间
        public static void ZSXBSJ(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            //table
            //sap.DT_CUS_CUS1002_REQITEMORDER order = new sap.DT_CUS_CUS1002_REQITEMORDER();
            //sap.DT_CUS_CUS1002_REQITEMORDER order1 = new sap.DT_CUS_CUS1002_REQITEMORDER();
            //order.ZBGDH = "1";
            //order.ZBGDZS = "2";
            //order.ZMYFS = "3";
            //order1.ZBGDH = "a";
            //order1.ZBGDZS = "b";
            //order1.ZMYFS = "c";

            //List<sap.DT_CUS_CUS1002_REQITEMORDER> orderList = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();
            //orderList.Add(order);
            //orderList.Add(order1);

            string sql = "select *　from list_order where id ='" + id + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["XIAOBAOTIME"] + "") && !string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                {
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    FOONO = dt.Rows[0]["FOONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["XIAOBAOTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["XIAOBAOTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    if (!string.IsNullOrEmpty(FOONO))
                    {
                        FOONO = FOONO.Remove(0, 4);
                    }
                    m.EVENT_CODE = "ZSXBSJ";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;
                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSXBSJ(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSXBSJ(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }
                }
            }
            //string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            //m.ORDER = orderList.ToArray();
        }


        //测试接口
        public static void testZSXBSJ(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            string FWONO = "00000320000000001631";
            string FOONO = "00000000800000015970";
            m.EVENT_CODE = "ZSXBSJ";
            m.FWO_ID = FWONO;
            m.FOO_ID = FOONO;
            m.EVENT_DAT = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;
            List<Msgobj> MSList = new List<Msgobj>();
            sap.DT_CUS_CUS1002_RES res;
            try
            {
                res = api.SI_CUS_CUS1002(mlist);
                MSList.Add(set_MObj(res.EV_ERROR, "ZSXBSJ(" + res.EV_MSG + ")"));
                save_log(MSList, FWONO, "3");
            }
            catch (Exception e)
            {
                MSList.Add(set_MObj("E", "ZSXBSJ(接口回调报错)"));
                save_log(MSList, FWONO, "3");
            }
        }

        // 单证过机
        public static void ZSDZGJ(string id)
        {
            id = "1605";
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            sap.DT_CUS_CUS1002_REQITEMORDER Declaration;
            List<sap.DT_CUS_CUS1002_REQITEMORDER> Declaration_List = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();

            string sql;
            DataTable dt;
            sql = "select *　from list_order where id ='" + id + "'";
            dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                {
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    FOONO = dt.Rows[0]["FOONO"] + "";
                    if (!string.IsNullOrEmpty(FOONO))
                    {
                        FOONO = FOONO.Remove(0, 4);
                    }
                    m.EVENT_CODE = "ZSDZGJ";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss"); ;
                    sql = "select * from list_declaration where ORDERCODE='" + dt.Rows[0]["CODE"] + "'";
                    dt = DBMgr.GetDataTable(sql);
                    int COMMODITYNUM = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Declaration = new sap.DT_CUS_CUS1002_REQITEMORDER();
                            Declaration.ZBGDH = dt.Rows[0]["DECLARATIONCODE"] + "";
                            Declaration.ZBGDZS = dt.Rows[0]["SHEETNUM"] + "";
                            //Declaration.ZMYFS = dt.Rows[0]["COMMODITYNUM"] + "";
                            COMMODITYNUM += int.Parse(dt.Rows[0]["COMMODITYNUM"] + "");
                            Declaration_List.Add(Declaration);
                        }
                    }
                    if (Declaration_List.Count > 0)
                    {
                        m.ORDER = Declaration_List.ToArray();
                    }
                    m.ZBGDGS = COMMODITYNUM + "";

                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;
                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSDZGJ(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSDZGJ(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }
                }

            }

        }


        //测试回传 sap 接口 关务接单
        public static void ZSGWJD_tm()
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");

            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型

            m.EVENT_CODE = "ZSBG_ABNO";
            //m.FWO_ID = "410000000001036";
            //m.FOO_ID = "800000000751";
            m.EVENT_DAT = "20160929101010";


            m.FWO_ID = "310000000001085";
            m.FOO_ID = "800000000474";
            m.ZDDCS = "10";
            m.ZDDBJ = "X";

            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;

            sap.DT_CUS_CUS1002_RES res = api.SI_CUS_CUS1002(mlist);


        }

        //预录入（空运） ZSYLR
        public static void ZSYLR_tm()
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");

            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型


            //table
            //sap.DT_CUS_CUS1002_REQITEMORDER order = new sap.DT_CUS_CUS1002_REQITEMORDER();
            //sap.DT_CUS_CUS1002_REQITEMORDER order1 = new sap.DT_CUS_CUS1002_REQITEMORDER();

            //order.ZBGDH = "1";
            //order.ZBGDZS = "2";
            //order.ZMYFS = "3";

            //order1.ZBGDH = "a";
            //order1.ZBGDZS = "b";
            //order1.ZMYFS = "c";

            //List<sap.DT_CUS_CUS1002_REQITEMORDER> orderList = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();
            //orderList.Add(order);
            //orderList.Add(order1);

            //string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            //m.EVENT_CODE = "ZSXBSJ";
            //m.FWO_ID = FWONO;
            //m.FOO_ID = FOONO;
            //m.EVENT_DAT = datetime;
            //m.ORDER = orderList.ToArray();



            //table
            sap.DT_CUS_CUS1002_REQITEMORDER order = new sap.DT_CUS_CUS1002_REQITEMORDER();
            sap.DT_CUS_CUS1002_REQITEMORDER order1 = new sap.DT_CUS_CUS1002_REQITEMORDER();

            order.ZBGDH = "1";
            order.ZBGDZS = "2";
            order.ZMYFS = "3";

            order1.ZBGDH = "a";
            order1.ZBGDZS = "b";
            order1.ZMYFS = "c";

            List<sap.DT_CUS_CUS1002_REQITEMORDER> orderList = new List<sap.DT_CUS_CUS1002_REQITEMORDER>();
            orderList.Add(order);
            orderList.Add(order1);


            m.EVENT_CODE = "ZSYLR";
            m.FWO_ID = "800000000750";
            m.FOO_ID = "410000000001036";
            m.EVENT_DAT = "20161017150505";
            m.ORDER = orderList.ToArray();


            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;

            sap.DT_CUS_CUS1002_RES res = api.SI_CUS_CUS1002(mlist);

        }

        //资料收集
        public static string ZSZLSJ_TM(string FWO, string FOO)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            m.EVENT_CODE = "ZSZLSJ";
            m.FWO_ID = FWO;
            m.FOO_ID = FOO;
            m.EVENT_DAT = "20161017150505";
            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;

            List<Msgobj> MSList = new List<Msgobj>();
            sap.DT_CUS_CUS1002_RES res;
            try
            {
                res = api.SI_CUS_CUS1002(mlist);
            }
            catch (Exception e)
            {
                MSList.Add(set_MObj("E", "ZSZLSJ(接口回调报错)"));
                save_log(MSList, FWO, "3");
                return "E";
            }
            MSList.Add(set_MObj(res.EV_ERROR, res.EV_MSG));
            save_log(MSList, FWO, "3");
            return res.EV_ERROR;
        }

        // 海关查验时间
        public static void ZSHGXY(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            DataTable dt;
            string sql;
            sap.DT_CUS_CUS1002_REQITEM[] mlist;
            List<Msgobj> MSList;
            sql = "select *　from list_order where id ='" + id + "'";
            dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONO"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CHAYANSTARTTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["CHAYANSTARTTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSHGXY";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;
                    MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSHGXY(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSHGXY(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }
                }

                string CODE = dt.Rows[0]["CODE"] + "";
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";
                if (!string.IsNullOrEmpty(ASSOCIATENO))
                {
                    sql = "select *　from list_order where ASSOCIATENO ='" + ASSOCIATENO + "' and CODE !='" + CODE + "'";
                    dt = DBMgr.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                        {
                            FOONO = dt.Rows[0]["FOONO"] + "";
                            FOONO = FOONO.Remove(0, 4);
                            FWONO = dt.Rows[0]["FWONO"] + "";
                            m.EVENT_CODE = "ZSHGXY";
                            m.FWO_ID = FWONO;
                            m.FOO_ID = FOONO;
                            if (!string.IsNullOrEmpty(EVENT_DAT))
                            {
                                m.EVENT_DAT = EVENT_DAT;
                            }
                            else
                            {
                                EVENT_DAT = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
                            }
                            mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                            mlist[0] = m;
                            MSList = new List<Msgobj>();
                            sap.DT_CUS_CUS1002_RES res;
                            try
                            {
                                res = api.SI_CUS_CUS1002(mlist);
                                MSList.Add(set_MObj(res.EV_ERROR, "ZSHGXY(" + res.EV_MSG + ")"));
                                save_log(MSList, FWONO, "3");
                            }
                            catch (Exception e)
                            {
                                MSList.Add(set_MObj("E", "ZSHGXY(接口回调报错)"));
                                save_log(MSList, FWONO, "3");
                            }
                        }
                    }
                }

            }
        }
        // 实物放行（口岸／属地）
        public static void ZSSWFX(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            DataTable dt;
            string sql;
            sap.DT_CUS_CUS1002_REQITEM[] mlist;
            List<Msgobj> MSList;
            sql = "select *　from list_order where id ='" + id + "'";
            dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONO"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SHIWUFANGXINGTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["SHIWUFANGXINGTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSSWFX";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSSWFX(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSSWFX(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }
                }

                string CODE = dt.Rows[0]["CODE"] + "";
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";
                if (!string.IsNullOrEmpty(ASSOCIATENO))
                {
                    sql = "select *　from list_order where ASSOCIATENO ='" + ASSOCIATENO + "' and CODE !='" + CODE + "'";
                    dt = DBMgr.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                        {
                            FOONO = dt.Rows[0]["FOONO"] + "";
                            FOONO = FOONO.Remove(0, 4);
                            FWONO = dt.Rows[0]["FWONO"] + "";
                            m.EVENT_DAT = EVENT_DAT;
                            m.EVENT_CODE = "ZSSWFX";
                            m.FWO_ID = FWONO;
                            m.FOO_ID = FOONO;
                            if (!string.IsNullOrEmpty(EVENT_DAT))
                            {
                                m.EVENT_DAT = EVENT_DAT;
                            }
                            else
                            {
                                EVENT_DAT = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
                            }
                            mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                            mlist[0] = m;

                            MSList = new List<Msgobj>();
                            sap.DT_CUS_CUS1002_RES res;
                            try
                            {
                                res = api.SI_CUS_CUS1002(mlist);
                                MSList.Add(set_MObj(res.EV_ERROR, "ZSSWFX(" + res.EV_MSG + ")"));
                                save_log(MSList, FWONO, "3");
                            }
                            catch (Exception e)
                            {
                                MSList.Add(set_MObj("E", "ZSSWFX(接口回调报错)"));
                                save_log(MSList, FWONO, "3");
                            }
                        }
                    }
                }




            }
        }
        // 现场报关（口岸／属地）
        public static void ZSXCBG(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型

            DataTable dt;
            string sql;
            sap.DT_CUS_CUS1002_REQITEM[] mlist;
            List<Msgobj> MSList;

            sql = "select *　from list_order where id ='" + id + "'";
            dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONO"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BAORUHAIGUANTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["BAORUHAIGUANTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSXCBG";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSXCBG(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSXCBG(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }

                }
                string CODE = dt.Rows[0]["CODE"] + "";
                string ASSOCIATENO = dt.Rows[0]["ASSOCIATENO"] + "";
                if (!string.IsNullOrEmpty(ASSOCIATENO))
                {
                    sql = "select *　from list_order where ASSOCIATENO ='" + ASSOCIATENO + "' and CODE !='" + CODE + "'";
                    dt = DBMgr.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FOONO"] + ""))
                        {
                            FOONO = dt.Rows[0]["FOONO"] + "";
                            FOONO = FOONO.Remove(0, 4);
                            FWONO = dt.Rows[0]["FWONO"] + "";
                            if (!string.IsNullOrEmpty(EVENT_DAT))
                            {
                                m.EVENT_DAT = EVENT_DAT;
                            }
                            else
                            {
                                EVENT_DAT = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
                            }
                            m.EVENT_CODE = "ZSXCBG";
                            m.FWO_ID = FWONO;
                            m.FOO_ID = FOONO;
                            m.EVENT_DAT = EVENT_DAT;
                            mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                            mlist[0] = m;

                            MSList = new List<Msgobj>();
                            sap.DT_CUS_CUS1002_RES res;
                            try
                            {
                                res = api.SI_CUS_CUS1002(mlist);
                                MSList.Add(set_MObj(res.EV_ERROR, "ZSXCBG(" + res.EV_MSG + ")"));
                                save_log(MSList, FWONO, "3");
                            }
                            catch (Exception e)
                            {
                                MSList.Add(set_MObj("E", "ZSXCBG(接口回调报错)"));
                                save_log(MSList, FWONO, "3");
                            }
                        }
                    }
                }



            }
        }
        // 报检查验
        public static void ZSBJCY(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            string sql = "select *　from list_order where id ='" + id + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONOBJ"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONOBJ"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CHAYANZHILINGXIAFATIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["CHAYANZHILINGXIAFATIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSBJCY";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSBJCY(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSBJCY(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }

                }
            }
        }
        // 商检放行
        public static void ZSSJFX(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            string sql = "select *　from list_order where id ='" + id + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONOBJ"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONOBJ"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BAOJIANFANGXINGTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["BAOJIANFANGXINGTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSSJFX";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSSJFX(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSSJFX(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }

                }
            }
        }
        // 商检查验完成
        public static void ZSSJCYWC(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");
            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            string sql = "select *　from list_order where id ='" + id + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            string EVENT_DAT = "";
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FOONOBJ"] + ""))
                {
                    FOONO = dt.Rows[0]["FOONOBJ"] + "";
                    FOONO = FOONO.Remove(0, 4);
                    FWONO = dt.Rows[0]["FWONO"] + "";
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CHAYANFANGXINGTIME"] + ""))
                    {
                        EVENT_DAT = Convert.ToDateTime(dt.Rows[0]["CHAYANFANGXINGTIME"]).ToString("yyyyMMddHHmmss");
                    }
                    m.EVENT_CODE = "ZSSJCYWC";
                    m.FWO_ID = FWONO;
                    m.FOO_ID = FOONO;
                    m.EVENT_DAT = EVENT_DAT;
                    sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
                    mlist[0] = m;

                    List<Msgobj> MSList = new List<Msgobj>();
                    sap.DT_CUS_CUS1002_RES res;
                    try
                    {
                        res = api.SI_CUS_CUS1002(mlist);
                        MSList.Add(set_MObj(res.EV_ERROR, "ZSSJCYWC(" + res.EV_MSG + ")"));
                        save_log(MSList, FWONO, "3");
                    }
                    catch (Exception e)
                    {
                        MSList.Add(set_MObj("E", "ZSSJCYWC(接口回调报错)"));
                        save_log(MSList, FWONO, "3");
                    }

                }
            }
        }

    }
}