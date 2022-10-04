using System;
using System.Net;
using InitManage.Commons;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using Simple.Http;

namespace InitManage.Services;

public class ResourceService : IResourceService
{
    private readonly IHttpService _httpService;

    public ResourceService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<IResourceEntity> GetResourceAsync(long id)
    {
        var response = await _httpService.SendRequestAsync<ResourceDTODown>($"{Constants.ApiBaseAdress}{Constants.ResourceEndPoint}/{id}", HttpMethod.Get);
        return response.Result;
    }

    public Task<IEnumerable<IBookingEntity>> GetResourceBookingsAsync(long resourceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookingWrapper>> GetResourceBookingsWrappersAsync(long resourceId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IResourceEntity>> GetResourcesAsync()
    {
        var response = await _httpService.SendRequestAsync<IEnumerable<ResourceDTODown>>($"{Constants.ApiBaseAdress}{Constants.ResourceEndPoint}", HttpMethod.Get);
        return response?.Result;
    }

    public async Task<bool> CreateResource(IResourceEntity resource)
    {
        var dto = new ResourceDTOUp(resource);
        var result = await _httpService.SendRequestAsync($"{Constants.ApiBaseAdress}{Constants.ResourceEndPoint}", HttpMethod.Post, dto);
        return result.HttpStatusCode == HttpStatusCode.OK;
    }
}

