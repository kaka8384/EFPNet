using EFPNet.Core;
using EFPNet.IService;
using System;
using System.Web;
using System.Web.Mvc;
using Autofac;
using EFPNet.ViewModel;
using EFPNet.Infrastructure.Tools;

namespace EFPNet.Web.MVC
{
    public class LogFilter : ActionFilterAttribute
    {
        private static IOperateLogService _logService;

        public string LogDesc { get; set; }
 
        public LogFilter()
        {
            var container = ApplicationContainer.GetContainer();
            _logService = container.Resolve<IOperateLogService>();
        }

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //AddLog(filterContext, OperateLogEvent.ActionExecuting);
        //}

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    //AddLog(filterContext, OperateLogEvent.ActionExecuted);
        //}

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    //AddLog(filterContext, OperateLogEvent.ResultExecuting);
        //}

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            AddLog(filterContext, OperateLogEvent.ResultExecuted);
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="opEvent"></param>
        private void AddLog(ControllerContext filterContext,OperateLogEvent opEvent)
        {
            OperateLogDto model = new OperateLogDto()
            {
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Desc = LogDesc,
                IP = EFP.Common.Tool.WebTools.GetIPAddress(),
                Event=opEvent
            };
            
            _logService.AddLog(model);
        }
    }
}
