using System;
using InitManage.Models.Entities;
using InitManage.Models.Interfaces;
using InitManage.Services.Interfaces;
using InitManage.Models.Wrappers;

namespace InitManage.Services;

public class MockedBookingService : IBookingService
{
    private readonly IResourceService _resourceService;

    public MockedBookingService(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public Task<bool> CreateBookingAsync(IBookingEntity booking)
    {
        throw new NotImplementedException();
    }

    public async Task<IBookingEntity> GetBookingAsync(long id)
    {
        var booking = new BookingEntity()
        {
            Id = 1,
            Capacity = 2,
            ResourceId = 1,
            //UserId = 1,
            Start = DateTime.Now,
            End = DateTime.Now.AddMinutes(30)
        };

        return booking;
    }

    public async Task<IEnumerable<IBookingEntity>> GetBookingsAsync()
    {
        var bookings = new List<BookingEntity>();

        for (int i = 1; i < 20; i++)
        {
            var booking = new BookingEntity()
            {
                Id = i,
                Capacity = new Random().Next(2, 28),
                ResourceId = 1,
                //UserId = 1,
                Start = DateTime.Now.AddMinutes(15 * i),
                End = DateTime.Now.AddMinutes(15 * i + 15)
            };

            bookings.Add(booking);
        }

        return bookings;
    }

    public async Task<IEnumerable<BookingWrapper>> GetBookingsWrappersAsync()
    {
        var bookingsWrappers = new List<BookingWrapper>();
        var bookings = await GetBookingsAsync();

        foreach (var booking in bookings)
        {
            var bookingWrapper = new BookingWrapper(booking);
            bookingWrapper.Resource = await _resourceService.GetResourceWrapperAsync(bookingWrapper.ResourceId);
            bookingsWrappers.Add(bookingWrapper);
        }

        return bookingsWrappers;
    }
}

