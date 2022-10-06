using System;
namespace InitManage.Commons;

public class Constants
{
    public const string ResourceIdNavigationParameter = "resource";

    //public const string ApiBaseAdress = "https://6329dd90d2c97d8c52728c68.mockapi.io/api/";
    public const string ApiBaseAdress = "http://10.4.0.84:3000/";
    public const string ResourceEndPoint = "resource";
    public const string ResourcesEndPoint = "resources";
    public const string AuthEndPoint = "login";
    public const string OptionEndPoint = "options";
    public const string TypeEndPoint = "types";

	public const string IconFont = nameof(IconFont);

	#region Page names

	public const string MainTabbedPage = nameof(MainTabbedPage);
	public const string CreateResourcePage = nameof(CreateResourcePage);
	public const string LoginPage = nameof(LoginPage);
	public const string MyResourcesPage = nameof(MyResourcesPage);
	public const string ResourcePage = nameof(ResourcePage);
	public const string ResourcesPage = nameof(ResourcesPage);
	public const string SettingsPage = nameof(SettingsPage);
	public const string NavigationPage = nameof(NavigationPage);

    #endregion


    #region Images names

    public const string BookImage = "book";
    public const string MagnifyingGlassImage = "magnifying_glass";
    public const string PinImage = "pin";
    public const string PeoplesImage = "peoples";
    public const string TagsImage = "tags";
    public const string CalendarImage = "calendar";
    public const string ToolboxImage = "toolbox";


    #endregion
}

