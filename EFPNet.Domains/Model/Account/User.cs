using System;
using System.Collections.Generic;
using EFPNet.Infrastructure.Tools;
using EFPNet.Domains.Model;

namespace EFPNet.Domains.Model
{
    public class User : AggregateRoot<Guid>
    {
        public User()
        {
            Roles = new List<Role>();
            //OperateLogs = new ICollection<OperateLog>();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// 操作日志列表
        /// </summary>
        public virtual ICollection<OperateLog> OperateLogs { get; set; }
    }
}
