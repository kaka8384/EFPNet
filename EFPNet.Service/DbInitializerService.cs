
using EFPNet.Domain.Data.Initialize;

namespace EFPNet.Service
{
    public  class DbInitializerService 
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            DatabaseInitializer.Initialize();
        }
    }
}
