using System;
using EFPNet.Infrastructure.Tools.Extensions;

namespace EFPNet.Infrastructure.Tools
{
    /// <summary>
    ///     公共辅助操作类
    /// </summary>
    public static class PublicHelper
    {
        #region 公共方法

        /// <summary>
        ///     检验参数合法性，数值类型不能小于0，引用类型不能为null，否则抛出相应异常
        /// </summary>
        /// <param name="arg"> 待检参数 </param>
        /// <param name="argName"> 待检参数名称 </param>
        /// <param name="canZero"> 数值类型是否可以等于0 </param>
        /// <exception>
        ///     <cref>InfrastructureException</cref>
        /// </exception>
        public static void CheckArgument(object arg, string argName, bool canZero = false)
        {
            if (arg == null)
            {
                var e = new ArgumentNullException(argName);
                throw ThrowInfrastructureException(string.Format("参数 {0} 为空引发异常。", argName), e);
            }
            Type type = arg.GetType();
            if (type.IsValueType && type.IsNumeric())
            {
                bool flag = !canZero ? arg.CastTo(0.0) <= 0.0 : arg.CastTo(0.0) < 0.0;
                if (flag)
                {
                    var e = new ArgumentOutOfRangeException(argName);
                    throw ThrowInfrastructureException(string.Format("参数 {0} 不在有效范围内引发异常。具体信息请查看系统日志。", argName), e);
                }
            }
            if (type == typeof(Guid) && (Guid)arg == Guid.Empty)
            {
                var e = new ArgumentNullException(argName);
                throw ThrowInfrastructureException(string.Format("参数{0}为空Guid引发异常。", argName), e);
            }
        }

        /// <summary>
        ///     向调用层抛出基础设施异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static InfrastructureException ThrowInfrastructureException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知基础设施异常，详情请查看日志信息。";
            }
            return e == null ? new InfrastructureException(string.Format("基础设施异常：{0}", msg)) : new InfrastructureException(string.Format("基础设施异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出数据访问异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知数据访问异常，详情请查看日志信息。";
            }
            return e == null
                ? new DataAccessException(string.Format("数据访问异常：{0}", msg))
                : new DataAccessException(string.Format("数据访问异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出应用服务异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ServiceException ThrowServiceException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知应用服务层异常，详情请查看日志信息。";
            }
            return e == null ? new ServiceException(string.Format("应用服务异常：{0}", msg)) : new ServiceException(string.Format("应用服务层异常：{0}", msg), e);
        }

        /// <summary>
        ///     向调用层抛出仓储异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static RepositoriesException ThrowRepositoriesException(string msg, Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知仓储异常，详情请查看日志信息。";
            }
            return e == null ? new RepositoriesException(string.Format("仓储异常：{0}", msg)) : new RepositoriesException(string.Format("仓储异常：{0}", msg), e);
        }

        #endregion
    }
}
