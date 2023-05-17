using YamlDotNet.Serialization;

namespace WindowsOnDeck.Services;

public sealed class YamlDataService : IYamlDataService
{
    private readonly IDeserializer _deserializer;

    public YamlDataService()
    {
        _deserializer = new DeserializerBuilder()
            .Build();
    }

    public IEnumerable<DownloadableItem> LoadDownloadableItems()
    {
        var yamlPath = Path.Combine(AppContext.BaseDirectory, Literals.AssetsFolder, Literals.DownloadsYaml);
        return _deserializer.Deserialize<List<DownloadableItem>>(File.ReadAllText(yamlPath));
    }
    
    public IEnumerable<WindowsTweak> LoadWindowsTweaks()
    {
        var yamlPath = Path.Combine(AppContext.BaseDirectory, Literals.AssetsFolder, Literals.TweaksYaml);
        return _deserializer.Deserialize<List<WindowsTweak>>(File.ReadAllText(yamlPath));
    }
}