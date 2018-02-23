using Data.Infrastructure;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override Role GetById(int id)
        {
            return DbContext.Roles.Include("RoleRights").Include("RoleRights.Right").SingleOrDefault(r => r.RoleID == id);
        }

        public object getRoleSideMenu(int RoleId)
        {
            return this.DbContext.Features
                .Where(f => f.Rights.Any(r => r.RoleRights.Any(rr => rr.RoleID == RoleId) && r.IsVisible) && f.IsVisible)
                .OrderBy(f => f.FeatureOrder)
                .Select(f => new
                {
                    f.FeatureName,
                    f.FeatureNameAr,
                    f.MenuIcon,
                    Rights = f.Rights.Where(r => r.RoleRights.Any(rr => rr.RoleID == RoleId))
                        .Select(right => new { right.RightID, right.RightName, right.RightNameAr, right.RightCode, right.RightOrder, right.MenuIcon, right.RightURL })
                        .OrderBy(rightOrder => rightOrder.RightOrder)
                });
        }

        public object getFeaturesRights()
        {
            return this.DbContext.Features
                .Include("Rights")
                .OrderBy(o => o.FeatureOrder)
                .ToList();
        }

        public bool canAccess(int role_id, int right_id)
        {
            return this.DbContext.RoleRights.
                       Include("Role").
                       Include("Right").
                       Where(a => a.RoleID == role_id
                                  && a.RightID == right_id).ToList().Count > 0;
        }

        public Model.DTO.PagedResult<Role> GetAll(FilterModel<Role> FilterObject)
        {
            Model.DTO.PagedResult<Role> RoleList = new Model.DTO.PagedResult<Role>();
            Expression<Func<Role, bool>> SearchCriteria = r => (r.RoleName.Contains(FilterObject.SearchObject.RoleName) || string.IsNullOrEmpty(FilterObject.SearchObject.RoleName));
            RoleList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
            return RoleList;
        }
        public bool RoleExists(string name, int id)
        {
            return DbContext.Roles.Any(d => d.RoleName == name && d.RoleID != id);
        }

    }


}
