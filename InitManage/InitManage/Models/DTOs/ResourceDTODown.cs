﻿using InitManage.Models.Interfaces;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs;

public class ResourceDTODown : IResourceEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("capacity")]
    public int Capacity { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }


    public ResourceDTODown()
    {
    }

    public ResourceDTODown(IResourceEntity resource)
    {
        Id = resource.Id;
        Name = resource.Name;
        Description = resource.Description;
        Image = resource.Image;
        Type = resource.Type;
        Capacity = resource.Capacity;
    }
}