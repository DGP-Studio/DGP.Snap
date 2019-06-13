using DGP.Snap.Window.Wallpaper;

namespace DGP.Snap.Window
{
    public static class WindowManager
    {
        private static WallpaperWindow _wallpaperWindow;
        public static WallpaperWindow WallpaperWindow
        {
            get
            {
                if (_wallpaperWindow == null||_wallpaperWindow.IsLoaded==false)
                {
                    _wallpaperWindow = new WallpaperWindow();
                }
                return _wallpaperWindow;
            }
            set
            {
                _wallpaperWindow = value;
            }

        }
    }

}
