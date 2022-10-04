using InitManage.Models.Interfaces;
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

    [JsonProperty("picture")]
    public string Image { get; set; }

    [JsonProperty("maxCapacity")]
    public int Capacity { get; set; }

    [JsonProperty("position")]
    public string Address { get; set; }

    [JsonProperty("typeId")]
    public long TypeId { get; set; }

    [JsonProperty("typeName")]
    public string TypeName { get; set; }


    public ResourceDTODown()
    {
    }

    public ResourceDTODown(IResourceEntity resource)
    {
        Id = resource.Id;
        Name = resource.Name;
        Description = resource.Description;
        Image = resource.Image;
        TypeId = resource.TypeId;
        TypeName = resource.TypeName;
        Capacity = resource.Capacity;
    }
}