using EFPNet.Infrastructure.Tools.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace EFPNet.Infrastructure.Tools.Ioc
{
    public class IocContainer
    {
        public static T GetService<T>()
        {
            T ret = default(T);
            try
            {
                ret = DependencyResolver.Current.GetService<T>();
            }
            catch (Exception ex)
            {
                LogHelper.Error("Ioc注入出错<" + typeof(T).Name + ">", ex);
            }
            return ret;
        }

        public static T GetServices<T>(string name)
        {
            T ret = default(T);
            try
            {
                ret = DependencyResolver.Current.GetServices<T>().FirstOrDefault(a => a.GetType().Name == name);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Ioc注入出错<" + typeof(T).Name + ">", ex);
            }
            return ret;
        }
    }
}
