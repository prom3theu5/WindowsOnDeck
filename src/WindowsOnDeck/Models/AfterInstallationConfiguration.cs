namespace WindowsOnDeck.Models;

public sealed class AfterInstallationConfiguration
{
    public PostInstallConfigurationType? TaskType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Path { get; set; }
    public string? Command { get; set; }
    public string? Arguments { get; set; }
    public string? WorkingDirectory { get; set; }
    public bool? UsePowershell { get; set; }
    public bool? UseProcessStart { get; set; }
    public bool? IsLocalAsset { get; set; }
}