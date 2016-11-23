﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SceneOfCustoms.Models
{
    public class OrderEn
    {


        public string BUSITYPE { get; set; }
        public string CODE { get; set; }
        public string FOONO { get; set; }
        public string TOTALNO { get; set; }
        public string DIVIDENO { get; set; }
        public string GOODSNUM { get; set; }
        public string GOODSWEIGHT { get; set; }
        public string PACKKIND { get; set; }
        public string REPWAYID { get; set; }
        public string DECLWAY { get; set; }
        public string TRADEWAYCODES { get; set; }
        public string CUSNO { get; set; }
        public string CUSTOMDISTRICTCODE { get; set; }

        public string PORTCODE { get; set; }
        public string SPECIALRELATIONSHIP { get; set; }
        public string PRICEIMPACT { get; set; }
        public string PAYPOYALTIES { get; set; }
        public string REPUNITCODE { get; set; }
        public string CREATEUSERNAME { get; set; }
        public string CREATETIME { get; set; }
        public string ARRIVEDNO { get; set; }
        public string CHECKEDGOODSNUM { get; set; }

        public string CHECKEDWEIGHT { get; set; }

        public string ENTRUSTTYPEID { get; set; }
        //报关集装箱车辆信息表
        public List<Declcontainertruck> Declcontainertruck { get; set; }
        public string GOODSXT { get; set; }
        public string BUSIUNITNAME { get; set; }
        public string GOODSTYPEID { get; set; }
        public string LADINGBILLNO { get; set; }
        public string ISPREDECLARE { get; set; }

        public string ENTRUSTREQUEST { get; set; }
        public string CONTRACTNO { get; set; }

        public string FIRSTLADINGBILLNO { get; set; }

        public string SECONDLADINGBILLNO { get; set; }

        public string MANIFEST { get; set; }
        public string WOODPACKINGID { get; set; }
        public string WEIGHTCHECK { get; set; }
        public string ISWEIGHTCHECK { get; set; }
        public string SHIPNAME { get; set; }
        public string FILGHTNO { get; set; }
        public string INSPUNITNAME { get; set; }
        public string TURNPRENO { get; set; }
        public string INVOICENO { get; set; }
        //public string DECLCARNO { get; set; }
        //public string SFGOODSUNIT { get; set; }

        public string FGOODSUNIT { get; set; }
        public string SGOODSUNIT { get; set; }
        public string ALLOWDECLARE { get; set; }
        public string ORDERCODE { get; set; }
        public string DOCSERVICECODE { get; set; }
        public string DOCSERVICENAME { get; set; }






        public string ASSOCIATENO { get; set; }
        public string CORRESPONDNO { get; set; }
        public string WTFS { get; set; }
        public string BUSINAME { get; set; }
        public string FGOODSUNITCODE { get; set; }
        public string SGOODSUNITCODE { get; set; }
        public string REPUNITNAME { get; set; }
        public string INSPUNITCODE { get; set; }
        public string FOONOBJ { get; set; }
        public string SUBMITUSERNAME { get; set; }
        public string SUBMITTIME { get; set; }
        public string BUSIUNITCODE { get; set; }
        public string SENDNUMBER { get; set; }
        public string NOWTIME { get; set; }

        public string XCBUSINAME { get; set; }

        

    }
}