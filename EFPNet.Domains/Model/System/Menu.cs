using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPNet.Infrastructure.Tools;

namespace EFPNet.Domains.Model
{
    public class Menu : AggregateRoot<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 对应的Action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 对应的Controller名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string MenuDesc{ get; set; }

        /// <summary>
        /// 是否页面
        /// </summary>
        public bool IsPage { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
