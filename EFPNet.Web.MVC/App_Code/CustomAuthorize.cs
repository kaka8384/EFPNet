using System;
using System.Web;
using System.Web.Mvc;
using Autofac;
using EFPNet.Core;
using EFPNet.IService;
using EFPNet.Infrastructure.Tools.Account;

namespace EFPNet.Web.MVC
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //private static IActionRightService _actionrightService;

        //public CustomAuthorizeAttribute(IActionRightService actionrightService)
        //{
        //    _actionrightService = actionrightService;
        //}

        public IActionRightService acs { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isHaveRight = false;  //当前登录用户是否有权限
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var roles = FormsPrincipal<UserInfo>.GetUserData().RoleId.Split('、'); //当前登录用户的角色
            if (roles.Length != 0)
            {
                isHaveRight = true;
            }
            //if (!isHaveRight)
            //{
            //    httpContext.Response.StatusCode = 403; 
            //}
            
            return isHaveRight;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var controller = filterContext.RouteData.Values["controller"].ToString(); //当前访问的controller名称
            var action = filterContext.RouteData.Values["action"].ToString();
            var container = ApplicationContainer.GetContainer();  
            var service = container.Resolve<IActionRightService>();
            var jj = acs;
            service.CheckRight(controller, action);
            //if (filterContext.HttpContext.Response.StatusCode == 403)
            //{
            //    filterContext.Result = new RedirectResult("/Admin/Dashboard");
            //} 
        }
    }
}
