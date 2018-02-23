using Data.Infrastructure;
using Data.Repositories;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public interface IRoleService
    {
        List<Role> GetAll();
        Model.DTO.PagedResult<Role> GetAll(int PageNumber, int PageSize, string SortBy, string SortDirection);
        Role GetRole(int id);
        void CreateRole(Role Role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void SaveRole();

        //custom methods
        object getRoleSideMenu(int RoleId);
        object getFeaturesRights();
        bool canAccess(int role_id, int right_id);
        PagedResult<Role> GetAll(FilterModel<Role> FilterObject);
        bool RoleExists(string name, int id);
    }
}
