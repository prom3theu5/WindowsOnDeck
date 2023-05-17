namespace WindowsOnDeck.ViewModels.Panels;

public partial class ErrorPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    [ObservableProperty] 
    private string? _animation;
    
    public ErrorPanelViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        Animation = Literals.ErrorOccurredAnimation.AnimationResource();
    }

    protected override bool HasLogMessages => false;

    [RelayCommand]
    private void Exit()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}