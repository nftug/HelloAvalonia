using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Framework.Models;
using HelloAvalonia.Shell.ViewModels;
using HelloAvalonia.UI.Adapters;
using HelloAvalonia.UI.Services;
using Pure.DI;

namespace HelloAvalonia.Shell.Composition;

internal static class AppDI
{
    internal static readonly Composition Composition = new();

    internal static IConfiguration Setup() =>
        DI.Setup(nameof(Composition))
            // Global registrations
            .Bind<IDialogService>().As(Lifetime.Singleton).To<DialogService>()
            .Bind().As(Lifetime.Singleton).To(ctx => new NavigationContext("/"))
            .RootBind<MainWindowViewModel>("MainWindow").As(Lifetime.Singleton).To<MainWindowViewModel>()

            // Counter feature registrations
            .Bind<CounterModel>().As(Lifetime.Singleton).To<CounterModel>()
            .RootBind<CounterPageViewModel>("CounterPage").As(Lifetime.Transient).To<CounterPageViewModel>()

            // CounterList feature registrations
            .Bind<CounterListModel>().As(Lifetime.Scoped).To<CounterListModel>()
            .RootBind<CounterListPageViewModel>("CounterListPage").As(Lifetime.Transient).To<CounterListPageViewModel>();
}
