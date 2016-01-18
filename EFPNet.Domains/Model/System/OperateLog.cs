using EFPNet.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Domains.Model
{
    public class OperateLog : AggregateRoot<Guid>
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateDate { get; set; }

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

       /// <summary>
       /// 操作用户ID
       /// </summary>
       public virtual Guid UserID { get; set; }

       /// <summary>
       /// 操作用户
       /// </summary>
       public virtual User OperateUser { get; set; }

       /// <summary>
       /// 添加用户至操作日志
       /// </summary>
       /// <param name="user">用户实例</param>
       //public void AddUserToOperateLog(User user)
       //{
       //    user.OperateLogs = new List<OperateLog> { this };
       //    OperateUser=user;
       //}
    }
}
