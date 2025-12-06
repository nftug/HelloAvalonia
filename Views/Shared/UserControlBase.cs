using System;
using Avalonia;
using Avalonia.Controls;

namespace HelloAvalonia.Views.Shared;

public abstract class UserControlBase : UserControl
{
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        (DataContext as IDisposable)?.Dispose();
    }
}
