using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Helpers
{
   public static class ConfigurationHelper
    {
      
        public static string GetConfigValue(string key)
        {
            string value = string.Empty;
            value = ConfigurationManager.AppSettings[key];
            return value;
        }
    }
}
