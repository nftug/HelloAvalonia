using Avalonia.Controls;
using FluentAvalonia.UI.Controls;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class FANavigationPageFactory(NavigationPageFactory factory) : INavigationPageFactory
{
    public Control GetPage(Type srcType) => throw new NotImplementedException();

    public Control GetPageFromObject(object target)
        => factory.GetPageFromPath(target.ToString() ?? string.Empty);
}
