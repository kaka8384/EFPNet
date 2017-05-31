using EFPNet.Infrastructure.Tools;
using EFPNet.ViewModel;
using EFPNet.ViewModel.Account;

namespace EFPNet.IService
{
    public interface IUserService
    {
        OperationResult Login(LoginDto dto);

        OperationResult AddUser(AddUserDto userDto);
    }
}
