namespace HelloAvalonia.Framework.Interfaces;

public interface ICompositionScope : IDisposable
{
    T Resolve<T>();
}

public interface ICompositionScopeFactory
{
    ICompositionScope CreateScope();
}
