using System;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;

namespace InitManage.Services.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<IBookingEntity>> GetBookingsAsync();
    Task<IBookingEntity> GetBookingAsync(long id);
    Task<IEnumerable<BookingWrapper>> GetBookingsWrappersAsync();

    Task<bool> CreateBookingAsync(IBookingEntity booking);
}

