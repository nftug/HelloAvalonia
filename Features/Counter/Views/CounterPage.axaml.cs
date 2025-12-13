using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.Counter.Views;

public partial class CounterPage : UserControlBase<CounterPageViewModel>
{
    public CounterPage()
    {
        InitializeComponent();
    }
}
