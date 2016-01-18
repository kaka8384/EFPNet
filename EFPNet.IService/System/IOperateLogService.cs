using EFPNet.Infrastructure.Tools;
using EFPNet.ViewModel;
namespace EFPNet.IService
{
    public interface IOperateLogService
    {
        OperationResult AddLog(OperateLogDto dto);
    }
}
