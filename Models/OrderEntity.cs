﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SceneOfCustoms.Models
{
    public class OrderEntity
    {
        public string ID { get; set; }

        //编号
        public string CODE { get; set; }

        //业务类型
        public string BUSITYPE { get; set; }

        //客户编号
        public string CUSNO { get; set; }

        //经营单位代码
        public string BUSIUNITCODE { get; set; }


        //合同发票号
        public string CONTRACTNO { get; set; }

        //总单号
        public string TOTALNO { get; set; }


        //分单号
        public string DIVIDENO { get; set; }


        //转关预录号
        public string TURNPRENO { get; set; }

        //通关单号
        public string CLEARANCENO { get; set; }


        //木质包装
        public string WOODPACKINGID { get; set; }

        //法检状况
        public string LAWCONDITION { get; set; }



        //件数
        public string GOODSNUM { get; set; }


        //重量
        public string GOODSWEIGHT { get; set; }


        //华东主单号
        public string REGNO { get; set; }


        //申报方式
        public string REPWAYID { get; set; }

        //申报关区
        public string CUSTOMDISTRICTCODE { get; set; }

        //口岸
        public string PORTCODE { get; set; }

        //报关申报单位
        public string REPUNITCODE { get; set; }

        //报检申报单位
        public string INSPUNITCODE { get; set; }

        //备注
        public string REMARK { get; set; }

        //委托单位(客户代码)
        public string CUSTOMERCODE { get; set; }

        //创建人ID
        public string CREATEUSERID { get; set; }

        //创建人
        public string CREATEUSERNAME { get; set; }

        //创建时间
        public string  CREATETIME { get; set; }

        //委托人ID
        public string SUBMITUSERID { get; set; }

        //委托人
        public string SUBMITUSERNAME { get; set; }

        //委托时间
        public string SUBMITTIME { get; set; }


        //客服认领人ID
        public string CSACCEPTUSERID { get; set; }

        //客服认领人
        public string CSACCEPTUSERNAME { get; set; }


        //状态
        public string STATUS { get; set; }

        //订单需求
        public string ENTRUSTREQUEST { get; set; }


        //委托类型
        public string ENTRUSTTYPEID { get; set; }

        //载货清单
        public string MANIFEST { get; set; }

        //船名
        public string SHIPNAME { get; set; }

        //航次
        public string FILGHTNO { get; set; }

        //提单号
        public string LADINGBILLNO { get; set; }


        //优先级
        public string PRIORITY { get; set; }


        //贸易方式代码
        public string TRADEWAYCODES { get; set; }

        //报关车号
        public string DECLCARNO { get; set; }


        //结算单位代码
        public string CLEARUNIT { get; set; }


        //业务类别
        public string BUSIKIND { get; set; }

        //报关方式
        public string DECLWAY { get; set; }

        //接单方式
        public string ORDERWAY { get; set; }

        //包装种类
        public string PACKKIND { get; set; }


        //毛重
        public string GOODSGW { get; set; }

        //净重
        public string GOODSNW { get; set; }

        //运抵编号
        public string ARRIVEDNO { get; set; }

        //路陆监管号
        public string LANDREGNO { get; set; }


        //客服指令
        public string CSREQUEST { get; set; }


        //结算备注
        public string CLEARREMARK { get; set; }


        //当前客服ID
        [MaxLength(50)]
        public string CSID { get; set; }

        //当前客服
        public string CSNAME { get; set; }


        //客服认领时间
        public string  CSACCEPTTIME { get; set; }

        //客服提交时间
        public string  CSSUBMITTIME { get; set; }

        //当前制单ID
        public string MOID { get; set; }

        //当前制单
        public string MONAME { get; set; }


        //制单认领时间
        public string  MOACCEPTTIME { get; set; }

        //当前审单ID
        public string COID { get; set; }

        //当前审单
        public string CONAME { get; set; }


        //审单认领时间
        public string  COACCEPTTIME { get; set; }

        //是否作废
        public string ISINVALID { get; set; }

        //一程提单号
        [MaxLength(255)]
        public string FIRSTLADINGBILLNO { get; set; }

        //二程提单号
        public string SECONDLADINGBILLNO { get; set; }


        //货物类型ID
        public string GOODSTYPEID { get; set; }

        //货物类型
        public string GOODSTYPENAME { get; set; }


        //集装箱号
        public string CONTAINERNO { get; set; }

        //是否二次转关
        public string SECONDTRANSIT { get; set; }

        //关联编号，两单关联
        public string ASSOCIATENO { get; set; }

        //业务对应号，四单关联
        public string CORRESPONDNO { get; set; }

        //附加状态
        public string EXTRASTATUS { get; set; }

        //内部类型进口/出口
        public string INTERNALTYPE { get; set; }

        //关联报关单号（作废，用ASSOCIATEPEDECLNO字段）
        public string ASSOCIATECUSTOMSNO { get; set; }

        //经营单位名称
        public string BUSIUNITNAME { get; set; }

        //报关申报单位名称
        public string REPUNITNAME { get; set; }

        //报检申报单位名称
        public string INSPUNITNAME { get; set; }

        //委托单位名称
        public string CUSTOMERNAME { get; set; }

        //结算单位名称
        public string CLEARUNITNAME { get; set; }

        //申报备案号
        public string FILINGNUMBER { get; set; }

        //归属地
        public string ATTRIBUTION { get; set; }

        //贸易方式1
        public string TRADEWAYCODES1 { get; set; }


        //订单开始时间
        public string  CSSTARTTIME { get; set; }


        //订单时长
        public string CSTIME { get; set; }


        //套数
        public string SETNUM { get; set; }


        //张数
        public string PAPERNUM { get; set; }


        //制单认领人ID
        public string MOACCEPTUSERID { get; set; }

        //制单认领人
        public string MOACCEPTUSERNAME { get; set; }

        //审单认领人ID
        public string COACCEPTUSERID { get; set; }

        //审单认领人
        public string COACCEPTUSERNAME { get; set; }


        //关联报关单号
        public string ASSOCIATEPEDECLNO { get; set; }

        //预制单异常（0正常，1异常），预制单异常的汇总
        //预制单异常
        public string ISPAUSE { get; set; }


        //关联贸易方式
        public string ASSOCIATETRADEWAY { get; set; }


        //报关状态
        public string DECLSTATUS { get; set; }


        //报检状态
        public string INSPSTATUS { get; set; }


        //报关单套数
        public string DECLSETNUM { get; set; }


        //报检单套数
        public string INSPSETNUM { get; set; }


        //报关单张数
        public string DECLSHEETNUM { get; set; }


        //报关草单套数
        public string PREDECLSETNUM { get; set; }


        //报检草单套数
        public string PREINSPSETNUM { get; set; }


        //制单开始时间
        public string  MOSTARTTIME { get; set; }


        //制单结束时间
        public string  MOENDTIME { get; set; }

        //审单开始时间
        public string  COSTARTTIME { get; set; }

        //审单结束时间
        public string  COENDTIME { get; set; }

        //审单时长
        public string COTIME { get; set; }

        //报关暂存（0非暂存，1暂存），报关异常的汇总
        //报关暂存
        public string DECLPAUSE { get; set; }

        //报检暂存（0非暂存，1暂存），报检异常的汇总
        //报检暂存
        public string INSPPAUSE { get; set; }


        //口岸名称
        public string PORTNAME { get; set; }

        //关区名称
        public string CUSTOMDISTRICTNAME { get; set; }

        //下单人电话
        public string SUBMITUSERPHONE { get; set; }

        //客服电话
        public string CSPHONE { get; set; }

        //业务编号
        public string YWBH { get; set; }

        //订单交付时间
        public string  FINISHTIME { get; set; }

        //审单是否退回（0未退回，1退回到制单环节从新拆分）

        //审单是否退回
        public string COBACK { get; set; }

        //暂存次数
        public string PAUSENUM { get; set; }

        //作废原因
        public string INVALIDREASON { get; set; }

        //经营单位简写名称
        public string BUSISHORTNAME { get; set; }

        //经营单位简写代码
        public string BUSISHORTCODE { get; set; }

        //客服订单交接
        public string CSISBACK { get; set; }

        //制单订单交接
        public string MOISBACK { get; set; }

        //审单订单交接
        public string COISBACK { get; set; }

        //进出口类型
        public string INOUTTYPE { get; set; }

        //制单提交时间
        public string  MOSUBMITTIME { get; set; }

        //审单提交时间
        public string  COSUBMITTIME { get; set; }

        //进出口类型(国内结转专用)
        public string IETYPE { get; set; }

        //客服提交人id
        public string CSSUBMITUSERID { get; set; }

        //客服提交人姓名
        public string CSSUBMITUSERNAME { get; set; }

        //特殊关系确认
        public string SPECIALRELATIONSHIP { get; set; }

        //价格影响确认
        public string PRICEIMPACT { get; set; }

        //支付特许权使用费确认
        public string PAYPOYALTIES { get; set; }

        //草单异常（0正常，1异常），草单异常的汇总
        //草单异常
        public string PREISPAUSE { get; set; }

        //现场报关单位ID
        public string SCENEDECLAREID { get; set; }

        //现场报检单位ID
        public string SCENEINSPECTID { get; set; }

        //pdf是否关联（提前PDF/报关单PDF，0未关联，1已关联）
        //pdf是否关联
        public string DECLPDF { get; set; }



        //CHECKPDF
        public string CHECKPDF { get; set; }

        //PREPDF
        public string PREPDF { get; set; }

        //转关单是否关联（0 未关联，1关联）
        //转关单是否关联
        public string ISTURNPRE { get; set; }

        //DECLFINISHTIME
        public string  DECLFINISHTIME { get; set; }

        //INSPFINISHTIME
        public string  INSPFINISHTIME { get; set; }

        //拆分条数
        public string SPLITNUM { get; set; }

        //是否需要自审（0不需要自审；1需自审）
        //是否需要自审
        public string SELFCHECK { get; set; }

        //自审确认（0自审未确认；1自审已确认）
        //自审确认
        public string ISSELFCHECK { get; set; }


        //自审确认日期
        public string  SELFCHECKTIME { get; set; }

        //自审确认人
        public string SELFCHECKUSERCODE { get; set; }

        //自审确认人姓名
        public string SELFCHECKUSERNAME { get; set; }

        //是否需要重量确认（0不需要重量确认；1需重量确认）
        //是否需要重量确认
        public string WEIGHTCHECK { get; set; }

        //重量确认（0重量未确认；1重量已确认）
        //重量确认
        public string ISWEIGHTCHECK { get; set; }

        //重量确认日期
        public string  WEIGHTCHECKTIME { get; set; }

        //重量确认人
        public string WEIGHTCHECKUSERCODE { get; set; }

        //重量确认人姓名
        public string WEIGHTCHECKUSERNAME { get; set; }

        //确认件数
        public string CHECKEDGOODSNUM { get; set; }

        //确认重量
        public string CHECKEDWEIGHT { get; set; }

        //打印状态
        public string PRINTSTATUS { get; set; }

        //作业单时间
        public string  ZUOYEDANTIME { get; set; }

        //过机时间
        public string  GUOJITIME { get; set; }

        public string GUOJIUSERID { get; set; }

        public string GUOJIUSERNAME { get; set; }

        //理单时间
        public string  LIDANTIME { get; set; }

        public string LIDANUSERID { get; set; }

        public string LIDANUSERNAME { get; set; }

        //理货资料时间
        public string  LIHUOZILIAOTIME { get; set; }

        public string LIHUOZILIAOUSERID { get; set; }

        public string LIHUOZILIAOUSERNAME { get; set; }

        //报关时间
        public string  BAOGUANTIME { get; set; }

        public string BAOGUANUSERID { get; set; }

        public string BAOGUANUSERNAME { get; set; }

        //单证放行时间
        public string  DANZHENGFANGXINGTIME { get; set; }

        public string DANZHENGFANGXINGUSERID { get; set; }

        public string DANZHENGFANGXINGUSERNAME { get; set; }

        //查验起始时间
        public string  CHAYANSTARTTIME { get; set; }

        public string CHAYANSTARTUSERID { get; set; }

        public string CHAYANSTARTUSERNAME { get; set; }

        //查验完成时间
        public string  CHAYANENDTIME { get; set; }

        public string CHAYANENDUSERID { get; set; }

        public string CHAYANENDUSERNAME { get; set; }

        //理货起始时间
        public string  LIHUOSTARTTIME { get; set; }

        public string LIHUOSTARTUSERID { get; set; }

        public string LIHUOSTARTUSERNAME { get; set; }

        //理货完成时间
        public string  LIHUOENDTIME { get; set; }

        public string LIHUOENDUSERID { get; set; }

        public string LIHUOENDUSERNAME { get; set; }

        //实物放行时间
        public string  SHIWUFANGXINGTIME { get; set; }

        public string SHIWUFANGXINGUSERID { get; set; }

        public string SHIWUFANGXINGUSERNAME { get; set; }

        //放行方式
        public string PASSMODE { get; set; }

        //是否查验
        public string IFCHAYAN { get; set; }

        //查验次数
        public string CHAYANTIMES { get; set; }

        //查验备注
        public string CHAYANREMARK { get; set; }

        //理货标识
        public string LIHUOSIGN { get; set; }

        //理货次数
        public string LIHUOTIMES { get; set; }

        //扣货标识
        public string KOUHUOSIGN { get; set; }

        //扣货时间
        public string  KOUHUOTIME { get; set; }

        //是否调档
        public string IFTIAODANG { get; set; }

        //调档时间
        public string  TIAODANGTIMES { get; set; }

        //实物加封时间
        public string  SHIWUJIAFENGTIME { get; set; }

        public string SHIWUJIAFENGUSERNAME { get; set; }

        public string SHIWUJIAFENGUSERID { get; set; }

        //FWO订单号
        public string FWONO { get; set; }

        //FO报关服务指令号
        public string FOCUSTOMSNO { get; set; }

        //FO报检服务指令号
        public string FOINSPECTNO { get; set; }

        //是否开箱查验
        public string IFKXCHAYAN { get; set; }

        public string LIDANDESC { get; set; }

        public string LIHUOZILIAODESC { get; set; }

        public string BAOGUANDESC { get; set; }

        public string DANZHENGFANGXINGDESC { get; set; }

        public string CHAYANSTARTDESC { get; set; }

        public string CHAYANENDDESC { get; set; }

        public string LIHUOSTARTDESC { get; set; }

        public string LIHUOENDDESC { get; set; }

        public string SHIWUFANGXINGDESC { get; set; }

        public string SHIWUJIAFENGDESC { get; set; }

        //FO报关服务指令号
        public string FOONO { get; set; }

        //收货方/发货方
        public string SFGOODSUNIT { get; set; }

        //集装箱车号组信息
        public string CONTAINERTRUCKS { get; set; }

        //货物形态
        public string GOODSXT { get; set; }

        //是否提前报关
        public string ISPREDECLARE { get; set; }

        //二线合同专用发票号
        public string INVOICENO { get; set; }

        //报检foo
        public string FOONOBJ { get; set; }


        //报关集装箱车辆信息表
        public List<Declcontainertruck> Declcontainertruck { get; set; }

    }
}