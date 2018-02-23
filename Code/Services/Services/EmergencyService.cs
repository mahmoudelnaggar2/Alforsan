using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;

namespace Services
{
    public class EmergencyService : IEmergencyService
    {
        private readonly IEmergencyRepository emergencyRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmergencyService(IEmergencyRepository emergencyRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.emergencyRepository = emergencyRepository;
        }
        public void CreateEmergency(Emergency emergency)
        {
            emergencyRepository.Add(emergency);
        }

        public void DeleteEmergency(Emergency emergency)
        {
            emergencyRepository.Delete(emergency);
        }

        public List<Emergency> GetAll()
        {
            return emergencyRepository.GetAll().ToList();
        }

        public Emergency GetEmergency(int id)
        {
            return emergencyRepository.GetById(id);
        }

        public void SaveEmergency()
        {
            unitOfWork.Commit();
        }

        public void UpdateEmergency(Emergency emergency)
        {
            emergencyRepository.Update(emergency.EmergencyId,emergency);
        }
    }
}
