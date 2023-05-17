namespace WindowsOnDeck.Interfaces;

public interface IDownloadService
{
    string DownloadDirectory { get; }
    bool RemoveDownloadDirectoryOnReboot();
    Task DownloadFileAsync(DownloadableItem downloadableItem, IProgress<int>? progress = null, CancellationToken cancellationToken = default);
}