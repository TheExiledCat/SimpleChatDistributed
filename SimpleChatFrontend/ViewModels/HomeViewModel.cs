using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.SimpleRouter;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Dumpify;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using RestSharp;
using SimpleChatFrontend.Helpers;
using SimpleChatShared;
using SimpleChatShared.DTOS;

namespace SimpleChatFrontend.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ContactDTO> contacts;

    NestedHistoryRouter<ViewModelBase, MainWindowViewModel> router;

    public HomeViewModel(NestedHistoryRouter<ViewModelBase, MainWindowViewModel> _router)
    {
        router = _router;
        RefreshContacts();
    }

    public void AddContact()
    {
        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            IMsBox<string> emailBox = MessageBoxManager.GetMessageBoxCustom(
                new MessageBoxCustomParams
                {
                    ContentTitle = "Input Required",
                    ContentMessage = "Enter your text below:",
                    Icon = Icon.Info,
                    ButtonDefinitions = new[]
                    {
                        new ButtonDefinition { Name = "OK", IsDefault = true },
                        new ButtonDefinition { Name = "Cancel", IsCancel = true },
                    },
                    Topmost = true,
                    InputParams = new InputParams()
                    {
                        Label = "Fill in the email",
                        Multiline = false,
                    },
                    CloseOnClickAway = false,
                    ShowInCenter = true,
                    WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
                }
            );
            await emailBox.ShowAsync();
            string? email = emailBox.InputValue;
            if (string.IsNullOrEmpty(email))
            {
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBoxManager
                    .GetMessageBoxStandard(
                        "Invalid email",
                        "Email adress is not a valid email adress"
                    )
                    .ShowAsync();
                return;
            }
            RestRequest request = new RestRequest($"users/add/{email}");
            RestResponse<ContactAddedDTO> response = ApiHelper.Client.ExecutePost<ContactAddedDTO>(
                request
            );
            if (response.IsSuccessStatusCode)
            {
                MessageBoxManager
                    .GetMessageBoxStandard(
                        "User added succesfully",
                        $"User {email} has been added to the contacts list."
                    )
                    .ShowAsync();
                RefreshContacts();
                return;
            }
            MessageBoxManager
                .GetMessageBoxStandard("Something went wrong", response.Content)
                .ShowAsync();
        });
    }

    public void RefreshContacts()
    {
        RestRequest request = new RestRequest($"users/contacts/{LocalStorage.Get("userId")}");
        RestResponse<ContactDTO[]> response = ApiHelper.Client.ExecuteGet<ContactDTO[]>(request);
        if (response.IsSuccessStatusCode)
        {
            Contacts = new ObservableCollection<ContactDTO>(response.Data);
        }
    }

    public void SelectRoom(ContactDTO contact)
    {
        ChatViewModel chat = (ChatViewModel)router.GoTo<HomeViewModel, ChatViewModel>()[2];
        chat.Contact = contact;
    }

    public void SignOut()
    {
        LocalStorage.Remove("userId");
        LocalStorage.Remove("token");
        router.GoTo<LoginViewModel>();
    }

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
