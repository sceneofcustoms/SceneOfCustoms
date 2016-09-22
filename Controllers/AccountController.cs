using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SceneOfCustoms.Common;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SceneOfCustoms.Models;
using System.IO;
namespace SceneOfCustoms.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.User u)
        {
            string returnUrl = Request["ReturnUrl"] + "";
            if (ModelState.IsValid)
            {
                string sql = "select * from sys_user where name = '" + u.NAME + "' and password = '" + Extension.ToSHA1(u.PASSWORD) + "'";
                DataTable dt = DBMgr.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["STATUS"] + "" != "1")
                    {
                        ModelState.AddModelError("ERROR", "账号已停用！");
                        return View(u);
                    }
                    FormsAuthentication.SetAuthCookie(u.NAME, false);
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }
                    else
                    {
                        Response.Redirect("/Home/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("ERROR", "账号/密码错误！");
                    return View(u);
                }
            }
            return View(u);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/Account/Login");
        }
    }
}
