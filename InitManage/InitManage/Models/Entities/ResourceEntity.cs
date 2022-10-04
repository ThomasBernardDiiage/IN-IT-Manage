using System;
using InitManage.Models.Interfaces;

namespace InitManage.Models.Entities;

public class ResourceEntity : IResourceEntity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public int Capacity { get; set; }

    public string Address { get; set; }

    public long TypeId { get; set; }

    public string TypeName { get; set; }


    public ResourceEntity()
    {
    }

    public ResourceEntity(IResourceEntity resource)
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

