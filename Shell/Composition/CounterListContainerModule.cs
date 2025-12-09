using HelloAvalonia.Features.CounterList.ViewModels;
using StrongInject;

namespace HelloAvalonia.Shell.Composition;

[Register(typeof(CounterListPageViewModel), Scope.InstancePerResolution)]
public class CounterListContainerModule;

public interface ICounterListContainers : IContainer<CounterListPageViewModel>;
