namespace HelloAvalonia.Features.CounterList.Models;

public record CounterListItem(Guid Id, int Value)
{
    public static CounterListItem CreateNew(int value) => new(Guid.NewGuid(), value);
}

public class CounterListItemComparer : IComparer<CounterListItem>
{
    public int Compare(CounterListItem? x, CounterListItem? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        return x.Value.CompareTo(y.Value);
    }
}
