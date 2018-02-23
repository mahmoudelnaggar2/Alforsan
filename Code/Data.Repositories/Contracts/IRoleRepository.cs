using Data.Infrastructure;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{

    public interface IRoleRepository : IRepository<Role>
    {
        object getRoleSideMenu(int RoleId);
        object getFeaturesRights();
        bool canAccess(int role_id, int right_id);
        Model.DTO.PagedResult<Role> GetAll(FilterModel<Role> FilterObject);
        bool RoleExists(string name, int id);
    }


}
