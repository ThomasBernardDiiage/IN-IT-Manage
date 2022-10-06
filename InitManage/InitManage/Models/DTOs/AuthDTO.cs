using System;
using Newtonsoft.Json;

namespace InitManage.Models.DTOs;



public class AuthDTODown
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public AuthDTODownData Data { get; set; }
}

public class AuthDTODownUserData
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; set; }
}

public class AuthDTODownData
{
    [JsonProperty("token")]
    public string Token { get; set; }

    [JsonProperty("userData")]
    public AuthDTODownUserData UserData { get; set; }
}


public class AuthDtoUp
{
    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}

