using System.Collections.Specialized;
using Avalonia.Threading;

namespace WindowsOnDeck.Views.Panels;

public partial class InstallingDownloadsPanel : UserControl
{
    public InstallingDownloadsPanel()
    {
        DataContextChanged += HandleLogs;
        AvaloniaXamlLoader.Load(this);
    }
    
    private void HandleLogs(object? sender, EventArgs e)
    {
        if (DataContext is InstallingDownloadsPanelViewModel viewModel)
        {
            viewModel.LogMessages!.CollectionChanged += LogMessagesOnCollectionChanged;        
        }
    }

    private async void LogMessagesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var logsInstallBox = this.FindControl<ListBox>("LogsInstall");
        var logsConfigBox = this.FindControl<ListBox>("LogsConfig");

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (logsInstallBox.IsVisible)
            {
                logsInstallBox.ScrollIntoView(logsInstallBox.ItemCount - 1);
            }

            if (logsConfigBox.IsVisible)
            {
                logsConfigBox.ScrollIntoView(logsConfigBox.ItemCount - 1);
            }
        });
    }
}