using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace EFPNet.Infrastructure.Tools.Account
{
    public class FormsPrincipal<TUserData> 
    where TUserData : class, new()
    {
        private readonly IIdentity _identity;
        private readonly TUserData _userData;

        public FormsPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if( ticket == null )
                throw new ArgumentNullException("ticket");
            if( userData == null )
                throw new ArgumentNullException("userData");

            _identity = new FormsIdentity(ticket);
            _userData = userData;
        }
    
        public TUserData UserData
        {
            get { return _userData; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        //public bool IsInRole(string role)
        //{
        //    // 把判断用户组的操作留给UserData去实现。

        //    var principal = _userData as IPrincipal;
        //    if( principal == null )
        //        throw new NotImplementedException();
        //    else
        //        return principal.IsInRole(role);
        //}

        /// <summary>
        /// 执行用户登录操作
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="userData">与登录名相关的用户信息</param>
        /// <param name="expiration">登录Cookie的过期时间</param>
        public static void SignIn(string loginName, TUserData userData, DateTime expiration)
        {
            if (string.IsNullOrEmpty(loginName))
                throw new ArgumentNullException("loginName");
            if (userData == null)
                throw new ArgumentNullException("userData");

            // 1. 把需要保存的用户数据转成一个字符串。
            string data = (new JavaScriptSerializer()).Serialize(userData);


            // 2. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            var ticket = new FormsAuthenticationTicket(1, loginName, DateTime.Now, expiration, true, data);


            // 3. 加密Ticket，变成一个加密的字符串。
            string cookieValue = FormsAuthentication.Encrypt(ticket);


            // 4. 根据加密结果创建登录Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath,
                    Expires = expiration
                };

            var context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            // 5. 写登录Cookie
            //context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Set(cookie);
        }

        public static UserInfo GetUserData()
        {

            if (HttpContext.Current.Session["UserInfo"] != null)
                return (UserInfo)HttpContext.Current.Session["UserInfo"];
            else
            {
                // 1. 读登录Cookie
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                    return null;
                try
                {
                    UserInfo userData = null;
                    // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                    {
                        // 3. 还原用户数据
                        userData = (new JavaScriptSerializer()).Deserialize<UserInfo>(ticket.UserData);
                    }

                    HttpContext.Current.Session["UserInfo"] = userData;
                    return userData;
                }
                catch
                {
                    /* 有异常也不要抛出，防止攻击者试探。 */
                    return null;
                }
            }
        }
    }
}
