using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Shell.ViewModels;

namespace HelloAvalonia.Shell.Composition;

public sealed class AppRoot(MainWindowViewModel root)
{
    public MainWindowViewModel Root { get; } = root;
}

public sealed class CounterPageRoot(CounterPageViewModel root)
{
    public CounterPageViewModel Root { get; } = root;
}

public sealed class CounterListPageRoot(CounterListPageViewModel root)
{
    public CounterListPageViewModel Root { get; } = root;
}
