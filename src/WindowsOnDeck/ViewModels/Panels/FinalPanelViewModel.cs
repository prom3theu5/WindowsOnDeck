using System.Diagnostics;

namespace WindowsOnDeck.ViewModels.Panels;

public partial class FinalPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    [ObservableProperty] 
    private string? _animation;
    
    public FinalPanelViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        Animation = Literals.CoffeeAnimation.AnimationResource();
    }

    protected override bool HasLogMessages => false;

    [RelayCommand]
    private void ExitAndRestart()
    {
        Process.Start("shutdown", "/r /t 0");
        
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
    
    [RelayCommand]
    private void Monzo()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "https://monzo.me/davesekula",
            CreateNoWindow = true,
            UseShellExecute = true,
        };
        
        Process.Start(startInfo);
    }
}