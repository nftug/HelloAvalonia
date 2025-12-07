using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Adapters.Contexts;
using HelloAvalonia.ViewModels.Contexts;
using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels;

public partial class GreetingConsumerViewModel : ViewModelBase
{
    [ObservableProperty] private GreetingContext? context;

    public void AttachViewHosts(IContextViewHost viewHost)
    {
        (Context, _) = viewHost.RequireContext<GreetingContext>();

        Context.Text
            .Skip(1)
            .Subscribe(text =>
            {
                Console.WriteLine($"GreetingConsumerViewModel received text: {text}");
            })
            .AddTo(Disposable);
    }
}
