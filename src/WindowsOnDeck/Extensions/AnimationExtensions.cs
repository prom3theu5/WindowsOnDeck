using Avalonia.Platform;

namespace WindowsOnDeck.Extensions;

public static class AnimationExtensions
{
    public static string? AnimationResource(this string animation)
    {
        var assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();

        return assetLoader?
            .GetAssets(new("avares://WindowsOnDeck/Assets/Lottie"), new("avares://WindowsOnDeck/"))
            .Where(x => x.AbsolutePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            .Select(x=> x.AbsoluteUri)
            .FirstOrDefault(x => x.Contains(animation));
    }
}