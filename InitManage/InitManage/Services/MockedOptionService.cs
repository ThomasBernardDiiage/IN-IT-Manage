using System;
using InitManage.Models.Entities;
using InitManage.Models.Interfaces;
using InitManage.Services.Interfaces;

namespace InitManage.Services;

public class MockedOptionService : IOptionService
{
    public MockedOptionService()
    {
    }

    public async Task<IEnumerable<IOptionEntity>> GetOptionsAsync()
    {
        var options = new List<OptionEntity>();


        for (int i = 0; i < 30; i++)
        {
            var option = new OptionEntity()
            {
                Id = i,
                Name = $"Option {i}",
                TypeId = 1
            };

            options.Add(option);
        }

        return options;
    }
}

