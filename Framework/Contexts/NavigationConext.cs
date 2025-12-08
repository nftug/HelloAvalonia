using R3;

namespace HelloAvalonia.Framework.Contexts;

public record Route(string Path, Type ViewModelType)
{
    public static Route Empty => new(string.Empty, typeof(object));
}

public class NavigationContext : ContextBase
{
    public IReadOnlyList<Route> Routes { get; }
    public ReadOnlyReactiveProperty<Route> CurrentRoute { get; }

    private readonly ReactiveProperty<string> _currentPath;
    private readonly List<Func<CancellationToken, Task<bool>>> _guards = [];

    public NavigationContext(IEnumerable<Route> routes, string initialPath)
    {
        Routes = routes.ToList().AsReadOnly();

        _currentPath = new ReactiveProperty<string>(initialPath).AddTo(Disposable);

        CurrentRoute = _currentPath
            .Select(path => Routes.FirstOrDefault(route => route.Path == path) ?? Route.Empty)
            .ToReadOnlyReactiveProperty(Route.Empty)
            .AddTo(Disposable);
    }

    public IDisposable RegisterGuard(Func<CancellationToken, Task<bool>> guard)
    {
        _guards.Add(guard);
        return R3.Disposable.Create(() => _guards.Remove(guard));
    }

    public async Task<bool> NavigateAsync(string path, CancellationToken cancellationToken = default)
    {
        if (_currentPath.Value == path) return true;

        bool canProceed = true;
        foreach (var guard in _guards)
        {
            if (canProceed && !await guard(cancellationToken))
            {
                canProceed = false;
                break;
            }
        }

        if (canProceed)
            _currentPath.Value = path;

        return canProceed;
    }
}
