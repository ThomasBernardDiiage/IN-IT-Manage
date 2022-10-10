using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData;
using InitManage.Commons;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using InitManage.Views.Pages;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;
using Sharpnado.TaskLoaderView;
using InitManage.Models.Entities;
using InitManage.Helpers.Interfaces;
using InitManage.Resources.Translations;

namespace InitManage.ViewModels.Resource;

public class ResourceViewModel : BaseViewModel
{
    private readonly IResourceService _resourceService;
    private readonly IBookingService _bookingService;
    private readonly INotificationHelper _notificationHelper;
    private readonly IPreferenceHelper _preferenceHelper;

    public ResourceViewModel(
        INavigationService navigationService,
        IResourceService resourceService,
        IBookingService bookingService,
        INotificationHelper notificationHelper,
        IPreferenceHelper preferenceHelper) : base(navigationService)
    {
        _resourceService = resourceService;
        _bookingService = bookingService;
        _notificationHelper = notificationHelper;
        _preferenceHelper = preferenceHelper;

        BookCommand = ReactiveCommand.CreateFromTask(OnBookCommand);

        _bookingsCache
            .Connect()
            .Bind(out _bookings)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();

        Loader = new TaskLoaderNotifier();
        BookingDate = DateTime.Parse($"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}");
        StartTime = TimeSpan.FromHours(DateTime.Now.Hour);
        EndTime = TimeSpan.FromHours(DateTime.Now.Hour);
    }


    #region Life cycle
    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        if (parameters.TryGetValue(Constants.ResourceIdNavigationParameter, out long resourceId))
        {
            Loader.Load(async _ =>
            {
                Resource = await _resourceService.GetResourceWrapperAsync(resourceId);
                Options = string.Join(", ", Resource?.Options?.Select(option => option.Name));

                //_bookingsCache.AddOrUpdate(bookings);
            });
        }
        else
            await NavigationService.GoBackAsync();
    }
    #endregion

    #region Properties

    public TaskLoaderNotifier Loader { get; }

    #region Resource

    private ResourceWrapper _resource;
    public ResourceWrapper Resource
    {
        get => _resource;
        set => this.RaiseAndSetIfChanged(ref _resource, value);
    }

    #endregion

    #region Options

    private string _option;
    public string Options
    {
        get => _option;
        set => this.RaiseAndSetIfChanged(ref _option, value);
    }

    #endregion

    #region Capacity
    private int _capacity;
    public int Capacity
    {
        get => _capacity;
        set => this.RaiseAndSetIfChanged(ref _capacity, value);
    }
    #endregion

    #region BookingDate

    private DateTime _bookingDate;
    public DateTime BookingDate
    {
        get => _bookingDate;
        set => this.RaiseAndSetIfChanged(ref _bookingDate, value);
    }

    #endregion

    #region StartTime

    private TimeSpan _startTime;
    public TimeSpan StartTime
    {
        get => _startTime;
        set => this.RaiseAndSetIfChanged(ref _startTime, value);
    }

    #endregion

    #region EndTime

    private TimeSpan _endTime;
    public TimeSpan EndTime
    {
        get => _endTime;
        set => this.RaiseAndSetIfChanged(ref _endTime, value);
    }

    #endregion

    #region Dynamic list Bookings
    private SourceCache<BookingWrapper, long> _bookingsCache = new SourceCache<BookingWrapper, long>(b => b.Id);
    private readonly ReadOnlyObservableCollection<BookingWrapper> _bookings;
    public ReadOnlyObservableCollection<BookingWrapper> Bookings => _bookings;
    #endregion

    #endregion

    #region Methods & Commands

    #region BookCommand

    public ReactiveCommand<Unit, Unit> BookCommand { get; private set; }
    private async Task OnBookCommand()
    {
        var booking = new BookingEntity()
        { 
            ResourceId = Resource.Id,
            Capacity = Capacity,
            Start = BookingDate.Add(StartTime),
            End = BookingDate.Add(EndTime)
        };

        

        if (await _bookingService.CreateBookingAsync(booking))
        {
            if (_preferenceHelper.IsNotificationEnabled)
                _notificationHelper.SendNotification
                    (AppResources.Reminder, $"{AppResources.YourBookingStartIn} {_preferenceHelper.TimeBeforeReceiveNotification.Minutes} {AppResources.Minutes}", BookingDate.Add(StartTime).AddMinutes(-_preferenceHelper.TimeBeforeReceiveNotification.Minutes));


            await NavigationService.GoBackAsync();
    
        }
    }

    #endregion


    #endregion
}

