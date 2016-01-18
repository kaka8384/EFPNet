using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EFPNet.Infrastructure.Tools.Account
{
    public class UserInfo 
    {
        public Guid UserId;
        public string RoleId;
        public string UserName;
        public string NickName;
        public string Email;
        public string Mobile;

        // 如果还有其它的用户信息，可以继续添加。

        public override string ToString()
        {
            return string.Format("UserId: {0}, RoleId: {1}, UserName: {2}, NickName: {3}, Email: {4}, Mobile: {5}",
                UserId, RoleId, UserName, NickName, Email, Mobile);
        }

        #region IPrincipal Members

        [ScriptIgnore]
        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        //public bool IsInRole(string role)
        //{
        //    if (string.Compare(role, "Admin", true) == 0)
        //        return GroupId == 1;
        //    else
        //        return GroupId > 0;
        //}

        #endregion
    }
}
