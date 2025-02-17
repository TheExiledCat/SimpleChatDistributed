using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.SimpleRouter;
using Avalonia.Threading;
using Dumpify;
using SimpleChatFrontend.ViewModels;
using SimpleChatShared;
using SimpleChatShared.DTOS;

namespace SimpleChatFrontend.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    public void SelectRoom(object sender, PointerPressedEventArgs args)
    {
        ContactDTO contact = ((Control)sender).DataContext as ContactDTO;
        ((HomeViewModel)DataContext).SelectRoom(contact);
    }
}
