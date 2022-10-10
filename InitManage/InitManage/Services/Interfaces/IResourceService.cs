using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using System;
using System.Net;

namespace InitManage.Services.Interfaces;

public interface IResourceService
{
    Task<bool> CreateResource(IResourceEntity resource);
    /// <summary>
    /// Get all existing resources
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ResourceWrapper>> GetResourcesWrapperAsync();

    /// <summary>
    /// Get a specific resource with his Id
    /// </summary>
    /// <param name="id">The id of the resource</param>
    /// <returns></returns>
    Task<ResourceWrapper> GetResourceWrapperAsync(long id);
}
