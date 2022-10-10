﻿using System;
namespace InitManage.Helpers.Interfaces;

public interface IPreferenceHelper
{
    bool IsAdmin { get; set; }
    string Mail { get; set; }
    string Token { get; set; }
    string Firstname { get; set; }
    string Lastname { get; set; }



    TimeSpan TimeBeforeReceiveNotification { get; set; }
    bool IsNotificationEnabled { get; set; }
}

