using InitManage.Helpers.Interfaces;
using InitManage.ViewModels.Booking;
using InitManage.ViewModels.Resource;
using InitManage.ViewModels.Setting;

namespace InitManage.Views.Pages;

public partial class MainTabbedPage : TabbedPage
{
    private readonly IPreferenceHelper _preferenceHelper;

	public MainTabbedPage()
	{
        /// MUST PUT THIS CODE HERE BECAUSE DI IS BROKEN PRISM TABBED PAGE

		InitializeComponent();

        #if IOS
		this.SelectedTabColor = (Color)Application.Current.Resources.MergedDictionaries.FirstOrDefault()["Primary"];
        #endif

        _preferenceHelper = ContainerLocator.Container.Resolve(typeof(IPreferenceHelper)) as IPreferenceHelper;

        var resourcesPage = ContainerLocator.Container.Resolve<ResourcesPage>();
        resourcesPage.BindingContext = ContainerLocator.Container.Resolve<ResourcesViewModel>();
        this.Children.Add(resourcesPage);

        var myResourcesPage = ContainerLocator.Container.Resolve<BookingsPage>();
        myResourcesPage.BindingContext = ContainerLocator.Container.Resolve<BookingsViewModel>();
        this.Children.Add(myResourcesPage);

        var settingPage = ContainerLocator.Container.Resolve<SettingsPage>();
		settingPage.BindingContext = ContainerLocator.Container.Resolve<SettingsViewModel>();
		this.Children.Add(settingPage);

        if (_preferenceHelper?.IsAdmin ?? false)
        {
            var createResourcePage = ContainerLocator.Container.Resolve<CreateResourcePage>();
            createResourcePage.BindingContext = ContainerLocator.Container.Resolve<CreateResourceViewModel>();
            this.Children.Add(createResourcePage);
        }
    }
}
