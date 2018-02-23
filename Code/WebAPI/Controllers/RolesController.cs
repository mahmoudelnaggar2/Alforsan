using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    public class RolesController : SecureController
    {
        private readonly Services.IRoleService _roleService;
        private readonly Services.IRoleRightService _roleRightService;
        private readonly Services.IUserService _userService;

        public RolesController(Services.IRoleService RoleService, Services.IRoleRightService RoleRightService, Services.IUserService UserService)
        {
            _roleService = RoleService;
            _roleRightService = RoleRightService;
            _userService = UserService;
        }

        // GET api/Roles
        public List<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        // GET api/Roles/1/10
        [HttpGet]
        [Route("api/Roles/{PageNumber}/{PageSize}/{SortBy}/{SortDirection}")]
        [ResponseType(typeof(Model.DTO.PagedResult<Role>))]
        public Model.DTO.PagedResult<Role> GetRoles(int PageNumber, int PageSize, string SortBy = "", string SortDirection = "")
        {
            return _roleService.GetAll(PageNumber, PageSize, SortBy, SortDirection);
        }

        // GET api/Roles/5
        [ResponseType(typeof(Role))]
        public IHttpActionResult GetRole(int id)
        {
            Role role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT api/Roles/5
        public IHttpActionResult PutRole(int id, Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.RoleID)
            {
                return BadRequest();
            }

            try
            {
                _roleService.UpdateRole(role);
                _roleService.SaveRole();
            }
            catch (Exception ex)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Roles
        [ResponseType(typeof(Role))]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _roleService.CreateRole(role);
            _roleService.SaveRole();

            return CreatedAtRoute("DefaultApi", new { id = role.RoleID }, role);
        }

        // DELETE api/Roles/5
        [ResponseType(typeof(Role))]
        public IHttpActionResult DeleteRole(int id)
        {
            Role role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else if (role.IsSystemRole == true)
            {
                return Conflict();
            }
            try
            {
                var users = _userService.GetUsersByRoleID(id, null);
                if (users.Count > 0)
                    return Conflict();

                _roleRightService.DeleteByRoleID(role.RoleRights);
                _roleRightService.SaveRoleRight();
                _roleService.DeleteRole(role);
                _roleService.SaveRole();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict();
            }


            return Ok(role);
        }


        private bool RoleExists(int id)
        {
            Role role = _roleService.GetRole(id);
            return role != null;
        }

        [HttpGet]
        [Route("api/Roles/getFeaturesRights/")]
        public object getFeaturesRights()
        {

            return _roleService.getFeaturesRights();
        }



        [HttpGet]
        [Route("api/Roles/getRoleSideMenu/")]
        public object getRoleSideMenu()
        {
            Helpers.SecurityHelper _security = new Helpers.SecurityHelper();
            int role_id = _security.getRoleIDFromToken();
            return _roleService.getRoleSideMenu(role_id);
        }
        [HttpGet]
        [Route("api/Roles/CanAccess/{right_id:int}")]
        public bool canAccess(int right_id)
        {
            Helpers.SecurityHelper _security = new Helpers.SecurityHelper();
            int role_id = _security.getRoleIDFromToken();
            return _roleService.canAccess(role_id, right_id);
        }
        // POST api/Roles/FilteredList"
        [HttpPost]
        [Route("api/Roles/FilteredList")]
        [ResponseType(typeof(Model.DTO.PagedResult<Role>))]
        public Model.DTO.PagedResult<Role> LoadFilteredUsers(FilterModel<Role> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.Role();
            }
            return _roleService.GetAll(FilterObject);
        }

        [HttpGet]
        [Route("api/Roles/RoleNameExists")]
        public bool RoleNameExists(string name, int id)
        {
            return _roleService.RoleExists(name, id);
        }


    }
}