namespace WindowsOnDeck.Services;

public sealed class DownloadService : IDownloadService
{
    private readonly HttpClient _httpClient;
    public string DownloadDirectory { get; }

    public DownloadService()
    {
        _httpClient = new();
        DownloadDirectory = Path.Combine(Path.GetTempPath(), Literals.TempFolderAppName);
        EnsureDirectoryExists();
    }

    public async Task DownloadFileAsync(DownloadableItem downloadableItem, IProgress<int>? progress = null, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(downloadableItem.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var contentLength = response.Content.Headers.ContentLength;

        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var fileStream = new FileStream(Path.Combine(DownloadDirectory, downloadableItem.OutputPath), FileMode.Create, FileAccess.Write, FileShare.None);

        var totalBytesRead = 0L;
        var buffer = new byte[8192];
        var isMoreToRead = true;

        do
        {
            var bytesRead = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
            if (bytesRead == 0)
            {
                isMoreToRead = false;
            }
            else
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);

                totalBytesRead += bytesRead;

                if (progress != null && contentLength.HasValue)
                {
                    progress.Report((int)((double)totalBytesRead / contentLength.Value * 100));
                }
            }
        } while (isMoreToRead);
    }
    
    public bool RemoveDownloadDirectoryOnReboot()
    {
        if (!NativeMethods.MoveFileEx(DownloadDirectory, null, MoveFileFlags.DelayUntilReboot))
        {
            Console.Error.WriteLine($"Unable to schedule {DownloadDirectory} for deletion");
            return false;
        }

        return true;
    }
    
    private void EnsureDirectoryExists()
    {
        if (!Directory.Exists(DownloadDirectory))
        {
            Directory.CreateDirectory(DownloadDirectory);
        }
    }
}