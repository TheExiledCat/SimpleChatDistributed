using System;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Dumpify;
using SimpleChatFrontend.Helpers;

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
        if (
            !string.IsNullOrEmpty(LocalStorage.Get("userId"))
            && !string.IsNullOrEmpty(LocalStorage.Get("token"))
        )
        {
            Console.WriteLine("Skipping login");
            router.GoTo<HomeViewModel>();
        }
        else
        {
            router.GoTo<LoginViewModel>();
        }
    }
}
