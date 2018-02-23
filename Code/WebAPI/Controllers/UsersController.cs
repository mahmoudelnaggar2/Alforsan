using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    public class UsersController :SecureController
    {
        private readonly Services.IUserService _userService;

        public UsersController(Services.IUserService UserService)
        {
            _userService = UserService;

        }

        // GET api/Users
        public List<User> GetUsers()
        {
            return _userService.GetAll();
        }

        // GET api/Users/1/10
        [HttpGet]
        [Route("api/Users/{PageNumber}/{PageSize}/{SortBy}/{SortDirection}")]
        [ResponseType(typeof(Model.DTO.PagedResult<User>))]
        public Model.DTO.PagedResult<User> GetUsers(int PageNumber, int PageSize, string SortBy = "", string SortDirection = "")
        {
            return _userService.GetAll(PageNumber,PageSize,SortBy,SortDirection);
        }
        // GET api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/Users/5
        public IHttpActionResult PutUser(int id, User user)
        {
            user.Role = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            try
            {
                Model.User _user = _userService.GetUser(id);
                if (user.UserPassword != _user.UserPassword)
                {
                    Helpers.SecurityHelper _securityHelper = new Helpers.SecurityHelper();
                    user.UserPassword = _securityHelper.Md5Encryption(user.UserPassword);
                }
                _userService.UpdateUser(user);
                _userService.SaveUser();
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
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

        // POST api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Helpers.SecurityHelper _securityHelper = new Helpers.SecurityHelper();
            user.UserPassword = _securityHelper.Md5Encryption(user.UserPassword);
            _userService.CreateUser(user);
            _userService.SaveUser();
            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }

        // DELETE api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(user);
            _userService.SaveUser();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            User user = _userService.GetUser(id);
            return user != null;
        }

        // GET api/Users/5
        [HttpGet]
        [Route("api/Users/CurrentUser/")]
        public IHttpActionResult GetCurrentUser()
        {
            Helpers.SecurityHelper _security = new Helpers.SecurityHelper();
            int id = _security.getUserIDFromToken();
            User user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/Users/FilteredList"
        [HttpPost]
        [Route("api/Users/FilteredList")]
        [ResponseType(typeof(Model.DTO.PagedResult<User>))]
        public Model.DTO.PagedResult<User> LoadFilteredUsers(FilterModel<User> FilterObject)
        {
            //if no search is applied
            if(FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.User();
            }
            return _userService.GetAll(FilterObject);
        }


        [HttpGet]
        [Route("api/Users/UserNameExists")]
        public bool UsernameExists(string name, int id)
        {
            return _userService.UsernameExists(name, id);
        }

    }
}