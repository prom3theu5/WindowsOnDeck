namespace WindowsOnDeck.Models;

public class WindowsTweak : BindableBase
{
    private bool _isSelected = true;
    
    public string? Name { get; set; }
    public List<ExecutionConfiguration>? Executions { get; set; }
    
    public bool IsSelected
    {
        get => _isSelected; 
        set => SetProperty(ref _isSelected, value);
    }
}