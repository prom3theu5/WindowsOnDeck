using System.Windows.Controls;

namespace WindowsOnDeck.ViewModels.Panels;

public partial class InstallingDownloadsPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    private readonly IDownloadService _downloadService;
    private readonly ICommandExecutionService _commandExecutionService;
    private readonly IConfigurationService _configurationService;
    private readonly SelectDownloadsPanelViewModel _selectDownloadsPanelViewModel;
    
    [ObservableProperty]
    private bool _installsFinished;

    [ObservableProperty]
    private bool _configurationFinished;

    [ObservableProperty]
    private string? _currentInstall;
    
    public InstallingDownloadsPanelViewModel(
        IRegionManager regionManager,
        IDownloadService downloadService,
        ICommandExecutionService commandExecutionService,
        IConfigurationService configurationService,
        SelectDownloadsPanelViewModel selectDownloadsPanelViewModel)
    {
        _regionManager = regionManager;
        _downloadService = downloadService;
        _commandExecutionService = commandExecutionService;
        _configurationService = configurationService;
        _selectDownloadsPanelViewModel = selectDownloadsPanelViewModel;
        _commandExecutionService.OnStdOutputMessage += AddMessageToLog;
        _commandExecutionService.OnStdErrorMessage += AddMessageToLog;
        CurrentInstall = string.Empty;
        InstallsFinished = false;
        ConfigurationFinished = false;
    }

    protected override bool HasLogMessages => true;

    public override async void OnNavigatedTo(NavigationContext navigationContext)
    {
        await InstallApps();
        await ConfigureApps();
        await CleanUp();
    }

    private async Task CleanUp()
    {
        ClearLogMessages();
        var scheduleResult = _downloadService.RemoveDownloadDirectoryOnReboot();
        
        AddMessageToLog(this,
            scheduleResult
                ? "Temporary download directory has been scheduled to be removed on reboot."
                : "Temporary download directory was failed to be scheduled for removal. Manually delete it after a reboot.");
        
        await Task.Delay(3000);
        ConfigurationFinished = true;
    }

    private async Task InstallApps()
    {
        try
        {
            foreach (var download in _selectDownloadsPanelViewModel
                              .DownloadableItems
                              .Where(x => x.IsSelected))
            {
                CurrentInstall = download.Name;

                foreach (var install in download.InstallationCommands)
                {
                    ClearLogMessages();

                    if (string.IsNullOrEmpty(install.Command))
                    {
                        continue;
                    }

                    var workingDir = install.WorkingDirectoryForExecutionConfiguration(_downloadService);
                    var result = await _commandExecutionService.ProcessExecutionConfiguration(install, workingDir);

                    if (!result)
                    {
                        throw new InvalidOperationException("Command did not execute successfully");
                    }

                    await Task.Delay(2000);
                }
            }

            InstallsFinished = true;
        }
        catch (InvalidOperationException)
        {
            _regionManager.RequestNavigate(Literals.ContentRegion, nameof(ErrorPanel));
        }
    }

    private async Task ConfigureApps()
    {
        try
        {
            foreach (var download in _selectDownloadsPanelViewModel
                         .DownloadableItems
                         .Where(x => x.IsSelected))
            {
                CurrentInstall = download.Name;

                foreach (var install in download.PostInstallConfiguration)
                {
                    ClearLogMessages();

                    var workingDir = install.WorkingDirectoryForPostInstallConfiguration(_downloadService);

                    await PerformConfiguration(install, workingDir);
                    
                    await Task.Delay(2000);
                }
            }
        }
        catch (InvalidOperationException)
        {
            _regionManager.RequestNavigate(Literals.ContentRegion, nameof(ErrorPanel));
        }
    }

    private async Task PerformConfiguration(AfterInstallationConfiguration install, string workingDir)
    {
        switch (install.TaskType)
        {
            case PostInstallConfigurationType.ScheduledTask:
                _configurationService.CreateScheduledTask(install.Name!, install.Path!, install.Description!);
                AddMessageToLog(this, $"Created Scheduled Login Task for: {install.Name}");
                return;
            case PostInstallConfigurationType.Command:
                var result = await _commandExecutionService.ProcessPostInstallConfiguration(install, workingDir); 
                if (!result)
                {
                    throw new InvalidOperationException("Command did not execute successfully");
                }
                return;
            case PostInstallConfigurationType.CreateShortcut:
                _configurationService.CreateShortcutOnDesktop(install.Name!, install.Path!);
                AddMessageToLog(this, $"Add Shortcut to desktop: {install.Name}");
                return;
            case null:
                return;
            default:
                return;
        }
    }

    [RelayCommand]
    private void FinalPage() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(FinalPanel));
}

