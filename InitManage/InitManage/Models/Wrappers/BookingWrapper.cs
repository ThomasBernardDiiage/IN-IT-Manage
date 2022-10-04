using System;
using System.Reactive;
using InitManage.Models.Interfaces;
using ReactiveUI;

namespace InitManage.Models.Wrappers;

public class BookingWrapper : ReactiveObject, IBookingEntity
{
    public BookingWrapper()
    {
        BookingTappedCommand = new Command(OnBookingTappedCommand);
    }

    public BookingWrapper(IBookingEntity booking):this()
    {
        Id = booking.Id;
        UserId = booking.UserId;
        ResourceId = booking.ResourceId;
        Start = booking.Start;
        End = booking.End;
        Capacity = booking.Capacity;
    }

    public long Id { get; set; }
    public long UserId { get; set; }
    public long ResourceId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Capacity { get; set; }

    public IResourceEntity Resource { get; set; }
    public IUserEntity User { get; set; }


    #region IsOverlayVisible

    private bool _isOverlayVisible;
    public bool IsOverlayVisible
    {
        get => _isOverlayVisible;
        set => this.RaiseAndSetIfChanged(ref _isOverlayVisible, value);
    }

    #endregion

    #region OnBookingTappedCommand

    public Command BookingTappedCommand { get; private set; }
    private void OnBookingTappedCommand()
    {
        IsOverlayVisible = !IsOverlayVisible;
        Console.WriteLine("========");
    }

    #endregion

}

