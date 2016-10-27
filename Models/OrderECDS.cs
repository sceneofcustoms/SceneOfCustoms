﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SceneOfCustoms.Models
{
    public class OrderECDS
    {
        public string BUSITYPE { get; set; }
        public string CODE { get; set; }
        public string FOONO { get; set; }
        //报关集装箱车辆信息表
        public List<Declcontainertruck> Declcontainertruck { get; set; }
        public string DOCSERVICECODE { get; set; }
        public string DOCSERVICENAME { get; set; }
    }
}