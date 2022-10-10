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

    public async Task<ResourceWrapper> GetResourceWrapperAsync(long id)
    {
        var response = await _httpService.SendRequestAsync<DetailledResourceDTODown>($"{Constants.ApiBaseAdress}{Constants.ResourceEndPoint}/{id}", HttpMethod.Get);
        var wrapper = new ResourceWrapper(response?.Result);


        return wrapper;
    }


    public async Task<IEnumerable<ResourceWrapper>> GetResourcesWrapperAsync()
    {
        var response = await _httpService.SendRequestAsync<IEnumerable<DetailledResourceDTODown>>($"{Constants.ApiBaseAdress}{Constants.ResourcesEndPoint}", HttpMethod.Get);
        return response?.Result?.Select(r => new ResourceWrapper(r));
    }

    public async Task<bool> CreateResource(IResourceEntity resource)
    {
        var dto = new ResourceDTOUp(resource);
        var result = await _httpService.SendRequestAsync($"{Constants.ApiBaseAdress}{Constants.ResourceEndPoint}", HttpMethod.Post, dto);
        return result.HttpStatusCode == HttpStatusCode.OK;
    }
}

