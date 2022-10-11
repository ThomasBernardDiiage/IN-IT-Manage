using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using InitManage.Helpers.Interfaces;
using InitManage.Models.Wrappers;
using AndroidApp = Android.App.Application;

namespace InitManage.Platforms.Android.Helpers;

public class AndroidNotificationHelper : INotificationHelper
{
    const string channelId = "default";
    const string channelName = "Default";
    const string channelDescription = "The default channel for notifications.";

    public const string TitleKey = "title";
    public const string MessageKey = "message";

    bool channelInitialized = false;
    int messageId = 0;
    int pendingIntentId = 0;

    NotificationManager manager;

    public event EventHandler NotificationReceived;

    public static AndroidNotificationHelper Instance { get; private set; }

    public AndroidNotificationHelper() => Initialize();

    public void Initialize()
    {
        if (Instance == null)
        {
            CreateNotificationChannel();
            Instance = this;
        }
    }

    public void SendNotification(string title, string message, DateTime? notifyTime = null)
    {
        if (!channelInitialized)
        {
            CreateNotificationChannel();
        }

        if (notifyTime != null)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.CancelCurrent);
            long triggerTime = GetNotifyTime(notifyTime.Value);
            AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
        }
        else
        {
            Show(title, message);
        }
    }

    public void ReceiveNotification(string title, string message)
    {
        var args = new NotificationEventArgs()
        {
            Title = title,
            Message = message,
        };
        NotificationReceived?.Invoke(null, args);
    }

    public void Show(string title, string message)
    {
        Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
        intent.PutExtra(TitleKey, title);
        intent.PutExtra(MessageKey, message);

        PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.UpdateCurrent);

        NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.init))
            .SetSmallIcon(Resource.Drawable.init)
            .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

        Notification notification = builder.Build();
        manager.Notify(messageId++, notification);
    }

    void CreateNotificationChannel()
    {
        manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelNameJava = new Java.Lang.String(channelName);
            var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
            {
                Description = channelDescription
            };
            manager.CreateNotificationChannel(channel);
        }

        channelInitialized = true;
    }

    long GetNotifyTime(DateTime notifyTime)
    {
        DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
        double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
        long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
        return utcAlarmTime; // milliseconds
    }

    public void CancelPush(int id)
    {
        var notificationManager = NotificationManagerCompat.From(AndroidApp.Context);
        notificationManager.CancelAll();
        CreateIntent(id);
    }

    private Intent CreateIntent(int id)
    {
        return new Intent(AndroidApp.Context, typeof(AndroidNotificationHelper)).SetAction("LocalNotifierIntent" + id);
    }
}


[BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
public class AlarmHandler : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        if (intent?.Extras != null)
        {
            string title = intent.GetStringExtra(AndroidNotificationHelper.TitleKey);
            string message = intent.GetStringExtra(AndroidNotificationHelper.MessageKey);

            AndroidNotificationHelper manager = AndroidNotificationHelper.Instance ?? new AndroidNotificationHelper();
            manager.Show(title, message);
        }
    }
}
