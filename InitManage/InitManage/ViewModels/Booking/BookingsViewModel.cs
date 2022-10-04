using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using ReactiveUI;
using Sharpnado.TaskLoaderView;
using InitManage.Resources.Translations;
using System.Reactive;

namespace InitManage.ViewModels.Booking;

public class BookingsViewModel : BaseViewModel
{
    private readonly IBookingService _bookingService;

    public BookingsViewModel(
        INavigationService navigationService,
        IBookingService bookingService)
        : base(navigationService)
    {
        _bookingService = bookingService;

        Loader = new TaskLoaderNotifier();

        _bookingsCache
            .Connect()
            .Bind(out _bookings)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();
    }

    #region Life cycle


    protected override async Task OnAppearingAsync()
    {
        if (!_bookingsCache.Items.Any())
        {
            Loader.Load(async _ =>
            {
                LoadingMessage = AppResources.LoadingResources;
                var bookingsWrapper = await _bookingService.GetBookingsWrappersAsync();
                _bookingsCache.AddOrUpdate(bookingsWrapper);
            });
        }
    }

    #endregion

    #region Properties

    public TaskLoaderNotifier Loader { get; }

    #region Dynamic list Resources
    private SourceCache<BookingWrapper, long> _bookingsCache = new SourceCache<BookingWrapper, long>(r => r.Id);
    private readonly ReadOnlyObservableCollection<BookingWrapper> _bookings;
    public ReadOnlyObservableCollection<BookingWrapper> Bookings => _bookings;
    #endregion



    #endregion

    #region Methods & Commands

    #endregion
}

