using EFPNet.Domains.Model;
using EFPNet.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.ViewModel
{
    public class OperateLogDto
    {
        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 操作用户IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 表示操作日志是在哪个事件记录的
        /// </summary>
        public OperateLogEvent Event { get; set; }
    }
}
