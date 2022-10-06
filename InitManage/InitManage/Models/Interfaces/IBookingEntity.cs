using System;
namespace InitManage.Models.Interfaces;

public interface IBookingEntity
{
    long Id { get; set; }
    //long UserId { get; set; }
    long ResourceId { get; set; }
    DateTime Start { get; set; }
    DateTime End { get; set; }
    int Capacity { get; set; }

}

