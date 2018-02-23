using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
   public interface ISettingService
    {
        List<Setting> GetAll();
        Setting GetSetting(int id);
        void CreateSetting(Setting setting);
        void UpdateSetting(Setting setting);
        void DeleteSetting(Setting setting);
        void SaveSetting();

    }
}
