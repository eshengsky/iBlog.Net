using ConfigHelper;
using iBlog.WebUI.Models;
using System.Web;

namespace iBlog.WebUI
{
    public static class Settings
    {
        public static SystemConfig Config
        {
            get
            {
                var configSettings = new ConfigSettings();
                var config = new SystemConfig(configSettings, "System", HttpContext.Current.Server.MapPath("~/Settings"));
                config.Load();
                return config;
            }
        }
    }
}