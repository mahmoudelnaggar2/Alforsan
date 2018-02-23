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
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository GenderRepository;
        private readonly IUnitOfWork UnitOfWork;

        public GenderService(IGenderRepository genderRepository,IUnitOfWork unitOfWork)
        {
            this.GenderRepository = genderRepository;
            this.UnitOfWork = unitOfWork;
        }
        public List<Gender> GetAll()
        {
            return GenderRepository.GetAll().ToList();
        }

        public void SaveGender()
        {
            UnitOfWork.Commit();
        }
    }
}
