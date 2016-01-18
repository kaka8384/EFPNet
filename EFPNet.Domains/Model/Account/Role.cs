using System.Collections.Generic;
using EFPNet.Infrastructure.Tools;
using System;
namespace EFPNet.Domains.Model
{
    public class Role:AggregateRoot<Guid>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// 角色对应的功能权限列表
        /// </summary>
        public List<ActionRight> ActionRights { get; set; }

        /// <summary>
        /// 添加用户至角色
        /// </summary>
        /// <param name="user">用户实例</param>
        public void AddUserToRole(User user)
        {
            user.Roles=new List<Role> {this};
            Users=new List<User> {user};
        }

        /// <summary>
        /// 添加功能权限至角色
        /// </summary>
        /// <param name="right">功能权限实例</param>
        public void AddActionRightsToRole(ActionRight right)
        {
            right.Roles = new List<Role> { this };
            ActionRights = new List<ActionRight> { right };
        }
    }
}
