namespace WindowsOnDeck.Models;

public class DownloadableItem : BindableBase
{
    private bool _isSelected = true;
    
    public required string Name { get; set; }

    public required string Url { get; set; }

    public required string OutputPath { get; set; }

    public List<ExecutionConfiguration> InstallationCommands { get; set; } = new();
    
    public List<AfterInstallationConfiguration> PostInstallConfiguration { get; set; } = new();

    public bool IsSelected
    {
        get => _isSelected; 
        set => SetProperty(ref _isSelected, value);
    }
}