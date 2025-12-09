using Avalonia.Controls;
using HelloAvalonia.Features.AboutPage.Views;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.Counter.Views;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Features.CounterList.Views;
using HelloAvalonia.Shell.Composition;

namespace HelloAvalonia.UI.Navigation.Adapters;

public sealed class NavigationPageFactory : IDisposable
{
    private Composition? _session;

    public Control GetPageFromPath(string path)
    {
        _session?.Dispose();

        return path switch
        {
            "/" => ResolvePage<CounterPage, CounterPageViewModel>(),
            "/counter-list" => ResolvePage<CounterListPage, CounterListPageViewModel>(),
            "/about" => new AboutPage(),
            _ => new TextBlock { Text = "Page not found." },
        };
    }

    public void Dispose()
    {
        _session?.Dispose();
    }

    private Control ResolvePage<T, TViewModel>()
        where T : Control, new()
    {
        _session = new Composition(AppDI.Composition);
        return new T { DataContext = _session.Resolve<TViewModel>() };
    }
}
