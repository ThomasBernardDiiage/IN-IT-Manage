using System;
using InitManage.Helpers.Interfaces;

namespace InitManage.Helpers;

public class PreferenceHelper : IPreferenceHelper
{

    #region IsAdmin
    public bool IsAdmin
    {
        get => Preferences.Get(nameof(IsAdmin), false);
        set => Preferences.Set(nameof(IsAdmin), value);
    }
    #endregion

    #region IsNotificationEnabled
    public bool IsNotificationEnabled
    {
        get => Preferences.Get(nameof(IsNotificationEnabled), false);
        set => Preferences.Set(nameof(IsNotificationEnabled), value);
    }
    #endregion

    #region Mail
    public string Mail
    {
        get => Preferences.Get(nameof(Mail), string.Empty);
        set => Preferences.Set(nameof(Mail), value);
    }
    #endregion

    #region TimeBeforeReceiveNotification
    public TimeSpan TimeBeforeReceiveNotification
    {
        get
        {
            var minutes = Preferences.Get(nameof(TimeBeforeReceiveNotification), 10);
            return TimeSpan.FromMinutes(minutes);
        }
        set
        {
            var minutes = value.Minutes;
            Preferences.Set(nameof(TimeBeforeReceiveNotification), minutes);
        }
    }
    #endregion


    #region Token
    public string Token
    {
        get => Preferences.Get(nameof(Token), string.Empty);
        set => Preferences.Set(nameof(Token), value);
    }
    #endregion
}

