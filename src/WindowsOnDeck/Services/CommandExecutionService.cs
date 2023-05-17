using System.Diagnostics;

namespace WindowsOnDeck.Services;

public class CommandExecutionService : ICommandExecutionService
{
    public event EventHandler<string>? OnStdOutputMessage;
    public event EventHandler<string>? OnStdErrorMessage;

    public Task<bool> ProcessExecutionConfiguration(ExecutionConfiguration install, string workingDir)
    {
        if (install.UsePowershell == true)
        {
            return ExecuteCommandInPowershellAsync(install.Command!, install.Arguments, workingDir);
        }
        
        if (install.UseProcessStart == true)
        {
            return ExecuteCommandUsingProcessStart(install.Command!, install.Arguments, workingDir);
        }

        return ExecuteCommandAsync(install.Command!, install.Arguments, workingDir);
    }
    
    public Task<bool> ProcessPostInstallConfiguration(AfterInstallationConfiguration install, string workingDir)
    {
        if (install.UsePowershell == true)
        {
            return ExecuteCommandInPowershellAsync(install.Command!, install.Arguments, workingDir);
        }
        
        if (install.UseProcessStart == true)
        {
            return ExecuteCommandUsingProcessStart(install.Command!, install.Arguments, workingDir);
        }

        return ExecuteCommandAsync(install.Command!, install.Arguments, workingDir);
    }
    
    public async Task<bool> ExecuteCommandAsync(string command, string? arguments, string? workingDirectory)
    {
        var executeCommand = Cli.Wrap(command)
            .WithArguments(arguments ?? string.Empty)
            .WithWorkingDirectory(workingDirectory ?? ".");

        await foreach(var cmdEvent in executeCommand.ListenAsync())
        {
           var success = HandleCliOutput(command, arguments, cmdEvent);
            if (!success)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<bool> ExecuteCommandUsingProcessStart(string command, string? arguments, string? workingDirectory)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(workingDirectory!, command),
            WorkingDirectory = workingDirectory,
            CreateNoWindow = false,
        };

        if (!string.IsNullOrEmpty(arguments))
        {
            startInfo.Arguments = arguments;
        }

        OnStdOutputMessage?.Invoke(this, $"Executed: {command} {arguments}");
        
        var process = new Process
        {
            StartInfo = startInfo
        };
        process.Start();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            return false;
        }

        return true;
    }
    
    public async Task<bool> ExecuteCommandInPowershellAsync(string command, string? arguments, string? workingDirectory)
    {
        var argumentList = new List<string> { command };
        if (!string.IsNullOrEmpty(arguments))
        {
            argumentList.Add(arguments);
        }

        var executeCommand = Cli.Wrap("powershell.exe")
            .WithArguments(argumentList)
            .WithWorkingDirectory(workingDirectory ?? ".");

        await foreach(var cmdEvent in executeCommand.ListenAsync())
        {
            HandleCliOutput(command, arguments, cmdEvent);
        }

        return true;
    }

    private bool HandleCliOutput(string command, string? arguments, CommandEvent cmdEvent)
    {
        switch (cmdEvent)
        {
            case StartedCommandEvent started:
                OnStdOutputMessage?.Invoke(this, $"Executed: {command} {arguments}");
                break;
            case StandardOutputCommandEvent stdOut:
                OnStdOutputMessage?.Invoke(this, stdOut.Text);
                break;
            case StandardErrorCommandEvent stdErr:
                OnStdErrorMessage?.Invoke(this, stdErr.Text);
                break;
            case ExitedCommandEvent exited:
                if (exited.ExitCode != 0)
                {
                    OnStdErrorMessage?.Invoke(this, $"Command exited with code {exited.ExitCode}");
                    return false;
                }
                break;
        }

        return true;
    }
}