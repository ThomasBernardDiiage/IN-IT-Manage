using CommunityToolkit.Maui;
using InitManage.Helpers.Interfaces;
using InitManage.Services;
using InitManage.Services.Interfaces;
using InitManage.ViewModels.Login;
using InitManage.ViewModels.Resource;
using InitManage.ViewModels.Setting;
using InitManage.Views.Pages;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Shared;
using Plugin.Firebase.Auth;
using Simple.Http;
using SkiaSharp.Views.Maui.Controls.Hosting;
using InitManage.Helpers;
using InitManage.Commons;
using InitManage.ViewModels.Booking;

#if IOS
using InitManage.Platforms.iOS.Helpers;
using Plugin.Firebase.iOS;
#elif ANDROID
using InitManage.Platforms.Android.Helpers;
using Plugin.Firebase.Android;
#elif MACCATALYST
using InitManage.Platforms.MacCatalyst.Helpers;
#elif WINDOWS
using InitManage.Platforms.Windows.Helpers;
#endif

namespace InitManage;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .UsePrism(prismAppBuilder => prismAppBuilder
                .RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterServices();
                    containerRegistry.RegisterNavigation();
                    containerRegistry.RegisterHelpers();
                })
                .OnAppStart(navigationService => navigationService.NavigateAsync($"{Constants.NavigationPage}/{Constants.LoginPage}"))
                )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("OpenSans-Semibold.ttf", Constants.IconFont);
            }).UseMauiCommunityToolkit()
            .RegisterFirebaseServices();

        return builder.Build();
    }

    private static void RegisterHelpers(this IContainerRegistry containerRegistry)
    {
#if ANDROID
		containerRegistry.RegisterSingleton<INotificationHelper, AndroidNotificationHelper>();
#elif IOS
		containerRegistry.RegisterSingleton<INotificationHelper, iOSNotificationHelper>();
#elif MACCATALYST
        containerRegistry.RegisterSingleton<INotificationHelper, MacNotificationHelper>();
#elif WINDOWS
        containerRegistry.RegisterSingleton<INotificationHelper, WindowsNotificationHelper>();
#endif

		containerRegistry.RegisterSingleton<IPreferenceHelper, PreferenceHelper>();
    }

	private static void RegisterServices(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IAlertDialogService, CommunityToolKitAlertDialogService>();
        containerRegistry.RegisterSingleton<IHttpService, HttpService>();

        containerRegistry.RegisterSingleton<IResourceService, ResourceService>();
        containerRegistry.RegisterSingleton<IUserService, UserService>();
        containerRegistry.RegisterSingleton<IOptionService, MockedOptionService>();
        containerRegistry.RegisterSingleton<ITypeService, TypeService>();
        containerRegistry.RegisterSingleton<IBookingService, MockedBookingService>();
    }

    private static void RegisterNavigation(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        containerRegistry.RegisterForNavigation<ResourcesPage, ResourcesViewModel>();
        containerRegistry.RegisterForNavigation<BookingsPage, BookingsViewModel>();
        containerRegistry.RegisterForNavigation<ResourcePage, ResourceViewModel>();
        containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<CreateResourcePage, CreateResourceViewModel>();

        containerRegistry.RegisterForNavigation<MainTabbedPage>();
        containerRegistry.RegisterForNavigation<NavigationPage>();
    }

    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                CrossFirebase.Initialize(app, launchOptions, CreateCrossFirebaseSettings());
                return false;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, state) =>
                CrossFirebase.Initialize(activity, state, CreateCrossFirebaseSettings())));
#endif
        });

        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
        return builder;
    }

    private static CrossFirebaseSettings CreateCrossFirebaseSettings()
    {
        return new CrossFirebaseSettings(isAuthEnabled: true);
    }
}

