using InitManage.Commons;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using Simple.Http;
using System.Net;

namespace InitManage.Services
{
    public class BookingService: IBookingService
    {
        private readonly IHttpService _httpService;

        public BookingService(IHttpService httpService)
        {
            _httpService = httpService;
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
            var response = await _httpService.SendRequestAsync<IEnumerable<DetailledBookingDTODown>>($"{Constants.ApiBaseAdress}{Constants.BookingEndPoint}/{Constants.UserEndPoint}", HttpMethod.Get);

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
            var result = await _httpService.SendRequestAsync($"{Constants.ApiBaseAdress}{Constants.BookingEndPoint}", HttpMethod.Post, dto);
            return result.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
