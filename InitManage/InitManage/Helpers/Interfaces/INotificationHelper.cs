using System;
namespace InitManage.Helpers.Interfaces;

public interface INotificationHelper
{
    event EventHandler NotificationReceived;
    void Initialize();
    void SendNotification(string title, string message, DateTime? notifyTime = null);
    void ReceiveNotification(string title, string message);
    void CancelPush(int id);
}

