using System;
using System.Reactive;
using InitManage.Models.DTOs;
using InitManage.Models.Entities;
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
        ResourceId = booking.ResourceId;
        Start = booking.Start;
        End = booking.End;
        Capacity = booking.Capacity;
    }

    public BookingWrapper(DetailledBookingDTODown detailledBookingDTODown)
    {
        Id = detailledBookingDTODown.IdBooking;
        ResourceId = detailledBookingDTODown.IdResource;
        Start = detailledBookingDTODown.Start;
        End = detailledBookingDTODown.End;
        Firstname = detailledBookingDTODown.Firstname;

        var resource = new ResourceEntity
        {
            Id = detailledBookingDTODown.IdResource,
            Name = detailledBookingDTODown.ResourceName,
            Description = detailledBookingDTODown.Description,
            Image = detailledBookingDTODown.Picture,
            Address = detailledBookingDTODown.Position,
            Capacity = detailledBookingDTODown.MaxCapacity,
        };
        Resource = resource; ;
    }

    public long Id { get; set; }
    //public long UserId { get; set; }
    public long ResourceId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Capacity { get; set; }
    public string Firstname { get; set; }


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

