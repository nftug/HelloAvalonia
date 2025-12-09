using FluentAvalonia.Core;
using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : DisposableBase
{
    private readonly CounterListModel _model;

    public DisposableViewListEnvelope<CounterListItem, CounterListItemViewModel> CountersViewEnvelope { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }
    public ReactiveCommand AddCommand { get; }
    public ReactiveCommand RemoveCommand { get; }

    public CounterListPageViewModel(CounterListModel model)
    {
        _model = model;

        CountersViewEnvelope = _model.Counters
            .ToDisposableViewListEnvelope(
                model => new CounterListItemViewModel(model, updated => _model.Counters.Update(updated, model)))
            .AddTo(Disposable);

        CountersSum = _model.Counters
            .ObserveChangedWithPrepend()
            .Select(_ => _model.Counters.Sum(c => c.Value))
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);

        AddCommand = new ReactiveCommand().AddTo(Disposable);
        RemoveCommand = _model.Counters
            .ObserveCountChanged(notifyCurrentCount: true)
            .Select(count => count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        AddCommand.Subscribe(_ => HandleAddCounter()).AddTo(Disposable);
        RemoveCommand.Subscribe(_ => HandleRemoveCounter()).AddTo(Disposable);
    }

    private void HandleAddCounter()
    {
        var nextValue = _model.Counters.Count > 0 ? _model.Counters[^1].Value + 1 : 0;
        _model.Counters.Add(CounterListItem.CreateNew(nextValue));
    }

    private void HandleRemoveCounter()
    {
        if (_model.Counters.Count > 0)
        {
            var index = _model.Counters.Count - 1;
            _model.Counters.RemoveAt(index);
        }
    }
}
