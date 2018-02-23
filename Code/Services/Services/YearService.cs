using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;

namespace Service
{
    public class YearService : IYearService
    {
        private readonly IYearRepository yearRepository;
        private readonly IUnitOfWork unitOfWork;
        public YearService(IYearRepository yearRepository, IUnitOfWork unitOfWork)
        {
            this.yearRepository = yearRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateYear(Year year)
        {
            try
            {
                yearRepository.Add(year);
                SaveYear();
            }
            catch (Exception e)
            {
            }
      
        }

        public void DeleteYear(Year year)
        {
            yearRepository.Delete(year);
            SaveYear();
        }

        public List<Year> GetAll()
        {
           return yearRepository.GetAll().ToList();
        }

        public Year GetYear(int id)
        {
            return yearRepository.GetById(id);
        }

        public void SaveYear()
        {
            unitOfWork.Commit();
        }

        public void UpdateYear(int id, Year year)
        {
            yearRepository.Update(id,year);
            SaveYear();
        }
        public bool YearNameUnique(string yearName, int yearId)
        {
            return yearRepository.YearNameUnique(yearName, yearId);
        }

        public bool ShortNameUnique(int shortName, int yearId)
        {
            return yearRepository.ShortNameUnique(shortName, yearId);
        }

        public void ActiveOnlyOneYear(int shortName)
        {
            yearRepository.ActiveOnlyOneYear(shortName);
            SaveYear();

        }

        public Year GetCurrentYear()
        {
            return yearRepository.GetCurrentYear();
        }
    }
}
