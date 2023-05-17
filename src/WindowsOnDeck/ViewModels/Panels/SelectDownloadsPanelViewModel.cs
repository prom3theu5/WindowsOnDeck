namespace WindowsOnDeck.ViewModels.Panels;

public partial class SelectDownloadsPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public SelectDownloadsPanelViewModel(IRegionManager regionManager, IYamlDataService staticDataService)
    {
        _regionManager = regionManager;
        DownloadableItems.AddRange(staticDataService.LoadDownloadableItems());
    }
   
    public ObservableCollection<DownloadableItem> DownloadableItems { get; } = new();

    [RelayCommand]
    private void ProcessDownloads() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(ProcessDownloadsPanel));

    protected override bool HasLogMessages => false;
}

