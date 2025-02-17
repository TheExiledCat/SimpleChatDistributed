using System;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Dumpify;
using SimpleChatFrontend.Helpers;

namespace SimpleChatFrontend.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(NestedHistoryRouter<ViewModelBase, MainWindowViewModel> router)
    {
        // register route changed event to set content to viewModel, whenever
        // a route changes

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
