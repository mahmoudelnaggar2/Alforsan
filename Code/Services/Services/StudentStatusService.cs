using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;

namespace Services
{
    public class StudentStatusService : IStudentStatusService
    {
        private readonly IStudentStatusRepository studentStatusRepository;

        public StudentStatusService(IStudentStatusRepository studentStatusRepository)
        {
            this.studentStatusRepository = studentStatusRepository;
        }
        public List<StudentStatus> GetAll()
        {
            return studentStatusRepository.GetAll().ToList();
        }
    }
}
