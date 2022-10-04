using System;
using InitManage.Models.Interfaces;

namespace InitManage.Models.Wrappers;

public class ResourceWrapper : IResourceEntity, ISearchable
{
    public ResourceWrapper()
    {
    }

    public ResourceWrapper(IResourceEntity resource)
    {
        Id = resource.Id;
        Name = resource.Name;
        Description = resource.Description;
        Image = resource.Image;
        TypeId = resource.TypeId;
        TypeName = resource.TypeName;
        Capacity = resource.Capacity;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string Address { get; set; }
    public long TypeId { get; set; }
    public string TypeName { get; set; }


    public bool IsCorrespondingToSearch(string searchedValue)
    {
        var nameIsMatching = Name?.ToLower()?.Contains(searchedValue?.ToLower()) ?? false;
        var descriptionIsMatching = Description?.ToLower()?.Contains(searchedValue?.ToLower()) ?? false;

        return nameIsMatching || descriptionIsMatching;
    }
}

