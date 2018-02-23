using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public static class SettingDTO
    {

        public static string DateFormat
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("DateFormat");
            }
        }

        public static int AdminRoleID
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("AdminRoleID").IntParse();
            }
        }

        public static int SalesRoleID
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("SalesRoleID").IntParse();
            }
        }

        public static int CustomerServiceRoleID
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("CustomerServiceRoleID").IntParse(); 
            }
        }

        public static int DigitCount
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("DigitCount").IntParse();
            }
        }

        public static string Delimeter
        {
            get
            {
                return ConfigurationHelper.GetConfigValue("Delimeter");
            }
        }
        

    }
}
