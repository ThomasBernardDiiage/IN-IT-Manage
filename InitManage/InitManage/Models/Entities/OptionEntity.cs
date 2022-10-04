using System;
using InitManage.Models.Interfaces;

namespace InitManage.Models.Entities;

public class OptionEntity : IOptionEntity
{
    public OptionEntity()
    {
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public long TypeId { get; set; }
}

