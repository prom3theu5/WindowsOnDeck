namespace WindowsOnDeck.ViewModels;

public abstract partial class ViewModelBase : ObservableObject, INavigationAware
{
    protected abstract bool HasLogMessages { get; }

    public ViewModelBase()
    {
        SetupLogMessages();
    }

    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
        // Auto-allow navigation
        return OnNavigatingTo(navigationContext);
    }

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
    }

    public virtual bool OnNavigatingTo(NavigationContext navigationContext)
    {
        return true;
    }
    
    protected void AddMessageToLog(object? sender, string e) => LogMessages?.Add(e);

    protected void ClearLogMessages() => LogMessages?.Clear();

    public ObservableCollection<string>? LogMessages { get; set; }
    
    private void SetupLogMessages()
    {
        if (HasLogMessages)
        {
            LogMessages = new();
        }
    }
}