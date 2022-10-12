using System;
using System.Net;
using InitManage.Commons;
using InitManage.Helpers.Interfaces;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;
using Simple.Http;

namespace InitManage.Services;

public class ResourceService : IResourceService
{
    private readonly IHttpService _httpService;
    private readonly IPreferenceHelper _preferenceHelper;

    public ResourceService(IHttpService httpService, IPreferenceHelper preferenceHelper)
    {
        _httpService = httpService;
        _preferenceHelper = preferenceHelper;
    }

    public async Task<ResourceWrapper> GetResourceWrapperAsync(long id)
    {
        var response = await _httpService.SendRequestAsync<DetailledResourceDTODown>($"{_preferenceHelper.ApiBaseAddress}{Constants.ResourceEndPoint}/{id}", HttpMethod.Get);
        var wrapper = new ResourceWrapper(response?.Result);


        return wrapper;
    }


    public async Task<IEnumerable<ResourceWrapper>> GetResourcesWrapperAsync()
    {
        var response = await _httpService.SendRequestAsync<IEnumerable<DetailledResourceDTODown>>($"{_preferenceHelper.ApiBaseAddress}{Constants.ResourcesEndPoint}", HttpMethod.Get);
        return response?.Result?.Select(r => new ResourceWrapper(r));
    }

    public async Task<bool> CreateResource(IResourceEntity resource)
    {
        var dto = new ResourceDTOUp(resource);
        var result = await _httpService.SendRequestAsync($"{_preferenceHelper.ApiBaseAddress}{Constants.ResourceEndPoint}", HttpMethod.Post, dto);
        return result.HttpStatusCode == HttpStatusCode.OK;
    }
}

