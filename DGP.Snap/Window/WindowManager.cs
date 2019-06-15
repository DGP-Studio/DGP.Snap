using DGP.Snap.Window.FrontSight;
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




        private static FrontSightWindow _frontSightWindow;
        public static FrontSightWindow FrontSightWindow
        {
            get
            {
                if (_frontSightWindow == null || _frontSightWindow.IsLoaded == false)
                {
                    _frontSightWindow = new FrontSightWindow();
                }
                return _frontSightWindow;
            }
        }
        public static bool IsFrontSightWindowShowing=false;
    }

}
