using Avalonia.Controls;
using HelloAvalonia.Features.AboutPage.Views;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.Counter.Views;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Features.CounterList.Views;
using HelloAvalonia.Framework.Interfaces;
using HelloAvalonia.Framework.Models;

namespace HelloAvalonia.Shell.Models;

public sealed class AppNavigationPageStore(ICompositionScopeFactory scopeFactory)
    : NavigationPageStoreBase(scopeFactory)
{
    protected override Control CreatePageFromPath(string path)
        => path switch
        {
            "/" => Resolve<CounterPage, CounterPageViewModel>(),
            "/counter-list" => Resolve<CounterListPage, CounterListPageViewModel>(),
            "/about" => new AboutPage(),
            _ => new TextBlock { Text = "Page not found." },
        };
}
