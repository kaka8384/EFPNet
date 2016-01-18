using EFPNet.Infrastructure.Tools;
using EFPNet.ViewModel;

namespace EFPNet.IService
{
    public interface IUserService
    {
        OperationResult Login(LoginDto dto);
    }
}
