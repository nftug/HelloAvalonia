using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.VisualTree;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Framework.Utils;

public static class FrameworkUtils
{
    public static IClassicDesktopStyleApplicationLifetime GetApplicationLifetime()
    {
        return Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime
            ?? throw new InvalidOperationException("Desktop application lifetime is not available.");
    }

    public static Window GetMainWindow()
    {
        return GetApplicationLifetime().MainWindow
            ?? throw new InvalidOperationException("Main window is not available.");
    }

    public static TControl GetControlFromWindow<TControl>()
        where TControl : Control
    {
        var window = GetMainWindow();
        return window.FindDescendantOfType<TControl>()
            ?? throw new InvalidOperationException($"Control of type {typeof(TControl).Name} not found.");
    }

    public static async Task InvokeAsync(
        CompositeDisposable disposables,
        Func<CancellationToken, Task> work,
        CancellationToken ct = default)
    {
        // Create a linked cancellation token source
        var innerCt = new CancellationTokenSource();
        disposables.Add(Disposable.Create(innerCt.Cancel));

        // If ct is cancelled, innerCt will be cancelled as well.
        innerCt = CancellationTokenSource.CreateLinkedTokenSource(ct, innerCt.Token);

        try
        {
            await work(innerCt.Token);
        }
        catch (OperationCanceledException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
    }

    public static Observable<Unit> ObserveChangedWithPrepend<T>(this IObservableCollection<T> source)
    {
        return source.ObserveChanged().Select(_ => Unit.Default).Prepend(Unit.Default);
    }

    public static ReactiveCommand<T> WithSubscribe<T>(
        this ReactiveCommand<T> command, Action<T> onExecute, CompositeDisposable disposables)
    {
        command.Subscribe(onExecute).AddTo(disposables);
        return command.AddTo(disposables);
    }

    public static ReactiveCommand<T> WithSubscribeAwait<T>(
        this ReactiveCommand<T> command,
        Func<T, CancellationToken, ValueTask> onExecuteAsync,
        CompositeDisposable disposables)
    {
        command.SubscribeAwait(onExecuteAsync).AddTo(disposables);
        return command.AddTo(disposables);
    }
}
