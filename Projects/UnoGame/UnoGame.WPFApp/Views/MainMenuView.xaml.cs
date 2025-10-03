using System.Windows.Controls;
using UnoGame.WPFApp.ViewModels;

namespace UnoGame.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public MainMenuView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = new MainMenuViewModel(_mainWindow);
        }
    }
}
