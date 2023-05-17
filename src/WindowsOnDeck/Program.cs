namespace WindowsOnDeck;

public class Program
{
    public static AppBuilder BuildAvaloniaApp()
    {
        var builder = AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .With(new Win32PlatformOptions
            {
                AllowEglInitialization = true,
                UseWindowsUIComposition = true,
            })
            .With(new X11PlatformOptions
            {
                UseGpu = true,
                EnableMultiTouch = true,
                UseDBusMenu = true,
            })
            .With(new AvaloniaNativePlatformOptions
            {
                UseGpu = true,
            })
            .With(new MacOSPlatformOptions
            {
                ShowInDock = true
            });

#if DEBUG
        builder.LogToTrace(
            LogEventLevel.Debug,
            LogArea.Property,
            LogArea.Layout,
            LogArea.Binding);
#endif
        return builder;
    }

    [ExcludeFromCodeCoverage]
    static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
}