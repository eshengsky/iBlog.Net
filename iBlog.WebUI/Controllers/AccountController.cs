using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using iBlog.Domain.Helpers;
using iBlog.Utility;

namespace iBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Authenticate(string userName, string password)
        {
            var name = ConfigurationManager.AppSettings["UserName"];
            var pwd = ConfigurationManager.AppSettings["PwdMd5"];
            if (userName == name && StringHelper.GetMd5(password) == pwd)
            {
                FormsAuthentication.SetAuthCookie(userName, true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn","Account");
        }
    }
}