using Data.Infrastructure;
using Data.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public interface IRoleRightService
    {
        RoleRight GetRoleRight(int id);
        void CreateRoleRight(RoleRight RoleRight);
        void SaveRoleRight();
        void DeleteByRoleID(List<RoleRight> RoleRight);
    }

}
