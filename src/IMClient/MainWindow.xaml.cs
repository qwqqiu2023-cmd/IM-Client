using System.Windows;
using IMClient.ViewModels;

namespace IMClient;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
