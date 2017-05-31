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
using EFPNet.ViewModel.Account;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Service
{
    internal class UserService : IUserService
    {
        private static IRepository<User,Guid> _userRepository;

        public UserService(IRepository<User, Guid> userRepository)
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

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto">用户数据</param>
        /// <returns></returns>
        public OperationResult AddUser(AddUserDto userDto)
        {
            var result = new OperationResult();
            PublicHelper.CheckArgument(userDto, "AddUserDto");  //检查参数
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<AddUserDto, User>();
            User user = mapper.Map(userDto);
            user.AddDate = DateTime.Now;
            int affectRows=_userRepository.Insert(user);
            if (affectRows>0)
            {
                result.ResultType = OperationResultType.Success;
            }
            return result;
        }
    }
}
