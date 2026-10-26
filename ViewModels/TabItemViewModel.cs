using CommunityToolkit.Mvvm.ComponentModel;

namespace Chaos_Assembly.ViewModels;
public class TabItemViewModel
{
    public string Header { get; }
    public object Content { get; }

    public TabItemViewModel(string header)
    {
        Header = header;
        
    }
}
