using SceneOfCustoms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace SceneOfCustoms.Common
{
    public class IFS
    {

        //推送到单证的数据
        public static void SaveDZOrder(string FWO)
        {
            ServiceReference1.CustomerServiceSoapClient danzheng = new ServiceReference1.CustomerServiceSoapClient();
            ServiceReference1.OrderEn DZOrder;
            List<ServiceReference1.OrderEn> DZOrderList = new List<ServiceReference1.OrderEn>();
            DataTable dt;
            string sql = "select * from list_order where FWONO ='" + FWO + "'  ";
            dt = DBMgr.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DZOrder = new ServiceReference1.OrderEn();

                DZOrder.CUSTOMERCODE = "KSJSBGYXGS";
                DZOrder.CUSTOMERNAME = "昆山吉时报关有限公司";
                DZOrder.CUSNO = dt.Rows[i]["CODE"] + "";
                DZOrder.REPNO = "";
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
                DZOrder.PACKKIND = dt.Rows[i]["PACKKIND"] + "";

                DZOrder.ORDERREQUEST = dt.Rows[i]["ENTRUSTREQUEST"] + "";
                DZOrder.REPUNITCODE = dt.Rows[i]["REPUNITCODE"] + "";
                DZOrder.REPUNITNAME = dt.Rows[i]["REPUNITNAME"] + "";
                DZOrder.INSPREPCODE = dt.Rows[i]["INSPUNITCODE"] + "";
                DZOrder.INSPREPNAME = dt.Rows[i]["INSPUNITNAME"] + "";
                DZOrder.TOTALNO = dt.Rows[i]["TOTALNO"] + "";
                DZOrder.DIVIDENO = dt.Rows[i]["DIVIDENO"] + "";
                DZOrder.TURNPRENO = dt.Rows[i]["TURNPRENO"] + "";
                DZOrder.PORTCODE = dt.Rows[i]["PORTCODE"] + "";
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
                DZOrderList.Add(DZOrder);
            }

            string DZ_res = danzheng.SendOrderData(DZOrderList.ToArray());
            Msgobj MO = new Msgobj();
            List<Msgobj> MSList = new List<Msgobj>();
            if (DZ_res == "success")
            {
                MO.MSG_TYPE = "S";
                MO.MSG_TXT = "下发成功";
            }
            else
            {
                MO.MSG_TYPE = "E";
                MO.MSG_TXT = DZ_res;
            }
            MSList.Add(MO);
            save_log(MSList, FWO + "", "2");
        }

        //保存现场数据
        public static int XCOrderData(List<OrderEn> ld)
        {
            string ASSOCIATENO = "";
            string CORRESPONDNO = "";
            string ASS1 = "";
            string ASS2 = "";
            List<List<OrderEn>> GroupOrder = GroupByFoo(ld);


            //关联号 如果4单 2单和4单关联号一样，下面在去判断 2单关联号
            foreach (List<OrderEn> ListOrder in GroupOrder)
            {
                if (GroupOrder.Count > 2)
                {
                    if (ListOrder[0].ENTRUSTTYPEID == "进口企业")
                    {
                        CORRESPONDNO = "GF" + ListOrder[0].ORDERCODE;
                        ASS1 = "GL" + ListOrder[0].ORDERCODE;

                    }
                    if (ListOrder[0].ENTRUSTTYPEID == "HUB 仓进")
                    {
                        ASS2 = "GL" + ListOrder[0].ORDERCODE;
                    }

                }
                else
                {
                    if (ListOrder[0].ENTRUSTTYPEID == "进口企业")
                    {
                        ASSOCIATENO = "GL" + ListOrder[0].ORDERCODE;
                    }
                }
            }

            DataTable dt;
            string sql = "";
            DateTime dtime = DateTime.Now;

            foreach (List<OrderEn> o in GroupOrder)
            {
                //关联号
                if (GroupOrder.Count > 2)
                {
                    if (o[0].ENTRUSTTYPEID == "HUB 仓进" || o[0].ENTRUSTTYPEID == "HUB 仓出")
                    {
                        ASSOCIATENO = ASS2;
                    }
                    else
                    {
                        ASSOCIATENO = ASS1;
                    }
                }
                //委托类型中文
                string WTFS = o[0].ENTRUSTTYPEID;



                //业务类型代码
                o[0].BUSITYPE = JudgeBusiType(o[0].BUSITYPE, o[0].ENTRUSTTYPEID);

                //委托类型代码 01,02,03。分别表示报关、报检、报关报检
                o[0].ENTRUSTTYPEID = GetENTRUSTTYPEID(o, o[0].BUSITYPE);
                //FWO订单号

                //总单号
                //分单号
                //件数
                //毛重
                //发货单位 
                string FGOODSUNITCODE = "";
                if (!string.IsNullOrEmpty(o[0].FGOODSUNIT) && o[0].FGOODSUNIT.Length > 10)
                {
                    FGOODSUNITCODE = o[0].FGOODSUNIT.Substring(o[0].FGOODSUNIT.Length - 10, 10);
                    o[0].FGOODSUNIT = o[0].FGOODSUNIT.Remove(o[0].FGOODSUNIT.Length - 10, 10);
                }
                //收货单位 
                string SGOODSUNITCODE = "";
                if (!string.IsNullOrEmpty(o[0].SGOODSUNIT) && o[0].SGOODSUNIT.Length > 10)
                {
                    SGOODSUNITCODE = o[0].SGOODSUNIT.Substring(o[0].SGOODSUNIT.Length - 10, 10);
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
                if (o[0].SPECIALRELATIONSHIP == "")
                {
                    o[0].SPECIALRELATIONSHIP = "0";
                }
                else
                {
                    o[0].SPECIALRELATIONSHIP = "1";
                }
                //价格影响确认
                if (o[0].PRICEIMPACT == "")
                {
                    o[0].PRICEIMPACT = "0";
                }
                else
                {
                    o[0].PRICEIMPACT = "1";
                }
                //支付特许权使用费确认
                if (o[0].PAYPOYALTIES == "")
                {
                    o[0].PAYPOYALTIES = "0";
                }
                else
                {
                    o[0].PAYPOYALTIES = "1";
                }
                //申报单位  报关 报检
                string REPUNITNAME = "";
                string INSPUNITCODE = "";
                if (o[0].ENTRUSTTYPEID == "01")
                {
                    if (!string.IsNullOrEmpty(o[0].REPUNITCODE) && o[0].REPUNITCODE.Length > 10)
                    {
                        o[0].REPUNITCODE = o[0].REPUNITCODE.Substring(o[0].REPUNITCODE.Length - 10, 10);
                        REPUNITNAME = o[0].REPUNITCODE.Remove(o[0].REPUNITCODE.Length - 10, 10);
                        o[0].INSPUNITNAME = "";
                    }

                }
                else if (o[0].ENTRUSTTYPEID == "02")
                {
                    if (!string.IsNullOrEmpty(o[0].INSPUNITNAME) && o[0].INSPUNITNAME.Length > 10)
                    {
                        o[0].REPUNITCODE = "";
                        INSPUNITCODE = o[0].INSPUNITNAME.Substring(o[0].INSPUNITNAME.Length - 10, 10);
                        o[0].INSPUNITNAME = o[0].INSPUNITNAME.Remove(o[0].INSPUNITNAME.Length - 10, 10);
                    }
                }
                else if (o[0].ENTRUSTTYPEID == "03")
                {

                    if (o[0].FOONO.Substring(0, 4) == "SOBG")
                    {
                        if (!string.IsNullOrEmpty(o[0].REPUNITCODE) && o[0].REPUNITCODE.Length > 10)
                        {
                            REPUNITNAME = o[0].REPUNITCODE.Remove(o[0].REPUNITCODE.Length - 10, 10);
                            o[0].REPUNITCODE = o[0].REPUNITCODE.Substring(o[0].REPUNITCODE.Length - 10, 10);
                        }
                        else
                        {
                            REPUNITNAME = "";
                            o[0].REPUNITCODE = "";
                        }



                        if (!string.IsNullOrEmpty(o[1].INSPUNITNAME) && o[1].INSPUNITNAME.Length > 10)
                        {
                            INSPUNITCODE = o[1].INSPUNITNAME.Substring(o[1].INSPUNITNAME.Length - 10, 10);
                            o[0].INSPUNITNAME = o[1].INSPUNITNAME.Remove(o[1].INSPUNITNAME.Length - 10, 10);
                        }
                        else
                        {
                            INSPUNITCODE = "";
                            o[0].INSPUNITNAME = "";
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(o[1].REPUNITCODE) && o[1].REPUNITCODE.Length > 10)
                        {
                            o[0].REPUNITCODE = o[1].REPUNITCODE.Substring(o[1].REPUNITCODE.Length - 10, 10);
                            REPUNITNAME = o[1].REPUNITCODE.Remove(o[1].REPUNITCODE.Length - 10, 10);
                        }
                        else
                        {
                            o[0].REPUNITCODE = "";
                            REPUNITNAME = "";
                        }


                        if (!string.IsNullOrEmpty(o[0].INSPUNITNAME) && o[0].INSPUNITNAME.Length > 10)
                        {
                            INSPUNITCODE = o[0].INSPUNITNAME.Substring(o[0].INSPUNITNAME.Length - 10, 10);
                            o[0].INSPUNITNAME = o[0].INSPUNITNAME.Remove(o[0].INSPUNITNAME.Length - 10, 10);
                        }
                        else
                        {
                            INSPUNITCODE = "";
                            o[0].INSPUNITNAME = "";
                        }

                    }
                }
                //报关/报检指令 
                string FOONOBJ = "";
                if (o[0].ENTRUSTTYPEID == "02")
                {
                    FOONOBJ = o[0].FOONO;//报检
                    o[0].FOONO = "";//报关
                }
                if (o[0].ENTRUSTTYPEID == "03")
                {
                    if (o[0].FOONO.Substring(0, 4) == "SOBG")
                    {
                        //第一条指令为报关的话， 第二条指令一定为报检
                        FOONOBJ = o[1].FOONO;
                    }
                    else
                    {
                        //第一条指令为报检的话，第二条指令一定为报关
                        FOONOBJ = o[0].FOONO;
                        o[0].FOONO = o[1].FOONO;
                    }
                }
                //委托人
                string SUBMITUSERNAME = o[0].CREATEUSERNAME;
                //委托时间
                string SUBMITTIME = "";
                if (!string.IsNullOrEmpty(o[0].CREATETIME) && o[0].CREATETIME.Length == 22)
                {
                    SUBMITTIME = DateTime.ParseExact(o[0].CREATETIME, "yyyyMMddHHmmss.fffffff", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
                }


                //运抵编号
                //实际件数
                //实际毛重
                //经营单位
                string BUSIUNITCODE = "";
                if (!string.IsNullOrEmpty(o[0].BUSIUNITNAME) && o[0].BUSIUNITNAME.Length > 10)
                {
                    BUSIUNITCODE = o[0].BUSIUNITNAME.Substring(o[0].BUSIUNITNAME.Length - 10, 10);
                    o[0].BUSIUNITNAME = o[0].BUSIUNITNAME.Remove(o[0].BUSIUNITNAME.Length - 10, 10);
                }
                //货物类型
                //报关提单号
                //是否提前报关
                if (o[0].ISPREDECLARE == "")
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
                if (o[0].WEIGHTCHECK == "")
                {
                    o[0].WEIGHTCHECK = "0";
                }
                else
                {
                    o[0].WEIGHTCHECK = "1";
                }
                //重量确认标识
                if (o[0].ISWEIGHTCHECK == "")
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
                //报关可执行
                if (o[0].ALLOWDECLARE == "")
                {
                    o[0].ALLOWDECLARE = "0";
                }
                else
                {
                    o[0].ALLOWDECLARE = "1";
                }


                sql = @"insert into List_Order(
                   ID,CREATETIME,URL,
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
                   WTFS
                  ) VALUES(
                   LIST_ORDER_ID.Nextval,sysdate,'SAP',
                   '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                   '{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}',to_date('{28}','yyyy-mm-dd hh24:mi:ss'),'{29}','{30}','{31}','{32}',
                   '{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}',
                   '{48}','{49}','{50}','{51}','{52}','{53}'
                  )";

                sql = string.Format(sql,
                    o[0].BUSITYPE, o[0].CODE, o[0].FOONO, FOONOBJ,
                    o[0].ENTRUSTTYPEID,
                    o[0].TOTALNO, o[0].DIVIDENO, o[0].GOODSNUM, o[0].GOODSWEIGHT,
                    o[0].FGOODSUNIT, FGOODSUNITCODE, o[0].SGOODSUNIT, SGOODSUNITCODE,
                    o[0].PACKKIND, o[0].REPWAYID, o[0].DECLWAY, o[0].TRADEWAYCODES,
                    o[0].CUSNO, o[0].CUSTOMDISTRICTCODE, o[0].PORTCODE, o[0].SPECIALRELATIONSHIP,
                    o[0].PRICEIMPACT, o[0].PAYPOYALTIES, REPUNITNAME, o[0].REPUNITCODE,
                    o[0].INSPUNITNAME, INSPUNITCODE, SUBMITUSERNAME, SUBMITTIME,
                    o[0].ARRIVEDNO, o[0].CHECKEDGOODSNUM, o[0].CHECKEDWEIGHT, o[0].BUSIUNITNAME,
                    BUSIUNITCODE, o[0].GOODSTYPEID, o[0].LADINGBILLNO, o[0].ISPREDECLARE,
                    o[0].ENTRUSTREQUEST, o[0].CONTRACTNO, o[0].FIRSTLADINGBILLNO, o[0].SECONDLADINGBILLNO,
                    o[0].MANIFEST, o[0].WOODPACKINGID, o[0].WEIGHTCHECK, o[0].ISWEIGHTCHECK,
                    o[0].SHIPNAME, o[0].FILGHTNO, o[0].TURNPRENO, o[0].INVOICENO,
                    o[0].ALLOWDECLARE, o[0].ORDERCODE, ASSOCIATENO, CORRESPONDNO,
                    WTFS
                    );
                DBMgr.ExecuteNonQuery(sql);
            }
            //保存到单证
            return 1;
        }

        //检查数据
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

                if (string.IsNullOrEmpty(o.ORDERCODE))
                {
                    MsgobjList.Add(set_MObj("E", "业务单号不可为空" + o.FOONO));
                }


                ////报关申报单位
                //if (o.FOONO.Substring(0, 4) == "SOBG" && string.IsNullOrEmpty(o.REPUNITCODE))
                //{
                //    MsgobjList.Add(set_MObj("E", "报关申报单位不可为空" + o.FOONO));
                //}

                ////报检申报单位
                //if (o.FOONO.Substring(0, 4) == "SOBJ" && string.IsNullOrEmpty(o.INSPUNITNAME))
                //{
                //    MsgobjList.Add(set_MObj("E", "报检申报单位不可为空" + o.FOONO));
                //}



                //if (string.IsNullOrEmpty(o.REPWAYID))
                //{
                //    MsgobjList.Add(set_MObj("E", "申报方式不可为空" + o.FOONO));
                //}
                //else
                //{

                //    sql = "select CODE,NAME from SYS_REPWAY where Enabled=1 and  NAME = '" + o.REPWAYID + "'";
                //    dt = DB_BaseData.GetDataTable(sql);
                //    if (dt.Rows.Count <= 0)
                //    {
                //        MsgobjList.Add(set_MObj("E", "申报方式(" + o.REPWAYID + ")无法匹配" + o.FOONO));
                //    }
                //}

                //if (string.IsNullOrEmpty(o.CUSTOMDISTRICTCODE))
                //{
                //    MsgobjList.Add(set_MObj("E", "申报关区不可为空" + o.FOONO));
                //}
                //else
                //{
                //    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1  and NAME='" + o.CUSTOMDISTRICTCODE + "' ORDER BY CODE";

                //    dt = DB_BaseData.GetDataTable(sql);
                //    if (dt.Rows.Count <= 0)
                //    {
                //        MsgobjList.Add(set_MObj("E", "申报关区(" + o.CUSTOMDISTRICTCODE + ")无法匹配" + o.FOONO));
                //    }

                //}

                ////  REPUNITCODE  INSPUNITCODE

                //if (string.IsNullOrEmpty(o.DECLWAY))
                //{
                //    MsgobjList.Add(set_MObj("E", "报关方式不可为空" + o.FOONO));
                //}
                //else
                //{
                //    sql = "select CODE,NAME from SYS_DECLWAY where enabled=1 and NAME ='" + o.DECLWAY + "'";
                //    dt = DB_BaseData.GetDataTable(sql);
                //    if (dt.Rows.Count <= 0)
                //    {
                //        MsgobjList.Add(set_MObj("E", "报关方式(" + o.DECLWAY + ")无法匹配" + o.FOONO));
                //    }
                //}

                //if (string.IsNullOrEmpty(o.PORTCODE))
                //{
                //    MsgobjList.Add(set_MObj("E", "口岸关区不可为空" + o.FOONO));
                //}
                //else
                //{
                //    sql = "select CODE,NAME from BASE_CUSTOMDISTRICT  where ENABLED=1 and NAME ='" + o.PORTCODE + "'";
                //    dt = DB_BaseData.GetDataTable(sql);
                //    if (dt.Rows.Count <= 0)
                //    {
                //        MsgobjList.Add(set_MObj("E", "口岸关区(" + o.PORTCODE + ")无法匹配" + o.FOONO));
                //    }
                //}

                //if (string.IsNullOrEmpty(o.BUSIUNITNAME))
                //{
                //    MsgobjList.Add(set_MObj("E", "经营单位不可为空" + o.FOONO));
                //}
                //else
                //{
                //    string name = o.BUSIUNITNAME.Remove(o.BUSIUNITNAME.Length - 10, 10);
                //    sql = "SELECT CODE,NAME FROM BASE_COMPANY where CODE is not null and enabled=1 and NAME ='" + name + "'";
                //    dt = DB_BaseData.GetDataTable(sql);
                //    if (dt.Rows.Count <= 0)
                //    {
                //        MsgobjList.Add(set_MObj("E", "经营单位(" + o.BUSIUNITNAME + ")无法匹配" + o.FOONO));
                //    }
                //}


                //if (string.IsNullOrEmpty(o.GOODSWEIGHT))
                //{
                //    MsgobjList.Add(set_MObj("E", "毛重不可为空" + o.FOONO));
                //}


                //if (string.IsNullOrEmpty(o.GOODSNUM))
                //{
                //    MsgobjList.Add(set_MObj("E", "件数不可为空" + o.FOONO));
                //}


                //if (string.IsNullOrEmpty(o.TRADEWAYCODES))
                //{
                //    MsgobjList.Add(set_MObj("E", "贸易方式不可为空" + o.FOONO));
                //}
                //else
                //{
                //    //o.TRADEWAYCODES = o.TRADEWAYCODES.Substring(0, o.TRADEWAYCODES.Length - 1);
                //    string[] arr = o.TRADEWAYCODES.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int i = 0; i < arr.Length; i++)
                //    {
                //        if (arr[i] == "暂时进出口")
                //        {
                //            arr[i] = "暂时进出货物";
                //        }

                //        //贸易方式 
                //        sql = @"select ID,CODE,NAME from BASE_DECLTRADEWAY WHERE enabled=1 and NAME ='" + arr[i] + "'";
                //        dt = DB_BaseData.GetDataTable(sql);
                //        if (dt.Rows.Count <= 0)
                //        {
                //            MsgobjList.Add(set_MObj("E", "贸易方式(" + arr[i] + ")无法匹配" + o.FOONO));
                //        }
                //    }

                //}


                ////空运进口
                //if (BUSITYPE == "11")
                //{
                //    if (string.IsNullOrEmpty(o.DIVIDENO))
                //    {
                //        MsgobjList.Add(set_MObj("E", "分单号不可为空" + o.FOONO));
                //    }

                //    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                //    {
                //        MsgobjList.Add(set_MObj("E", "木质包装不可为空" + o.FOONO));
                //    }

                //}


                ////海
                //if (BUSITYPE == "21" || BUSITYPE == "22")
                //{
                //    if (string.IsNullOrEmpty(o.SHIPNAME))
                //    {
                //        MsgobjList.Add(set_MObj("E", "船名不可为空" + o.FOONO));
                //    }

                //    if (string.IsNullOrEmpty(o.FILGHTNO))
                //    {
                //        MsgobjList.Add(set_MObj("E", "航次不可为空" + o.FOONO));
                //    }

                //    if (string.IsNullOrEmpty(o.GOODSTYPEID))
                //    {
                //        MsgobjList.Add(set_MObj("E", "货物类型不可为空" + o.FOONO));
                //    }
                //}


                ////陆
                //if (BUSITYPE == "30" || BUSITYPE == "31")
                //{
                //    if (string.IsNullOrEmpty(o.WOODPACKINGID))
                //    {
                //        MsgobjList.Add(set_MObj("E", "木质包装不可为空" + o.FOONO));
                //    }
                //}



                //二次调整
                if (string.IsNullOrEmpty(o.BUSIUNITNAME))
                {
                    MsgobjList.Add(set_MObj("E", "经营单位不可为空" + o.FOONO));
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

        //获取委托类型
        public static string GetENTRUSTTYPEID(List<OrderEn> oes, string busitype)
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



        public static void Callback_TM(string type, string id)
        {
            if (type == "XIAOBAO")
            {
                ZSXBSJ(id);
            }
        }


        // 报检异常
        public static void ZSBJ_ABNO(string id)
        {
            sap.SI_CUS_CUS1002Service api = new sap.SI_CUS_CUS1002Service();
            api.Timeout = 6000000;
            api.Credentials = new NetworkCredential("soapcall", "soapcall");

            sap.DT_CUS_CUS1002_REQITEM m = new sap.DT_CUS_CUS1002_REQITEM();//模型
            string sql = "select *　from list_order where id ='" + id + "'";
            DataTable dt = DBMgr.GetDataTable(sql);
            string FWONO = "";
            string FOONO = "";
            if (dt.Rows.Count > 0)
            {
                FWONO = dt.Rows[0]["FWONO"] + "";
                FOONO = dt.Rows[0]["FOONOBJ"] + "";
                m.ZDDCS = dt.Rows[0]["TIAODANGTIMES"] + "";//调档次数
                m.ZBGSDCS = dt.Rows[0]["SHANDANTOTAL"] + "";//删单次数
                m.ZBGGDCS = dt.Rows[0]["GAIDANTOTAL"] + "";//改单次数
                m.ZHGXYBZ = dt.Rows[0]["CHAYANREMARK"] + "";//查验备注

                //调档标记

                //删单
                if (dt.Rows[0]["IFSHANDAN"] + "" == "1")
                {
                    m.ZSFBGSD = "X";
                }
                //改单
                if (dt.Rows[0]["IFGAIDAN"] + "" == "1")
                {
                    m.ZSFBGGD = "X";
                }

                //查验
                if (dt.Rows[0]["IFCHAYAN"] + "" == "1")
                {
                    m.ZBJCYBJ = "X";
                }

                //熏蒸
                if (dt.Rows[0]["IFXUNZHENG"] + "" == "1")
                {
                    m.ZXZBJ = "X";
                }
                //调档标记
            }
            if (!string.IsNullOrEmpty(FOONO))
            {
                FOONO = FOONO.Remove(0, 4);
            }

            string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            m.EVENT_CODE = "ZSBJ_ABNO";
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
                MSList.Add(set_MObj(res.EV_ERROR, "ZSBJ_ABNO(" + res.EV_MSG + ")"));
                save_log(MSList, FWONO, "3");
            }
            catch (Exception e)
            {
                MSList.Add(set_MObj("E", "ZSBJ_ABNO(接口回调报错)"));
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
                FWONO = dt.Rows[0]["FWONO"] + "";
                FOONO = dt.Rows[0]["FOONO"] + "";
                if (!string.IsNullOrEmpty(dt.Rows[0]["XIAOBAOTIME"] + ""))
                {
                    EVENT_DAT = DateTime.ParseExact(dt.Rows[0]["XIAOBAOTIME"] + "", "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyyMMddHHmmss");
                }
            }
            if (!string.IsNullOrEmpty(FOONO))
            {
                FOONO = FOONO.Remove(0, 4);
            }


            //string datetime = DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmss");
            m.EVENT_CODE = "ZSXBSJ";
            m.FWO_ID = FWONO;
            m.FOO_ID = FOONO;
            m.EVENT_DAT = EVENT_DAT;
            //m.ORDER = orderList.ToArray();
            sap.DT_CUS_CUS1002_REQITEM[] mlist = new sap.DT_CUS_CUS1002_REQITEM[1];
            mlist[0] = m;

            List<Msgobj> MSList = new List<Msgobj>();
            sap.DT_CUS_CUS1002_RES res;
            try
            {
                res = api.SI_CUS_CUS1002(mlist);
                MSList.Add(set_MObj(res.EV_ERROR, "ZSZLSJ(" + res.EV_MSG + ")"));
                save_log(MSList, FWONO, "3");
            }
            catch (Exception e)
            {
                MSList.Add(set_MObj("E", "ZSZLSJ(接口回调报错)"));
                save_log(MSList, FWONO, "3");
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

    }
}