using DGP.Snap.Helper;
using DGP.Snap.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Setting
{
    /// <summary>
    /// 将 <see cref="string"/>,<see cref="object"/> 对写入设置字典
    /// </summary>
    internal class SettingService
    {
        private const string settingsfile = "settings.json";
        private readonly string settingspath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settingsfile);

        public Dictionary<string, object> AppSettings { get; set; } = new Dictionary<string, object>();

        public async Task SaveSettingsAsync()
        {
            using (FileStream fileStream = new FileStream(settingspath, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(await Json.StringifyAsync(AppSettings));
                }
            }
        }

        private async Task<Dictionary<string, object>> ReadSettingsAsync()
        {
            using (FileStream fileStream = new FileStream(settingspath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    char[] buffer = new char[streamReader.BaseStream.Length];
                    await streamReader.ReadAsync(buffer, 0, (int)streamReader.BaseStream.Length);
                    return await Json.ToObjectAsync<Dictionary<string, object>>(new string(buffer));
                }
            }
        }
        /// <summary>
        /// 仅初始化时调用
        /// </summary>
        /// <returns>无实际返回值</returns>
        public async Task RetriveSettingsAsync()
        {
            AppSettings = await ReadSettingsAsync();
            if (AppSettings == null)
                AppSettings = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get
            {
                try
                {
                    return !AppSettings.TryGetValue(key, out object value) ? null : value;
                }
                catch
                {
                    return new object();
                }
            }
            set
            {
                AppSettings.AddOrSet(key, value);
            }
        }

        #region 单例
        private static SettingService instance;
        private static object _lock = new object();
        private SettingService()
        {
            
        }
        public static SettingService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new SettingService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }
}
