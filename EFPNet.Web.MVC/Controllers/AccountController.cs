using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EFPNet.IService;
using EFPNet.Infrastructure.Tools;
using EFPNet.ViewModel;

namespace EFPNet.Web.MVC.Controllers
{
    public class AccountController : ControllerBase
    {
       private static IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;  
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        //[LogFilter(LogDesc="显示系统登录页面")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [LogFilter(LogDesc = "登录操作")]
        public ActionResult Login(LoginDto model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //string returnUrl = ViewBag.ReturnUrl;
                OperationResult result=_userService.Login(model);
                if (result.ResultType == OperationResultType.Success)
                {
                   
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码不正确！");
                }
            }
            return View();
        }

        //
        // GET: /Account/LogOff
         [LogFilter(LogDesc = "注销")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
