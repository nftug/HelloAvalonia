using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.UI.Adapters;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterActionViewModel : DisposableBase
{
    public IReadOnlyBindableReactiveProperty<bool> IsLoading { get; }
    public ReactiveCommand IncrementCommand { get; }
    public ReactiveCommand DecrementCommand { get; }
    public ReactiveCommand ResetCommand { get; }

    public CounterActionViewModel(CounterModel model, IDialogService dialogService)
    {
        IsLoading = model.IsLoading.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        IncrementCommand = model.IsLoading
            .CombineLatest(model.Count, (isLoading, count) => !isLoading && count < int.MaxValue)
            .ToReactiveCommand()
            .AddTo(Disposable);

        DecrementCommand = model.IsLoading
            .CombineLatest(model.Count, (isLoading, count) => !isLoading && count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        ResetCommand = model.IsLoading
            .CombineLatest(model.Count, (isLoading, count) => !isLoading && count != 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        IncrementCommand
            .SubscribeAwait(async (_, ct) => await model.InvokeAsync(async innerCt =>
            {
                var current = model.Count.CurrentValue;
                await model.SetCountAsync(current + 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        DecrementCommand
            .SubscribeAwait(async (_, ct) => await model.InvokeAsync(async innerCt =>
            {
                var current = model.Count.CurrentValue;
                await model.SetCountAsync(current - 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        ResetCommand
            .SubscribeAwait(async (_, ct) => await model.InvokeAsync(async innerCt =>
            {
                var result = await dialogService.ShowDialogAsync(
                    "Reset Counter",
                    "Are you sure you want to reset the counter to zero?",
                    new YesNoDialogButtons("Yes", "No"),
                    innerCt);

                if (result == DialogResult.Yes)
                    await model.SetCountAsync(0, TimeSpan.FromMilliseconds(1000), innerCt);
            }))
            .AddTo(Disposable);
    }
}
