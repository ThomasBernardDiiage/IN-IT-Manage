using System;
using Newtonsoft.Json;

namespace InitManage.Models.Interfaces;

public interface IResourceEntity
{
    long Id { get; set;}

    string Name { get; set; }

    string Description { get; set; }

    string Image { get; set; }

    string Address { get; set; }

    int Capacity { get; set; }

    long TypeId { get; set; }

    string TypeName { get; set; }
}

