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

    public interface IUserService
    {
        List<User> GetAll();
        Model.DTO.PagedResult<User> GetAll(int PageNumber, int PageSize, string SortBy, string SortDirection);
        User GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void SaveUser();
        User UserLogin(string username, string password);
        Model.DTO.PagedResult<User> GetAll(FilterModel<User> FilterObject);

        List<User> GetUsersByRoleID(int roleId,string name);
        bool UsernameExists(string name, int id);
    }
}
