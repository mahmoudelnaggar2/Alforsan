using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;

namespace Services
{
    public class ReligionService:IReligionService
    {
        private readonly IReligionRepository religionRepository;

        public ReligionService(IReligionRepository religionRepository)
        {
            this.religionRepository = religionRepository;
        }

        public List<Religion> GetAll()
        {
            return religionRepository.GetAll().ToList();
        }
    }
}
