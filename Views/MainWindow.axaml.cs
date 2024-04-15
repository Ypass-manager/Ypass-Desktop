using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YpassDesktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        // Définir la taille minimale de la fenêtre
        this.MinWidth = 1250;
        this.MinHeight = 750;
    }
}