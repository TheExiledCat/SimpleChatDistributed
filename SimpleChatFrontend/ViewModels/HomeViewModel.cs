using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using SimpleChatFrontend.Helpers;
using SimpleChatShared;

namespace SimpleChatFrontend.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ChatContact> contacts;

    public void AddContact()
    {
        var messageBox = MessageBoxManager.GetMessageBoxCustom(
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
                InputParams = new InputParams() { Label = "Fill in the email", Multiline = false },
            }
        );
        string email = messageBox.ShowAsync().Result;
    }
}
