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
using UnoGame.BackEnd.Interfaces;
using UnoGame.WPFApp.ViewModels;

namespace UnoGame.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView(MainWindow mainWindow, List<IPlayer> players)
        {
            InitializeComponent();
            DataContext = new GameViewModel(mainWindow, players);
        }
    }
}
