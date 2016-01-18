using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using EFPNet.IService;
using EFPNet.Infrastructure.Tools;
using EFPNet.Infrastructure.Tools.Account;
using EFPNet.ViewModel;
using EmitMapper;

namespace EFPNet.Service
{
    internal class UserService : IUserService
    {
        private static IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        public OperationResult Login(LoginDto dto)
        {
            PublicHelper.CheckArgument(dto, "LoginDto");  //检查参数
            ObjectsMapper<LoginDto, User> mapper = ObjectMapperManager.DefaultInstance.GetMapper<LoginDto, User>();
            User user = mapper.Map(dto);
            var account=_userRepository.ReadEntities.SingleOrDefault(a => a.UserName == user.UserName
                                                          && a.Password == user.Password);
            if (account == null)
            {
                return new OperationResult(OperationResultType.Warning, "登录的用户名或密码错误。");
            }
            var userinfo = new UserInfo()
            {
                UserId=account.Id,
                UserName=account.UserName,
                NickName=account.NickName,
                RoleId = string.Join(",", account.Roles),
                Email=account.Email,
                Mobile=account.Mobile
            };

            DateTime expiration = dto.RememberMe? DateTime.Now.AddDays(7)
                : DateTime.Now.Add(FormsAuthentication.Timeout);
            FormsPrincipal<UserInfo>.SignIn(dto.UserName, userinfo, expiration);  //
            //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            return new OperationResult(OperationResultType.Success, "登录成功。");
        }
    }
}
