using HelloAvalonia.Framework.ViewModels;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : ViewModelBase
{
    private readonly ObservableList<CounterListItemViewModel> _counters;

    public NotifyCollectionChangedSynchronizedViewList<CounterListItemViewModel> CountersView { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }
    public ReactiveCommand AddCommand { get; }
    public ReactiveCommand RemoveCommand { get; }

    public CounterListPageViewModel()
    {
        _counters = [..
            Enumerable.Range(0, 5).Select(i => new CounterListItemViewModel(Guid.NewGuid(), i, OnItemValueChanged))];

        CountersView = _counters.ToNotifyCollectionChangedSlim().AddTo(Disposable);

        CountersSum = _counters
            .ObserveChanged()
            .Select(_ => Unit.Default)
            .Prepend(Unit.Default)
            .Select(_ => _counters.Sum(c => c.Value))
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);

        AddCommand = new ReactiveCommand().AddTo(Disposable);
        RemoveCommand = _counters
            .ObserveCountChanged()
            .Select(count => count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        AddCommand.Subscribe(_ => HandleAddCounter()).AddTo(Disposable);
        RemoveCommand.Subscribe(_ => HandleRemoveCounter()).AddTo(Disposable);
    }

    private void HandleAddCounter()
    {
        var nextValue = _counters.Count > 0 ? _counters[^1].Value + 1 : 0;
        _counters.Add(new(Guid.NewGuid(), nextValue, OnItemValueChanged));
    }

    private void HandleRemoveCounter()
    {
        if (_counters.Count > 0)
        {
            _counters.RemoveAt(_counters.Count - 1);
        }
    }

    private void OnItemValueChanged(Guid id, int newValue)
    {
        if (_counters.FirstOrDefault(c => c.Id == id) is { } target)
        {
            target.Dispose();
            var index = _counters.IndexOf(target);
            _counters[index] = new CounterListItemViewModel(id, newValue, OnItemValueChanged);
        }
    }
}
