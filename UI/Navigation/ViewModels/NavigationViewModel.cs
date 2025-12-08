using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.UI.Navigation.ViewModels;

public partial class NavigationViewModel : ViewModelBase
{
    private readonly ReactiveCommand<Type> _navigateCommand;

    [ObservableProperty] IReadOnlyBindableReactiveProperty<string>? pageTitle;
    public Observable<Type> NavigateRequested => _navigateCommand;
    public BindableReactiveProperty<NavigationViewItem?> SelectedItem { get; }

    public IEnumerable<NavigationViewItem> MenuItems { get; }
    public IEnumerable<NavigationViewItem> FooterMenuItems { get; }

    public NavigationViewModel(
        IEnumerable<NavigationViewItem> menuItems, IEnumerable<NavigationViewItem> footerMenuItems)
    {
        MenuItems = menuItems;
        FooterMenuItems = footerMenuItems;
        _navigateCommand = new ReactiveCommand<Type>().AddTo(Disposable);
        SelectedItem = new BindableReactiveProperty<NavigationViewItem?>().AddTo(Disposable);
    }

    public override void AttachViewHost(IViewHost viewHost)
    {
        var context = viewHost.RequireContext<NavigationContext>();

        PageTitle = context.CurrentRoute
            .Select(route =>
            {
                var menuItem = FindMenuItemByRoute(route);
                return menuItem?.Content?.ToString() ?? string.Empty;
            })
            .ToReadOnlyBindableReactiveProperty(string.Empty)
            .AddTo(Disposable);

        context.CurrentRoute
            .ObserveOnUIThreadDispatcher()
            .Subscribe(route => _navigateCommand.Execute(route.ViewModelType))
            .AddTo(Disposable);

        context.CurrentRoute
            .Select(FindMenuItemByRoute)
            .WhereNotNull()
            .Subscribe(item => SelectedItem.Value = item)
            .AddTo(Disposable);

        SelectedItem
            .WhereNotNull()
            .SubscribeAwait(async (item, ct) =>
            {
                if (item.Tag is string path)
                {
                    bool navigated = await context.NavigateAsync(path, ct);
                    if (!navigated)
                    {
                        // Revert selection if navigation failed
                        var currentRoute = context.CurrentRoute.CurrentValue;
                        SelectedItem.Value = FindMenuItemByRoute(currentRoute);
                    }
                }
            })
            .AddTo(Disposable);
    }

    private NavigationViewItem? FindMenuItemByRoute(Route route)
        => MenuItems.Concat(FooterMenuItems)
            .FirstOrDefault(item => item.Tag is string path && path == route.Path);
}
