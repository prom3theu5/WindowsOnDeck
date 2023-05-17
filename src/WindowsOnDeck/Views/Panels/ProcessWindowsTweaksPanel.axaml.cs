using System.Collections.Specialized;
using Avalonia.Threading;

namespace WindowsOnDeck.Views.Panels;

public partial class ProcessWindowsTweaksPanel : UserControl
{
    public ProcessWindowsTweaksPanel()
    {
        DataContextChanged += HandleLogs;
        AvaloniaXamlLoader.Load(this);
    }

    private void HandleLogs(object? sender, EventArgs e)
    {
        if (DataContext is ProcessWindowsTweaksPanelViewModel viewModel)
        {
            viewModel.LogMessages!.CollectionChanged += LogMessagesOnCollectionChanged;        
        }
    }

    private async void LogMessagesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var logsBox = this.FindControl<ListBox>("Logs");

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            logsBox.ScrollIntoView(logsBox.ItemCount - 1);
        });
    }
}