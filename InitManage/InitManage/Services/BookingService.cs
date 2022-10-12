using InitManage.Commons;
using InitManage.Helpers.Interfaces;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using Simple.Http;
using System.Net;

namespace InitManage.Services;

public class BookingService: IBookingService
{
    private readonly IHttpService _httpService;
    private readonly IPreferenceHelper _preferenceHelper;

    public BookingService(IHttpService httpService, IPreferenceHelper preferenceHelper)
    {
        _httpService = httpService;
        _preferenceHelper = preferenceHelper;
    }

    public Task<IBookingEntity> GetBookingAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IBookingEntity>> GetBookingsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookingWrapper>> GetBookingsWrappersAsync()
    {
        var response = await _httpService.SendRequestAsync<IEnumerable<DetailledBookingDTODown>>($"{_preferenceHelper.ApiBaseAddress}{Constants.BookingEndPoint}/{Constants.UserEndPoint}", HttpMethod.Get);

        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            var wrappers = response.Result?.Select(x => new BookingWrapper(x));
            return wrappers;
        }
        return null;
    }

    public async Task<bool> CreateBookingAsync(IBookingEntity booking)
    {
        var dto = new BookingDTOUp(booking);
        var result = await _httpService.SendRequestAsync($"{_preferenceHelper.ApiBaseAddress}{Constants.BookingEndPoint}", HttpMethod.Post, dto);
        return result.HttpStatusCode == HttpStatusCode.OK;
    }
}
