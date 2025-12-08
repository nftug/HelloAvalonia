using HelloAvalonia.Framework.ViewModels;
using HelloAvalonia.Framework.Views;
using HelloAvalonia.UI.Navigation.ViewModels;
using R3;

namespace HelloAvalonia.UI.Navigation.Views;

public partial class NavigationView : UserControlBase
{
    public NavigationView()
    {
        InitializeComponent();

        // To prevent transition animation flicker on initial load
        ContentFrame.IsVisible = false;
    }

    protected override void OnViewModelSet(ViewModelBase viewModel)
    {
        base.OnViewModelSet(viewModel);

        if (viewModel is NavigationViewModel navigationViewModel)
        {
            navigationViewModel.NavigateRequested
                .Subscribe(t =>
                {
                    ContentFrame.NavigateFromObject(t);
                    ContentFrame.IsVisible = true;
                })
                .AddTo(Disposable);
        }
    }
}