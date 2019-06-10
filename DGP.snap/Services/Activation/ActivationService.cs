using DGP.Snap.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Snap.Services.Activation
{
    //internal class ActivationService
    //{
    //    private readonly App _app;
    //    private readonly Type _defaultNavItem;
    //    private Lazy<UIElement> _shell;

    //    private object _lastActivationArgs;

    //    public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
    //    {
    //        _app = app;
    //        _shell = shell;
    //        _defaultNavItem = defaultNavItem;
    //    }

    //    public async Task ActivateAsync(object activationArgs)
    //    {
    //        if (IsInteractive(activationArgs))
    //        {
    //            // Initialize things like registering background task before the app is loaded
    //            await InitializeAsync();
    //            UserDataService.Initialize();
    //            IdentityService.InitializeWithAadAndPersonalMsAccounts();
    //            var silentLoginSuccess = await IdentityService.AcquireTokenSilentAsync();
    //            if (!silentLoginSuccess || !IdentityService.IsAuthorized())
    //            {
    //                await RedirectLoginPageAsync();
    //            }

    //            // Do not repeat app initialization when the Window already has content,
    //            // just ensure that the window is active
    //            if (Window.Current.Content == null)
    //            {
    //                // Create a Frame to act as the navigation context and navigate to the first page
    //                Window.Current.Content = _shell?.Value ?? new Frame();
    //            }
    //        }

    //        if (IdentityService.IsLoggedIn())
    //        {
    //            await HandleActivationAsync(activationArgs);
    //        }

    //        _lastActivationArgs = activationArgs;

    //        if (IsInteractive(activationArgs))
    //        {
    //            // Ensure the current window is active
    //            Window.Current.Activate();

    //            // Tasks after activation
    //            await StartupAsync();
    //        }
    //    }

    //    private async void OnLoggedIn(object sender, EventArgs e)
    //    {
    //        if (_shell?.Value != null)
    //        {
    //            Window.Current.Content = _shell.Value;
    //        }
    //        else
    //        {
    //            var frame = new Frame();
    //            Window.Current.Content = frame;
    //            NavigationService.Frame = frame;
    //        }

    //        await ThemeSelectorService.SetRequestedThemeAsync();
    //        await HandleActivationAsync(_lastActivationArgs);
    //    }

    //    public async Task RedirectLoginPageAsync()
    //    {
    //        var frame = new Frame();
    //        NavigationService.Frame = frame;
    //        Window.Current.Content = frame;
    //        await ThemeSelectorService.SetRequestedThemeAsync();
    //        NavigationService.Navigate<Views.LogInPage>();
    //    }

    //    private async Task InitializeAsync()
    //    {
    //        SampleDataService.Initialize("ms-appx:///Assets");
    //        await ThemeSelectorService.InitializeAsync();
    //    }

    //    private async Task HandleActivationAsync(object activationArgs)
    //    {
    //        var activationHandler = GetActivationHandlers()
    //                                            .FirstOrDefault(h => h.CanHandle(activationArgs));

    //        if (activationHandler != null)
    //        {
    //            await activationHandler.HandleAsync(activationArgs);
    //        }

    //        if (IsInteractive(activationArgs))
    //        {
    //            var defaultHandler = new DefaultLaunchActivationHandler(_defaultNavItem);
    //            if (defaultHandler.CanHandle(activationArgs))
    //            {
    //                await defaultHandler.HandleAsync(activationArgs);
    //            }
    //        }
    //    }

    //    private async Task StartupAsync()
    //    {
    //        await ThemeSelectorService.SetRequestedThemeAsync();

    //        // TODO WTS: This is a sample to demonstrate how to add a UserActivity. Please adapt and move this method call to where you consider convenient in your app.
    //        await UserActivityService.AddSampleUserActivity();
    //        await FirstRunDisplayService.ShowIfAppropriateAsync();
    //        await WhatsNewDisplayService.ShowIfAppropriateAsync();
    //    }

    //    private IEnumerable<ActivationHandler> GetActivationHandlers()
    //    {
    //        yield return Singleton<ToastNotificationsService>.Instance;
    //        yield return Singleton<SchemeActivationHandler>.Instance;
    //        yield return Singleton<ShareTargetActivationHandler>.Instance;
    //    }

    //    private bool IsInteractive(object args)
    //    {
    //        return args is IActivatedEventArgs;
    //    }

    //    public void SetShell(Lazy<UIElement> shell)
    //    {
    //        _shell = shell;
    //    }

    //    internal async Task ActivateFromShareTargetAsync(ShareTargetActivatedEventArgs activationArgs)
    //    {
    //        var shareTargetHandler = GetActivationHandlers().FirstOrDefault(h => h.CanHandle(activationArgs));
    //        if (shareTargetHandler != null)
    //        {
    //            await shareTargetHandler.HandleAsync(activationArgs);
    //        }
    //    }
    //}
}
