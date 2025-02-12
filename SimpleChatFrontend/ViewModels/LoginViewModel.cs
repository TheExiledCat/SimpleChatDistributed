using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Dumpify;
using FastCache;
using FastCache.Extensions;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using RestSharp;
using SimpleChatFrontend.Helpers;
using SimpleChatShared.DTOS;

namespace SimpleChatFrontend.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    [EmailAddress]
    [Required]
    private string email;

    [ObservableProperty]
    [Required]
    private string password;

    [ObservableProperty]
    [Required]
    private string name;
    HistoryRouter<ViewModelBase> router;

    [ObservableProperty]
    bool registerMode = false;

    public LoginViewModel(HistoryRouter<ViewModelBase> _router)
    {
        router = _router;
    }

    public void SubmitLogin()
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageBoxManager
                .GetMessageBoxStandard("Failed login", "Email or password missing")
                .ShowAsync();
            return;
        }
        RestRequest request = new RestRequest("users/login");
        request.AddQueryParameter("email", Email);
        request.AddQueryParameter("password", Password);

        RestResponse<LoginDTO> response = ApiHelper
            .Client.ExecuteGetAsync<LoginDTO>(request)
            .Result;
        if (response.IsSuccessStatusCode)
        {
            LoginDTO? login = response.Data;

            LocalStorage.Set("userId", login.Id);
            LocalStorage.Set("token", login.Token);
            router.GoTo<HomeViewModel>();

            return;
        }
        MessageBoxManager
            .GetMessageBoxStandard("Something went wrong", response.Content)
            .ShowAsync();
    }

    public void ToggleRegisterMode()
    {
        RegisterMode = !RegisterMode;
    }

    public void Register()
    {
        RestRequest request = new RestRequest("users/register");
        request
            .AddQueryParameter("name", Name)
            .AddQueryParameter("email", Email)
            .AddQueryParameter("password", Password);
        RestResponse? response = ApiHelper.Client.ExecutePostAsync(request).Result;
        if (!response.IsSuccessStatusCode)
        {
            MessageBoxManager
                .GetMessageBoxStandard("Something went wrong", response.Content)
                .ShowAsync();

            return;
        }
        MessageBoxManager
            .GetMessageBoxStandard("Success", "You have registered succesfully")
            .ShowAsync();
        Email = "";
        Password = "";
        Name = "";
        ToggleRegisterMode();
    }
}
