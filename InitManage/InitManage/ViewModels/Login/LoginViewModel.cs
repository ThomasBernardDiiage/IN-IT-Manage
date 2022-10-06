using InitManage.Helpers.Interfaces;
using InitManage.Resources.Translations;
using InitManage.Services.Interfaces;
using InitManage.Views.Pages;
using Plugin.Firebase.CloudMessaging;
using ReactiveUI;
using Sharpnado.TaskLoaderView;
using InitManage.Commons;

namespace InitManage.ViewModels.Login;

public class LoginViewModel : BaseViewModel
{
    private readonly INotificationHelper _notificationHelper;
    private readonly IPreferenceHelper _preferenceHelper;
    private readonly IUserService _userService;

    public LoginViewModel(
        INavigationService navigationService,
        INotificationHelper notificationHelper,
        IPreferenceHelper preferenceHelper,
        IUserService userService)
        : base(navigationService)
    {
        _notificationHelper = notificationHelper;
        _preferenceHelper = preferenceHelper;
        _userService = userService;

        LoginCommand = new TaskLoaderCommand(OnLoginCommand);
    }

    #region Life cycle

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

		try // Work only on Android (Must be tested on iOS real device)
		{
			_notificationHelper.Initialize();

			await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
			var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
			Console.WriteLine(token);

			_notificationHelper.SendNotification(AppResources.LocalNotificationStatus, AppResources.Ok);
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex);
		}

#if DEBUG
        Password = "admin";
#endif
        Mail = _preferenceHelper.Mail;
    }

    #endregion

    #region Properties

    #region Mail
    private string _mail;
    public string Mail
    {
        get => _mail;
        set => this.RaiseAndSetIfChanged(ref _mail, value);
    }
    #endregion

    #region Password
    private string _password;
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    #endregion

    #endregion

    #region Methods & Commands

    #region OnLoginCommand

    public TaskLoaderCommand LoginCommand { get; }
    private async Task OnLoginCommand()
    {
		LoadingMessage = AppResources.LoginInProgress;
        var isLoginSuccess = await _userService.LoginAsync(Mail, Password);

        if (isLoginSuccess)
        {
            _preferenceHelper.Mail = Mail;
            Mail = string.Empty;
            Password = string.Empty;

            await NavigationService.NavigateAsync(Constants.MainTabbedPage);
        }
		else
		{
			throw new Exception("Wrong username or password");
		}
	}
    #endregion

    #endregion
}

