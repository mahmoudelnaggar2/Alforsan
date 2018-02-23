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

    public class RoleRightService : IRoleRightService
    {
        private readonly IRoleRightRepository RoleRightRepository;
        private readonly IUnitOfWork unitOfWork;

        public RoleRightService(IRoleRightRepository RoleRightRepository, IUnitOfWork unitOfWork)
        {
            this.RoleRightRepository = RoleRightRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public RoleRight GetRoleRight(int id)
        {
            var RoleRight = RoleRightRepository.GetById(id);
            return RoleRight;
        }

        public void CreateRoleRight(RoleRight RoleRight)
        {
            RoleRightRepository.Add(RoleRight);
        }

        public void SaveRoleRight()
        {
            unitOfWork.Commit();
        }

        public void DeleteByRoleID (List<RoleRight> RoleRight)
        {
            foreach(RoleRight item in RoleRight.ToList())
            {
                RoleRightRepository.Delete(item);
            }
        }
   

        #endregion
    }
}
