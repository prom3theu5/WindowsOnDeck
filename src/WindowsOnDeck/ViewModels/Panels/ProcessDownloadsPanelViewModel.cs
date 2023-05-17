namespace WindowsOnDeck.ViewModels.Panels;

public partial class ProcessDownloadsPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    private readonly IDownloadService _downloadService;
    private readonly SelectDownloadsPanelViewModel _selectDownloadsPanelViewModel;
    private readonly Progress<int> _progress = new();

    [ObservableProperty]
    private bool _downloadsFinished;

    [ObservableProperty]
    private double _downloadProgress;

    [ObservableProperty]
    private string? _currentDownload;
    
    [ObservableProperty]
    private string? _animation;

    public ProcessDownloadsPanelViewModel(IRegionManager regionManager, IDownloadService downloadService, SelectDownloadsPanelViewModel selectDownloadsPanelViewModel)
    {
        _regionManager = regionManager;
        _downloadService = downloadService;
        _selectDownloadsPanelViewModel = selectDownloadsPanelViewModel;
        _progress.ProgressChanged += UpdateProgress;
        _animation = Literals.DownloadingAnimation.AnimationResource();
        CurrentDownload = string.Empty;
        DownloadsFinished = false;
    }

    protected override bool HasLogMessages => false;

    /// <summary>Navigation completed successfully.</summary>
    /// <param name="navigationContext">Navigation context.</param>
    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        Task.Run(async () =>
        {
            foreach (var downloadableItem in _selectDownloadsPanelViewModel
                         .DownloadableItems
                         .Where(x=> x.IsSelected))
            {
                DownloadProgress = 0;
                CurrentDownload = downloadableItem.Name;
                await _downloadService.DownloadFileAsync(downloadableItem, _progress);
                DownloadProgress = 0;
            }
                    
            DownloadsFinished = true;
        });
    }

    [RelayCommand]
    private void SelectTweaks() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(InstallingDownloadsPanel));

    private void UpdateProgress(object? sender, int value) => 
        DownloadProgress = value;
}

