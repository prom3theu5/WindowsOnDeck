namespace WindowsOnDeck.ViewModels.Panels;

public partial class IntroPanelViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    [ObservableProperty]
    private bool _acceptedTerms;
    

    public IntroPanelViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    [RelayCommand]
    private void GoToSelectDownloads() => 
        _regionManager.RequestNavigate(
            Literals.ContentRegion,
            nameof(SelectWindowsTweaksPanel));

    protected override bool HasLogMessages => false;
}