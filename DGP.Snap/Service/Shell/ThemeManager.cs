using DGP.Snap.Service.Setting;
using MahApps.Metro;
using System;
using System.Windows;
using System.Windows.Media;

namespace DGP.Snap.Service.Shell
{
    public class ThemeManager
    {
        private const string AppStyleBackgroundColor = "AppStyleBackgroundColor";
        private const string AppStyleForegroundColor = "AppStyleForegroundColor";

        private static bool isBlack=true;

        public static void Initialize()
        {
            if (SettingService.GetInstance()["App_Theme"] == null)
                isBlack = true;
            else
            {
                //SettingService s = SettingService.GetInstance();
                isBlack = 
                    SettingService.GetInstance()["App_Theme"] == (object)Theme.Dark;
            }
                

            if (isBlack)//黑变白
            {
                SetDarkTheme();
            }
            else//白变黑
            {
                SetLightTheme();
            }
        }

        public static void ToggleTheme()
        {
            if (isBlack)//黑变白
            {
                SetLightTheme();
            }  
            else//白变黑
            {
                SetDarkTheme();
            }  
        }

        private static void SetLightTheme()
        {
            Application.Current.Resources.Remove(AppStyleBackgroundColor);
            Application.Current.Resources.Remove(AppStyleForegroundColor);

            Application.Current.Resources.Add(AppStyleBackgroundColor, new SolidColorBrush(Color.FromArgb(153, 255, 255, 255)));
            Application.Current.Resources.Add(AppStyleForegroundColor, new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)));
            SettingService.GetInstance().AppSettings["App_Theme"] = Theme.Light;
            isBlack = false;
        }

        private static void SetDarkTheme()
        {
            Application.Current.Resources.Remove(AppStyleBackgroundColor);
            Application.Current.Resources.Remove(AppStyleForegroundColor);

            Application.Current.Resources.Add(AppStyleBackgroundColor, new SolidColorBrush(Color.FromArgb(153, 0, 0, 0)));
            Application.Current.Resources.Add(AppStyleForegroundColor, new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            SettingService.GetInstance().AppSettings["App_Theme"] = Theme.Dark;
            isBlack = true;
        }

        public static Theme GetCurrentTheme()
        {
            return isBlack ? Theme.Dark : Theme.Light;
        }
    }
}
