using System;
using InitManage.Services.Interfaces;
using InitManage.Helpers.Interfaces;
using Simple.Http;
using InitManage.Models.DTOs;
using InitManage.Commons;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InitManage.Services;

public class UserService : IUserService
{
    private readonly IPreferenceHelper _preferenceHelper;
    private readonly IHttpService _httpService;

    public UserService(IPreferenceHelper preferenceHelper, IHttpService httpService)
    {
        _preferenceHelper = preferenceHelper;
        _httpService = httpService;
    }

    public async Task LogoutAsync()
    {
        _preferenceHelper.Token = string.Empty;
        _preferenceHelper.Token = string.Empty;
        _httpService.HttpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<bool> LoginAsync(string mail, string password)
    {
        var dto = new AuthDtoUp()
        {
            Username = mail,
            Password = password
        };

        var response = await _httpService.SendRequestAsync<AuthDTODown>($"{Constants.ApiBaseAdress}{Constants.AuthEndPoint}", HttpMethod.Post, dto);

        if (response?.Result != null)
        {
            _preferenceHelper.Token = response?.Result?.Data?.Token;
            _httpService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _preferenceHelper.Token);

            // TODO MUST REMOVE THIS
            _preferenceHelper.IsAdmin = response?.Result?.Data?.UserData?.IsAdmin ?? false;
            _preferenceHelper.Mail = "hello";

            return true;
        }

        return false;
    }
}

