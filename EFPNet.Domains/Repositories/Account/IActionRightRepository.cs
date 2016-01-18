using System;
using System.Linq;
using EFPNet.Domains.Model;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Domains.Repositories
{
    public interface IActionRightRepository : IRepository<ActionRight, Guid>
    {
        string[] GetRoles(string controller, string action);
    }
}