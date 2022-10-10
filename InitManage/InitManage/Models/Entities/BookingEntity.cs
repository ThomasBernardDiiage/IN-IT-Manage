using System;
using InitManage.Models.Interfaces;

namespace InitManage.Models.Entities;

public class BookingEntity : IBookingEntity
{
    public BookingEntity()
    {
    }

    public long Id { get; set; }
    //public long UserId { get; set; }
    public long ResourceId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Capacity { get; set; }
    public string Firstname { get; set; }
}

