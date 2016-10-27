﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneOfCustoms.Common;
using SceneOfCustoms.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace SceneOfCustoms
{
    /// <summary>
    /// ECDSSyncData 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ECDSSyncData : System.Web.Services.WebService
    {

       [WebMethod(Description = @"测试阶段：调通即可")]
        public List<Msgobj> SyncData(List<OrderECDS> ld)
        {
            Msgobj MO = new Msgobj();
            MO.MSG_TXT = "测试通过";
            MO.MSG_TYPE = "S";
            List<Msgobj> MSList = new List<Msgobj>();
            MSList.Add(MO);
            return MSList;
        }

    }
}
