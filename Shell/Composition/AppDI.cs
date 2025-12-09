using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.Shell.ViewModels;
using HelloAvalonia.UI.Adapters;
using HelloAvalonia.UI.Navigation.Adapters;
using HelloAvalonia.UI.Navigation.ViewModels;
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
            .Bind().As(Lifetime.Transient).To<NavigationPageFactory>()
            .Bind<NavigationViewModel>().As(Lifetime.Transient).To<NavigationViewModel>()
            .Bind<MainWindowViewModel>().As(Lifetime.Transient).To<MainWindowViewModel>()

            // Counter feature registrations
            .Bind<CounterContext>().As(Lifetime.Scoped).To<CounterContext>()
            .Bind<CounterActionViewModel>().As(Lifetime.Transient).To<CounterActionViewModel>()
            .Bind<CounterDisplayViewModel>().As(Lifetime.Transient).To<CounterDisplayViewModel>()
            .Bind<CounterPageViewModel>().As(Lifetime.Transient).To<CounterPageViewModel>()

            // CounterList feature registrations
            .Bind<CounterListPageViewModel>().As(Lifetime.Transient).To<CounterListPageViewModel>()

            // Composition roots
            .Root<CounterPageRoot>("CounterPage")
            .Root<CounterListPageRoot>("CounterListPage")
            .Root<AppRoot>("App");
}