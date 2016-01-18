using System;
using System.Collections.Generic;
using EFPNet.Infrastructure.Tools;

namespace EFPNet.Domains.Model
{
    public class ActionRight : AggregateRoot<Guid>
    {
        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 功能权限类型 1.页面 2.操作
        /// </summary>
        public short ActionType { get; set; }

        /// <summary>
        /// Action描述
        /// </summary>
        public string ActionDesc { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 权限对应的角色列表
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
