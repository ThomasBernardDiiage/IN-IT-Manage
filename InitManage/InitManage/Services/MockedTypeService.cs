using System;
using InitManage.Models.Entities;
using InitManage.Models.Interfaces;
using InitManage.Services.Interfaces;

namespace InitManage.Services;

public class MockedTypeService : ITypeService
{
    public MockedTypeService()
    {
    }

    public async Task<IEnumerable<ITypeEntity>> GetTypesAsync()
    {
        var types = new List<TypeEntity>();

        for (int i = 1; i < 20; i++)
        {
            var type = new TypeEntity()
            {
                Id = i,
                Name = $"Type {i}"
            };

            types.Add(type);
        }

        return types;
    }
}

