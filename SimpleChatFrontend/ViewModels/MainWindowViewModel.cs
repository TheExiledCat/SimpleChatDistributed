using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleChatFrontend.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase content = default!;

    public MainWindowViewModel(HistoryRouter<ViewModelBase> router)
    {
        // register route changed event to set content to viewModel, whenever
        // a route changes
        router.CurrentViewModelChanged += viewModel =>
        {
            Content = viewModel;
        };

        // change to HomeView
        router.GoTo<LoginViewModel>();
    }
}
