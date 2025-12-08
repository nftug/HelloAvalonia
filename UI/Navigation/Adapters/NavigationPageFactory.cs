using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.Counter.Views;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class NavigationPageFactory : INavigationPageFactory
{
    public Control GetPage(Type viewModelType) =>
        viewModelType switch
        {
            Type t when t == typeof(CounterPageViewModel) => new CounterPage(),
            _ => new TextBlock { Text = "Page Not Found" }
        };

    public Control GetPageFromObject(object target)
    {
        return target is Type t ? GetPage(t) : new TextBlock { Text = "Invalid Page Type" };
    }
}
