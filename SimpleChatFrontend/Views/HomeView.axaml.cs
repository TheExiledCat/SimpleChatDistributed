using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Dumpify;

namespace SimpleChatFrontend.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        App.Toplevel.Resized += (sender, args) => Resize();

        InitializeComponent();
        Dispatcher.UIThread.Post(Resize, DispatcherPriority.Background);
    }

    void Resize()
    {
        List<Button> buttons =
        [
            AccountButton,
            GroupCreateButton,
            JoinGroupButton,
            AddContactButton,
        ];
        buttons.ForEach(b =>
        {
            b.CornerRadius = new CornerRadius(b.Bounds.Width);
        });
    }
}
