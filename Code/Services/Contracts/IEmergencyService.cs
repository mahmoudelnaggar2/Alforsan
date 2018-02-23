using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IEmergencyService
    {
        List<Emergency> GetAll();
        Emergency GetEmergency(int id);
        void CreateEmergency(Emergency emergency);
        void UpdateEmergency(Emergency emergency);
        void DeleteEmergency(Emergency emergency);
        void SaveEmergency();
    }
}
