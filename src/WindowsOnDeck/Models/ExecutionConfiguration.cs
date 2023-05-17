namespace WindowsOnDeck.Models;

public sealed class ExecutionConfiguration
{
    public string? Command { get; set; }
    public string? Arguments { get; set; }
    public bool? IsLocalAsset { get; set; }
    public string? WorkingDirectory { get; set; }
    public bool? UsePowershell { get; set; }
    public bool? UseProcessStart { get; set; }
}