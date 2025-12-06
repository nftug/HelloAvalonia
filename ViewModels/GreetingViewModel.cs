using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels;

public class GreetingViewModel : ViewModelBase
{
    public ReadOnlyReactiveProperty<string?> Greeting { get; }
    public BindableReactiveProperty<string?> Text { get; }

    public GreetingViewModel(Observable<string?> greeting)
    {
        Greeting = greeting.ToReadOnlyReactiveProperty().AddTo(Disposable);
        Text = new BindableReactiveProperty<string?>().AddTo(Disposable);
    }
}
