using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.SimpleRouter;
using dotenv.net;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;
using SimpleChatFrontend.Helpers;
using SimpleChatFrontend.ViewModels;
using SimpleChatFrontend.Views;

namespace SimpleChatFrontend;

public partial class App : Application
{
    public static Window Toplevel;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // In this example we use Microsoft DependencyInjection (instead of ReactiveUI / Splat)
        // Splat would also work, just use the according methods
        DotEnv.Load();
        LocalStorage.Initialize();
        IServiceProvider services = ConfigureServices();
        var mainViewModel = services.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
            Toplevel = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        // Add the HistoryRouter as a service
        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(
            t => (ViewModelBase)s.GetRequiredService(t)
        ));

        // Add the ViewModels as a service (Main as singleton, others as transient)
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<HomeViewModel>();
        return services.BuildServiceProvider();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove = BindingPlugins
            .DataValidators.OfType<DataAnnotationsValidationPlugin>()
            .ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
