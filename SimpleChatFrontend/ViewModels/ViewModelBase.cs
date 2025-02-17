using Avalonia.SimpleRouter.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleChatFrontend.ViewModels;

public abstract partial class ViewModelBase : ObservableValidator, ISimpleRoute<ViewModelBase>
{
    [ObservableProperty]
    private ViewModelBase? content = default;
}
