using System;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleChatFrontend.Views;
using SimpleChatShared.DTOS;

namespace SimpleChatFrontend.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    private ContactDTO contact;

    public ContactDTO Contact
    {
        get => contact;
        set
        {
            contact = value;
            OnPropertyChanged(nameof(Contact));
        }
    }

    public void LoadNewestMessages() { }

    public void LoadOlderMessages() { }
}
