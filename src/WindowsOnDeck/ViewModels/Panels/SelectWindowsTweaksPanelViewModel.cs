namespace WindowsOnDeck.ViewModels.Panels;

public partial class SelectWindowsTweaksPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public SelectWindowsTweaksPanelViewModel(IRegionManager regionManager, IYamlDataService staticDataService)
    {
        _regionManager = regionManager;
        WindowsTweaks.AddRange(staticDataService.LoadWindowsTweaks());
    }
    
    public ObservableCollection<WindowsTweak> WindowsTweaks { get; } = new();

    [RelayCommand]
    private void ProcessTweaks() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(ProcessWindowsTweaksPanel));

    protected override bool HasLogMessages => false;
}

