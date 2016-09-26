using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneOfCustoms.Models
{
    public class Declcontainertruck
    {

        public int ID { get; set; }

        //订单号
        public string ORDERCODE { get; set; }

        //集装箱号
        public string CONTAINERNO { get; set; }

        //集装箱英文尺寸
        public string CONTAINERSIZEE { get; set; }

        //集装箱中文尺寸
        public string CONTAINERSIZEC { get; set; }

        //集装箱重量
        public decimal CONTAINERWEIGHT { get; set; }
         
        //集装箱类型
        public string CONTAINERTYPE { get; set; }

        //HS编码
        public string HSCODE { get; set; }

        //白卡号[2325150014]
        public string CDCARNO { get; set; }

        //车牌信息[苏EM4820]
        public string CDCARNAME { get; set; }

        //车队信息
        public string UNITNO { get; set; }

        public string ELESHUT { get; set; }

        public string FORMATNAME { get; set; }
    }
}
