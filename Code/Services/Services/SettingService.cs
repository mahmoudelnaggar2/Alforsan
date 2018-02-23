using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;
using Data.Infrastructure;

namespace Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository settingRepository;
        private readonly IUnitOfWork unitOfWork;
        public SettingService(ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            this.settingRepository = settingRepository;
            this.unitOfWork = unitOfWork;

        }
        public void CreateSetting(Setting setting)
        {
            settingRepository.Add(setting);
            SaveSetting();
        }

        public void DeleteSetting(Setting setting)
        {
            settingRepository.Delete(setting);
            SaveSetting();
        }

        public List<Setting> GetAll()
        {
            return settingRepository.GetAll().ToList();
        }

        public Setting GetSetting(int id)
        {
            return settingRepository.GetById(id);
        }

        public void SaveSetting()
        {
            unitOfWork.Commit();
        }

        public void UpdateSetting(Setting setting)
        {
            settingRepository.Update(setting.SettingId, setting);
            SaveSetting();
        }
    }
}
