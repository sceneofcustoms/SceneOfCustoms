﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace SceneOfCustoms.Controllers
{
    public class InterfaceController : Controller
    {


        //public static void SaveDZOrder(string FWO, string ONLYCODE)
        //{
        //    ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
        //    ServiceReference1.OrderEn DZOrder;
        //    List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();
        //    ServiceReference1.ContainerEn ContainerEn;
        //    ServiceReference1.FileEn FileEn;
        //    List<ServiceReference1.FileEn> FileEnList = new List<ServiceReference1.FileEn>();
        //    List<ServiceReference1.ContainerEn> ContainerEnList = new List<ServiceReference1.ContainerEn>();
        //    DataTable dt;
        //    DataTable dtCon;
        //    DataTable dtFile;
        //    string sql = "select * from list_order where ONLYCODE ='" + ONLYCODE + "'";
        //    dt = DBMgr.GetDataTable(sql);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DZOrder = new ServiceReference1.OrderEn();
        //        DZOrder.DOCSERVICECODE = "GWYKS";
        //        DZOrder.DOCSERVICENAME = "关务云昆山";
        //        DZOrder.CUSTOMERCODE = "KSJSBGYXGS";
        //        DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";
        //        DZOrder.CUSNO = dt.Rows[i]["CODE"] + "";
        //        DZOrder.FIRSTLADINGBILLNO = dt.Rows[i]["FIRSTLADINGBILLNO"] + ""; //海关提单号
        //        DZOrder.SECONDLADINGBILLNO = dt.Rows[i]["SECONDLADINGBILLNO"] + ""; //国检提单号
        //        DZOrder.ENTRUSTTYPE = dt.Rows[i]["ENTRUSTTYPEID"] + "";
        //        DZOrder.BUSITYPE = dt.Rows[i]["BUSITYPE"] + "";
        //        DZOrder.REPWAYID = dt.Rows[i]["REPWAYID"] + "";
        //        DZOrder.CUSTOMAREACODE = dt.Rows[i]["CUSTOMDISTRICTCODE"] + "";
        //        DZOrder.DECLWAY = dt.Rows[i]["DECLWAY"] + "";
        //        DZOrder.BUSIUNITCODE = dt.Rows[i]["BUSIUNITCODE"] + "";
        //        DZOrder.BUSIUNITNAME = dt.Rows[i]["BUSIUNITNAME"] + "";
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["GOODSNUM"] + ""))
        //        {
        //            DZOrder.GOODSNUM = Decimal.Parse(dt.Rows[i]["GOODSNUM"] + "");
        //        }
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["GOODSWEIGHT"] + ""))
        //        {
        //            DZOrder.GOODSGW = Decimal.Parse(dt.Rows[i]["GOODSWEIGHT"] + "");
        //        }
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["CHECKEDWEIGHT"] + ""))
        //        {
        //            DZOrder.GOODSNW = Decimal.Parse(dt.Rows[i]["CHECKEDWEIGHT"] + "");
        //        }
        //        DZOrder.PACKKINDNAME = dt.Rows[i]["PACKKIND"] + "";
        //        DZOrder.GOODSTYPEID = dt.Rows[i]["GOODSTYPEID"] + "";
        //        //贸易方式
        //        //DZOrder.TRADEWAYCODE = dt.Rows[i]["TRADEWAYCODES"] + "";
        //        DZOrder.ORDERREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
        //        DZOrder.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
        //        DZOrder.REPUNITNAME = dt.Rows[i]["REPUNITNAME"] + "";
        //        DZOrder.INSPREPCODE = dt.Rows[i]["INSPUNITCODE"] + "";
        //        DZOrder.INSPREPNAME = dt.Rows[i]["INSPUNITNAME"] + "";
        //        DZOrder.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
        //        DZOrder.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
        //        DZOrder.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
        //        DZOrder.PORTNAME = dt.Rows[i]["PORTCODE"] + "";


        //        //DZOrder.PORTNAME = dt.Rows[i]["PORTCODE"] + "";
        //        DZOrder.TRADEWAYNAME = dt.Rows[i]["TRADEWAYCODES"] + "";

        //        if (!string.IsNullOrEmpty(dt.Rows[i]["PAYPOYALTIES"] + ""))
        //        {
        //            DZOrder.PAYPOYALTIES = Int32.Parse(dt.Rows[i]["PAYPOYALTIES"] + "");
        //        }

        //        if (!string.IsNullOrEmpty(dt.Rows[i]["PRICEIMPACT"] + ""))
        //        {
        //            DZOrder.PRICEIMPACT = Int32.Parse(dt.Rows[i]["PRICEIMPACT"] + "");
        //        }
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["SPECIALRELATIONSHIP"] + ""))
        //        {
        //            DZOrder.SPECIALRELATIONSHIP = Int32.Parse(dt.Rows[i]["SPECIALRELATIONSHIP"] + "");
        //        }
        //        DZOrder.CORRESPONDNO = dt.Rows[i]["CORRESPONDNO"] + "";
        //        DZOrder.ASSOCIATENO = dt.Rows[i]["ASSOCIATENO"] + "";
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["SUBMITTIME"] + ""))
        //        {
        //            DZOrder.SUBMITTIME = Convert.ToDateTime(dt.Rows[i]["SUBMITTIME"]);
        //        }
        //        DZOrder.SUBMITUSERNAME = dt.Rows[i]["SUBMITUSERNAME"] + "";

        //        DZOrder.ARRIVEDNO = dt.Rows[i]["ARRIVEDNO"] + "";
        //        DZOrder.MANIFEST = dt.Rows[i]["MANIFEST"] + "";

        //        DZOrder.WOODPACKINGID = dt.Rows[i]["WOODPACKINGID"] + "";
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["WEIGHTCHECK"] + ""))
        //        {
        //            DZOrder.WEIGHTCHECK = Int32.Parse(dt.Rows[i]["WEIGHTCHECK"] + "");
        //        }
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["ISWEIGHTCHECK"] + ""))
        //        {
        //            DZOrder.ISWEIGHTCHECK = Int32.Parse(dt.Rows[i]["ISWEIGHTCHECK"] + "");
        //        }
        //        DZOrder.SHIPNAME = dt.Rows[i]["SHIPNAME"] + "";
        //        DZOrder.FILGHTNO = dt.Rows[i]["FILGHTNO"] + "";
        //        DZOrder.Number = Int32.Parse(dt.Rows[i]["SENDNUMBER"] + "");
        //        DZOrder.PLATFORMCODE = "xinguanwu";

        //        //集装箱
        //        sql = "select * from list_Declcontainertruck where ordercode='" + dt.Rows[i]["CODE"] + "'";
        //        dtCon = DBMgr.GetDataTable(sql);
        //        for (int j = 0; j < dtCon.Rows.Count; j++)
        //        {
        //            ContainerEn = new ServiceReference1.ContainerEn();
        //            ContainerEn.CDCARNAME = dtCon.Rows[j]["CDCARNAME"] + "";//沪BL1353
        //            ContainerEn.CDCARNO = dtCon.Rows[j]["CDCARNO"] + "";//2200172079
        //            ContainerEn.CONTAINERNO = dtCon.Rows[j]["CONTAINERNO"] + "";//TCLU5430888
        //            if (dtCon.Rows[i]["CONTAINERTYPE"].ToString().Length == 4)
        //            {
        //                ContainerEn.CONTAINERTYPE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Substring(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//hou GP
        //                ContainerEn.CONTAINERSIZE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Remove(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//qian   20
        //            }
        //            ContainerEnList.Add(ContainerEn);
        //        }
        //        DZOrder.ContainerList = ContainerEnList.ToArray();

        //        //文件
        //        sql = "select * from list_attachment where ordercode='" + dt.Rows[i]["CODE"] + "'";
        //        dtFile = DBMgr.GetDataTable(sql);
        //        string activeDir = @"C:\fileserver\";
        //        for (int j = 0; j < dtFile.Rows.Count; j++)
        //        {
        //            FileEn = new ServiceReference1.FileEn();
        //            FileEn.FileContent = GetFileData(activeDir + dtFile.Rows[j]["FILEPATH"] + "");
        //            FileEn.FileFormat = ServiceReference1.FileFormatEnum.PDF;
        //            FileEnList.Add(FileEn);
        //        }
        //        DZOrder.Files = FileEnList.ToArray();
        //        DZOrderList.Add(DZOrder);
        //    }

        //    string DZ_res = danzheng.SendOrderData(DZOrderList.ToArray());
        //    Msgobj MO = new Msgobj();
        //    List<Msgobj> MSList = new List<Msgobj>();

        //    if (DZ_res == "success")
        //    {
        //        string NewTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
        //        MO.MSG_TYPE = "S";
        //        MO.MSG_TXT = "下发成功";
        //        sql = @"update list_order set IFSEND='1',SENDTIME =to_date('" + NewTime + "','yyyy-mm-dd hh24:mi:ss') where  ONLYCODE ='" + ONLYCODE + "'";
        //        DBMgr.ExecuteNonQuery(sql);
        //    }
        //    else
        //    {
        //        MO.MSG_TYPE = "E";
        //        MO.MSG_TXT = DZ_res;
        //    }
        //    MSList.Add(MO);
        //    save_log(MSList, FWO + "", "2");
        //}

        //测试
        public ActionResult test()
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
                    DZOrder = new ServiceReference1.OrderEn();
                    DZOrder.DOCSERVICECODE = "GWYKS";
                    DZOrder.DOCSERVICENAME = "关务云昆山";
                    DZOrder.CUSTOMERCODE = "KSJSBGYXGS";
                    DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";
                    DZOrder.CUSNO = "xinguanwu1";
                    DZOrder.FIRSTLADINGBILLNO = "xinguanwu";
                    DZOrder.SECONDLADINGBILLNO = "xinguanwu";
                    DZOrder.ENTRUSTTYPE = "xinguanwu";
                    DZOrder.BUSITYPE = "xinguanwu";
                    DZOrder.REPWAYID = "xinguanwu";
                    DZOrder.CUSTOMAREACODE = "xinguanwu";
                    DZOrder.DECLWAY = "xinguanwu";
                    DZOrder.BUSIUNITCODE = "xinguanwu";
                    DZOrder.BUSIUNITNAME = "xinguanwu";

                    DZOrder.PACKKINDNAME = "xinguanwu";
                    DZOrder.GOODSTYPEID = "xinguanwu";
                    //贸易方式
                    //DZOrder.TRADEWAYCODE = dt.Rows[i]["TRADEWAYCODES"] + "";
                    DZOrder.ORDERREQUEST = "xinguanwu";
                    DZOrder.REPUNITCODE = "xinguanwu";
                    DZOrder.REPUNITNAME = "xinguanwu";
                    DZOrder.INSPREPCODE = "xinguanwu";
                    DZOrder.INSPREPNAME = "xinguanwu";
                    DZOrder.TOTALNO = "xinguanwu";
                    DZOrder.DIVIDENO = "xinguanwu";
                    DZOrder.TURNPRENO = "xinguanwu";
                    DZOrder.PORTNAME = "xinguanwu";


                    //DZOrder.PORTNAME = dt.Rows[i]["PORTCODE"] + "";
                    DZOrder.TRADEWAYNAME = "xinguanwu";



                    DZOrder.SUBMITUSERNAME = "xinguanwu";

                    DZOrder.ARRIVEDNO = "xinguanwu";
                    DZOrder.MANIFEST = "xinguanwu";

                    DZOrder.WOODPACKINGID = "xinguanwu";
         
                    DZOrder.SHIPNAME = "xinguanwu";
                    DZOrder.FILGHTNO = "xinguanwu";
                    DZOrder.Number = 2;
                    DZOrder.PLATFORMCODE = "xinguanwu";

                    //集装箱
                    //sql = "select * from list_Declcontainertruck where ordercode='" + dt.Rows[i]["CODE"] + "'";
                    //dtCon = DBMgr.GetDataTable(sql);
                    //for (int j = 0; j < dtCon.Rows.Count; j++)
                    //{
                    //    ContainerEn = new ServiceReference1.ContainerEn();
                    //    ContainerEn.CDCARNAME = dtCon.Rows[j]["CDCARNAME"] + "";//沪BL1353
                    //    ContainerEn.CDCARNO = dtCon.Rows[j]["CDCARNO"] + "";//2200172079
                    //    ContainerEn.CONTAINERNO = dtCon.Rows[j]["CONTAINERNO"] + "";//TCLU5430888
                    //    if (dtCon.Rows[i]["CONTAINERTYPE"].ToString().Length == 4)
                    //    {
                    //        ContainerEn.CONTAINERTYPE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Substring(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//hou GP
                    //        ContainerEn.CONTAINERSIZE = dtCon.Rows[j]["CONTAINERTYPE"].ToString().Remove(dtCon.Rows[j]["CONTAINERTYPE"].ToString().Length - 2, 2);//qian   20
                    //    }
                    //    ContainerEnList.Add(ContainerEn);
                    //DZOrder.ContainerList = ContainerEnList.ToArray();

                    //文件
                    //sql = "select * from list_attachment where ordercode='" + dt.Rows[i]["CODE"] + "'";
                    //dtFile = DBMgr.GetDataTable(sql);
                    //string activeDir = @"C:\fileserver\";
                    //for (int j = 0; j < dtFile.Rows.Count; j++)
                    //{
                    string activeDir = @"C:\fileserver\";
                    FileEn = new ServiceReference1.FileEn();
                    FileEn.FileContent = GetFileData(activeDir + "/2016-11-26/60c4a1b7-89cf-4108-abaa-795884b3a5a6.pdf");
                    FileEn.FileFormat = ServiceReference1.FileFormatEnum.PDF;
                    FileEnList.Add(FileEn);
                    //}
                    DZOrder.Files = FileEnList.ToArray();

                    DZOrderList.Add(DZOrder);
            //    }

                string DZ_res = danzheng.SendOrderData(DZOrderList.ToArray());



 


            //string UserName = ConfigurationManager.AppSettings["FTPUserName"];
            //string Password = ConfigurationManager.AppSettings["FTPPassword"];
            //System.Uri Uri = new Uri("ftp://" + ConfigurationManager.AppSettings["FTPServer"] + ":" + ConfigurationManager.AppSettings["FTPPortNO"]);

            //FtpHelper ftp = new FtpHelper(Uri, UserName, Password);
            //if (dt.Rows.Count > 0)
            //{
            //    if (ftp.FileExist(dt.Rows[0]["FILEPATH"] + ""))
            //    {
            //        string localpath = @"d:\ftptest\" + dt.Rows[0]["FILENAME"];
            //        ftp.DownloadFile(dt.Rows[0]["FILEPATH"] + "", localpath);
            //        FileInfo fi = new FileInfo(localpath);
            //        string new_name = Guid.NewGuid().ToString();
            //        fi.MoveTo(@"d:\ftptest\" + new_name + ".pdf");
            //        ftp.UploadFile(@"d:\ftptest\" + new_name + ".pdf", "/2016-11-24/" + new_name + ".pdf");
            //    }
            //}


            //string test = "/2016-11-19/61a39279-8d14-47f4-8dc0-7b4e33e44632.pdf";
            //string tSt = test.Substring(0,11);

            //string activeDir = @"C:\fileserver\";
            //string Path = "";
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Path = activeDir + dt.Rows[i]["FILEPATH"] + "";
            //    byte[] asdasd = GetFileData(Path);
            //}
            return View();
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

        //public byte[] GetFileData(string fileUrl)
        //{
        //    FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
        //    try
        //    {
        //        byte[] buffur = new byte[fs.Length];
        //        fs.Read(buffur, 0, (int)fs.Length);

        //        return buffur;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBoxHelper.ShowPrompt(ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {

        //            //关闭资源
        //            fs.Close();
        //        }
        //    }
        //}

        public string save_dl()
        {
            IDatabase db = SeRedis.redis.GetDatabase();
            string json = "{\"ONLYCODE\":'0c111a4a-965a-425a-ab2c-963180c01380'}";
            db.ListRightPush("XGW_CheckFile", json);
            return "";
        }

        [HttpPost]
        //测试tm过来数据
        public string testTm()
        {
            List<OrderEn> ld = new List<OrderEn>();
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            OrderEn obj;
            string ids = Request.Form["ids"];
            string sql = "select * from list_sapfoo where id in(" + ids + ") order by id desc";
            DataTable dt = DBMgr.GetDataTable(sql);
            DataTable dtdecl;
            bd.SyncDataFromSapSoapClient xc = new bd.SyncDataFromSapSoapClient();
            bd.OrderEn lcorder = new bd.OrderEn();
            List<bd.OrderEn> list = new List<bd.OrderEn>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lcorder = new bd.OrderEn();
                lcorder.BUSITYPE = dt.Rows[i]["BUSITYPE"] + "";
                lcorder.CODE = dt.Rows[i]["FWONO"] + "";

                lcorder.SENDURL = dt.Rows[i]["SENDURL"] + "";
                lcorder.TONGGUANFSCODE = dt.Rows[i]["TONGGUANFSCODE"] + "";
                lcorder.TONGGUANFSNAME = dt.Rows[i]["TONGGUANFSNAME"] + "";
                lcorder.CGGROUPCODE = dt.Rows[i]["CGGROUPCODE"] + "";
                lcorder.CGGROUPNAME = dt.Rows[i]["CGGROUPNAME"] + "";

                lcorder.FOONO = dt.Rows[i]["FOONO"] + "";
                lcorder.ORDERCODE = dt.Rows[i]["CODE"] + "";
                lcorder.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
                lcorder.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
                lcorder.GOODSNUM = dt.Rows[i]["GOODSNUM"] + "";
                lcorder.GOODSWEIGHT = dt.Rows[i]["GOODSWEIGHT"] + "";
                lcorder.PACKKIND = dt.Rows[i]["PACKKIND"] + "";
                lcorder.REPWAYID = dt.Rows[i]["REPWAYID"] + "";
                lcorder.DECLWAY = dt.Rows[i]["DECLWAY"] + "";
                lcorder.TRADEWAYCODES = dt.Rows[i]["TRADEWAYCODES"] + "";
                lcorder.CUSNO = dt.Rows[i]["CUSNO"] + "";
                lcorder.CUSTOMDISTRICTCODE = dt.Rows[i]["CUSTOMDISTRICTCODE"] + "";
                lcorder.PORTCODE = dt.Rows[i]["PORTCODE"] + "";
                lcorder.PRICEIMPACT = dt.Rows[i]["PRICEIMPACT"] + "";
                lcorder.PAYPOYALTIES = dt.Rows[i]["PAYPOYALTIES"] + "";
                //lcorder.SFGOODSUNIT = dt.Rows[i]["SFGOODSUNIT"] + "";

                lcorder.FGOODSUNIT = dt.Rows[i]["FGOODSUNIT"] + "";
                lcorder.SGOODSUNIT = dt.Rows[i]["SGOODSUNIT"] + "";
                lcorder.ALLOWDECLARE = dt.Rows[i]["ALLOWDECLARE"] + "";

                lcorder.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
                lcorder.CREATEUSERNAME = dt.Rows[i]["CREATEUSERNAME"] + "";
                lcorder.CREATETIME = dt.Rows[i]["CREATETIME"] + "";
                lcorder.ARRIVEDNO = dt.Rows[i]["ARRIVEDNO"] + "";
                lcorder.CHECKEDGOODSNUM = dt.Rows[i]["CHECKEDGOODSNUM"] + "";
                lcorder.CHECKEDWEIGHT = dt.Rows[i]["CHECKEDWEIGHT"] + "";
                lcorder.ENTRUSTTYPEID = dt.Rows[i]["ENTRUSTTYPEID"] + "";
                lcorder.GOODSXT = dt.Rows[i]["GOODSXT"] + "";
                lcorder.BUSIUNITNAME = dt.Rows[i]["BUSIUNITNAME"] + "";
                lcorder.GOODSTYPEID = dt.Rows[i]["GOODSTYPEID"] + "";
                lcorder.LADINGBILLNO = dt.Rows[i]["LADINGBILLNO"] + "";
                lcorder.ISPREDECLARE = dt.Rows[i]["ISPREDECLARE"] + "";
                lcorder.ENTRUSTREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
                lcorder.CONTRACTNO = dt.Rows[i]["CONTRACTNO"] + "";
                lcorder.FIRSTLADINGBILLNO = dt.Rows[i]["FIRSTLADINGBILLNO"] + "";
                lcorder.SECONDLADINGBILLNO = dt.Rows[i]["SECONDLADINGBILLNO"] + "";
                lcorder.MANIFEST = dt.Rows[i]["MANIFEST"] + "";
                lcorder.WOODPACKINGID = dt.Rows[i]["WOODPACKINGID"] + "";
                lcorder.WEIGHTCHECK = dt.Rows[i]["WEIGHTCHECK"] + "";
                lcorder.ISWEIGHTCHECK = dt.Rows[i]["ISCHECKEDWEIGHT"] + "";
                lcorder.SHIPNAME = dt.Rows[i]["SHIPNAME"] + "";
                lcorder.FILGHTNO = dt.Rows[i]["FILGHTNO"] + "";
                lcorder.INSPUNITNAME = dt.Rows[i]["INSPUNITCODE"] + "";
                lcorder.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
                lcorder.INVOICENO = dt.Rows[i]["INVOICENO"] + "";
                lcorder.SPECIALRELATIONSHIP = dt.Rows[i]["SPECIALRELATIONSHIP"] + "";

                List<bd.Declcontainertruck> dlist = new List<bd.Declcontainertruck>();

                sql = "select * from list_declcontainertruck where ordercode='" + dt.Rows[i]["CODE"] + "" + "'";
                dtdecl = DBMgr.GetDataTable(sql);
                for (int j = 0; j < dtdecl.Rows.Count; j++)
                {
                    bd.Declcontainertruck d = new bd.Declcontainertruck();
                    d.CDCARNAME = dtdecl.Rows[j]["CDCARNAME"] + "";
                    d.CDCARNO = dtdecl.Rows[j]["CDCARNO"] + "";
                    d.CONTAINERNO = dtdecl.Rows[j]["CONTAINERNO"] + "";
                    d.CONTAINERTYPE = dtdecl.Rows[j]["CONTAINERTYPE"] + "";
                    dlist.Add(d);
                }

                lcorder.Declcontainertruck = dlist.ToArray();
                list.Add(lcorder);
            }

            bd.Msgobj[] mo = xc.SyncData(list.ToArray());

            if (list.Count > 0)
            {

                //MSList = IFS.CheckData(ld);



                //    if (MSList.Count <= 0)
                //    {
                //        //int Order_Res = IFS.XCOrderData(ld);




                //        //ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
                //        //ServiceReference1.OrderEn DZOrder;
                //        //List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();


                //        //if (Order_Res == 1)
                //        //{
                //        //    MSList.Add(IFS.set_MObj("S", "保存成功"));
                //        //    IFS.SaveDZOrder(ld[0].CODE);

                //        //}
                //        //else
                //        //{
                //        //    MSList.Add(IFS.set_MObj("E", "保存失败"));
                //        //}
                //    }

            }
            //else
            //{
            //    MSList.Add(IFS.set_MObj("E", "没有指令"));
            //}

            //IFS.save_log(MSList, list[0].CODE, "1");

            return "1";
        }


        //转换单证数据
        //private ServiceReference1.OrderEn ZDOrderData(List<OrderEn> ListOrder)
        //{
        //    string sql = "";
        //    string name = "";
        //    DataTable dt;
        //    ServiceReference1.OrderEn DZOrder = new ServiceReference1.OrderEn();

        //    DZOrder.CUSNO = ListOrder[0].CODE; //企业编号
        //    DZOrder.REPNO = ""; //申报单位编号   --
        //    DZOrder.ENTRUSTTYPE =  GetENTRUSTTYPEID(ListOrder, ListOrder[0].BUSITYPE); //委托类型代码

        //    //委托类型名称
        //    if (DZOrder.ENTRUSTTYPE == "01")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报关";
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "02")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报检";
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "03")
        //    {
        //        DZOrder.ENTRUSTTYPENAME = "报关报检";
        //    }

        //    //业务类型代码
        //    DZOrder.BUSITYPE = JudgeBusiType(ListOrder[0].BUSITYPE, ListOrder[0].ENTRUSTTYPEID);
        //    //业务类型名称 --
        //    DZOrder.BUSITYPENAME = "";

        //    //申报方式代码
        //    sql = "select CODE, NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + ListOrder[0].REPWAYID + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.REPWAYID = dt.Rows[0]["CODE"] + "";
        //        //申报方式名称 --
        //        DZOrder.REPWAYNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //申报关区代码
        //    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + ListOrder[0].CUSTOMDISTRICTCODE + "' ORDER BY CODE";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.CUSTOMAREACODE = dt.Rows[0]["CODE"] + "";
        //        //申报关区代码 --
        //        DZOrder.CUSTOMAREANAME = dt.Rows[0]["NAME"] + "";
        //    }
        //    //报关方式代码
        //    sql = "select CODE,NAME  from SYS_DECLWAY where enabled=1 and NAME ='" + ListOrder[0].DECLWAY + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.DECLWAY = dt.Rows[0]["CODE"] + "";
        //        //报关方式名称 --
        //        DZOrder.DECLWAYNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //经营单位代码
        //    name = ListOrder[0].BUSIUNITNAME.Remove(ListOrder[0].BUSIUNITNAME.Length - 10, 10);
        //    sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
        //    dt = DB_BaseData.GetDataTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DZOrder.BUSIUNITCODE = dt.Rows[0]["CODE"] + "";
        //        //经营单位名称
        //        DZOrder.BUSIUNITNAME = dt.Rows[0]["NAME"] + "";
        //    }

        //    //经营单位社会号
        //    DZOrder.BUSIUNITNUM = "";

        //    //件数
        //    DZOrder.GOODSNUM = Int32.Parse(ListOrder[0].GOODSNUM);

        //    //毛重
        //    DZOrder.GOODSGW = decimal.Parse(ListOrder[0].GOODSWEIGHT);


        //    //净重
        //    //if (string.IsNullOrEmpty(ListOrder[0].CHECKEDWEIGHT))
        //    //{
        //    //    DZOrder.GOODSNW = decimal.Parse(ListOrder[0].CHECKEDWEIGHT);

        //    //}

        //    //包装种类名称
        //    DZOrder.PACKKINDNAME = ListOrder[0].PACKKIND;

        //    //订单要求 --
        //    DZOrder.ORDERREQUEST = ListOrder[0].ENTRUSTREQUEST;

        //    //委托单位代码
        //    DZOrder.CUSTOMERCODE = "3223980001";
        //    //委托单位代码
        //    DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";


        //    //申报单位  报关 报检

        //    if (DZOrder.ENTRUSTTYPE == "01")
        //    {

        //        DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "02")
        //    {
        //        DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //        DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //    }
        //    else if (DZOrder.ENTRUSTTYPE == "03")
        //    {
        //        DZOrder.REPUNITCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.REPUNITNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].REPUNITCODE.Length - 10, 10);
        //        DZOrder.INSPREPCODE = ListOrder[0].REPUNITCODE.Substring(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //        DZOrder.INSPREPNAME = ListOrder[0].REPUNITCODE.Remove(ListOrder[0].INSPUNITNAME.Length - 10, 10);
        //    }




        //    //总单号
        //    DZOrder.TOTALNO = ListOrder[0].TOTALNO;

        //    //分单号
        //    DZOrder.TOTALNO = ListOrder[0].TOTALNO;

        //    //转关预录入号
        //    DZOrder.TURNPRENO = ListOrder[0].TURNPRENO;

        //    //进出口岸
        //    DZOrder.PORTCODE = ListOrder[0].PORTCODE;

        //    //委托时间
        //    DZOrder.SUBMITTIME = DateTime.Now;

        //    //委托人员
        //    DZOrder.SUBMITUSERNAME = ListOrder[0].CREATEUSERNAME;

        //    //运抵编号
        //    DZOrder.ARRIVEDNO = ListOrder[0].ARRIVEDNO;

        //    //货物类型
        //    DZOrder.GOODSTYPEID = ListOrder[0].GOODSTYPEID;

        //    //海关提单号 二程提单号
        //    DZOrder.SECONDLADINGBILLNO = ListOrder[0].SECONDLADINGBILLNO;

        //    //国检提单号 一程提单号
        //    DZOrder.FIRSTLADINGBILLNO = ListOrder[0].FIRSTLADINGBILLNO;

        //    //载货清单号
        //    DZOrder.MANIFEST = ListOrder[0].MANIFEST;

        //    //载货清单号
        //    DZOrder.MANIFEST = ListOrder[0].MANIFEST;

        //    //木质包装
        //    DZOrder.WOODPACKINGID = ListOrder[0].WOODPACKINGID;



        //    if (ListOrder[0].WEIGHTCHECK == "")
        //    {
        //        ListOrder[0].WEIGHTCHECK = "0";
        //    }
        //    else
        //    {
        //        ListOrder[0].WEIGHTCHECK = "1";
        //    }


        //    if (ListOrder[0].ISWEIGHTCHECK == "")
        //    {
        //        ListOrder[0].ISWEIGHTCHECK = "0";
        //    }
        //    else
        //    {
        //        ListOrder[0].ISWEIGHTCHECK = "1";
        //    }

        //    //是否需要重量确认
        //    DZOrder.WEIGHTCHECK = Int32.Parse(ListOrder[0].WEIGHTCHECK);

        //    //重量确认
        //    DZOrder.ISWEIGHTCHECK = Int32.Parse(ListOrder[0].ISWEIGHTCHECK);

        //    //船名
        //    DZOrder.SHIPNAME = ListOrder[0].SHIPNAME;

        //    //航次
        //    DZOrder.FILGHTNO = ListOrder[0].FILGHTNO;

        //    //通关单号
        //    //DZOrder.CLEARANCENO = ListOrder[0].CLEARANCENO;

        //    return DZOrder;
        //}






    }
}