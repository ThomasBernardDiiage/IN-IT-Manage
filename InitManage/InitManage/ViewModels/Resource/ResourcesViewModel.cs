using ReactiveUI;
using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using InitManage.Services.Interfaces;
using System.Reactive;
using InitManage.Models.Wrappers;
using DynamicData.PLinq;
using InitManage.Models.Interfaces;
using InitManage.Views.Pages;
using InitManage.Commons;
using Sharpnado.TaskLoaderView;
using InitManage.Resources.Translations;

namespace InitManage.ViewModels.Resource;

public class ResourcesViewModel : BaseViewModel
{
    private readonly IResourceService _resourceService;
    private readonly IOptionService _optionService;
	private readonly ITypeService _typeService;

    public ResourcesViewModel(
        INavigationService navigationService,
        IResourceService resourceService,
        IOptionService optionService,
		ITypeService typeService) : base(navigationService)
    {
        _resourceService = resourceService;
        _optionService = optionService;
		_typeService = typeService;

        ResourceTappedCommand = ReactiveCommand.Create<IResourceEntity, Task>(OnResourceTappedCommand);
        OptionEntryTappedCommand = ReactiveCommand.CreateFromTask(OnOptionEntryTappedCommand);

        var searchFilter = this.WhenAnyValue(vm => vm.SearchBarText)
            .Select(query =>
            {
                if (!string.IsNullOrEmpty(query))
                    return new Func<ResourceWrapper, bool>(resource => resource.IsCorrespondingToSearch(SearchBarText));
                else
                    return new Func<ResourceWrapper, bool>(resource => true);
            });

        var capacityFilter = this.WhenAnyValue(vm => vm.SelectedCapacity)
            .Select(query => new Func<ResourceWrapper, bool>(resource => resource.Capacity >= query));

        var addressFilter = this.WhenAnyValue(vm => vm.Address)
            .Select(query =>
            {
                if (!string.IsNullOrEmpty(query))
                    return new Func<ResourceWrapper, bool>(resource => resource.Address?.Contains(Address) ?? false);
                return new Func<ResourceWrapper, bool>(resource => true);
            });

        var typeResourceFilter = this.WhenAnyValue(vm => vm.SelectedType)
            .Select(query =>
            {
                if (query is not null)
                    return new Func<ResourceWrapper, bool>(r => r?.TypeId == query?.Id);
                return new Func<ResourceWrapper, bool>(r => true);
            });

        var typeOptionFilter = this.WhenAnyValue(vm => vm.SelectedType)
            .Select(query =>
            {
                if (query is not null)
                    return new Func<OptionWrapper, bool>(option => option?.TypeId == query?.Id);
                return new Func<OptionWrapper, bool>(option => true);
            });

        var timeFilter = this.WhenAnyValue(vm => vm.SelectedDate, vm => vm.SelectedTime)
            .Select(query =>
            {
                return new Func<ResourceWrapper, bool>(r => !(r.Bookings?.Any(booking => SelectedDate.Add(SelectedTime).IsBetween(booking.Start, booking.End)) ?? false));
            });

        _resourcesCache
            .Connect()
            .Filter(searchFilter)
            .Filter(capacityFilter)
            .Filter(addressFilter)
            .Filter(timeFilter)
            .Filter(typeResourceFilter)
            .Bind(out _resources)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();

        _optionsCache
            .Connect()
            .Filter(typeOptionFilter)
            .Bind(out _options)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe();

        Loader = new TaskLoaderNotifier();
        SelectedDate = DateTime.Now;
        SelectedTime = TimeSpan.FromHours(DateTime.Now.Hour);
    }

    #region Life cycle

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        if (!_resourcesCache.Items.Any())
        {
            Loader.Load(async _ =>
            {
                LoadingMessage = AppResources.LoadingResources;
                var resources = await _resourceService.GetResourcesWrapperAsync();

                if (resources == null)
                    throw new Exception("Error when resources recuperation");

                _resourcesCache.AddOrUpdate(resources);

                ResourcesCapacities = resources.Select(r => r.Capacity).OrderBy(x => x).Distinct().ToList();

				LoadingMessage = AppResources.LoadingTypes;
                Types = await _typeService.GetTypesAsync();

                LoadingMessage = AppResources.LoadingOptions;
                _optionsCache.AddOrUpdate((await _optionService.GetOptionsAsync()).Select(option => new OptionWrapper(option)));

            });
        }
    }

    #endregion

    #region Properties

    public TaskLoaderNotifier Loader { get; }

    #region IsOptionsVisible

    private bool _isOptionsVisible;
    public bool IsOptionsVisible
    {
        get => _isOptionsVisible;
        set => this.RaiseAndSetIfChanged(ref _isOptionsVisible, value);
    }

    #endregion

    #region SearchBarText
    private string _searchBarText;
    public string SearchBarText
    {
        get => _searchBarText;
        set => this.RaiseAndSetIfChanged(ref _searchBarText, value);
    }
    #endregion

    #region Address

    private string _address;
    public string Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }

    #endregion

    #region Dynamic list Resources
    private SourceCache<ResourceWrapper, long> _resourcesCache = new SourceCache<ResourceWrapper, long>(r => r.Id);
    private readonly ReadOnlyObservableCollection<ResourceWrapper> _resources;
    public ReadOnlyObservableCollection<ResourceWrapper> Resources => _resources;
    #endregion

    #region Dynamic list Options
    private SourceCache<OptionWrapper, long> _optionsCache = new SourceCache<OptionWrapper, long>(o => o.Id);
    private readonly ReadOnlyObservableCollection<OptionWrapper> _options;
    public ReadOnlyObservableCollection<OptionWrapper> Options => _options;
    #endregion

    #region ResourcesCapacities

    private List<int> _resourcesCapacities;
    public List<int> ResourcesCapacities
    {
        get => _resourcesCapacities;
        set => this.RaiseAndSetIfChanged(ref _resourcesCapacities, value);
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

    #region SelectedType

    private ITypeEntity _selectedType;
    public ITypeEntity SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    #endregion

    #region SelectedCapacity

    private int _selectedCapacity;
    public int SelectedCapacity
    {
        get => _selectedCapacity;
        set => this.RaiseAndSetIfChanged(ref _selectedCapacity, value);
    }

    #endregion

    #region SelectedDate

    private DateTime _selectedDate;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
    }

    #endregion

    #region SelectedTime

    private TimeSpan _selectedTime;
    public TimeSpan SelectedTime
    {
        get => _selectedTime;
        set => this.RaiseAndSetIfChanged(ref _selectedTime, value);
    }

    #endregion

    #endregion

    #region Methods & Commands

    #region OnResourceTappedCommand
    public ReactiveCommand<IResourceEntity, Task> ResourceTappedCommand { get; private set; }
    private async Task OnResourceTappedCommand(IResourceEntity resource)
    {
        var parameters = new NavigationParameters { { Constants.ResourceIdNavigationParameter, resource?.Id } };
        await NavigationService.NavigateAsync(Constants.ResourcePage, parameters);
    }
    #endregion

    #region OnOptionEntryTappedCommand
    public ReactiveCommand<Unit, Unit> OptionEntryTappedCommand { get; private set; }
    private async Task OnOptionEntryTappedCommand() => IsOptionsVisible = !IsOptionsVisible;
    #endregion

    #endregion
}

