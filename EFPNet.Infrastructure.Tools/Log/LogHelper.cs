using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Infrastructure.Tools.Log
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger("EFPNet.Logger");

        /// <summary>
        /// 将指定的<see cref="Exception"/>实例详细信息写入日志。
        /// </summary>
        /// <param name="ex">需要将详细信息写入日志的<see cref="Exception"/>实例。</param>
        public static void Error(Exception ex)
        {
            log.Error("Exception caught", ex);
        }

        /// <summary>
        /// 将异常写入日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }

        /// <summary>
        /// 将指定的字符串信息写入日志。
        /// </summary>
        /// <param name="message">需要写入日志的字符串信息。</param>
        public static void Info(string message)
        {
            log.Info(message);
        }
    }

}
