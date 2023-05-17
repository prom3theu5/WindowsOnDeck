using DK.WshRuntime;
using Microsoft.Win32.TaskScheduler;

namespace WindowsOnDeck.Services;

public class ConfigurationService : IConfigurationService
{
    public void CreateScheduledTask(string name, string path, string description)
    {
        using TaskService ts = new();
        
        var td = ts.NewTask();
        td.RegistrationInfo.Description = description;
        td.Triggers.Add(new LogonTrigger());
        td.Settings.DisallowStartIfOnBatteries = false;
        td.Settings.StopIfGoingOnBatteries = false;
        td.Actions.Add(new ExecAction(path));
        ts.RootFolder.RegisterTaskDefinition(name, td);
    }

    public void CreateShortcutOnDesktop(string name, string path)
    {
        var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), name);
        WshInterop.CreateShortcut(shortcutPath, name, path, null, null);
    }
}
