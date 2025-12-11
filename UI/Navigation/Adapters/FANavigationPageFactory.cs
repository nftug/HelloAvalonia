using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Shell.Models;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class FANavigationPageFactory(AppNavigationPageStore factory) : INavigationPageFactory
{
    public Control GetPage(Type srcType) => throw new NotImplementedException();

    public Control GetPageFromObject(object target)
        => factory.LoadPageFromNavigation(target.ToString() ?? string.Empty);
}
