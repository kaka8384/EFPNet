using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using EFPNet.Infrastructure.Tools;
using EFPNet.Infrastructure.Tools.Account;
using EFPNet.IService;
using EFPNet.ViewModel;
using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Service
{
    internal class OperateLogService : IOperateLogService
    {
        private static IOperateLogRepository _operatelogRepository;

        public OperateLogService(IOperateLogRepository operatelogRepository)
        {
            _operatelogRepository = operatelogRepository;
        }

        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public OperationResult AddLog(OperateLogDto dto)
        {
            ObjectsMapper<OperateLogDto, OperateLog> mapper = ObjectMapperManager.DefaultInstance.GetMapper<OperateLogDto, OperateLog>();
            OperateLog opLog = mapper.Map(dto);
            opLog.Id = Guid.NewGuid();
            opLog.OperateDate = DateTime.Now;
            opLog.AddDate = DateTime.Now;
            var userInfo=FormsPrincipal<UserInfo>.GetUserData();  //取用户信息
            if (userInfo != null)
            {
                opLog.UserID = userInfo.UserId;
                opLog.LastOperateUser = userInfo.UserName.ToString();
            }
           
            var result=_operatelogRepository.Insert(opLog);
            return result > 0 ? new OperationResult(OperationResultType.Success) : new OperationResult(OperationResultType.NoChanged);
        }
    }
}
