using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;

namespace Services
{
    public class CustodyService:ICustodyService
    {

        private readonly ICustodyRepository custodyRepository;
       
        public CustodyService(ICustodyRepository custodyRepository)
        {
            this.custodyRepository = custodyRepository;
        }

        public List<Custody> GetAll()
        {
            return custodyRepository.GetAll().ToList();
        }
    }
}
