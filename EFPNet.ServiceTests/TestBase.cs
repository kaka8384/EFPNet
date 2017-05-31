using EFPNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.ServiceTests
{
    public class TestBase
    {
        public TestBase()
        {
            DbInitializerService.Initialize();
        }
    }
}
