namespace WindowsOnDeck;

public class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }
    
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Services
        containerRegistry.RegisterSingleton<IYamlDataService, YamlDataService>();
        containerRegistry.RegisterSingleton<IDownloadService, DownloadService>();
        containerRegistry.RegisterSingleton<ICommandExecutionService, CommandExecutionService>();
        containerRegistry.RegisterSingleton<IConfigurationService, ConfigurationService>();
        containerRegistry.RegisterSingleton<SelectDownloadsPanelViewModel>();
        containerRegistry.RegisterSingleton<SelectWindowsTweaksPanelViewModel>();

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        containerRegistry.RegisterForNavigation<ErrorPanel, ErrorPanelViewModel>();
        containerRegistry.RegisterForNavigation<IntroPanel, IntroPanelViewModel>();
        containerRegistry.RegisterForNavigation<SelectDownloadsPanel, SelectDownloadsPanelViewModel>();
        containerRegistry.RegisterForNavigation<ProcessDownloadsPanel, ProcessDownloadsPanelViewModel>();
        containerRegistry.RegisterForNavigation<SelectWindowsTweaksPanel, SelectWindowsTweaksPanelViewModel>();
        containerRegistry.RegisterForNavigation<ProcessWindowsTweaksPanel, ProcessWindowsTweaksPanelViewModel>();
        containerRegistry.RegisterForNavigation<InstallingDownloadsPanel, InstallingDownloadsPanelViewModel>();
        containerRegistry.RegisterForNavigation<FinalPanel, FinalPanelViewModel>();
    }
    
    protected override AvaloniaObject CreateShell() => 
        Container.Resolve<MainWindow>();
    
    protected override void OnInitialized() =>
        Container.Resolve<IRegionManager>()
            .RegisterViewWithRegion(Literals.ContentRegion, typeof(IntroPanel));
}