using ReactiveUI;
using InitManage.Models.Entities;
using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using InitManage.Services.Interfaces;
using System.Reactive;
using InitManage.Models.Wrappers;
using InitManage.Commons;
using InitManage.Models.Interfaces;

namespace InitManage.ViewModels.Resource;

public class CreateResourceViewModel : BaseViewModel
{
    private readonly IResourceService _resourceService;
    private readonly IOptionService _optionService;
    private readonly ITypeService _typeService;

    public CreateResourceViewModel(
        INavigationService navigationService,
        IResourceService resourceService,
        IOptionService optionService,
        ITypeService typeService) : base(navigationService)
    {
        _resourceService = resourceService;
        _optionService = optionService;
        _typeService = typeService;



        var canExecuteCreate = this.WhenAnyValue(vm => vm.Name, vm => vm.Description, vm => vm.Picture, vm => vm.Adress, vm => vm.Type, vm => vm.Capacity)
                .Select(query =>
                {
                    var IsNameOk = !string.IsNullOrEmpty(Name);
                    var IDescriptionOk = !string.IsNullOrEmpty(Description);
                    var IsPictureOk = !string.IsNullOrEmpty(Picture);
                    var IsAdressOk = !string.IsNullOrEmpty(Adress);
                    var IsTypeOk = Type != null;
                    var IsCapacityOk = Capacity > 0;

                    return IsNameOk && IDescriptionOk && IsAdressOk && IsTypeOk && IsCapacityOk && IsPictureOk;
                })
                .ObserveOn(RxApp.MainThreadScheduler);

        CreateCommand = ReactiveCommand.CreateFromTask(OnCreateCommand, canExecuteCreate);

        _optionsCache
            .Connect()
            .Bind(out _options)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();


        Picture = "https://image.jimcdn.com/app/cms/image/transf/dimension=940x10000:format=jpg/path/s398965e309713775/image/ia5d911c472440089/version/1478270869/image.jpg";
    }

    #region Life cycle

    protected override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        var options = await _optionService.GetOptionsAsync();
        _optionsCache.AddOrUpdate(options.Select(option => new OptionWrapper(option)));


        Types = await _typeService.GetTypesAsync();
    }

    #endregion

    #region Properties

    #region Picture

    private string _picture;
    public string Picture
    {
        get => _picture;
        set => this.RaiseAndSetIfChanged(ref _picture, value);
    }

    #endregion

    #region Name

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    #endregion

    #region Description

    private string _description;
    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    #endregion

    #region Capacity

    private int _capacity;
    public int Capacity
    {
        get => _capacity;
        set => this.RaiseAndSetIfChanged(ref _capacity, value);
    }

    #endregion

    #region Type

    private ITypeEntity _type;
    public ITypeEntity Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    #endregion

    #region Types

    private IEnumerable<ITypeEntity> _types;
    public IEnumerable<ITypeEntity> Types
    {
        get => _types;
        set => this.RaiseAndSetIfChanged(ref _types, value);
    }

    #endregion

    #region Adress

    private string _adress;
    public string Adress
    {
        get => _adress;
        set => this.RaiseAndSetIfChanged(ref _adress, value);
    }

    #endregion

    #region Options
    private SourceCache<OptionWrapper, long> _optionsCache = new SourceCache<OptionWrapper, long>(r => r.Id);
    private readonly ReadOnlyObservableCollection<OptionWrapper> _options;
    public ReadOnlyObservableCollection<OptionWrapper> Options => _options;
    #endregion

    #endregion


    #region Methods & Commands

    #region OnCreateCommand

    public ReactiveCommand<Unit, Unit> CreateCommand { get; private set; }
    private async Task OnCreateCommand()
    {
        var resource = new ResourceEntity()
        {
            Name = _name,
            Capacity = _capacity,
            Description = _description,
            Image = _picture,
            Address = _adress,
            TypeId = Type.Id
        };

        var resourceCreated = await _resourceService.CreateResource(resource);

        if (resourceCreated)
            await NavigationService.NavigateAsync($"{Constants.MainTabbedPage}?{KnownNavigationParameters.SelectedTab}={Constants.ResourcesPage}");
    }
	#endregion

    #endregion
}