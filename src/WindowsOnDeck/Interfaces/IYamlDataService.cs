namespace WindowsOnDeck.Interfaces;

public interface IYamlDataService
{
    IEnumerable<DownloadableItem> LoadDownloadableItems();
    IEnumerable<WindowsTweak> LoadWindowsTweaks();
}