using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Chaos_Assembly.ViewModels;

public partial class MainViewModel : ViewModelBase
{

[ObservableProperty]
    private TabItemViewModel? _selectedTab;

}
