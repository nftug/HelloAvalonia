using Avalonia.Controls;
using HelloAvalonia.ViewModels;

namespace HelloAvalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }
}
