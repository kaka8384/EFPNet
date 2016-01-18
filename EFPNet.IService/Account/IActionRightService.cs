using System.Linq;
using EFPNet.Domains.Model;

namespace EFPNet.IService
{
    public interface IActionRightService
    {
        /// <summary>
        /// 验证指定的Action是否存在权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool CheckRight(string controller, string action);
    }
}