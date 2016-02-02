using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using EFPNet.Infrastructure.Tools.Account;
using EFPNet.Infrastructure.Tools.Log;

namespace EFPNet.Web.MVC.Controllers
{
    /// <summary>
    /// 表示“控制器”Controller类型的基类型，所有项目下的Controller都应该继承于此基类型。
    /// </summary>
    [HandleError]
    [CustomAuthorize]
    public abstract class ControllerBase : Controller
    {
        #region Private Constants
        private const string SuccessPageAction = "SuccessPage";
        private const string SuccessPageController = "Home";
        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取当前登录用户的信息。
        /// </summary>
        protected UserInfo UserData
        {
            get
            {
                return FormsPrincipal<UserInfo>.GetUserData();
                //if (Session["UserInfo"] != null)
                //    return (UserInfo)Session["UserInfo"];
                //else
                //{
                //    // 1. 读登录Cookie
                //    var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                //    if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                //        return null;
                //    try
                //    {
                //        UserInfo userData = null;
                //        // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                //        if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                //        {
                //            // 3. 还原用户数据
                //            userData = (new JavaScriptSerializer()).Deserialize<UserInfo>(ticket.UserData);
                //        }

                //        Session["UserInfo"] = userData;
                //        return userData;                      
                //    }
                //    catch 
                //    { 
                //        /* 有异常也不要抛出，防止攻击者试探。 */
                //        return null;
                //    }
                //}
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 重写基类在Action之前执行的方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //CurrentUserInfo = Session["UserInfo"] as UserInfo;

            //检验用户是否已经登录，如果登录则不执行，否则则执行下面的跳转代码
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    //RedirectToAction("Login", "Account");
            //    Response.Redirect("/Account/Login");
            //}
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 重写异常处理方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.Error(filterContext.Exception);
            // 执行基类中的OnException
            base.OnException(filterContext);
        }

        /// <summary>
        /// 将页面重定向到成功页面。
        /// </summary>
        /// <param name="pageTitle">需要在成功页面显示的成功信息。</param>
        /// <param name="action">成功信息显示后返回的Action名称。默认值：Index。</param>
        /// <param name="controller">成功信息显示后返回的Controller名称。默认值：Home。</param>
        /// <param name="waitSeconds">在成功页面停留的时间（秒）。默认值：3。</param>
        /// <returns>执行的Action Result。</returns>
        protected ActionResult RedirectToSuccess(string pageTitle, string action = "Index", string controller = "Home", int waitSeconds = 3)
        {
            return this.RedirectToAction(SuccessPageAction, SuccessPageController, new { pageTitle = pageTitle, retAction = action, retController = controller, waitSeconds = waitSeconds });
        }

        #endregion
    }
}