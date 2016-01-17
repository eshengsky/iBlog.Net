using ConfigHelper;

namespace iBlog.WebUI.Models
{
    public class AboutConfig
    {
        private readonly IConfigSettings _configSettings;
        private readonly string _configName;
        private readonly string _configPath;

        public AboutConfig() { }

        public AboutConfig(IConfigSettings configSettings, string configName, string configPath)
        {
            _configSettings = configSettings;
            _configName = configName;
            _configPath = configPath;
        }

        /// <summary>
        /// 第一行文本
        /// </summary>
        [IsSetting]
        public string FirstLine { get; set; }

        /// <summary>
        /// 第二行文本
        /// </summary>
        [IsSetting]
        public string SecondLine { get; set; }

        /// <summary>
        /// 头像图片地址
        /// </summary>
        [IsSetting]
        public string PhotoPath { get; set; }

        /// <summary>
        /// 第三行文本
        /// </summary>
        [IsSetting]
        public string ThirdLine { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        [IsSetting]
        public string Profile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [IsSetting]
        public string Wechat { get; set; }

        /// <summary>
        /// 二维码图片地址
        /// </summary>
        [IsSetting]
        public string QrcodePath { get; set; }

        /// <summary>
        /// Email地址
        /// </summary>
        [IsSetting]
        public string Email { get; set; }

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