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

    static IConfiguration Setup() =>
        DI.Setup(nameof(Composition))
            // Global registrations
            .Bind<IDialogService>().As(Lifetime.Singleton).To<DialogService>()
            .Bind().As(Lifetime.Singleton).To(ctx => new NavigationContext("/"))

            // Shell specific registrations
            .Bind<MainWindowViewModel>().As(Lifetime.Transient).To<MainWindowViewModel>()

            // Counter feature registrations
            .Bind<CounterModel>().As(Lifetime.Scoped).To<CounterModel>()
            .Bind<CounterPageViewModel>().As(Lifetime.Transient).To<CounterPageViewModel>()

            // CounterList feature registrations
            .Bind<CounterListModel>().As(Lifetime.Singleton).To<CounterListModel>()
            .Bind<CounterListPageViewModel>().As(Lifetime.Transient).To<CounterListPageViewModel>()

            // Composition roots
            .Root<CounterPageRoot>("CounterPage")
            .Root<CounterListPageRoot>("CounterListPage")
            .Root<AppRoot>("App");
}