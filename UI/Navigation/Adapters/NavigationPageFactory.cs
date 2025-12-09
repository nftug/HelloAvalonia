using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Features.AboutPage.Views;
using HelloAvalonia.Features.Counter.Views;
using HelloAvalonia.Features.CounterList.Views;
using HelloAvalonia.Shell.Composition;

namespace HelloAvalonia.UI.Navigation.Adapters;

public sealed class NavigationPageFactory : INavigationPageFactory, IDisposable
{
    private Composition? _session;

    public Control GetPage(Type srcType) => throw new NotImplementedException();

    public Control GetPageFromObject(object target)
    {
        _session?.Dispose();

        return target switch
        {
            "/" => ResolvePage<CounterPage>(s => s.CounterPage.Root),
            "/counter-list" => ResolvePage<CounterListPage>(s => s.CounterListPage.Root),
            "/about" => new AboutPage(),
            _ => new TextBlock { Text = "Page not found." },
        };
    }

    public void Dispose()
    {
        _session?.Dispose();
    }

    private Control ResolvePage<T>(Func<Composition, IDisposable> viewModelFactory)
        where T : Control, new()
    {
        _session = new Composition(AppDI.Composition);
        return new T { DataContext = viewModelFactory(_session) };
    }
}
