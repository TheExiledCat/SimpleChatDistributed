using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleChatFrontend.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    private string title = "Hallo";

    public LoginViewModel()
    {
        //
    }
}
