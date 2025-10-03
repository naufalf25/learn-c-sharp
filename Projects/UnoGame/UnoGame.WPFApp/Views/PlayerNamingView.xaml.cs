using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoGame.WPFApp.ViewModels;

namespace UnoGame.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerNamingView.xaml
    /// </summary>
    public partial class PlayerNamingView : UserControl
    {
        public PlayerNamingView(MainWindow mainWindow, int playerCount)
        {
            InitializeComponent();
            DataContext = new PlayerNamingViewModel(mainWindow, playerCount);
        }
    }
}
