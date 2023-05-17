namespace WindowsOnDeck.Interfaces;

public interface ICommandExecutionService
{
    event EventHandler<string>? OnStdOutputMessage;
    event EventHandler<string>? OnStdErrorMessage;
    Task<bool> ExecuteCommandAsync(string command, string? arguments, string? workingDirectory);
    Task<bool> ExecuteCommandInPowershellAsync(string command, string? arguments, string? workingDirectory);
    Task<bool> ExecuteCommandUsingProcessStart(string command, string? arguments, string? workingDirectory);
    Task<bool> ProcessExecutionConfiguration(ExecutionConfiguration install, string workingDir);
    Task<bool> ProcessPostInstallConfiguration(AfterInstallationConfiguration install, string workingDir);
}