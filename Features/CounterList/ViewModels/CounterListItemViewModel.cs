using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListItemViewModel : ViewModelBase
{
    public Guid Id { get; }
    public int Value { get; }
    public ReactiveCommand IncrementCommand { get; }
    public ReactiveCommand DecrementCommand { get; }

    public CounterListItemViewModel(Guid id, int initialValue, Action<Guid, int> onValueChanged)
    {
        Id = id;
        Value = initialValue;

        IncrementCommand = new ReactiveCommand().AddTo(Disposable);
        DecrementCommand = new ReactiveCommand().AddTo(Disposable);

        IncrementCommand.Subscribe(_ =>
        {
            onValueChanged(Id, Value + 1);
        })
        .AddTo(Disposable);

        DecrementCommand.Subscribe(_ =>
        {
            onValueChanged(Id, Value - 1);
        })
        .AddTo(Disposable);
    }
}
