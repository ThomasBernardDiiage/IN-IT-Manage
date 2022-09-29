using System;
using System.Reactive;
using InitManage.Helpers.Interfaces;
using ReactiveUI;

namespace InitManage.ViewModels.Setting;

public class SettingsViewModel : BaseViewModel
{
    private readonly IPreferenceHelper _preferenceHelper;

    public SettingsViewModel(INavigationService navigationService, IPreferenceHelper preferenceHelper) : base(navigationService)
    {
        _preferenceHelper = preferenceHelper;

        LogoutCommand = ReactiveCommand.CreateFromTask(OnLogoutCommand);

        if (Application.Current.RequestedTheme == AppTheme.Dark) DarkModeEnabled = true;
        Application.Current.RequestedThemeChanged += OnThemeChanged;

        Reminders = new[] { TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30) };
        Reminder = Reminders.FirstOrDefault();
        IsNotificationEnabled = _preferenceHelper.IsNotificationEnabled;
    }

    #region Properties

    #region DarkModeEnabled

    private bool _darkModeEnabled;
    public bool DarkModeEnabled
    {
        get => _darkModeEnabled;
        set => this.RaiseAndSetIfChanged(ref _darkModeEnabled, value);
    }

    #endregion

    #region Reminders

    private IEnumerable<TimeSpan> _reminders;
    public IEnumerable<TimeSpan> Reminders
    {
        get => _reminders;
        set => this.RaiseAndSetIfChanged(ref _reminders, value);
    }

    #endregion

    #region Reminder

    private TimeSpan _reminder;
    public TimeSpan Reminder
    {
        get => _reminder;
        set
        {
            this.RaiseAndSetIfChanged(ref _reminder, value);
            _preferenceHelper.TimeBeforeReceiveNotification = value;
        }
    }

    #endregion

    #region IsNotificationEnabled

    private bool _isNotificationEnabled;
    public bool IsNotificationEnabled
    {
        get => _isNotificationEnabled;
        set
        {
            this.RaiseAndSetIfChanged(ref _isNotificationEnabled, value);
            _preferenceHelper.IsNotificationEnabled = value;
        }
    }

    #endregion

    #endregion

    #region Methods & Commands

    #region OnLogoutCommand
    public ReactiveCommand<Unit, Unit> LogoutCommand { get; private set; }
    private async Task OnLogoutCommand() =>  await NavigationService.GoBackToRootAsync();
    #endregion

    #region OnThemeChanged
    private void OnThemeChanged(object sender, AppThemeChangedEventArgs e) => DarkModeEnabled = e?.RequestedTheme == AppTheme.Dark;
    #endregion

    #endregion
}

