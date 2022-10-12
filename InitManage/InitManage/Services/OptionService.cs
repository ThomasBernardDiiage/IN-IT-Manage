using System;
using InitManage.Models.DTOs;
using InitManage.Models.Interfaces;
using InitManage.Services.Interfaces;
using Simple.Http;
using InitManage.Commons;
using InitManage.Helpers.Interfaces;

namespace InitManage.Services;

public class OptionService : IOptionService
{
    private readonly IHttpService _httpService;
    private readonly IPreferenceHelper _preferenceHelper;

    public OptionService(IHttpService httpService, IPreferenceHelper preferenceHelper)
    {
        _httpService = httpService;
        _preferenceHelper = preferenceHelper;
    }

    public async Task<IEnumerable<IOptionEntity>> GetOptionsAsync()
    {
        var response = await _httpService.SendRequestAsync<IEnumerable<OptionDTO>>($"{_preferenceHelper.ApiBaseAddress}{Constants.OptionEndPoint}", HttpMethod.Get);
        return response?.Result;
    }
}

