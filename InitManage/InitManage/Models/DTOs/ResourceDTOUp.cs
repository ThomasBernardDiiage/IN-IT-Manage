using InitManage.Models.Interfaces;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs;

public class ResourceDTOUp
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("picture")]
    public string Picture { get; set; }

    [JsonProperty("maxCapacity")]
    public int MaxCapacity { get; set; }

    [JsonProperty("position")]
    public string Position { get; set; }

    [JsonProperty("typeId")]
    public long TypeId { get; set; }


    public ResourceDTOUp()
    {
    }

    public ResourceDTOUp(IResourceEntity resource)
    {
        Name = resource.Name;
        Description = resource.Description;
        Picture = resource.Image;
        TypeId = resource.TypeId;
        MaxCapacity = resource.Capacity;
        Position = resource.Address;
    }
}