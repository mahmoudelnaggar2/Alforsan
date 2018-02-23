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

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository RoleRepository;
        private readonly IRoleRightRepository RoleRightRepository;
        private readonly IUnitOfWork unitOfWork;


        public RoleService(IRoleRepository RoleRepository, IRoleRightRepository RoleRightRepository, IUnitOfWork unitOfWork)
        {
            this.RoleRepository = RoleRepository;
            this.RoleRightRepository = RoleRightRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members

        public List<Role> GetAll()
        {
            List<Role> Roles = RoleRepository.GetAll().ToList();
            return Roles;
        }

        public Model.DTO.PagedResult<Role> GetAll(int PageNumber, int PageSize, string SortBy = "", string SortDirection = "")
        {
            List<string> Includes = new List<string>();
            Model.DTO.PagedResult<Role> Roles = RoleRepository.GetAll(PageNumber, PageSize, Includes, SortBy, SortDirection);
            return Roles;
        }

        public Role GetRole(int id)
        {
            var Role = RoleRepository.GetById(id);
            return Role;
        }

        public void CreateRole(Role Role)
        {
            RoleRepository.Add(Role);
        }

        public void UpdateRole(Role role)
        {
            foreach (RoleRight item in role.RoleRights)
            {
                item.Right = null;
                if (item.RoleID > 0)
                {
                    RoleRightRepository.Update(item.RoleRightID, item);
                }
                else if (item.RoleID < 0)
                {
                    item.RoleID = role.RoleID;
                    RoleRightRepository.Add(item);
                }
                else if (item.RoleRightID > 0)
                {
                    RoleRight roleRight = RoleRightRepository.GetById(item.RoleRightID);
                    RoleRightRepository.Delete(roleRight);
                }
            }
            role.RoleRights = null;
            RoleRepository.Update(role.RoleID, role);
        }

        public void DeleteRole(Role role)
        {
               RoleRepository.Delete(role);
        }

        public void GetFeaturesRights(Role role)
        {
            RoleRepository.Delete(role);
        }



        public void SaveRole()
        {
            unitOfWork.Commit();
        }



        #endregion

        #region Custom Methods
        public object getRoleSideMenu(int RoleId)
        {
            return this.RoleRepository.getRoleSideMenu(RoleId);
        }
        public object getFeaturesRights()
        {
            return this.RoleRepository.getFeaturesRights();
        }

        public bool canAccess(int role_id, int right_id)
        {
            return this.RoleRepository.canAccess(role_id, right_id);
        }

        public PagedResult<Role> GetAll(FilterModel<Role> FilterObject)
        {
            FilterObject.Includes = new List<string>();
            return RoleRepository.GetAll(FilterObject);
        }
        public bool RoleExists(string name, int id)
        {
            return RoleRepository.RoleExists(name, id);
        }
        #endregion
    }
}
