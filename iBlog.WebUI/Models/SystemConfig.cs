using ConfigHelper;

namespace iBlog.WebUI.Models
{
    public class SystemConfig
    {
        private readonly IConfigSettings _configSettings;
        private readonly string _configName;
        private readonly string _configPath;

        public SystemConfig() { }

        public SystemConfig(IConfigSettings configSettings, string configName, string configPath)
        {
            _configSettings = configSettings;
            _configName = configName;
            _configPath = configPath;
        }

        /// <summary>
        /// 网站名称
        /// </summary>
        [IsSetting]
        public string SiteName { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        [IsSetting]
        public string SiteDomain { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        [IsSetting]
        public string RecordNo { get; set; }

        /// <summary>
        /// 网站Logo
        /// </summary>
        [IsSetting]
        public string LogoPath { get; set; }

        /// <summary>
        /// 每页显示文章数
        /// </summary>
        [IsSetting]
        public int PageSize { get; set; }

        /// <summary>
        /// 生成文章目录
        /// </summary>
        [IsSetting]
        public bool ShowMenu { get; set; }

        /// <summary>
        /// 缓存过期时间（分钟）
        /// </summary>
        [IsSetting]
        public int CacheExpired { get; set; }

        /// <summary>
        /// 百度翻译key
        /// </summary>
        [IsSetting]
        public string TranslateKey { get; set; }

        /// <summary>
        /// 启用统计功能
        /// </summary>
        [IsSetting]
        public bool EnableStatistics { get; set; }

        /// <summary>
        /// 百度统计id
        /// </summary>
        [IsSetting]
        public string StatisticsId { get; set; }

        /// <summary>
        /// 启用分享功能
        /// </summary>
        [IsSetting]
        public bool EnableShare { get; set; }

        /// <summary>
        /// JiaThis分享id
        /// </summary>
        [IsSetting]
        public string JiaThisId { get; set; }

        /// <summary>
        /// 显示文章评论
        /// </summary>
        [IsSetting]
        public bool ShowComments { get; set; }

        /// <summary>
        /// 畅言appid
        /// </summary>
        [IsSetting]
        public string ChangyanId { get; set; }

        /// <summary>
        /// 畅言conf
        /// </summary>
        [IsSetting]
        public string ChangyanConf { get; set; }

        /// <summary>
        /// 显示留言
        /// </summary>
        [IsSetting]
        public bool ShowGuestbook { get; set; }

        /// <summary>
        /// 友言管理id
        /// </summary>
        [IsSetting]
        public string YouyanId { get; set; }

        public void Save()
        {
            _configSettings.Save<XmlConfig>(this, _configName, _configPath);
        }

        public void Load()
        {
            _configSettings.Load<XmlConfig>(this, _configName, _configPath);
        }
    }
}