using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneOfCustoms.Controllers
{
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
            //JObject json_user = Extension.Get_UserInfo(HttpContext.User.Identity.Name);
            //return json_user.GetValue("REALNAME") + "";
            return "测试用户";
        }
	}
}