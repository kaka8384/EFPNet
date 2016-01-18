using System;
using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using System.Linq;
namespace EFPNet.Repositories
{
    internal class ActionRightRepository : EFRepositoryBase<ActionRight, Guid>, IActionRightRepository
    {
        public string[] GetRoles(string controller, string action)
        {
           return ReadEntities
               .SingleOrDefault(a => a.ControllerName == controller && a.ActionName == action)
               .Roles
               .Select(b=>b.RoleName).ToArray();
        }
    }
}
