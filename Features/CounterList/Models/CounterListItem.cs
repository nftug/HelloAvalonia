namespace HelloAvalonia.Features.CounterList.Models;

public record CounterListItem(Guid Id, int Value)
{
    public static CounterListItem CreateNew(int value) => new(Guid.NewGuid(), value);
}