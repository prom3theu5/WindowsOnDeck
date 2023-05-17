namespace WindowsOnDeck.Interfaces;

public interface IConfigurationService
{
    void CreateScheduledTask(string name, string path, string description);
    void CreateShortcutOnDesktop(string name, string path);
}
