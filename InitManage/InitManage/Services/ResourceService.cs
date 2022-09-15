﻿using System;
using System.Net;
using CsharpTools.Services.Interfaces;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using InitManage.Services.Interfaces;

namespace InitManage.Services;

public class ResourceService : IResourceService
{
    private readonly IHttpService _httpService;
    public ResourceService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public Task<IResource> GetResourceAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IBooking>> GetResourceBookingsAsync(long resourceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookingWrapper>> GetResourceBookingsWrappersAsync(long resourceId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IResource>> GetResourcesAsync()
    {
        var resutl = await _httpService.SendHttpRequest<IEnumerable<ResourceDTODown>>("http://10.4.0.112:3000/api/", HttpMethod.Get);
        return resutl.Content;
        }

    public async Task<HttpStatusCode> CreateResources(ResourceDTOCreate DTO)
    {
        var result = await _httpService.SendHttpRequest<IEnumerable<ResourceDTOCreate>>("http://10.4.0.112:3000/api/resource", HttpMethod.Post, DTO);
        return result.Status;
    }
}

