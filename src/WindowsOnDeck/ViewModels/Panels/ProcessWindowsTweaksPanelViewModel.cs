namespace WindowsOnDeck.ViewModels.Panels;

public partial class ProcessWindowsTweaksPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    private readonly ICommandExecutionService _commandExecutionService;
    private readonly IDownloadService _downloadService;
    private readonly SelectWindowsTweaksPanelViewModel _selectWindowsTweaksPanelViewModel;

    [ObservableProperty]
    private bool _applyingFinished;

    [ObservableProperty]
    private string? _currentTweak;
    
    public ProcessWindowsTweaksPanelViewModel(
        IRegionManager regionManager,
        ICommandExecutionService commandExecutionService,
        IDownloadService downloadService,
        SelectWindowsTweaksPanelViewModel selectWindowsTweaksPanelViewModel)
    {
        _regionManager = regionManager;
        _commandExecutionService = commandExecutionService;
        _downloadService = downloadService;
        _selectWindowsTweaksPanelViewModel = selectWindowsTweaksPanelViewModel;
        _currentTweak = string.Empty;
        _commandExecutionService.OnStdOutputMessage += AddMessageToLog;
        _commandExecutionService.OnStdErrorMessage += AddMessageToLog;
    }

    protected override bool HasLogMessages => true;

    public override async void OnNavigatedTo(NavigationContext navigationContext)
    {
        try
        {
            await _commandExecutionService.ExecuteCommandInPowershellAsync("set-executionpolicy unrestricted -Force", null, null);
            
            foreach (var windowsTweak in _selectWindowsTweaksPanelViewModel
                             .WindowsTweaks
                             .Where(x => x.IsSelected))
            {
                CurrentTweak = windowsTweak.Name;

                foreach (var tweakExecution in windowsTweak.Executions!)
                {
                    ClearLogMessages();

                    var workingDir = tweakExecution.WorkingDirectoryForExecutionConfiguration(_downloadService);

                    var result = await _commandExecutionService.ProcessExecutionConfiguration(tweakExecution, workingDir);

                    if (!result)
                    {
                        throw new InvalidOperationException("Command did not execute successfully");
                    }

                    await Task.Delay(2000);
                }
            }

            ApplyingFinished = true;
        }
        catch (InvalidOperationException)
        {
            _regionManager.RequestNavigate(Literals.ContentRegion, nameof(ErrorPanel));
        }
    }

    [RelayCommand]
    private void InstallApps() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(SelectDownloadsPanel));
}

