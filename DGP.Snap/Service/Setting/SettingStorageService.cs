using DGP.Snap.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Setting
{
    class SettingStorageService : ISupportSingleton
    {
        public SettingStorageService() { }

        private const string settingsfile = "settings.json";
        private readonly string settingspath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settingsfile);
        private Dictionary<string, object> appSettings;

        public Dictionary<string, object> AppSettings 
        { 
            get
            {
                if (appSettings == null)
                    return appSettings = new Dictionary<string, object>();
                return appSettings;
            }
            set
            { appSettings = value; }
        }
        public async Task SaveSettingsAsync()
        {
            using (FileStream fs = new FileStream(settingspath, FileMode.Create))
            {
                //写入
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(await Json.StringifyAsync(AppSettings));
                }
            }
        }

        private async Task<Dictionary<string, object>> ReadSettingsAsync()
        {
            //using (FileStream fileStream = new FileStream(settingspath, FileMode.OpenOrCreate))
            //return await Task.Run(() => { return new AppSettings(); });
            //return await Json.ToObjectAsync<Dictionary<string, object>>(File.ReadAllText(settingspath));

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

        public async Task RetriveSettingsAsync()
        {
            AppSettings = await ReadSettingsAsync();
        }

        public object this[string key]
        {
            get
            {
                return AppSettings[key];
            }
            set
            {
                AppSettings[key] = value;
            }
        }

    }
}
