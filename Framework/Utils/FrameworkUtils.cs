using R3;

namespace HelloAvalonia.Framework.Utils;

public static class FrameworkUtils
{
    public static async Task InvokeAsync(CompositeDisposable disposables, Func<CancellationToken, Task> work)
    {
        // Create a linked cancellation token source
        var cts = new CancellationTokenSource();
        disposables.Add(Disposable.Create(cts.Cancel));

        try
        {
            await work(cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
    }
}
