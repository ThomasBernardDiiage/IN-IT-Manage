﻿using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Models.Wrappers;
using System;
using System.Net;

namespace InitManage.Services.Interfaces;

public interface IResourceService
{
    Task<HttpStatusCode> CreateResources(ResourceDTOCreate DTO);
    /// <summary>
    /// Get all existing resources
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<IResource>> GetResourcesAsync();

    /// <summary>
    /// Get a specific resource with his Id
    /// </summary>
    /// <param name="id">The id of the resource</param>
    /// <returns></returns>
    Task<IResource> GetResourceAsync(long id);

    /// <summary>
    /// Return all bookings of a resource
    /// </summary>
    /// <param name="resourceId">The id of the resource</param>
    /// <returns></returns>
    Task<IEnumerable<IBooking>> GetResourceBookingsAsync(long resourceId);

    Task<IEnumerable<BookingWrapper>> GetResourceBookingsWrappersAsync(long resourceId);
}

