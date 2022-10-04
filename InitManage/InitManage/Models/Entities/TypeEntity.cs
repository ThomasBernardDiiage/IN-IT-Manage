using System;
using InitManage.Models.Interfaces;

namespace InitManage.Models.Entities;

public class TypeEntity : ITypeEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
}

