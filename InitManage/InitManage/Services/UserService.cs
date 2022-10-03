using System;
using InitManage.Services.Interfaces;
using InitManage.Helpers.Interfaces;
using Simple.Http;
using InitManage.Models.DTOs;
using InitManage.Commons;

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

    public async Task<bool> LoginAsync(string mail, string password)
    {
        var dto = new AuthDtoUp()
        {
            Mail = mail,
            Password = password
        };

        var response = await _httpService.SendRequestAsync<AuthDTODown>($"{Constants.ApiBaseAdress}{Constants.AuthEndPoint}", HttpMethod.Post, dto);

        if (response?.Result != null)
        {
            _preferenceHelper.IsAdmin = response.Result.IsAdmin;

            // TODO MUST REMOVE THIS
            _preferenceHelper.IsAdmin = true;

            _preferenceHelper.Mail = response.Result.Mail;

            return true;
        }

        return false;
    }
}

