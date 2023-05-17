namespace WindowsOnDeck.Extensions;

public static class StringExtensions
{
    public static string WorkingDirectoryForExecutionConfiguration(this ExecutionConfiguration executionConfiguration, IDownloadService downloadService)
    {
        var workingDir = executionConfiguration.GetWorkingDirectory(downloadService);
        
        if (!string.IsNullOrEmpty(executionConfiguration.WorkingDirectory))
        {
            workingDir = Path.Combine(
                executionConfiguration.GetWorkingDirectory(downloadService),
                executionConfiguration.WorkingDirectory);
        }

        return workingDir;
    }
    
    public static string WorkingDirectoryForPostInstallConfiguration(this AfterInstallationConfiguration executionConfiguration, IDownloadService downloadService)
    {
        var workingDir = executionConfiguration.GetWorkingDirectory(downloadService);
        
        if (!string.IsNullOrEmpty(executionConfiguration.WorkingDirectory))
        {
            workingDir = Path.Combine(
                executionConfiguration.GetWorkingDirectory(downloadService),
                executionConfiguration.WorkingDirectory);
        }

        return workingDir;
    }

    private static string GetWorkingDirectory(this ExecutionConfiguration executionConfiguration, IDownloadService downloadService) =>
        executionConfiguration.IsLocalAsset == true ? 
            Path.Combine(AppContext.BaseDirectory, "Assets") : 
            downloadService.DownloadDirectory;
    
    private static string GetWorkingDirectory(this AfterInstallationConfiguration configuration, IDownloadService downloadService) =>
        configuration.IsLocalAsset == true ? 
            Path.Combine(AppContext.BaseDirectory, "Assets") : 
            downloadService.DownloadDirectory;
}