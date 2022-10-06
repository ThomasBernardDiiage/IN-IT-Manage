using System;
namespace InitManage.Services.Interfaces;

public interface IUserService
{
    Task<bool> LoginAsync(string mail, string password);

    Task LogoutAsync();
}

