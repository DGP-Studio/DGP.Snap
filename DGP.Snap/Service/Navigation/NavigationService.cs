using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DGP.Snap.Service.Navigation
{
    public static class NavigationService
    {
        public static event NavigatedEventHandler Navigated;

        public static event NavigationFailedEventHandler NavigationFailed;

        private static Frame _frame;
        private static object _lastParamUsed;
        /// <summary>
        /// 当前 <see cref="NavigationService"/> 正在控制的 Frame
        /// </summary>
        public static Frame Frame
        {
            get
            {


                //_frame = mainWindow?.CurrentFrame;
                return _frame;
            }

            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        public static bool CanGoBack => Frame.CanGoBack;

        public static bool CanGoForward => Frame.CanGoForward;

        public static bool GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
                return true;
            }

            return false;
        }

        public static void GoForward() => Frame.GoForward();
        /// <summary>
        /// DGP Studio实现，已知存在内存不释放的问题，因为是 static
        /// </summary>
        private static List<Page> pages = new List<Page>();
        /// <summary>
        /// 对于已创建的<paramref name="pageType"/>类型的实例，提供记忆导航与导航加速，问题是占用的内存大
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool Navigate(Type pageType, object parameter = null)
        {
            // Don't open the same page multiple times
            if (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParamUsed)))
            {
                object content = null;
                int index = -1;

                if ((index = pages.FindIndex(apage => apage.GetType() == pageType)) >= 0)
                {
                    content = pages[index];
                }
                else
                {
                    content = pageType.Assembly.CreateInstance(pageType.FullName);
                }

                var navigationResult = Frame.Navigate(content, parameter);
                if (navigationResult)
                {
                    _lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 异步导航到包含 <typeparamref name="TPage"/>类型 的实例化对象的内容。
        /// 调用时需确保 <see cref="MainWindow"/> 实例化。
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool Navigate<TPage>(object parameter = null) where TPage : Page
        {
            //确保MainWindow存在
            return Navigate(typeof(TPage), parameter);
        }

        private static void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += Frame_Navigated;
                _frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        private static void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= Frame_Navigated;
                _frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        private static void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private static void Frame_Navigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);
    }
}
