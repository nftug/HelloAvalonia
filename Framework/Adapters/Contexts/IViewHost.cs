namespace HelloAvalonia.Framework.Adapters.Contexts;

public interface IViewHost
{
    ContextReturn<T>? ResolveContext<T>(string? name = null) where T : class;
    ContextReturn<T> RequireContext<T>(string? name = null) where T : class;
}
