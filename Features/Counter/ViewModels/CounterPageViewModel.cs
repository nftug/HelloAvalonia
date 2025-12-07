using HelloAvalonia.Framework.ViewModels;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterPageViewModel : ViewModelBase
{
    public CounterDisplayViewModel CounterDisplayViewModel { get; } = new();

    public CounterActionViewModel CounterActionViewModel { get; } = new();
}
