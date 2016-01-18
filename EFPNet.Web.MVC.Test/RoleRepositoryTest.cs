using System.Linq;
using Autofac;
using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using EFPNet.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFPNet.Web.MVC.Test
{
    
    
    /// <summary>
    ///这是 RoleRepositoryTest 的测试类，旨在
    ///包含所有 RoleRepositoryTest 单元测试
    ///</summary>
    [TestClass()]
    public class RoleRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void RoleRepositoryTestMethod1()
        {
            DbInitializerService.Initialize();
            var rrepository = Container.GetContainer().Resolve<IRoleRepository>();
            var urepository = Container.GetContainer().Resolve<IUserRepository>();
            var r = new Role { RoleName = "系统管理员",RoleDesc = ""};
            var user = urepository.ReadEntities.FirstOrDefault();
            r.AddUserToRole(user);
            rrepository.Insert(r);
        }

        /// <summary>
        /// 角色添加测试
        /// </summary>
        [TestMethod]
        public void RoleRepositoryAdd()
        {
            DbInitializerService.Initialize();
            var rrepository = Container.GetContainer().Resolve<IRoleRepository>();
            var r = new Role { RoleName = "员工1", RoleDesc = "普通员工" };
            rrepository.Insert(r);
        }

        [TestMethod]
        public void RoleRepositoryQuery()
        {
            DbInitializerService.Initialize();
            var rrepository = Container.GetContainer().Resolve<IRoleRepository>();
            var role = rrepository.ReadEntities.FirstOrDefault();
        }
    }
}
