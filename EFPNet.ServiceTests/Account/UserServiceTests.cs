using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFPNet.Service;
using NUnit.Framework;
using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.ServiceTests;
using EFPNet.Domains.Repositories;
using Autofac;
using EFPNet.Infrastructure.Data;
using EFPNet.ViewModel.Account;
using EFPNet.Infrastructure.Tools;
using EFPNet.IService;

namespace EFPNet.Service.Tests
{
    [TestFixture()]
    public class UserServiceTests : TestBase
    {
        [Test()]
        public void AddUserTest()
        {
            var urepository = Container.GetContainer().Resolve<IRepository<User, Guid>>();
            var userService = Container.GetContainer().Resolve<IUserService>();
            //UserService service = new UserService(urepository);
            AddUserDto user = new AddUserDto()
            {
                UserName="admin",
                Email="",
                NickName="管理员",
                RealName="",
                Mobile="",
                Password="123456"
            };
            var result = userService.AddUser(user);
            Assert.IsTrue(result.ResultType == OperationResultType.Success, "");
        }
    }
}
