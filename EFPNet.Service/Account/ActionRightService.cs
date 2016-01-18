using System;
using System.Linq;
using System.Linq.Expressions;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using EFPNet.IService;
using EFPNet.Infrastructure.Tools.Account;

namespace EFPNet.Service
{
    internal class ActionRightService : IActionRightService
    {
        private static IActionRightRepository _actionrightRepository;

        public ActionRightService(IActionRightRepository actionrightRepository)
        {
            _actionrightRepository = actionrightRepository;
        }

        /// <summary>
        /// 判断是否存在对应Action的权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool CheckRight(string controller, string action)
        {
            Expression<Func<ActionRight, bool>> predicate =
                a => a.ControllerName == controller && a.ActionName == action;
            if (!_actionrightRepository.IsExist(predicate))
            {
                return true;
            }
            var usableRole = _actionrightRepository.GetRoles(controller, action);
            if (usableRole != null)
            {
                var currentRoles = FormsPrincipal<UserInfo>.GetUserData().RoleId.Split('、'); //当前登录用户的角色
                var finalRole = usableRole.Join(currentRoles, a => a, b => b, (a, b) => a);  //当前登录用户存在相关的角色
                if (finalRole.Any())  
                {
                    return true;
                }
            }
            return false;
        }
    }
}